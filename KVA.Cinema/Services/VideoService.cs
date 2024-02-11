﻿namespace KVA.Cinema.Services
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

        private const string POSTER_UPLOAD_PATH = "upload/videoPreview";

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
                Name = x.Title,
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
                Name = x.Title,
                Description = x.Description,
                Length = x.Length,
                CountryId = x.CountryId,
                ReleasedIn = x.ReleasedIn,
                Views = x.Views,
                PreviewFileName = x.Preview,
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

        private string SaveFile(IFormFile file, string destinationFolderName)
        {
            //checks
            
            DirectoryInfo drInfo = new DirectoryInfo(destinationFolderName);

            if (!drInfo.Exists)
            {
                drInfo.Create();
            }

            string fileNewName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string pathToFile = Path.Combine(destinationFolderName, fileNewName);

            using (var stream = new FileStream(pathToFile, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileNewName;
        }

        public void CreateAsync(VideoCreateViewModel videoData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(videoData.Name,
                                                        videoData.CountryId,
                                                        videoData.ReleasedIn,
                                                        videoData.PegiId,
                                                        videoData.LanguageId,
                                                        videoData.DirectorId))
            //videoData.GenresId))
            {
                throw new ArgumentNullException("One or more required fields have no value");
            }

            if (videoData.Name.Length > TITLE_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {TITLE_LENGHT_MAX} symbols");
            }

            if (videoData.ReleasedIn.ToUniversalTime() > DateTime.UtcNow)
            {
                throw new ArgumentException($"Only released video can be uploaded");
            }

            if (Context.Videos.FirstOrDefault(x => x.Title == videoData.Name && x.DirectorId == videoData.DirectorId) != default)
            {
                throw new DuplicatedEntityException($"Video with title \"{videoData.Name}\" by this director is already exist");
            }

            Guid videoId = Guid.NewGuid();

            string previewNewName = null;

            if (videoData.Preview != null)
            {
                string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, POSTER_UPLOAD_PATH);

                previewNewName = SaveFile(videoData.Preview, uploadsFolder);
            }

            Video newVideo = new Video()
            {
                Id = videoId,
                Title = videoData.Name,
                Description = videoData.Description,
                Length = videoData.Length,
                CountryId = videoData.CountryId,
                ReleasedIn = videoData.ReleasedIn.ToUniversalTime(),
                Views = 0,
                Preview = previewNewName,
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

            var preview = video.Preview;

            Context.Videos.Remove(video);
            Context.SaveChanges();

            if (preview != null)
            {
                var previewFolderPath = Path.Combine(HostEnvironment.WebRootPath, POSTER_UPLOAD_PATH);
                var previewFullPath = previewFolderPath + "\\" + preview;

                File.Delete(previewFullPath);
            }
        }

        public void Update(Guid videoId, VideoEditViewModel newVideoData) //add check for id
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(videoId,
                                                        newVideoData.Name,
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

            if (newVideoData.Name.Length > TITLE_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {TITLE_LENGHT_MAX} symbols");
            }

            if (newVideoData.ReleasedIn.ToUniversalTime() > DateTime.UtcNow)
            {
                throw new ArgumentException($"Only released video can be uploaded");
            }

            Video duplicate = Context.Videos.FirstOrDefault(x =>
                                                               x.Title == newVideoData.Name &&
                                                               x.DirectorId == newVideoData.DirectorId &&
                                                               x.Id != newVideoData.Id);
            if (duplicate != default)
            {
                throw new DuplicatedEntityException($"Video with title \"{newVideoData.Name}\" by this director is already exist");
            }

            string previewNewName = null;

            if (newVideoData.Preview != null)
            {
                string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, POSTER_UPLOAD_PATH);

                previewNewName = SaveFile(newVideoData.Preview, uploadsFolder);
            }

            var preview = video.Preview;

            video.Title = newVideoData.Name;
            video.Description = newVideoData.Description;
            video.Length = newVideoData.Length;
            video.CountryId = newVideoData.CountryId;
            video.ReleasedIn = newVideoData.ReleasedIn.ToUniversalTime();
            video.Views = video.Views;
            video.Preview = previewNewName;
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

            if (preview != null)
            {
                var previewFolderPath = Path.Combine(HostEnvironment.WebRootPath, POSTER_UPLOAD_PATH);
                var previewFullPath = previewFolderPath + "\\" + preview;

                File.Delete(previewFullPath);
            }
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
