using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Application.Builders;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Application.Params;
using Worktop.Core.Application.Services;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
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

        public async Task<PagedList<Message>> GetMessages(MessageFiltersParams filtersParams)
        {
            PagedList<Message> messages = (PagedList<Message>)await database.MessageRepository.GetMessagesInGlobalChat((filtersParams.PageNumber, filtersParams.PageSize));

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