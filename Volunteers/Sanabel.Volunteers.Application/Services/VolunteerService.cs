using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Core.Validation;
using BusinessSolutions.Common.Infra.Log;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Volunteers.Application.Models;
using Sanabel.Volunteers.Domain.Model;
using Sanabel.Volunteers.Domain.Repositories;
using Sanabel.Volunteers.Domain.Specifications;
using Sanabel.Volunteers.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Sanabel.Volunteers.Application.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerUnitOfWork _volunteerUnitOfWork;
        private readonly ILogger _logger;
        public VolunteerService(IVolunteerUnitOfWork volunteerUnitOfWork, ILogger logger)
        {
            Guard.ArgumentIsNull<ArgumentNullException>(volunteerUnitOfWork, nameof(volunteerUnitOfWork));
            Guard.ArgumentIsNull<ArgumentNullException>(logger, nameof(logger));
            _volunteerUnitOfWork = volunteerUnitOfWork;
            _logger = logger;
        }

        public async Task<EntityResult> AddVolunteer(VolunteerViewModel volunteerModel)
        {
            try
            {
                Volunteer volunteer = new Volunteer(volunteerModel.Name, volunteerModel.Email
                    , volunteerModel.Phone, volunteerModel.CityId, volunteerModel.DistrictId)
                {
                    Gender = (Domain.Model.Genders)volunteerModel.Gender,
                    HasCar = volunteerModel.HasCar,
                    Address = volunteerModel.Address,
                    Notes = volunteerModel.Notes,
                };

                var validationResult = ValidateVolunteer(volunteer);
                if(!validationResult.IsValid)
                {
                    return EntityResult.Failed(validationResult.ValidationErrors
                        .Select(c => new EntityError(c.Message, ValidationErrorTypes.BusinessError))
                        .ToArray());
                }

                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _volunteerUnitOfWork.VolunteerRepository.AddVolunteer(volunteer);
                    await _volunteerUnitOfWork.SaveAsync();
                    
                    transactionScope.Complete();
                    volunteerModel.Id = volunteer.Id;
                    return EntityResult.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public async Task<VolunteerViewModel> GetVolunteer(Guid id)
        {
            var volunteer = await _volunteerUnitOfWork.VolunteerRepository.GetVolunteerById(id);
            return GetVolunteerViewModel(volunteer);
        }

        public async Task<PagedEntity<ViewVolunteerViewModel>> SearchVolunteers(SearchVolunteersViewModel searchVolunteerModel)
        {
            var result = await _volunteerUnitOfWork.VolunteerRepository.SearchVolunteer(searchVolunteerModel.VolunteerName
                , searchVolunteerModel.VolunteerEmail, searchVolunteerModel.Phone, searchVolunteerModel.CountryId, searchVolunteerModel.RegionId
                , searchVolunteerModel.CityId, searchVolunteerModel.DistrictId
                , searchVolunteerModel.Gender == null ? null : (Domain.Model.Genders?)searchVolunteerModel.Gender, searchVolunteerModel.PageIndex, searchVolunteerModel.PageSize);

            return new PagedEntity<ViewVolunteerViewModel>(result.Items.Select(c => GetViewVolunteerViewModel(c)), result.TotalCount);
        }

        public async Task<EntityResult> UpdateVolunteer(VolunteerViewModel volunteerModel)
        {
            try
            {
                var volunteer = await _volunteerUnitOfWork.VolunteerRepository.GetVolunteerById(volunteerModel.Id);
                Guard.ArgumentIsNull<ArgumentException>(volunteer, nameof(volunteer), VolunteerResource.VolunteerNotFound);
                volunteer.Update(volunteerModel.Name
                    , volunteerModel.Email, volunteerModel.Phone
                    , volunteerModel.CityId
                    , volunteerModel.DistrictId);
                volunteer.Gender = (Domain.Model.Genders)volunteerModel.Gender;
                volunteer.HasCar = volunteerModel.HasCar;
                volunteer.Address = volunteerModel.Address;
                volunteer.Notes = volunteerModel.Notes;


                await _volunteerUnitOfWork.VolunteerRepository.UpdateVolunteer(volunteer);
                await _volunteerUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        private VolunteerViewModel GetVolunteerViewModel(Volunteer volunteer)
        {
            if (volunteer == null)
                return null;

            return new VolunteerViewModel
            {
                Address = volunteer.Address,
                CityId = volunteer.CityId,
                CountryId = volunteer.City?.Region?.CountryId,
                DistrictId = volunteer.DistrictId,
                Email = volunteer.Email,
                Name = volunteer.Name,
                Gender = (Models.Genders)volunteer.Gender,
                HasCar = volunteer.HasCar,
                Id = volunteer.Id,
                Phone = volunteer.Phone,
                Notes = volunteer.Notes,
                RegionId = volunteer.City?.RegionId,
                CityName = volunteer.City?.Name,
                CountryName = volunteer.City?.Region?.Country.Name,
                DistrictName = volunteer.District?.Name,
                RegionName = volunteer.City?.Region?.Name,
            };
        }

        private ViewVolunteerViewModel GetViewVolunteerViewModel(Volunteer volunteer)
        {
            if (volunteer == null)
                return null;

            return new ViewVolunteerViewModel
            {
                Address = volunteer.Address,
                CityName = volunteer.City?.Name,
                CountryName = volunteer.City?.Region?.Country.Name,
                DistrictName = volunteer.District?.Name,
                VolunteerEmail = volunteer.Email,
                VolunteerName = volunteer.Name,
                RegionName = volunteer.City?.Region?.Name,
                Gender = (Models.Genders)volunteer.Gender,
                Phone = volunteer.Phone,
                Id = volunteer.Id,
            };
        }

        private ValidationResult ValidateVolunteer(Volunteer volunteer)
        {
            EntityValidator<Volunteer> entityValidator = new EntityValidator<Volunteer>();
            entityValidator.Add("Phone Is Unique"
                , new ValidationRule<Volunteer>(new VolunteerPhoneIsUniqueSpecifications(_volunteerUnitOfWork.VolunteerRepository)
                , nameof(volunteer.Phone), VolunteerResource.PhoneExist));

            entityValidator.Add("Email Is Unique"
                , new ValidationRule<Volunteer>(new VolunteerEmailIsUniqueSpecifications(_volunteerUnitOfWork.VolunteerRepository)
                , nameof(volunteer.Phone), VolunteerResource.EmailExist));

            return entityValidator.Validate(volunteer);
        }
    }
}
