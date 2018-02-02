﻿using BusinessSolutions.Common.EntityFramework;
using Sanabel.Volunteers.Domain.Repositories;
using Sanabel.Volunteers.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Infra.UnitOfWork
{
    public class VolunteerUnitOfWork : BaseUnitOfWork, IVolunteerUnitOfWork
    {
        public IVolunteerRepository _volunteerRepository;
        public VolunteerUnitOfWork(VolunteersDbCotext dbContext) 
            : base(dbContext)
        {
            _volunteerRepository = new VolunteersRepository(dbContext);
        }

        public IVolunteerRepository VolunteerRepository => _volunteerRepository;
    }
}