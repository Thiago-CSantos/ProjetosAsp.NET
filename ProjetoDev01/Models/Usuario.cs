using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDev1.Models
{
    [Table("Usuario")]
    public class Usuario
    {

        public Usuario() { }

        [Key] // Define como chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Required]
        [Column("Email")]
        public required string Email { get; set; }
        [Required]
        [Column("Senha")]
        public required string Senha { get; set; }
    }
}
