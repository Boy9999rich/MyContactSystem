using ContactSystem.Application.Dtos;
using ContactSystem.Application.Interfaces;
using ContactSystem.Application.Services;
using ContactSystem.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Tests
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly ContactService _contactService;

        public ContactServiceTests()
        {
            _contactRepositoryMock = new Mock<IContactRepository>();
            _contactService = new ContactService(_contactRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsContactDto()
        {
            // Arrange
            var contact = new Contact
            {
                ContactId = 1,
                Name = "John",
                Email = "john@example.com",
                Phone = "123456",
                Address = "Somewhere"
            };
            _contactRepositoryMock.Setup(repo => repo.SelectByIdAsync(1)).ReturnsAsync(contact);

            // Act
            var result = await _contactService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsException()
        {
            _contactRepositoryMock.Setup(repo => repo.SelectByIdAsync(It.IsAny<long>())).ReturnsAsync((Contact)null);

            var ex = await Assert.ThrowsAsync<Exception>(() => _contactService.GetByIdAsync(999));

            Assert.Equal("Contact not fount with id 999", ex.Message);
        }

        [Fact]
        public async Task PostAsync_ShouldInsertContact()
        {
            var createDto = new ContactCreateDto
            {
                Name = "Ali",
                Email = "ali@mail.com",
                Phone = "99890",
                Address = "Tashkent"
            };

            _contactRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Contact>())).ReturnsAsync(10);

            var result = await _contactService.PostAsync(createDto);

            Assert.Equal(10, result);
        }

        [Fact]
        public void GetAll_ReturnsListOfContactDtos()
        {
            var contacts = new List<Contact>
        {
            new Contact { ContactId = 1, Name = "A", Email = "a@mail.com", Phone = "111", Address = "A street" },
            new Contact { ContactId = 2, Name = "B", Email = "b@mail.com", Phone = "222", Address = "B street" }
        }.AsQueryable();

            _contactRepositoryMock.Setup(repo => repo.SelectAll()).Returns(contacts);

            var result = _contactService.GetAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task UpdateAsync_ValidContact_UpdatesSuccessfully()
        {
            var existingContact = new Contact { ContactId = 1, Name = "Old", Email = "old@mail.com", Phone = "123", Address = "Old St" };
            var updatedDto = new ContactDto { ContactId = 1, Name = "New", Email = "new@mail.com", Phone = "456", Address = "New St" };

            _contactRepositoryMock.Setup(repo => repo.SelectByIdAsync(1)).ReturnsAsync(existingContact);

            await _contactService.UpdateAsync(updatedDto);

            _contactRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Contact>(c =>
                c.Name == "New" && c.Email == "new@mail.com"
            )), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsException()
        {
            _contactRepositoryMock.Setup(repo => repo.SelectByIdAsync(It.IsAny<long>())).ReturnsAsync((Contact)null);

            await Assert.ThrowsAsync<Exception>(() => _contactService.DeleteAsync(999));
        }

    }
}
