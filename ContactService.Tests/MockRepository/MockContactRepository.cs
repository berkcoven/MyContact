using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactService.Core.Interfaces.Repository;
using ContactService.Core.Models;
using Moq;

namespace ContactService.Tests.MockRepository
{
    public class MockContactRepository
    {
        public static Mock<IPersonRepository> GetContactRepositoryMock()
        {
            var mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Person>
                {
                    new Person { Id = Guid.Parse("0195ede6-8715-7245-bc80-10285751aa98"), FirstName = "John",LastName="Doe", Company = "Tech Inc" },
                    new Person { Id = Guid.NewGuid(), FirstName = "Jane",LastName="Doe", Company = "Noway Inc" }
                });

            return mockRepo;
        }
        public static Mock<IContactInformationRepository> GetContactInfoRepositoryMock()
        {
            var mockRepo = new Mock<IContactInformationRepository>();

            return mockRepo;
        }
    }
}
