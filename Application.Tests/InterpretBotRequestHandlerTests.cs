using FinancialChat.Abstractions.MessageBroker;
using FinancialChat.Application.Bot.Commands;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinancialChat.Application.Tests
{
    public class InterpretBotRequestHandlerTests
    {
        private readonly InterpretBotRequestHandler _classUnderTest;
        private Mock<IStockRequestSender> _stockRequestSenderMock;

        public InterpretBotRequestHandlerTests()
        {
            _stockRequestSenderMock = new Mock<IStockRequestSender>();

            _classUnderTest = new InterpretBotRequestHandler(_stockRequestSenderMock.Object);

        }

        [Fact]
        public async Task Send_Message_To_Broker_If_Valid_Bot_Command()
        {
            // Act
            await _classUnderTest.Handle(new InterpretBotRequest("/stock=aapl.us"), CancellationToken.None);

            // Assert
            _stockRequestSenderMock.Verify(x => x.SendStockRequest("aapl.us"), Times.Once);
        }

        [Fact]
        public async Task Return_True_If_Valid_Bot_Command()
        {
            // Act
            var result = await _classUnderTest.Handle(new InterpretBotRequest("/stock=aapl.us"), CancellationToken.None);

            // Assert
            result.ShouldBe(true);
        }

        [Fact]
        public async Task Return_False_If_Not_A_Valid_Bot_Command()
        {
            // Act
            var result = await _classUnderTest.Handle(new InterpretBotRequest("regular message"), CancellationToken.None);

            // Assert
            result.ShouldBe(false);
        }


    }
}
