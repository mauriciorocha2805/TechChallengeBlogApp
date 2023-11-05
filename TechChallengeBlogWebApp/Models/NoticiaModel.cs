using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechChallengeBlogWebApp.Models
{
    public class NoticiaModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor informe o Título.")]
        [DisplayName("Título")]
        [StringLength(100)]
        public string? Titulo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor informe o Conteúdo.")]
        [DisplayName("Conteúdo")]
        [StringLength(200)]
        public string? Conteudo { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Data de Publicação")]
        [Required(ErrorMessage = "Informe a data.")]
        public DateTime DataPublicacao { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor informe o Autor.")]
        [StringLength(50)]
        public string? Autor { get; set; }
    }
}