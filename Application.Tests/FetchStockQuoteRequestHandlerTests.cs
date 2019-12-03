using FinancialChat.Abstractions.ChatRoom;
using FinancialChat.Abstractions.HostingContext;
using FinancialChat.Abstractions.StockService;
using FinancialChat.Application.Bot.Commands;
using FinancialChat.Domain;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinancialChat.Application.Tests
{
    public class FetchStockQuoteRequestHandlerTests
    {
        private readonly FetchStockQuoteRequestHandler _classUnderTest;
        private Mock<IStockService> _stockServiceMock;
        private Mock<IChatRoom> _chatRoomMock;
        private Mock<IDateTime> _dateTimeServiceMock;

        public FetchStockQuoteRequestHandlerTests()
        {
            _stockServiceMock = new Mock<IStockService>();
            _chatRoomMock = new Mock<IChatRoom>();
            _dateTimeServiceMock = new Mock<IDateTime>();
            _dateTimeServiceMock.Setup(x => x.Now).Returns(new DateTime(2019, 12, 3, 10, 15, 2));

            _classUnderTest = new FetchStockQuoteRequestHandler(_stockServiceMock.Object,
                                                                _chatRoomMock.Object,
                                                                _dateTimeServiceMock.Object);
        }

        [Fact]
        public async Task Send_Stock_Result_To_Room_If_Any()
        {
            // Arrange 
            _stockServiceMock.Setup(x => x.GetStockQuote("aapl.us"))
                             .ReturnsAsync("APPL.US quote is $93.42 per share");

            // Act
            await _classUnderTest.Handle(new FetchStockQuoteRequest("aapl.us"), CancellationToken.None);

            // Assert
            _chatRoomMock.Verify(x =>
                    x.SendMessageToAll(It.Is<ChatMessage>(x => x.Message == "APPL.US quote is $93.42 per share")),
                    Times.Once);
        }
    }

}
