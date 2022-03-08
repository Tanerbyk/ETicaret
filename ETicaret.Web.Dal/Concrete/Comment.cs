using System.ComponentModel.DataAnnotations;

namespace ETicaret.Shared.Dal.Concrete
{
    public class Comment : BaseEntity
    {
        [Key]
        public int CommentID { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public int ProductID { get; set; }

        public Product Product { get; set; }







    }
}