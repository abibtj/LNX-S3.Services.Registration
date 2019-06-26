﻿using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3.Services.Registration.Domain.Models
{
    public class School: IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; } // Primary, Secondary //***TODO: an enumeration might be better
        public string Address { get; private set; } //***TODO: create an address object
        public DateTime CreatedAt { get; private set; }

        private School()
        {

        }

        public School(string name, string category, string address)
        {
            //***TODO: Do proper validation of the parameters before assignment
            Id = new Guid();
            Name = name;
            Category = category;
            Address = address;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
