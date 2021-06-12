using System;
using System.Threading.Tasks;
using Worktop.Core.Domain.Data.Repositories;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Domain.Data
{
    public interface IDatabase : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRepository<TaskItem> TaskRepository { get; }
        IRepository<Job> JobRepository { get; }
        IOpinionRepository OpinionRepository { get; }
        IArticleRepository ArticleRepository { get; }
        IMailRepository MailRepository { get; }
        IMessageRepository MessageRepository { get; }
        IRepository<Connection> ConnectionRepository { get; }
        IChatRoomRepository ChatRoomRepository { get; }
        IRepository<File> FileRepository { get; }
        IDirectoryRepository DirectoryRepository { get; }

        Task<bool> Complete();
    }
}