namespace KVA.Cinema.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class ValidAgeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Minimum age allowed
        /// </summary>
        public int MinAge { get; set; }

        /// <summary>
        /// Maximum age allowed
        /// </summary>
        public int MaxAge { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minAge">Minimum age allowed</param>
        /// <param name="maxAge">Maximum age allowed</param>
        public ValidAgeAttribute(int minAge, int maxAge)
        {
            MinAge = minAge;
            MaxAge = maxAge;
        }

        public override bool IsValid(object birthDate)
        {
            return (DateTime)birthDate <= DateTime.Now.AddYears(-MinAge) && (DateTime)birthDate >= DateTime.Now.AddYears(-MaxAge);
        }            
    }
}

