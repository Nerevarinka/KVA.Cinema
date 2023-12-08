﻿namespace KVA.Cinema.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentMark
    {
        public Guid Id { get; set; }

        /// <summary>
        /// True for like, false for dislike
        /// </summary>
        public bool Type { get; set; }

        public Guid CommentId { get; set; }

        public Guid UserId { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual User User { get; set; }
    }
}
