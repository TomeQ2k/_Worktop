using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Data;
using Worktop.Core.Extensions;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;
using Worktop.Models.Helpers.Pagination;
using Worktop.Core.Params;
using Worktop.Core.Builders;

namespace Worktop.Core.Services
{
    public class Messenger : IMessenger
    {
        private readonly IDatabase database;

        private readonly int currentUserId;

        public Messenger(IDatabase database, IHttpContextAccessor httpContextAccessor)
        {
            this.database = database;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<PagedList<Message>> GetMessages(FetchMessagesParams filterParams)
        {
            var messages = (await database.MessageRepository.Filter(m => m.ChatRoomId == null))
                .OrderByDescending(m => m.DateSent)
                .ToPagedList<Message>(filterParams.PageNumber, filterParams.PageSize);

            messages.Reverse();

            return messages;
        }

        public async Task<Message> Send(string text, string senderName, string roomId = null)
        {
            var message = new MessageBuilder()
                .SetText(text)
                .SentBy(currentUserId, senderName)
                .InChatRoom(roomId)
                .Build();

            database.MessageRepository.Add(message);

            return await database.Complete() ? message : null;
        }
    }
}