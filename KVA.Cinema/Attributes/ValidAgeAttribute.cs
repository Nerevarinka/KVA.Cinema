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
        public int AgeMin { get; set; }

        /// <summary>
        /// Maximum age allowed
        /// </summary>
        public int AgeMax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ageMin">Minimum age allowed</param>
        /// <param name="ageMax">Maximum age allowed</param>
        public ValidAgeAttribute(int ageMin, int ageMax)
        {
            AgeMin = ageMin;
            AgeMax = ageMax;
        }

        public override bool IsValid(object birthDate)
        {
            return (DateTime)birthDate <= DateTime.Now.AddYears(-AgeMin) && (DateTime)birthDate >= DateTime.Now.AddYears(-AgeMax);
        }            
    }
}

