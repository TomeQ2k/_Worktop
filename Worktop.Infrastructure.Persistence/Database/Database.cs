using System.Threading.Tasks;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Data.Repositories;
using Worktop.Core.Domain.Entities;
using Worktop.Infrastructure.Persistence.Database.Repositories;

namespace Worktop.Infrastructure.Persistence.Database
{
    public class Database : IDatabase
    {
        private readonly DataContext context;

        public Database(DataContext context)
        {
            this.context = context;
        }

        private IUserRepository userRepository;
        public IUserRepository UserRepository => userRepository ?? new UserRepository(context);

        private IRepository<TaskItem> taskRepository;
        public IRepository<TaskItem> TaskRepository => taskRepository ?? new Repository<TaskItem>(context);

        private IRepository<Job> jobRepository;
        public IRepository<Job> JobRepository => jobRepository ?? new Repository<Job>(context);

        private IOpinionRepository opinionRepository;
        public IOpinionRepository OpinionRepository => opinionRepository ?? new OpinionRepository(context);

        private IArticleRepository articleRepository;
        public IArticleRepository ArticleRepository => articleRepository ?? new ArticleRepository(context);

        private IMailRepository mailRepository;
        public IMailRepository MailRepository => mailRepository ?? new MailRepository(context);

        private IMessageRepository messageRepository;
        public IMessageRepository MessageRepository => messageRepository ?? new MessageRepository(context);

        private IRepository<Connection> connectionRepository;
        public IRepository<Connection> ConnectionRepository => connectionRepository ?? new Repository<Connection>(context);

        private IChatRoomRepository chatRoomRepository;
        public IChatRoomRepository ChatRoomRepository => chatRoomRepository ?? new ChatRoomRepository(context);

        private IRepository<File> fileRepository;
        public IRepository<File> FileRepository => fileRepository ?? new Repository<File>(context);

        private IDirectoryRepository directoryRepository;
        public IDirectoryRepository DirectoryRepository => directoryRepository ?? new DirectoryRepository(context);

        public async Task<bool> Complete()
            => await context.SaveChangesAsync() > 0;

        public void Dispose()
        {
            context.Dispose();
        }
    }
}