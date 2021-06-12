using System.Collections.Generic;
using System.Linq;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
{
    public class OpinionsViewModel : BaseViewModel
    {
        public List<Opinion> PositiveOpinions { get; set; }
        public List<Opinion> NegativeOpinions { get; set; }

        public OpinionsViewModel(List<Opinion> opinions)
        {
            Title = "Opinions";

            FilterOpinions(opinions);
        }

        #region private

        private void FilterOpinions(List<Opinion> opinions)
        {
            PositiveOpinions = opinions.Where(o => !o.IsNegative).ToList();
            NegativeOpinions = opinions.Where(o => o.IsNegative).ToList();
        }

        #endregion
    }
}