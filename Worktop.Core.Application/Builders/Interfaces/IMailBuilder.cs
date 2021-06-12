using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Builders.Interfaces
{
    public interface IMailBuilder : IBuilder<Mail>
    {
        IMailBuilder SetSubject(string subject);
        IMailBuilder SetContent(string content);
        IMailBuilder SentFrom(User sender);
        IMailBuilder SentTo(User receiver);
    }
}