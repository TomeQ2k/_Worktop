using System.Threading.Tasks;
using System;
using Worktop.Models.Domain;
using Worktop.Data.Repositories.Interfaces;

namespace Worktop.Data
{
    public interface IDatabase : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRepository<TaskItem> TaskRepository { get; }
        IRepository<Job> JobRepository { get; }
        IRepository<Opinion> OpinionRepository { get; }
        IRepository<Article> ArticleRepository { get; }
        IMailRepository MailRepository { get; }
        IRepository<Message> MessageRepository { get; }
        IRepository<Connection> ConnectionRepository { get; }
        IChatRoomRepository ChatRoomRepository { get; }
        IRepository<Models.Domain.File> FileRepository { get; }
        IDirectoryRepository DirectoryRepository { get; }

        Task<bool> Complete();
    }
}