using FinancialChat.Abstractions.Persistence;
using FinancialChat.Domain;
using System.Threading.Tasks;

namespace FinancialChat.Persistence
{
    public class ChatUnitOfWork : IChatUow
    {
        protected FinancialChatDbContext _dbContext;

        public ChatUnitOfWork(FinancialChatDbContext context)
        {
            _dbContext = context;
        }

        public IRepository<ChatMessage> ChatMessages
            => new Repository<FinancialChatDbContext, ChatMessage>(_dbContext);

        public async Task Complete()
            => await _dbContext.SaveChangesAsync();
    }
}
