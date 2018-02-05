using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Log;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Volunteers.Application.Models;
using Sanabel.Volunteers.Domain.Model;
using Sanabel.Volunteers.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Application.Services
{
    public class VolunteerService : IVolunteerService
    {
        private IVolunteerUnitOfWork _volunteerUnitOfWork;
        private ILogger _logger;
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

                await _volunteerUnitOfWork.VolunteerRepository.AddVolunteer(volunteer);
                await _volunteerUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
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
                , searchVolunteerModel.Gender == null? null : (Domain.Model.Genders?)searchVolunteerModel.Gender, searchVolunteerModel.PageIndex, searchVolunteerModel.PageSize);

            return new PagedEntity<ViewVolunteerViewModel>(result.Items.Select(c => GetViewVolunteerViewModel(c)), result.TotalCount);
        }

        public async Task<EntityResult> UpdateVolunteer(VolunteerViewModel volunteerModel)
        {
            try
            {
                var volunteer = await _volunteerUnitOfWork.VolunteerRepository.GetVolunteerById(volunteerModel.Id);
                Guard.ArgumentIsNull<ArgumentException>(volunteer, nameof(volunteer), Localization.VolunteerResource.VolunteerNotFound);
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
                throw ex;
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
    }
}
