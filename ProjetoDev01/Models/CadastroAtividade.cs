using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDev01.Models
{
    [Table("CadastroAtividade")]
    public class CadastroAtividade
    {
        public CadastroAtividade()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Column("CodAtividade")]
        public string? CodAtividade { get; set; }

        [Required]
        [Column("Grupo")]
        public required string? Grupo { get; set; }

        [Required]
        [Column("CodigoFederal")]
        public required string? CodigoFederal { get; set; }

        [Required]
        [Column("Descricao")]
        public required string? Descricao { get; set; }

        [Required]
        [Column("AtivPrevistaExcecao")]
        public required string? AtivPrevistaExcecao { get; set; }
        
        [Required]
        [Column("RetencaoObrigatoria")]
        public required string? RetencaoObrigatoria { get; set; }

        [Required]
        [Column("Aliquota")]
        public required float? Aliquota { get; set; }

        [Required]
        [Column("Recolhimento")]
        public required string? Recolhimento { get; set; }
    }
}
