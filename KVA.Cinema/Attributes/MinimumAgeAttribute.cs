namespace KVA.Cinema.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class ValidAgeAttribute : ValidationAttribute
    {
        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public ValidAgeAttribute(int minAge, int maxAge)
        {
            MinAge = minAge;
            MaxAge = maxAge;
        }

        public override bool IsValid(object birthDate)
        {
            return (DateTime)birthDate >= DateTime.Now.AddYears(-MinAge) && (DateTime)birthDate <= DateTime.Now.AddYears(-MaxAge);
        }            
    }
}

