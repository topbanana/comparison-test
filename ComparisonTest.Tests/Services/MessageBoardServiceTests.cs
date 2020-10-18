using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using ComparisonTest.Models;
using ComparisonTest.Services;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace ComparisonTest.Tests.Services
{
    public class MessageBoardServiceTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly Fixture _fixture;

        public MessageBoardServiceTests()
        {
            _autoMocker = new AutoMocker();
            _fixture = new Fixture();
        }

        private IMessageBoardService ClassUnderTest => _autoMocker.CreateInstance<MessageBoardService>();

        [Fact]
        public async Task GetAllMessages_WhenInvoked_ShouldRetrieveMessagesFromRepository()
        {
            // arrange
            _autoMocker.GetMock<IRepository<MessageEntity>>()
                .Setup(x => x.GetAll())
                .Returns(_fixture.CreateMany<MessageEntity>().ToAsyncEnumerable());
            // act
            await ClassUnderTest.GetAllMessages().ToListAsync();
            // assert
            _autoMocker.GetMock<IRepository<MessageEntity>>().Verify(x => x.GetAll());
        }

        [Fact]
        public async Task GetAllMessages_WhenInvoked_ShouldReturnConvertedEntities()
        {
            // arrange
            var expected = _fixture.CreateMany<MessageEntity>().ToArray();
            _autoMocker.GetMock<IRepository<MessageEntity>>()
                .Setup(x => x.GetAll())
                .Returns(expected.ToAsyncEnumerable());
            // act
            var results = await ClassUnderTest.GetAllMessages().ToListAsync();
            // assert
            // ReSharper disable once CoVariantArrayConversion
            results.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task AddMessage_WithMessage_ShouldAddEntityToRepository()
        {
            // arrange
            // act
            await ClassUnderTest.AddMessage(_fixture.Create<Message>());
            // assert
            _autoMocker.GetMock<IRepository<MessageEntity>>().Verify(x => x.Add(It.IsAny<MessageEntity>()));
        }

        [Fact]
        public async Task AddMessage_WithMessage_ShouldAddEntityToRepositoryWithEquivalentProperties()
        {
            // arrange
            var message = _fixture.Create<Message>();
            MessageEntity capturedMessageEntity = null;
            _autoMocker.GetMock<IRepository<MessageEntity>>().Setup(x => x.Add(It.IsAny<MessageEntity>()))
                .Callback<MessageEntity>(m => capturedMessageEntity = m);
            // act
            await ClassUnderTest.AddMessage(message);
            // assert
            capturedMessageEntity.Should().BeEquivalentTo(message);
        }
    }
}