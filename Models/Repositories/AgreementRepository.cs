using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace SandP.Models.Repositories
{
    public class AgreementRepository :IAgreementRepository
    {
        private readonly AgreementContext _context;

        public AgreementRepository(AgreementContext context)
        {
                _context = context;
                ;
        }
        public async Task<IEnumerable<Agreement>> GetAll()
        {
            return await _context.Agreements.ToListAsync();
        }

        public async Task<Agreement> Get(string CNP)
        {
            return await _context.Agreements.Where(x => x.CNP == CNP).FirstOrDefaultAsync();
        }


        public async Task<Agreement> Create(Agreement agreement)
        {
            _context.Agreements.Add(agreement);
            await _context.SaveChangesAsync();

            return agreement;
        }

        public async Task Update(Agreement agreement)
        {
            _context.Entry(agreement).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string CNP)
        {
            var entityToDelete = await _context.Agreements.FindAsync(CNP);
            _context.Agreements.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
