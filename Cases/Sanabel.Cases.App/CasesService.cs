﻿using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Core.Specifications;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Cases.App.Model;
using Sanabel.Cases.App.Resources;
using Sanable.Cases.Domain.Model;
using Sanable.Cases.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Cases.App
{
    public class CasesService : ICasesService
    {
        private ICaseUnitOfWork _caseUnitOfWork;
        public CasesService(ICaseUnitOfWork caseUnitOfWork)
        {
            _caseUnitOfWork = caseUnitOfWork ?? throw new ArgumentNullException("caseUnitOfWork");
        }

        public async Task<EntityResult> AddCase(CaseViewModel caseModel)
        {
            try
            {
                if (caseModel == null)
                    throw new ArgumentNullException(nameof(caseModel));

                Case newCase = new Case();
                PopulateCase(newCase, caseModel);

                var validationResult = ValidationCase(newCase);
                if(validationResult != null)
                    return EntityResult.Failed(validationResult.ToArray());

                _caseUnitOfWork.CaseRepository.Add(newCase);
                await _caseUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EntityResult> DeleteCase(Guid caseId)
        {
            try
            {
                _caseUnitOfWork.CaseRepository.Remove(caseId);
                await _caseUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CaseViewModel> GetCase(Guid caseId)
        {
            Case currentCase = await _caseUnitOfWork.CaseRepository.GetByIDAsync(caseId);
            return GetCaseViewModel(currentCase);
        }

        public async Task<PagedEntity<CaseViewModel>> GetCases(CaseSearchViewModel searchViewModel)
        {
            if (searchViewModel == null)
                throw new ArgumentNullException(nameof(searchViewModel));

            PagedEntity<Case> result = await _caseUnitOfWork.CaseRepository
                .SearchCases(searchViewModel.CaseName, searchViewModel.Phone
                , (Sanable.Cases.Domain.Model.CaseTypes)searchViewModel.CaseType, searchViewModel.CountryId
                , searchViewModel.RegionId, searchViewModel.CityId, searchViewModel.DistrictId
                , searchViewModel.PageIndex, searchViewModel.PageSize);

            return new PagedEntity<CaseViewModel>(result.Items.Select(c => GetCaseViewModel(c)), result.TotalCount);
        }

        public async Task<EntityResult> UpdateCase(CaseViewModel caseModel)
        {
            try
            {
                if (caseModel == null)
                    throw new ArgumentNullException(nameof(caseModel));

                Case currentCase = await _caseUnitOfWork.CaseRepository.GetByIDAsync(caseModel.CaseId);
                if (currentCase == null)
                    throw new ArgumentException(CasesResource.CaseIsNotExist, nameof(caseModel));

                PopulateCase(currentCase, caseModel);
                var validationResult = ValidationCase(currentCase);
                if (validationResult != null)
                    return EntityResult.Failed(validationResult.ToArray());

                _caseUnitOfWork.CaseRepository.Update(currentCase);
                await _caseUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PopulateCase(Case currentCase, CaseViewModel caseModel)
        {
            if (currentCase == null)
                throw new ArgumentNullException("currentCase");
            if (caseModel == null)
                throw new ArgumentNullException("caseModel");

            currentCase.Address = caseModel.Address;
            currentCase.CaseType = (Sanable.Cases.Domain.Model.CaseTypes)caseModel.CaseType;
            currentCase.CityId = caseModel.CityId;
            currentCase.Description = caseModel.Description;
            currentCase.DistrictId = caseModel.DistrictId;
            currentCase.Gender = (Sanable.Cases.Domain.Model.Genders)caseModel.Gender;
            currentCase.Name = caseModel.CaseName;
            currentCase.Phone = caseModel.Phone;
        }

        private CaseViewModel GetCaseViewModel(Case currentCase)
        {
            if (currentCase == null)
                return null;
            return new CaseViewModel
            {
                Address = currentCase.Address,
                CaseId = currentCase.Id,
                CaseType = (Model.CaseTypes)currentCase.CaseType,
                CaseName = currentCase.Name,
                CityId = currentCase.CityId,
                CityName = currentCase.City?.Name,
                CountryName = currentCase.City?.Region?.Country?.Name,
                Description = currentCase.Description,
                DistrictId = currentCase.DistrictId,
                DistrictName = currentCase.District?.Name,
                Gender = (Model.Genders)currentCase.Gender,
                Phone = currentCase.Phone,
                RegionName = currentCase.City?.Region?.Name,
                CountryId = currentCase.City?.Region?.CountryId,
                RegionId = currentCase.City?.RegionId,
            };
        }

        private List<ValidationError> ValidationCase(Case caseToValidate)
        {
            List<ValidationError> result = new List<ValidationError>();
            var isNameExistSpecification = new ExpressionSpecification<Case>(c => c.Id != caseToValidate.Id
                && c.Name.ToLower().Trim() == caseToValidate.Name.ToLower().Trim());

            if (_caseUnitOfWork.CaseRepository.Find(isNameExistSpecification).Any())
                result.Add(new ValidationError(CasesResource.CaseNameExist, ValidationErrorTypes.BusinessError));

            var isPhoneExistSpecification = new ExpressionSpecification<Case>(c => c.Id != caseToValidate.Id
                && c.Phone.ToLower().Trim() == caseToValidate.Phone.ToLower().Trim());

            if (_caseUnitOfWork.CaseRepository.Find(isNameExistSpecification).Any())
                result.Add(new ValidationError(CasesResource.CaseNameExist, ValidationErrorTypes.BusinessError));

            return result.Count == 0 ? null : result;
        }
    }
}