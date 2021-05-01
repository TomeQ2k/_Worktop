using Worktop.Models.Domain;

namespace Worktop.Core.Builders.Interfaces
{
    public interface IMailBuilder : IBuilder<Mail>
    {
        IMailBuilder SetSubject(string subject);
        IMailBuilder SetContent(string content);
        IMailBuilder SentFrom(User sender);
        IMailBuilder SentTo(User receiver);
    }
}