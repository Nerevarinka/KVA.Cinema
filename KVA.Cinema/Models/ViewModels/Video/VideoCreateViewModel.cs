namespace KVA.Cinema.Models.ViewModels.Video
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class VideoCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required1 field")]
        [StringLength(1, ErrorMessage = "Title length cannot be more than 128 symbols]]]]]]]]]]")]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [StringLength(6, ErrorMessage = "Title length cannot be more than XXX_600_XXX symbols")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Length in minutes")]
        public int Length { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        [Required(ErrorMessage = "Required2 field")]
        [DataType(DataType.Date)]
        [Display(Name = "Released in")]
        public DateTime ReleasedIn { get; set; }

        [Display(Name = "Views")]
        public int Views { get; set; }

        [Display(Name = "Poster")]
        public IFormFile Preview { get; set; }

        public string PreviewFileName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "PEGI")]
        public Guid PegiId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Language")]
        public Guid LanguageId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Director")]
        public Guid DirectorId { get; set; }

        //[Required(ErrorMessage = "Required field")]
        //[Display(Name = "Genres")]
        //public Guid GenresId { get; set; }
    }
}
