namespace KVA.Cinema.Models.ViewModels.Video
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class VideoEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(128, ErrorMessage = "Title length cannot be more than 128 symbols")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(600, ErrorMessage = "Title length cannot be more than 600 symbols")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Length in minutes")]
        public int Length { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Country")]
        public Guid CountryId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date)]
        [Display(Name = "Released in")]
        public DateTime ReleasedIn { get; set; }

        [Display(Name = "Views")]
        public int Views { get; set; }

        [Display(Name = "Preview (poster)")]
        public string Preview { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "PEGI")]
        public Guid PegiId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Language")]
        public Guid LanguageId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Director")]
        public Guid DirectorId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Genres")]
        public Guid GenresId { get; set; }
    }
}
