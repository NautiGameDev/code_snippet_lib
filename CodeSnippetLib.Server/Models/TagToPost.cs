using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSnippetLib.Server.Models
{
    [Table("TagToPost")]
    public class TagToPost
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Post? Post { get; set; }
        public int TagId { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
