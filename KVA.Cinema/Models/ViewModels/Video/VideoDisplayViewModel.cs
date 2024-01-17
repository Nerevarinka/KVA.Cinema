namespace KVA.Cinema.Models.ViewModels.Video
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class VideoDisplayViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Country")]
        public int Length { get; set; }

        public Guid CountryId { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Released in")]
        public DateTime ReleasedIn { get; set; }

        [Display(Name = "Views")]
        public int Views { get; set; }

        [Display(Name = "Preview (poster)")]
        public string Preview { get; set; }

        public Guid PegiId { get; set; }

        [Display(Name = "PEGI")]
        public string PegiName { get; set; }

        public Guid LanguageId { get; set; }

        [Display(Name = "Language")]
        public string LanguageName { get; set; }

        public Guid DirectorId { get; set; }

        [Display(Name = "Director")]
        public string DirectorName { get; set; }

        public Guid GenresId { get; set; }

        [Display(Name = "Genre")]
        public string GenreName { get; set; }
    }
}
