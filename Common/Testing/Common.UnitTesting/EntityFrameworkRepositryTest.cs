using BusinessSolutions.Common.EntityFramework;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Common.UnitTesting
{
    [TestFixture]
    public class EntityFrameworkRepositryTest
    {
        private static List<TestEntity> entities = new List<TestEntity>();
        private BaseEntityFrameworkRepository<int, TestEntity, TestDomainEntity> repository;
        private Mock<DbSet<TestEntity>> mockSet;
        private Mock<TestDbContext> mockContext;
        private const int _notExistItemId = 150;
        private const int _existItemId = 1;
        private const int _existPage = 2;
        private const int _notExistPage = 5;
        private const int _pageSize = 5;

        [OneTimeSetUp]
        public void Init()
        {
            for (int i = 1; i <= 20; i++)
                entities.Add(new TestEntity { Id = i, Name = "Entity" + i });
        }

        [SetUp]
        public void Setup()
        {
            PrepareSet();
            PrepareContext();
            repository = new TestRespository(mockContext.Object);
        }

        private void PrepareContext()
        {
            mockContext = new Mock<TestDbContext>();
            mockContext.Setup(c => c.TestEntities).Returns(mockSet.Object);
            mockContext.Setup(c => c.Set<TestEntity>()).Returns(mockSet.Object);
        }

        private void PrepareSet()
        {
            var query = entities.AsQueryable();
            mockSet = new Mock<DbSet<TestEntity>>();
            mockSet.As<IDbAsyncEnumerable<TestEntity>>().Setup(c => c.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TestEntity>(query.GetEnumerator()));

            mockSet.As<IQueryable<TestEntity>>().Setup(c => c.Provider)
                .Returns(new TestDbAsyncQueryProvider<TestEntity>(query.Provider));

            mockSet.As<IQueryable<TestEntity>>().Setup(c => c.Expression).Returns(query.Expression);
            mockSet.As<IQueryable<TestEntity>>().Setup(c => c.ElementType).Returns(query.ElementType);
            mockSet.As<IQueryable<TestEntity>>().Setup(c => c.GetEnumerator()).Returns(query.GetEnumerator());

            mockSet.Setup(c => c.Find(It.IsAny<object[]>()))
                .Returns<object[]>((a) => query.FirstOrDefault(c => c.Id == (int)a[0]));

            mockSet.Setup(c => c.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(async (a) =>
                {
                    var item = await Task.Delay(2000).ContinueWith((x) => query.FirstOrDefault(c => c.Id == (int)a[0]));
                    return item;
                });

            mockSet.Setup(c => c.FindAsync(It.IsAny<CancellationToken>(), It.IsAny<object[]>()))
                .Returns<CancellationToken, object[]>(async (token, a) =>
                {
                    var item = await Task.Delay(3000, token).ContinueWith((x) =>
                    query.FirstOrDefault(c => c.Id == (int)a[0]
                    ), token);
                    return item;
                });

            mockSet.Setup(c => c.Attach(It.IsAny<TestEntity>()));
            mockSet.Setup(c => c.Remove(It.IsAny<TestEntity>()));
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (repository != null)
                repository = null;
        }

        [Test]
        public void CheckThatRespositoryIsNotNull()
        {
            repository.Should().NotBeNull();
        }

        #region Add
        [Test]
        public void Add_NullEntity_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => repository.Add(null));
        }

        [Test]
        public void Add_NewItem_ItemAdded()
        {
            //Arrange
            var item = new TestDomainEntity { Id = 30, Name = "test" };

            //Act
            repository.Add(item);

            //Assert
            mockSet.Verify(c => c.Add(It.IsAny<TestEntity>()), Times.Once);
        }
        #endregion

        #region GetAll
        [Test]
        public void GetAll_ReturnItem_ReturnAllItems()
        {
            //Act
            var items = repository.GetAll();

            //Assert
            items.Count.Should().Be(20);
        }
        #endregion

        #region GetAllAsync
        [Test]
        public async Task GetAllAsync_ReturnItem_ReturnAllItems()
        {
            //Act
            var items = await repository.GetAllAsync();

            //Assert
            items.Should().NotBeNull();
            items.Count().Should().Be(20);
        }

        [Test]
        public void GetAllAsync_WithCancelTrue_ThrowCancelException()
        {
            //Arrange
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            tokenSource.Cancel();

            //Act 
            AsyncTestDelegate test = async ()
               => await repository.GetAllAsync(token);

            //Assert
            Assert.ThrowsAsync<TaskCanceledException>(test);
        }

        [Test]
        public async Task GetAllAsync_WithCancelFalse_ReturnAllItems()
        {
            //Arrange
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            //Act
            var result = await repository.GetAllAsync(token);

            //Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(20);
        }
        #endregion

        #region GetByID
        [Test]
        public void GetByID_IfItemNotFound_ReturnNull()
        {
            //Act
            var item = repository.GetByID(_notExistItemId);

            //Assert
            item.Should().BeNull();
        }

        [Test]
        public void GetByID_IfItemExist_ReturnItem()
        {
            //Act
            var item = repository.GetByID(_existItemId);

            //Assert
            item.Should().NotBeNull();
            item.Id.Should().Be(1);
        }

        [Test]
        public async Task GetByIDAsync_IfItemNotFound_ReturnNull()
        {
            var item = await repository.GetByIDAsync(150);
            Assert.IsNull(item);
        }

        [Test]
        public async Task GetByIDAsync_ItemExist_ReturnItem()
        {
            //Act
            var item = await repository.GetByIDAsync(_existItemId);

            //Assert
            item.Should().NotBeNull();
            item.Id.Should().Be(1);
        }

        [Test]
        public void GetByIDAsync_WithCancelTokenThatCancelled_ThrowCancelException()
        {
            //Arrange
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            tokenSource.Cancel();
            AsyncTestDelegate action = async () => await repository.GetByIDAsync(token, _notExistItemId);

            //Assert
            Assert.ThrowsAsync<TaskCanceledException>(action);
        }

        [Test]
        public async Task GetByIDAsync_WithCancelTokenNotCancelled_ReturnItem()
        {
            //Arrange
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            //Act
            var item = await repository.GetByIDAsync(token, _existItemId);

            //Assert
            item.Should().NotBeNull();
            item.Id.Should().Be(1);
        }
        #endregion

        #region PageAll
        [Test]
        public void PageAll_OutOfRangePage_ReturnTotalCountsAndEmptyItems()
        {
            var pageItems = repository.PageAll(_notExistPage, _pageSize);

            pageItems.Should().NotBeNull();
            pageItems.TotalCount.Should().Be(20);
            pageItems.Items.Should().BeEmpty();
        }

        [Test]
        public void PageAll_ExistPage_ReturnItem()
        {
            var pageItems = repository.PageAll(_existPage, _pageSize);

            pageItems.Should().NotBeNull();
            pageItems.TotalCount.Should().Be(20);
            pageItems.Items.Should().HaveCount(5);
            pageItems.Items[0].Id.Should().Be(11);
        }

        [Test]
        public async Task PageAllAsync_OutOfRangePage_ReturnTotalCountsAndEmptyItems()
        {
            var pageItems = await repository.PageAllAsync(_notExistPage, _pageSize);

            pageItems.Should().NotBeNull();
            pageItems.TotalCount.Should().Be(20);
            pageItems.Items.Should().BeEmpty();
        }

        [Test]
        public async Task PageAllAsync_ExistPage_ReturnItem()
        {
            var pageItems = await repository.PageAllAsync(_existPage, _pageSize);

            pageItems.Should().NotBeNull();
            pageItems.TotalCount.Should().Be(20);
            pageItems.Items.Should().HaveCount(5);
            pageItems.Items[0].Id.Should().Be(11);
        }

        [Test]
        public void PageAllAsync_WithCancelTokenThatCancelled_RaiseTaskCancelledException()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            tokenSource.Cancel();

            AsyncTestDelegate action = async () => await repository.PageAllAsync(token, _existPage, _pageSize);

            Assert.ThrowsAsync<TaskCanceledException>(action);
        }

        [Test]
        public async Task PageAllAsync_WithCancelTokenNotCancelled_ReturnItems()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            var pageItems = await repository.PageAllAsync(token, _existPage, _pageSize);

            pageItems.Should().NotBeNull();
            pageItems.TotalCount.Should().Be(20);
            pageItems.Items.Should().HaveCount(5);
            pageItems.Items[0].Id.Should().Be(11);
        }
        #endregion

        #region Update
        [Test]
        public void Update_GetEntry_AttachToSet()
        {
            //Arrange
            var entity = new TestDomainEntity() { Id = 2, Name = "Updated Entity" };

            //Act
            repository.Update(entity);

            //Assert
            mockSet.Verify(c => c.Attach(It.IsAny<TestEntity>()), Times.Exactly(1));
        }
        #endregion

        #region Remove
        [Test]
        public void Remove_ExistingItem_RemoveItem()
        {
            //Act
            repository.Remove(_existItemId);

            //Assert
            mockSet.Verify(c => c.Remove(It.IsAny<TestEntity>()), Times.Exactly(1));
        }

        [Test]
        public void Remove_NonExistingItem_NotRemoveItem()
        {
            //Act
            repository.Remove(_notExistItemId);

            //Assert
            mockSet.Verify(c => c.Remove(It.IsAny<TestEntity>()), Times.Never());
        }
        #endregion


    }
}
