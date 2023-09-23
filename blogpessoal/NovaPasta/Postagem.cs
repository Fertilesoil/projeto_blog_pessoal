using System.ComponentModel.DataAnnotations.Schema;

namespace blogpessoal.NovaPasta
{
    public class Postagem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Titulo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
    }
}
