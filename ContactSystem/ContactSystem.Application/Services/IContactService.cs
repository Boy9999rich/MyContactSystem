using ContactSystem.Application.Dtos;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Services;

public interface IContactService
{
    Task<long> PostAsync(ContactCreateDto contact);
    Task DeleteAsync(long contactId);
    Task<ContactDto> GetByIdAsync(long contactId);
    ICollection<ContactDto> GetAll();
    Task UpdateAsync(ContactDto contact);
}