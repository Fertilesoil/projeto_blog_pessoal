using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using blogpessoal.Model;

namespace blogpessoal.Model
{
    public class Postagem : Auditable
    {
        [Key] // Chave Primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto Incremento
        public long Id { get; set; }

        [Column (TypeName ="varchar")]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Texto { get; set; } = string.Empty;
    }
}
