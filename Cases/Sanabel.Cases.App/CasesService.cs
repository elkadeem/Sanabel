using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Core.Specifications;
using BusinessSolutions.Common.Infra.Log;
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
        private readonly ICaseUnitOfWork _caseUnitOfWork;
        private readonly ILogger _logger;

        public CasesService(ICaseUnitOfWork caseUnitOfWork, ILogger logger)
        {
            this._caseUnitOfWork = caseUnitOfWork ?? throw new ArgumentNullException("caseUnitOfWork");
            this._logger = logger;
        }

        public async Task<EntityResult> AddCase(CaseViewModel caseModel)
        {
            try
            {
                if (caseModel == null)
                    throw new ArgumentNullException(nameof(caseModel));

                Case newCase = new Case();
                PopulateCase(newCase, caseModel);
                newCase.CaseStatus = Sanable.Cases.Domain.Model.CaseStatus.New;

                var validationResult = ValidationCase(newCase);
                if (validationResult != null)
                    return EntityResult.Failed(validationResult.ToArray());

                _caseUnitOfWork.CaseRepository.Add(newCase);
                await _caseUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
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
                _logger.Error(ex);
                throw;
            }
        }

        public async Task<EntityResult> UpdateCaseStatus(CaseActionViewModel caseModel)
        {
            try
            {
                if (caseModel == null)
                    throw new ArgumentNullException(nameof(caseModel));

                Case currentCase = await _caseUnitOfWork.CaseRepository.GetByIDAsync(caseModel.CaseId);
                if (currentCase == null)
                    throw new ArgumentException(CasesResource.CaseIsNotExist, nameof(caseModel));

                var caseAction = new CaseAction
                {
                    CaseActionDate = DateTime.Now,
                    CaseId = currentCase.Id,
                    Comment = caseModel.Comment,
                    OldStatus = currentCase.CaseStatus,
                    Status = (Sanable.Cases.Domain.Model.CaseStatus)caseModel.Status,
                    StartApplyDate = caseModel.StartApplyDate,
                };

                currentCase.CaseActions.Add(caseAction);
                currentCase.CaseStatus = caseAction.Status;
                if (caseAction.Status == Sanable.Cases.Domain.Model.CaseStatus.Suspended)
                {
                    currentCase.CaseSuspensionDate = caseAction.StartApplyDate;
                }

                await _caseUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public async Task<CaseViewModel> GetCase(Guid caseId)
        {
            Case currentCase = await _caseUnitOfWork.CaseRepository.GetByIDAsync(caseId);
            return GetCaseViewModel(currentCase);
        }

        public async Task<CaseActionViewModel> GetCaseAction(Guid caseId)
        {
            Case currentCase = await _caseUnitOfWork.CaseRepository.GetByIDAsync(caseId);
            CaseActionViewModel caseAction = GetCaseActionViewModel(currentCase);
            return caseAction;
        }

        public async Task<PagedEntity<CaseViewModel>> GetCases(SearchCaseViewModel searchViewModel)
        {
            if (searchViewModel == null)
                throw new ArgumentNullException(nameof(searchViewModel));

            var caseSearch = new CaseSearch
            {
                CaseName = searchViewModel.CaseName,
                CaseStatus = (Sanable.Cases.Domain.Model.CaseStatus)searchViewModel.CaseStatus,
                CaseType = (Sanable.Cases.Domain.Model.CaseTypes)searchViewModel.CaseType,
                CityId = searchViewModel.CityId,
                CountryId = searchViewModel.CountryId,
                DistrictId = searchViewModel.DistrictId,
                Phone = searchViewModel.Phone,

            };

            PagedEntity<Case> result = await _caseUnitOfWork.CaseRepository
                .SearchCases(caseSearch
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
                _logger.Error(ex);
                throw;
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
                CaseStatus = (Model.CaseStatus)currentCase.CaseStatus,
            };
        }

        private List<EntityError> ValidationCase(Case caseToValidate)
        {
            List<EntityError> result = new List<EntityError>();
            var isNameExistSpecification = new ExpressionSpecification<Case>(c => c.Id != caseToValidate.Id
                && c.Name.ToLower().Trim() == caseToValidate.Name.ToLower().Trim());

            if (_caseUnitOfWork.CaseRepository.Find(isNameExistSpecification).Any())
                result.Add(new EntityError(CasesResource.CaseNameExist, ValidationErrorTypes.BusinessError));

            var isPhoneExistSpecification = new ExpressionSpecification<Case>(c => c.Id != caseToValidate.Id
                && c.Phone.ToLower().Trim() == caseToValidate.Phone.ToLower().Trim());

            if (_caseUnitOfWork.CaseRepository.Find(isNameExistSpecification).Any())
                result.Add(new EntityError(CasesResource.CaseNameExist, ValidationErrorTypes.BusinessError));

            if (_caseUnitOfWork.CaseRepository.Find(isPhoneExistSpecification).Any())
                result.Add(new EntityError(CasesResource.CasePhoneExist, ValidationErrorTypes.BusinessError));

            return result.Count == 0 ? null : result;
        }

        private CaseActionViewModel GetCaseActionViewModel(Case currentCase)
        {
            if (currentCase == null)
                return null;
            //get case action by case ID

            return new CaseActionViewModel
            {
                CaseId = currentCase.Id,
                //Action=currentCase.CaseStatus.ToString(),
                //CaseActionDate = DateTime.Now,
                //CaseSuspensionDate = DateTime.Now,
                //Comment= "",
                //CreatedBy="username",
                //oldStatus = (Model.CaseStatus)currentCase.CaseStatus,
                //Status = CaseStatusTypes.Rejected,   
                Case = GetCaseViewModel(currentCase)
            };
        }

        public int GetCasesCount(Model.CaseStatus? caseStatus)
        {
            return _caseUnitOfWork.CaseRepository
                .GetCasesCount((Sanable.Cases.Domain.Model.CaseStatus?)caseStatus);
        }

        #region Case Aids
        public async Task<CaseAidsViewModel> GetCaseAids(Guid caseId)
        {
            if (caseId == Guid.Empty)
                throw new ArgumentNullException(nameof(caseId));

            var currentCase = await _caseUnitOfWork.CaseRepository
                 .GetCaseWithAids(caseId);

            if (currentCase == null)
                throw new ArgumentException(CasesResource.CaseIsNotExist, nameof(caseId));

            return new CaseAidsViewModel()
            {
                Case = GetCaseViewModel(currentCase),
                CaseId = currentCase.Id,
                CaseAids = currentCase.CaseAids.Select(c => new CaseAidViewModel
                {
                    AidDate = c.AidDate,
                    AidId = c.Id,
                    Amount = c.AidAmount,
                    CaseId = c.CaseId,
                    Description = c.AidDescription,
                    Notes = c.Notes
                }).ToList()
            };

        }

        public async Task<EntityResult> AddCaseAid(Guid caseId
            , CaseAidViewModel caseAidViewModel)
        {
            try
            {
                Guard.GuidIsEmpty<ArgumentNullException>(caseId, nameof(caseId));
                Guard.ArgumentIsNull<ArgumentNullException>(caseAidViewModel
                    , nameof(caseAidViewModel));

                var currentCase = await _caseUnitOfWork.CaseRepository
                     .GetCaseWithAids(caseId);

                if (currentCase == null)
                    throw new ArgumentException(CasesResource.CaseIsNotExist
                        , nameof(caseId));

                currentCase.AddAid(
                    (Sanable.Cases.Domain.Model.AidTypes)caseAidViewModel.AidType
                    , caseAidViewModel.Description
                    , caseAidViewModel.AidDate
                    , caseAidViewModel.Amount
                    , caseAidViewModel.Notes);

                await _caseUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

        }

        public async Task<EntityResult> UpdateCaseAid(CaseAidViewModel caseAidViewModel)
        {
            try
            {
                Guard.ArgumentIsNull<ArgumentNullException>(caseAidViewModel
                    , nameof(caseAidViewModel));

                var currentCase = await _caseUnitOfWork.CaseRepository
                     .GetCaseWithAids(caseAidViewModel.CaseId);

                if (currentCase == null)
                    throw new ArgumentException(CasesResource.CaseIsNotExist
                        , nameof(caseAidViewModel));

                currentCase.UpdateAid(
                    caseAidViewModel.AidId
                    , caseAidViewModel.Description
                    , caseAidViewModel.AidDate
                    , caseAidViewModel.Amount
                    , caseAidViewModel.Notes);

                await _caseUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        public async Task<EntityResult> DeleteCaseAid(CaseAidViewModel caseAidViewModel)
        {
            try
            {
                Guard.ArgumentIsNull<ArgumentNullException>(caseAidViewModel
                    , nameof(caseAidViewModel));

                var currentCase = await _caseUnitOfWork.CaseRepository
                     .GetCaseWithAids(caseAidViewModel.CaseId);

                if (currentCase == null)
                    throw new ArgumentException(CasesResource.CaseIsNotExist
                        , nameof(caseAidViewModel));

                currentCase.DeleteAid(caseAidViewModel.AidId);
                await _caseUnitOfWork.SaveAsync();
                return EntityResult.Success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
        #endregion

    }
}
