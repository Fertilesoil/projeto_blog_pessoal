using blogpessoal.Model;

namespace blogpessoal.Service
{
    public interface ITemaService
    {
        Task<IEnumerable<Tema>> GetAll();

        Task<Tema?> GetById(long id);

        Task<IEnumerable<Tema>> GetByDescricao(string descricao);

        Task<Tema?> Create(Tema temas);

        Task<Tema?> Update(Tema temas);

        Task Delete(Tema temas);
    }
}
