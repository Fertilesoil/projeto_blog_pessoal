using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace blogpessoal.Model
{
    public class Tema 
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Descricao { get; set; } = string.Empty;

        [InverseProperty("Tema")] // Indica chave estrangeira
        [JsonIgnore]
        public virtual ICollection<Postagem>? Postagem { get; set; }
    }
}
