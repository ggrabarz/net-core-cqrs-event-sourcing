using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using NetCoreCqrs.Api.Tests.App_Infrastructure.ClassFixture;
using NetCoreCqrs.Domain.Inventory;
using NetCoreCqrs.Persistence.RepositoryCache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreCqrs.Api.Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0052:Remove unread private members")]
    public class ItemsManagementTestsNoCache : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly Fixture _fixture;
        private readonly Mock<IRepositoryCache<InventoryItem>> _repositoryCacheMock;

        public ItemsManagementTestsNoCache(CustomWebApplicationFactory<Startup> factory)
        {
            _repositoryCacheMock = new Mock<IRepositoryCache<InventoryItem>>(MockBehavior.Loose);
            _repositoryCacheMock.Setup(x => x.GetOrDefault(It.IsAny<string>())).Returns(() => null);
            _factory = factory;
            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<IRepositoryCache<InventoryItem>>();
                    services.AddSingleton(_repositoryCacheMock.Object);
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _fixture = new Fixture();
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Given_Nothing_When_CallingGetItems_Returns_Successful()
        {
            // Arrange
            // Act
            var responseMessage = await _client.GetAsync("api/values");
            // Assert
            Assert.True(responseMessage.IsSuccessStatusCode);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Given_RandomItemId_When_CallingGetItemDetails_Returns_NotFound()
        {
            // Arrange
            var randomItemId = Guid.NewGuid().ToString();
            // Act
            var responseMessage = await _client.GetAsync($"api/values/{randomItemId}");
            // Assert
            Assert.False(responseMessage.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Given_NewItem_When_CallingAddItemsAndVerifyItemWasAdded_Returns_ItemDetails()
        {
            // Arrange
            var randomItemName = $"itemName{_fixture.Create<string>()}";
            // Act
            var addItemResponseMessage = await _client.PostAsync($"api/values?name={randomItemName}", null);
            Assert.True(addItemResponseMessage.IsSuccessStatusCode);
            var itemId = await addItemResponseMessage.Content.ReadAsStringAsync();
            await Task.Delay(TimeSpan.FromSeconds(1));
            var getItemDetailsResponseMessage = await _client.GetAsync($"api/values/{itemId}");
            var itemDetails = await getItemDetailsResponseMessage.Content.ReadAsStringAsync();
            // Assert
            Assert.True(getItemDetailsResponseMessage.IsSuccessStatusCode);
            Assert.Contains(randomItemName, itemDetails);
            Assert.Contains(itemId, itemDetails);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Given_NewItem_When_CallingAddItemAndChangeName_Returns_NewName()
        {
            // Arrange
            var randomItemName = $"itemName{_fixture.Create<string>()}";
            var secondRandomItemName = $"itemName{_fixture.Create<string>()}";
            // Act
            var addItemResponseMessage = await _client.PostAsync($"api/values?name={randomItemName}", null);
            Assert.True(addItemResponseMessage.IsSuccessStatusCode);
            var itemId = await addItemResponseMessage.Content.ReadAsStringAsync();
            var changeItemNameResponseMessage = await _client.PostAsync($"api/values/{itemId}/ChangeName?name={secondRandomItemName}&version=0", null);
            Assert.True(changeItemNameResponseMessage.IsSuccessStatusCode);
            await Task.Delay(TimeSpan.FromSeconds(1));
            var getItemDetailsResponseMessage = await _client.GetAsync($"api/values/{itemId}");
            var itemDetails = await getItemDetailsResponseMessage.Content.ReadAsStringAsync();
            // Assert
            Assert.True(getItemDetailsResponseMessage.IsSuccessStatusCode);
            Assert.Contains(secondRandomItemName, itemDetails);
            Assert.Contains(itemId, itemDetails);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Given_NewItem_When_CallingAddItemAndIncreaseValue_Returns_NewValue()
        {
            // Arrange
            var randomItemName = $"itemName{_fixture.Create<string>()}";
            // Act
            var addItemResponseMessage = await _client.PostAsync($"api/values?name={randomItemName}", null);
            Assert.True(addItemResponseMessage.IsSuccessStatusCode);
            var itemId = await addItemResponseMessage.Content.ReadAsStringAsync();
            var increaseValueResponseMessage = await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=5&version=0", null);
            Assert.True(increaseValueResponseMessage.IsSuccessStatusCode);
            var decreaseValueResponseMessage = await _client.PostAsync($"api/values/{itemId}/DecreaseAmmount?number=2&version=1", null);
            Assert.True(decreaseValueResponseMessage.IsSuccessStatusCode);
            await Task.Delay(TimeSpan.FromSeconds(1));
            var getItemDetailsResponseMessage = await _client.GetAsync($"api/values/{itemId}");
            var itemDetails = await getItemDetailsResponseMessage.Content.ReadAsStringAsync();
            // Assert
            Assert.True(getItemDetailsResponseMessage.IsSuccessStatusCode);
            Assert.Contains(randomItemName, itemDetails);
            Assert.Contains(itemId, itemDetails);
            Assert.Contains("\"currentCount\":3", itemDetails);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Given_NewItemAndChanges_When_CallingMultipleChangesOnItem_Returns_CorrectValues()
        {
            // Arrange
            var randomItemName = $"itemName{_fixture.Create<string>()}";
            var secondRandomItemName = $"itemName{_fixture.Create<string>()}";
            // Act
            var addItemResponseMessage = await _client.PostAsync($"api/values?name={randomItemName}", null);
            Assert.True(addItemResponseMessage.IsSuccessStatusCode);
            var itemId = await addItemResponseMessage.Content.ReadAsStringAsync();
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=5&version=0", null);
            await _client.PostAsync($"api/values/{itemId}/DecreaseAmmount?number=2&version=1", null);
            await _client.PostAsync($"api/values/{itemId}/ChangeName?name={secondRandomItemName}&version=2", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=4&version=3", null);
            await _client.PostAsync($"api/values/{itemId}/DecreaseAmmount?number=1&version=4", null);
            await Task.Delay(TimeSpan.FromSeconds(2));
            var getItemDetailsResponseMessage = await _client.GetAsync($"api/values/{itemId}");
            var itemDetails = await getItemDetailsResponseMessage.Content.ReadAsStringAsync();
            // Assert
            Assert.True(getItemDetailsResponseMessage.IsSuccessStatusCode);
            Assert.Contains(secondRandomItemName, itemDetails);
            Assert.Contains(itemId, itemDetails);
            Assert.Contains("\"currentCount\":6", itemDetails);
            Assert.Contains("\"version\":5", itemDetails);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Given_NewItem_When_CallingAddAndDeactivate_Returns_NotFound()
        {
            // Arrange
            var randomItemName = $"itemName{_fixture.Create<string>()}";
            // Act
            var addItemResponseMessage = await _client.PostAsync($"api/values?name={randomItemName}", null);
            Assert.True(addItemResponseMessage.IsSuccessStatusCode);
            var itemId = await addItemResponseMessage.Content.ReadAsStringAsync();
            var responseMessage = await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=5&version=0", null);
            Assert.True(responseMessage.IsSuccessStatusCode);
            var deleteResponseMessage = await _client.DeleteAsync($"api/values/{itemId}?version=1");
            Assert.True(deleteResponseMessage.IsSuccessStatusCode);
            await Task.Delay(TimeSpan.FromSeconds(1));
            var getItemDetailsResponseMessage = await _client.GetAsync($"api/values/{itemId}");
            // Assert
            Assert.False(getItemDetailsResponseMessage.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, getItemDetailsResponseMessage.StatusCode);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Given_NewItemAndChanges_When_CallingTwentyChangesOnItem_Returns_CorrectValues()
        {
            // Arrange
            var randomItemName = $"itemName{_fixture.Create<string>()}";
            // Act
            var addItemResponseMessage = await _client.PostAsync($"api/values?name={randomItemName}", null);
            Assert.True(addItemResponseMessage.IsSuccessStatusCode);
            var itemId = await addItemResponseMessage.Content.ReadAsStringAsync();
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=5&version=0", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=1", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=2", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=3", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=4", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=5", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=6", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=7", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=8", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=9", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=10", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=11", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=12", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=13", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=14", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=15", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=16", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=17", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=18", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=19", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=20", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=21", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=22", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=23", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=2&version=24", null);
            await Task.Delay(TimeSpan.FromSeconds(2));
            var getItemDetailsResponseMessage = await _client.GetAsync($"api/values/{itemId}");
            var itemDetails = await getItemDetailsResponseMessage.Content.ReadAsStringAsync();
            // Assert
            Assert.True(getItemDetailsResponseMessage.IsSuccessStatusCode);
            Assert.Contains(itemId, itemDetails);
            Assert.Contains("\"currentCount\":30", itemDetails);
            Assert.Contains("\"version\":25", itemDetails);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Given_NewTwoItemsAndChanges_When_ChangesOnItems_Returns_TwoCorrectItems()
        {
            // Arrange
            var randomItemName = $"itemName{_fixture.Create<string>()}";
            var randomItemName2 = $"itemName{_fixture.Create<string>()}";
            // Act
            var addItemResponseMessage = await _client.PostAsync($"api/values?name={randomItemName}", null);
            var addItemResponseMessage2 = await _client.PostAsync($"api/values?name={randomItemName2}", null);
            Assert.True(addItemResponseMessage.IsSuccessStatusCode);
            Assert.True(addItemResponseMessage2.IsSuccessStatusCode);
            var itemId = await addItemResponseMessage.Content.ReadAsStringAsync();
            var itemId2 = await addItemResponseMessage2.Content.ReadAsStringAsync();
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=5&version=0", null);
            await _client.PostAsync($"api/values/{itemId}/IncreaseAmmount?number=1&version=1", null);
            await _client.PostAsync($"api/values/{itemId2}/IncreaseAmmount?number=1&version=0", null);
            await Task.Delay(TimeSpan.FromSeconds(2));
            var getItemDetailsResponseMessage = await _client.GetAsync($"api/values/{itemId}");
            var getItemDetailsResponseMessage2 = await _client.GetAsync($"api/values/{itemId2}");
            var getItemsListResponseMessage = await _client.GetAsync($"api/values");
            await Task.Delay(TimeSpan.FromSeconds(2));
            var itemsList = JsonConvert.DeserializeObject<IEnumerable<InventoryItemListDto>>(await getItemsListResponseMessage.Content.ReadAsStringAsync());
            // Assert
            var itemDetails = itemsList.First(x => x.Id == Guid.Parse(itemId));
            var itemDetails2 = itemsList.First(x => x.Id == Guid.Parse(itemId2));
            Assert.Equal(randomItemName, itemDetails.Name);
            Assert.Equal(randomItemName2, itemDetails2.Name);
        }
    }

    public class InventoryItemListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public InventoryItemListDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
