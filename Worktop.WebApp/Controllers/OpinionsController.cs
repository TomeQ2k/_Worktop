using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Application.Services;
using Worktop.WebApp.ViewModels;

namespace Worktop.WebApp.Controllers
{
    public class OpinionsController : Controller
    {
        private readonly IOpinionManager opinionManager;
        private readonly IMapper mapper;

        public OpinionsController(IOpinionManager opinionManager, IMapper mapper)
        {
            this.opinionManager = opinionManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(new OpinionsViewModel((await opinionManager.FetchOpinions()).ToList()));
    }
}