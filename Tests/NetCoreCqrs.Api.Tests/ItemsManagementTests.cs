using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using NetCoreCqrs.Api.Tests.App_Infrastructure.ClassFixture;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreCqrs.Api.Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0052:Remove unread private members")]
    public class ItemsManagementTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly Fixture _fixture;
        public ItemsManagementTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
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
    }
}
