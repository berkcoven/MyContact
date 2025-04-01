using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ContactService.Application.Services;
using ContactService.Core.DTOs.ContactInfoDtos;
using ContactService.Core.DTOs.PersonDtos;
using ContactService.Core.Interfaces.Repository;
using ContactService.Core.Interfaces.Services;
using ContactService.Core.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;

namespace ContactService.Tests.Services
{
    public class ContactServiceTests
    {
        private readonly Mock<IPersonRepository> _mockContactRepository;
        private readonly Mock<IContactInformationRepository> _mockContactInfoRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IPersonService _personService;
        private readonly IContactInformationService _contactInformationService;

        public ContactServiceTests()
        {
            _mockContactRepository = MockRepository.MockContactRepository.GetContactRepositoryMock();
            _mockContactInfoRepository = MockRepository.MockContactRepository.GetContactInfoRepositoryMock();
            _mockMapper = new Mock<IMapper>();
            _personService = new PersonService(_mockContactRepository.Object, _mockMapper.Object);
            _contactInformationService = new ContactInformationService(_mockContactInfoRepository.Object,_mockContactRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllContacts_ShouldReturnContacts()
        {
            
            var people = new List<Person>
            {
                new Person { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Company = "TestCorp" },
                new Person { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Company = "AnotherCorp" }
            };

            _mockContactRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(people);

            _mockMapper
                .Setup(m => m.Map<List<PersonGetAllResponse>>(people))
                .Returns(new List<PersonGetAllResponse>
                {
                    new PersonGetAllResponse { FirstName = "John", LastName = "Doe", Company = "TestCorp" },
                    new PersonGetAllResponse { FirstName = "Jane", LastName = "Doe", Company = "AnotherCorp" }
                });

            // Act
            var result = await _personService.GetAllAsync();

            // Assert
            result.Data.Should().NotBeNull();
            result.Data.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public async Task GetPersonByIdAsync_ReturnsPerson_WhenPersonExists()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var person = new Person
            {
                Id = personId,
                FirstName = "John",
                LastName = "Doe",
                Company = "TestCorp"
            };

            _mockContactRepository
                .Setup(repo => repo.GetByIdAsync(personId))
                .ReturnsAsync(person);

            _mockMapper
                .Setup(m => m.Map<PersonDto>(person))
                .Returns(new PersonDto
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Company = "TestCorp"
                });

            // Act
            var result = await _personService.GetPersonByIdAsync(personId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("TestCorp", result.Company);

            _mockContactRepository.Verify(r => r.GetByIdAsync(personId), Times.Once);
        }
        [Fact]
        public async Task AddPersonAsync_ShouldAddPerson_WhenValidPerson()
        {
 
            var newPersonDto = new PersonDto
            {
                FirstName = "Sam",
                LastName = "Smith",
                Company = "TechCorp"
            };

            var newPerson = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "Sam",
                LastName = "Smith",
                Company = "TechCorp"
            };

            _mockMapper.Setup(m => m.Map<Person>(newPersonDto)).Returns(newPerson);
            _mockContactRepository.Setup(repo => repo.AddPersonAsync(newPerson)).ReturnsAsync(newPerson);

            
            var result = await _personService.AddPersonAsync(newPersonDto);

            
            result.Data.Should().NotBeNull();
            result.Data.FirstName.Should().Be("Sam");
            result.Data.LastName.Should().Be("Smith");
            result.Data.Company.Should().Be("TechCorp");

           
            _mockContactRepository.Verify(r => r.AddPersonAsync(It.IsAny<Person>()), Times.Once);
            _mockMapper.Verify(m => m.Map<Person>(newPersonDto), Times.Once);
        }
        [Fact]
        public async Task GetPersonCountByLocationAsync_ShouldReturnPersonCount_WhenLocationIsValid()
        {
            // Arrange: Test verilerini hazırlıyoruz
            var location = "istanbul";
            var expectedCount = 1;
            _mockContactRepository.Setup(repo => repo.CountPersonsByLocationAsync(location)).ReturnsAsync(expectedCount);

            // Act: GetPersonCountByLocationAsync metodunu çağırıyoruz
            var result = await _personService.GetPersonCountByLocationAsync(location);

            // Assert: Sonuçları doğruluyoruz
            result.Should().NotBeNull();
            result.Data.Should().Be(expectedCount);

            _mockContactRepository.Verify(r => r.CountPersonsByLocationAsync(location), Times.Once);
        }
        [Fact]
        public async Task GetPhoneNumberCountByLocationAsync_ShouldReturnPhoneNumberCount_WhenLocationIsValid()
        {
            // Arrange: Test verilerini hazırlıyoruz
            var location = "istanbul";
            var expectedCount = 1;
            _mockContactRepository.Setup(repo => repo.CountPhoneNumbersByLocationAsync(location)).ReturnsAsync(expectedCount);

            // Act: GetPhoneNumberCountByLocationAsync metodunu çağırıyoruz
            var result = await _personService.GetPhoneNumberCountByLocationAsync(location);

            // Assert: Sonuçları doğruluyoruz
            result.Should().NotBeNull();
            result.Data.Should().Be(expectedCount);

            _mockContactRepository.Verify(r => r.CountPhoneNumbersByLocationAsync(location), Times.Once);
        }
        [Fact]
        public async Task AddContactInfoAsync_ShouldReturnContactInformation_WhenValidDtoIsProvided()
        {

            var testPerson = new Person { Id = Guid.Parse("0195ede6-8715-7245-bc80-10285751aa98"), FirstName = "John", LastName = "Doe", Company = "Tech Inc" };
                   
            var addContactInfoDto = new AddContactInfoDto
            {
                PersonId = testPerson.Id,
                InfoType = InfoTypeEnum.Phone,  
                InfoContent = "1234567890"
            };

            var contactInfo = new ContactInformation
            {
                Id = Guid.NewGuid(),
                PersonId = addContactInfoDto.PersonId,
                InfoType = addContactInfoDto.InfoType,
                InfoContent = addContactInfoDto.InfoContent
            };
            _mockContactRepository
               .Setup(repo => repo.GetByIdAsync(addContactInfoDto.PersonId))
               .ReturnsAsync(testPerson);



            _mockContactInfoRepository.Setup(repo => repo.AddAsync(It.IsAny<ContactInformation>()))
          .ReturnsAsync(contactInfo);

          
            var result = await _contactInformationService.AddContactInfoAsync(addContactInfoDto);

       
            result.Should().NotBeNull();
            result.PersonId.Should().Be(addContactInfoDto.PersonId);
            result.InfoType.Should().Be(addContactInfoDto.InfoType);
            result.InfoContent.Should().Be(addContactInfoDto.InfoContent);

            _mockContactInfoRepository.Verify(r => r.AddAsync(It.IsAny<ContactInformation>()), Times.Once);

        }
        [Fact]
        public async Task DeletePersonAsync_ShouldReturnFalse_WhenPersonDoesNotExist()
        {
            var testPerson = new Person { Id = Guid.Parse("0195ede6-8715-7245-bc80-10285751aa98"), FirstName = "John", LastName = "Doe", Company = "Tech Inc" };


            
            _mockContactRepository.Setup(repo => repo.DeleteAsync(testPerson));
            _mockContactRepository
              .Setup(repo => repo.GetByIdAsync(testPerson.Id))
              .ReturnsAsync(testPerson);
            
            var result = await _personService.DeletePersonAsync(testPerson.Id);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeTrue();  

            _mockContactRepository.Verify(r => r.DeleteAsync(testPerson), Times.Once);
        }



    }
}
