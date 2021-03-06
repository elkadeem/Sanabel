﻿using BusinessSolutions.Common.Core.Entities;
using BusinessSolutions.Common.Core.Events;
using BusinessSolutions.Common.Infra.Validation;
using CommonSettings.Domain.Entities;
using Sanabel.Volunteers.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanabel.Volunteers.Domain.Model
{
    public class Volunteer : AggregateRoot
    {
        private Volunteer()
        {
        }

        public Volunteer(string name
            , string email
            , string phone
            , int cityId
            , int? districtId)
        {

            Guard.StringIsNull<ArgumentNullException>(name, nameof(name));
            Guard.StringIsNull<ArgumentNullException>(email, nameof(email));
            Guard.StringIsNull<ArgumentNullException>(phone, nameof(phone));
            Guard.LessThanOrEqualZero(cityId, nameof(cityId));

            Id = Guid.NewGuid();
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.CityId = cityId;
            this.DistrictId = districtId;

            AddDomainEvent(new VolunteerCreated
            {
                CityId = this.CityId,
                DistrictId = this.DistrictId,
                Email = this.Email,
                Name = this.Name,
            });
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public int CityId { get; private set; }

        public int? DistrictId { get; private set; }

        public bool HasCar { get; set; }

        public Genders Gender { get; set; }

        public string Address { get; set; }

        public string Notes { get; set; }

        public City City { get; private set; }

        public District District { get; private set; }

        public void Update(string name, string email, string phone, int cityId, int? districtId)
        {
            Guard.StringIsNull<ArgumentNullException>(name, nameof(name));
            Guard.StringIsNull<ArgumentNullException>(email, nameof(email));
            Guard.StringIsNull<ArgumentNullException>(phone, nameof(phone));
            Guard.LessThanOrEqualZero(cityId, nameof(cityId));

            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.CityId = cityId;
            this.DistrictId = districtId;

            AddDomainEvent(new VolunteerUpdated
            {
                CityId = this.CityId,
                DistrictId = this.DistrictId,
                Email = this.Email,
                Name = this.Name,
            });
        }
    }
}
