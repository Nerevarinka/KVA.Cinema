namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Models.ViewModels.Video;
    using KVA.Cinema.Utilities;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class VideoService : IService<VideoCreateViewModel, VideoDisplayViewModel, VideoEditViewModel>
    {
        /// <summary>
        /// Maximum length allowed for title
        /// </summary>
        private const int TITLE_LENGHT_MAX = 128;

        private CinemaContext Context { get; set; }

        private IWebHostEnvironment HostEnvironment { get; }

        public VideoService(CinemaContext db, IWebHostEnvironment hostEnvironment)
        {
            Context = db;
            HostEnvironment = hostEnvironment;
        }

        public IEnumerable<VideoCreateViewModel> Read()
        {
            List<Video> videos = Context.Videos.ToList();

            return videos.Select(x => new VideoCreateViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Length = x.Length,
                CountryId = x.CountryId,
                ReleasedIn = x.ReleasedIn,
                Views = x.Views,
                PreviewFileName = x.Preview,
                PegiId = x.PegiId,
                LanguageId = x.LanguageId,
                DirectorId = x.DirectorId,

                //temp. TO DO
                //GenresId = x.VideoGenres.Any() ? x.VideoGenres.First().GenreId : Guid.Empty,
            });
        }

        public IEnumerable<VideoDisplayViewModel> ReadAll()
        {
            return Context.Videos.Select(x => new VideoDisplayViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Length = x.Length,
                CountryId = x.CountryId,
                ReleasedIn = x.ReleasedIn,
                Views = x.Views,
                Preview = x.Preview,
                PegiId = x.PegiId,
                LanguageId = x.LanguageId,
                DirectorId = x.DirectorId,
                CountryName = x.Country.Name,
                PegiName = x.Pegi.Type.ToString() + "+",
                LanguageName = x.Language.Name,
                DirectorName = x.Director.Name,

                //temp. TO DO
                //GenresId = x.VideoGenres.Any() ? x.VideoGenres.First().GenreId : Guid.Empty,
            }).ToList();
        }

        public string SaveFile(IFormFile file, string folderName)
        {
            //checks

            string fileName = file.FileName;

            string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, "upload/videoPreview");

            DirectoryInfo directoryInfo = new DirectoryInfo(uploadsFolder);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            string pathToFile = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(pathToFile, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        public void CreateAsync(VideoCreateViewModel videoData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(videoData.Title,
                                                        videoData.CountryId,
                                                        videoData.ReleasedIn,
                                                        videoData.PegiId,
                                                        videoData.LanguageId,
                                                        videoData.DirectorId))
            //videoData.GenresId))
            {
                throw new ArgumentNullException("One or more required fields have no value");
            }

            if (videoData.Title.Length > TITLE_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {TITLE_LENGHT_MAX} symbols");
            }

            if (videoData.ReleasedIn > DateTime.UtcNow)
            {
                throw new DuplicatedEntityException($"Only released video can be uploaded");
            }

            if (Context.Videos.FirstOrDefault(x => x.Title == videoData.Title && x.DirectorId == videoData.DirectorId) != default)
            {
                throw new DuplicatedEntityException($"Video with title \"{videoData.Title}\" by this director is already exist");
            }

            Guid videoId = Guid.NewGuid();

            if (videoData.Preview != null)
            {
                string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, "upload/videoPreview");
                DirectoryInfo drInfo = new DirectoryInfo(uploadsFolder);

                if (!drInfo.Exists)
                {
                    drInfo.Create();
                }

                videoData.PreviewFileName = Path.Combine(uploadsFolder, videoData.Preview.FileName);

                using (var stream = new FileStream(videoData.PreviewFileName, FileMode.Create))
                {
                    videoData.Preview.CopyTo(stream);
                }
            }

            Video newVideo = new Video()
            {
                Id = videoId,
                Title = videoData.Title,
                Description = videoData.Description,
                Length = videoData.Length,
                CountryId = videoData.CountryId,
                ReleasedIn = videoData.ReleasedIn,
                Views = 0,
                Preview = videoData.Preview.FileName, //videoData.PreviewSource,
                PegiId = videoData.PegiId,
                LanguageId = videoData.LanguageId,
                DirectorId = videoData.DirectorId,
                //VideoGenres = new List<VideoGenre>()
                //{
                //    new VideoGenre
                //    {
                //        Id = Guid.NewGuid(),
                //        GenreId = videoData.GenresId,
                //        VideoId = videoId
                //    }
                //}
            };

            Context.Videos.Add(newVideo);
            Context.SaveChanges();
        }

        public void Delete(Guid videoId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(videoId))
            {
                throw new ArgumentNullException("Video Id has no value");
            }

            Video video = Context.Videos.FirstOrDefault(x => x.Id == videoId);

            if (video == default)
            {
                throw new EntityNotFoundException($"Video with Id \"{videoId}\" not found");
            }

            Context.Videos.Remove(video);
            Context.SaveChanges();
        }

        public void Update(Guid videoId, VideoEditViewModel newVideoData) //add check for id
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(videoId,
                                                        newVideoData.Title,
                                                        newVideoData.CountryId,
                                                        newVideoData.ReleasedIn,
                                                        newVideoData.PegiId,
                                                        newVideoData.LanguageId,
                                                        newVideoData.DirectorId))
            //newVideoData.GenresId))
            {
                throw new ArgumentNullException("One or more required fields have no value");
            }

            Video video = Context.Videos.FirstOrDefault(x => x.Id == videoId);

            if (video == default)
            {
                throw new EntityNotFoundException($"Video with id \"{videoId}\" not found");
            }

            if (newVideoData.Title.Length > TITLE_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {TITLE_LENGHT_MAX} symbols");
            }

            Video duplicate = Context.Videos.FirstOrDefault(x =>
                                                               x.Title == newVideoData.Title &&
                                                               x.DirectorId == newVideoData.DirectorId &&
                                                               x.Id != newVideoData.Id);
            if (duplicate != default)
            {
                throw new DuplicatedEntityException($"Video with title \"{newVideoData.Title}\" by this director is already exist");
            }

            video.Title = newVideoData.Title;
            video.Description = newVideoData.Description;
            video.Length = newVideoData.Length;
            video.CountryId = newVideoData.CountryId;
            video.ReleasedIn = newVideoData.ReleasedIn;
            video.Views = video.Views;
            video.Preview = newVideoData.Preview;
            video.PegiId = newVideoData.PegiId;
            video.LanguageId = newVideoData.LanguageId;
            video.DirectorId = newVideoData.DirectorId;
            //video.VideoGenres = new List<VideoGenre>() //Check this
            //    {
            //        new VideoGenre
            //        {
            //            Id = Guid.NewGuid(),
            //            GenreId = newVideoData.GenresId,
            //            VideoId = newVideoData.Id
            //        }
            //    };

            Context.SaveChanges();
        }

        public bool IsEntityExist(Guid videoId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(videoId))
            {
                return false;
            }

            Video video = Context.Videos.FirstOrDefault(x => x.Id == videoId);

            return video != default;
        }
    }
}
