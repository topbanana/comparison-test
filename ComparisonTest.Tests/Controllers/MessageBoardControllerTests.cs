using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using ComparisonTest.Controllers;
using ComparisonTest.Models;
using ComparisonTest.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using Xunit;

namespace ComparisonTest.Tests.Controllers
{
    public class MessageBoardControllerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly Fixture _fixture;

        public MessageBoardControllerTests()
        {
            _autoMocker = new AutoMocker();
            _fixture = new Fixture();
        }

        private MessageBoardController ClassUnderTest => _autoMocker.CreateInstance<MessageBoardController>();

        [Fact]
        public void Get_WhenInvoked_RetrievesAllMessagesFromService()
        {
            // arrange
            // act
            ClassUnderTest.Get();
            // assert
            _autoMocker.GetMock<IMessageBoardService>().Verify(x => x.GetAllMessages());
        }

        [Fact]
        public void Get_WhenInvoked_ReturnsAllMessagesFromService()
        {
            // arrange
            var expected = _fixture.CreateMany<Message>().ToArray();
            _autoMocker.GetMock<IMessageBoardService>().Setup(x => x.GetAllMessages())
                .Returns(expected.ToAsyncEnumerable());
            // act
            var result = ClassUnderTest.Get();
            // assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task AddAsync_WithMessage_AddsMessageWithService()
        {
            // arrange
            var message = _fixture.Create<Message>();
            // act
            await ClassUnderTest.Add(message);
            // assert
            _autoMocker.GetMock<IMessageBoardService>().Verify(x => x.AddMessage(message));
        }


        [Fact]
        public async Task AddAsync_WithMessage_ReturnsOk()
        {
            // arrange
            // act
            var result = await ClassUnderTest.Add(_fixture.Create<Message>());
            // assert
            result.Should().NotBeNull().And.BeAssignableTo<OkResult>();
        }
    }
}