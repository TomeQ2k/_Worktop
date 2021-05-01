using System.Threading.Tasks;
using Worktop.Data.Repositories;
using Worktop.Data.Repositories.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Data
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

        private IRepository<Opinion> opinionRepository;
        public IRepository<Opinion> OpinionRepository => opinionRepository ?? new Repository<Opinion>(context);

        private IRepository<Article> articleRepository;
        public IRepository<Article> ArticleRepository => articleRepository ?? new Repository<Article>(context);

        private IMailRepository mailRepository;
        public IMailRepository MailRepository => mailRepository ?? new MailRepository(context);

        private IRepository<Message> messageRepository;
        public IRepository<Message> MessageRepository => messageRepository ?? new Repository<Message>(context);

        private IRepository<Connection> connectionRepository;
        public IRepository<Connection> ConnectionRepository => connectionRepository ?? new Repository<Connection>(context);

        private IChatRoomRepository chatRoomRepository;
        public IChatRoomRepository ChatRoomRepository => chatRoomRepository ?? new ChatRoomRepository(context);

        private IRepository<Models.Domain.File> fileRepository;
        public IRepository<Models.Domain.File> FileRepository => fileRepository ?? new Repository<Models.Domain.File>(context);

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