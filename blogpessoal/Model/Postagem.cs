
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using blogpessoal.Model;
using Newtonsoft.Json;

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
        [StringLength(8000)]
        public string Texto { get; set; } = string.Empty;

        public virtual Tema? Tema { get; set; } //Chave estrangeira 
        public virtual User? Usuario { get; set; } //Chave estrangeira 
    }
}
