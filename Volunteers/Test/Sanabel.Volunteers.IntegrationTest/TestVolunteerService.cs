using NUnit.Framework;
using Sanabel.Volunteers.Application.Services;
using Sanabel.Volunteers.Infra;
using Sanabel.Volunteers.Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.IntegrationTest
{
    [TestFixture]
    public class TestVolunteerService
    {
        private IVolunteerService _volunteerService;
        private CommonSettings.DAL.CommonSettingDataContext _commonSettingDataContext;
        [OneTimeSetUp]
        public void Initiat()
        {
            VolunteersDbCotext volunteersDbCotext = new VolunteersDbCotext();
            VolunteerUnitOfWork unitOfWork = new VolunteerUnitOfWork(volunteersDbCotext);
            _volunteerService = new VolunteerService(unitOfWork, new TestLogger());

            _commonSettingDataContext = new CommonSettings.DAL.CommonSettingDataContext();
        }

        [Test]
        public async Task CreateVolunteer_WithValidData_CreateVolunteer()
        {
            var volunteer = new Application.Models.VolunteerViewModel {
                Address = "Address",
                CityId = _commonSettingDataContext.Cities.First().Id,
                Email ="new@email.com",
                Gender = Application.Models.Genders.Male,
                Name = "Name",
                Notes = "Notes",
                Phone = "Phone",
                DistrictId = _commonSettingDataContext.Districts.First().Id,
            };
             var result = await _volunteerService.AddVolunteer(volunteer);
            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        public async Task CreateVolunteer_WithDublicatedData_ReturnErrors()
        {
            var volunteer = new Application.Models.VolunteerViewModel
            {
                Address = "Address",
                CityId = _commonSettingDataContext.Cities.First().Id,
                Email = "new1@email.com",
                Gender = Application.Models.Genders.Male,
                Name = "Name",
                Notes = "Notes",
                Phone = "Phone1",
                DistrictId = _commonSettingDataContext.Districts.First().Id,
            };
            var result = await _volunteerService.AddVolunteer(volunteer);
            Assert.IsTrue(result.Succeeded);

            volunteer = new Application.Models.VolunteerViewModel
            {
                Address = "Address",
                CityId = _commonSettingDataContext.Cities.First().Id,
                Email = "new1@email.com",
                Gender = Application.Models.Genders.Male,
                Name = "Name",
                Notes = "Notes",
                Phone = "Phone1",
                DistrictId = _commonSettingDataContext.Districts.First().Id,
            };

            result = await _volunteerService.AddVolunteer(volunteer);
            Assert.IsFalse(result.Succeeded);
        }

        [Test]
        public async Task UpdateVolunteer_WithValidData_UpdateVolunteer()
        {
            var volunteer = new Application.Models.VolunteerViewModel
            {
                Address = "Address",
                CityId = _commonSettingDataContext.Cities.First().Id,
                Email = "new@email.com",
                Gender = Application.Models.Genders.Male,
                Name = "Name",
                Notes = "Notes",
                Phone = "Phone",
                DistrictId = _commonSettingDataContext.Districts.First().Id,
            };
            var result = await _volunteerService.AddVolunteer(volunteer);
            Assert.IsTrue(result.Succeeded);
            
            volunteer.Address = "UpdatedAddress";
            volunteer.Name = "UpdateName";

            result = await _volunteerService.UpdateVolunteer(volunteer);
            Assert.IsTrue(result.Succeeded);

            var updatedVolunteer = await _volunteerService.GetVolunteer(volunteer.Id);
            Assert.AreEqual(updatedVolunteer.Address, volunteer.Address);
            Assert.AreEqual(updatedVolunteer.Name, volunteer.Name);
        }
    }
}
