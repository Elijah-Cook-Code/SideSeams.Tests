using Data.Repositories;
using Moq;
using SideSeams.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideSeams.Tests.RepoTests
{
    public class ClientRepositoryTests
    {
        private readonly Mock<IClientRepository> _mockRepo;

        public ClientRepositoryTests()
        {
            _mockRepo = new Mock<IClientRepository>();
        }

        [Fact]
        public async Task GetClientById_ShouldReturnClient_WhenClientExists()
        {
            // Arrange
            var testClient = new ClientInfo { Id = 1, Name = "John Doe" };
            _mockRepo.Setup(repo => repo.GetClientByIdAsync(1)).ReturnsAsync(testClient);

            // Act
            var result = await _mockRepo.Object.GetClientByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task GetAllClients_ShouldReturnClientList()
        {
            // Arrange
            var clients = new List<ClientInfo>
            {
                new ClientInfo { Id = 1, Name = "Alice" },
                new ClientInfo { Id = 2, Name = "Bob" }
            };
            _mockRepo.Setup(repo => repo.GetClientsAsync()).ReturnsAsync(clients);

            // Act
            var result = await _mockRepo.Object.GetClientsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "Alice");
            Assert.Contains(result, c => c.Name == "Bob");
        }

        [Fact]
        public async Task AddClient_ShouldCallRepositoryOnce()
        {
            // Arrange
            var newClient = new ClientInfo { Id = 3, Name = "Charlie" };
            _mockRepo.Setup(repo => repo.AddClientAsync(It.IsAny<ClientInfo>())).Returns(Task.CompletedTask);

            // Act
            await _mockRepo.Object.AddClientAsync(newClient);

            // Assert
            _mockRepo.Verify(repo => repo.AddClientAsync(newClient), Times.Once);
        }

    }
}
