using blogpessoal.Data;
using blogpessoal.Model;
using Microsoft.EntityFrameworkCore;

namespace blogpessoal.Service.Implements
{
    public class TemaService : ITemaService
    {
        private readonly AppDbContext _context;

        public TemaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tema>> GetAll()
        {
            return await _context.Temas
                        .Include(t => t.Postagem)
                        .ToListAsync();
        }

        public async Task<Tema?> GetById(long id)
        {
            try
            {
                var Tema = await _context.Temas
                                .Include(t => t.Postagem)
                                .FirstAsync(i => i.Id == id);

                return Tema;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Tema>> GetByDescricao(string descricao)
        {
            var Tema = await _context.Temas
                            .Include(t => t.Postagem)
                            .Where(t => t.Descricao.Contains(descricao))
                            .ToListAsync();

            return Tema;
        }

        public async Task<Tema?> Create(Tema temas)
        {
            _context.Temas.Add(temas);
            await _context.SaveChangesAsync();

            return temas;
        }

        public async Task<Tema?> Update(Tema temas)
        {
            var TemaUpdate = await _context.Temas.FindAsync(temas.Id);

            if (TemaUpdate is null)
                return null;

            _context.Entry(TemaUpdate).State = EntityState.Detached;
            _context.Entry(temas).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return temas;
        }
        public async Task Delete(Tema temas)
        {
            _context.Remove(temas);
            await _context.SaveChangesAsync();

        }
    }
}
