using FinancialChat.Abstractions.ChatRoom;
using FinancialChat.Abstractions.HostingContext;
using FinancialChat.Abstractions.Persistence;
using FinancialChat.Application.Bot.Commands;
using FinancialChat.Application.Chat.Commands;
using FinancialChat.Domain;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinancialChat.Application.Tests
{
    public class SendMessageRequestHandlerTests
    {
        private readonly SendMessageRequestHandler _classUnderTest;
        private Mock<IChatRoom> _chatRoomMock;
        private Mock<IChatUow> _chatUow;
        private Mock<IDateTime> _dateTimeServiceMock;
        private Mock<IMediator> _mediatorMock;
        private Mock<IRepository<ChatMessage>> _chatMessageRepositoryMock;

        public SendMessageRequestHandlerTests()
        {
            _chatRoomMock = new Mock<IChatRoom>();
            _chatUow = new Mock<IChatUow>();
            _dateTimeServiceMock = new Mock<IDateTime>();
            _mediatorMock = new Mock<IMediator>();
            _chatMessageRepositoryMock = new Mock<IRepository<ChatMessage>>();

            _chatUow.SetupGet(a => a.ChatMessages).Returns(_chatMessageRepositoryMock.Object);
            _dateTimeServiceMock.Setup(x => x.Now).Returns(new DateTime(2019, 12, 3, 10, 15, 2));

            _classUnderTest = new SendMessageRequestHandler(_chatRoomMock.Object,
                                                            _chatUow.Object,
                                                            _dateTimeServiceMock.Object,
                                                            _mediatorMock.Object);
        }

        [Fact]
        public async Task Do_Not_Save_When_Message_Is_Bot_Command()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<InterpretBotRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);
            var request = new SendMessageRequest("user", "/stock=aapl.us");

            // Act
            var result = await _classUnderTest.Handle(request, CancellationToken.None);

            // Assert
            _chatMessageRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<ChatMessage>()), Times.Never);
        }

        [Fact]
        public async Task Send_Message_To_Room_When_Message_Is_Bot_Command()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<InterpretBotRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);
            var request = new SendMessageRequest("user", "/stock=aapl.us");

            // Act
            var result = await _classUnderTest.Handle(request, CancellationToken.None);

            // Assert
            _chatRoomMock.Verify(x => x.SendMessageToAll(It.Is<ChatMessage>(x => x.Message == "/stock=aapl.us")), Times.Once);
        }

        [Fact]
        public async Task Save_Message_To_Db_When_Message_Is_Regular()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<InterpretBotRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(false);
            var request = new SendMessageRequest("user", "regular message");

            // Act
            var result = await _classUnderTest.Handle(request, CancellationToken.None);

            // Assert
            _chatMessageRepositoryMock.Verify(x => x.CreateAsync(It.Is<ChatMessage>(x => x.Message == "regular message")), Times.Once);
        }

        [Fact]
        public async Task Send_Message_To_Room_When_Message_Is_Regular()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<InterpretBotRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(false);
            var request = new SendMessageRequest("user", "regular message");

            // Act
            var result = await _classUnderTest.Handle(request, CancellationToken.None);

            // Assert
            _chatRoomMock.Verify(x => x.SendMessageToAll(It.Is<ChatMessage>(x => x.Message == "regular message")), Times.Once);
        }


    }
}
