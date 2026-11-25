using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSnippetLib.Server.Models
{
    [Table("Tags")]
    public class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
