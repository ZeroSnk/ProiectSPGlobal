namespace SandP.Models.Repositories
{
    public interface IAgreementRepository
    {
        Task<IEnumerable<Agreement>> GetAll();
        Task<Agreement> Get(string CNP);
        Task<Agreement> Create(Agreement agreement);
        Task Update(Agreement agreement);
        Task Delete(string CNP);
    }
}
