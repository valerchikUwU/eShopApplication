namespace eShopApplication.Application.AppData.Accounts.Repository
{
    public interface IAccountRepository
    {
        Task<Guid> AddAccountAsync(Domain.Account.Account account, CancellationToken cancellationToken);
        Task<Domain.Account.Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Domain.Account.Account> GetAccountByNameAsync(string name, CancellationToken cancellationToken);
        Task<List<Domain.Account.Account>> GetAllAsync(CancellationToken cancellationToken);
        Task<Guid> DeleteAccount(Guid id, CancellationToken cancellationToken);
        Task<Domain.Account.Account> UpdateAccount(Guid id, CancellationToken cancellationToken);
    }
}
