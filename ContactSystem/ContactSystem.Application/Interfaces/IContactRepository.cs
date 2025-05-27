using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Interfaces;

public interface IContactRepository
{
    Task<long> InsertAsync(Contact contact);
    Task DeleteAsync(Contact contact);
    Task<Contact?> SelectByIdAsync(long contactId);
    IQueryable<Contact> SelectAll();
    Task UpdateAsync(Contact contact);
    Task<int> SaveChangesAsync();
}
