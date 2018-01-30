using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.Infra.Validation;
using Sanabel.Volunteers.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Application.Services
{
    public interface IVolunteerService
    {
        Task<EntityResult> AddVolunteer(VolunteerViewModel volunteerModel);

        Task<PagedEntity<ViewVolunteerViewModel>> SearchVolunteers(SearchVolunteersViewModel searchVolunteerModel);

        Task<VolunteerViewModel> GetVolunteer(Guid id);

        Task<EntityResult> UpdateVolunteer(VolunteerViewModel VolunteerModel);
    }
}
