using System;
using System.Collections.Generic;
using System.Text;

namespace YouTube.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public Video Video { get; set; }

        public ApplicationUser Poster { get; set; }

        public string Content { get; set; }

        public Comment Parent { get; set; }

        public DateTime CreatedOn { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
