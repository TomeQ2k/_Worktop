using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class OpinionCardViewModel
    {
        public Opinion Opinion { get; set; }

        public static OpinionCardViewModel Build(Opinion opinion) => new OpinionCardViewModel { Opinion = opinion };
    }
}