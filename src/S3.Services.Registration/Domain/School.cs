using S3.Common.Types;
using System;

namespace S3.Services.Registration.Domain
{
    public class School: BaseEntity
    {
        public string Name { get; private set; }
        public string Category { get; private set; } // Primary, Secondary //***TODO: an enumeration might be better
        public Address Address { get; private set; } //***TODO: create an address object

        //private School()
        //{

        //}

        public School(Guid id, string name, string category, Address address)
         : base(id)
        {
            SetName(name);
            SetCategory(category);
            SetAddress(address);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new S3Exception("empty_school_name",
                    "School name cannot be empty.");
            }

            Name = name.Trim().ToLowerInvariant();
            SetUpdatedDate();
        }

        public void SetCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                throw new S3Exception("empty_category_name",
                    "School category cannot be empty.");
            }

            Category = category.Trim().ToLowerInvariant();
            SetUpdatedDate();
        }

        public void SetAddress(Address address)
        {
            if (address is null)
            {
                throw new S3Exception("empty_school_address",
                    "School address cannot be empty.");
            }

            Address = address;
            SetUpdatedDate();
        }
    }
}
