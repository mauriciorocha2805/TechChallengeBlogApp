#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Blog.Domain.Entities
{
    [Table("Noticia", Schema = "dbo")]
    public class Noticia
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }
    }
}