using ContactSystem.Application.Dtos;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Services;

public static class MapService
{
    public static Contact ConvertToContactEntity(ContactCreateDto dto)
    {
        return new Contact
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
        };
    }

    public static ContactDto ConverToContactDto(Contact contact)
    {
        return new ContactDto
        {
            ContactId = contact.ContactId,
            Name = contact.Name,
            Email = contact.Email,
            Phone = contact.Phone,
            Address = contact.Address,
        };
    }

}
