using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace App.Blog.Domain.Entities
{
    [Table("Sistema", Schema = "dbo")]
    public partial class Sistema
    {
        [Key]
        public int SistemaId { get; set; }
        public bool Ativo { get; set; }
        public string Chave { get; set; }
        public string Descricao { get; set; }
    }
}