using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Extensions;
using Worktop.Core.Helpers;
using Worktop.Core.Params;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;
using Worktop.ViewModels;
using Worktop.ViewModels.Components;

namespace Worktop.Controllers
{
    [Authorize(Policy = Constants.AdminPolicy)]
    public class PanelController : Controller
    {
        private readonly IPanelManager panelManager;
        private readonly IOpinionManager opinionManager;
        private readonly IMapper mapper;

        private static WorkerDetailsViewModel workerViewModel;

        public PanelController(IPanelManager panelManager, IOpinionManager opinionManager, IMapper mapper)
        {
            this.panelManager = panelManager;
            this.opinionManager = opinionManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
            => View(new PanelViewModel(await panelManager.GetWorkers(new FilterWorkersParams())));

        [HttpPost]
        public async Task<IActionResult> FilterWorkers(PanelViewModel viewModel, [FromQuery] int pageNumber = 1)
            => View("Index", viewModel.FilterWorkers(await panelManager.FilterWorkers(FilterWorkersParams.Build
            (
                viewModel.UserName,
                viewModel.Email,
                viewModel.SortType,
                viewModel.IsAdmin
            ).CurrentPage(pageNumber) as FilterWorkersParams)));

        [HttpGet]
        public async Task<IActionResult> Details(int id)
            => View(workerViewModel = (WorkerDetailsViewModel)mapper.Map<User, WorkerDetailsViewModel>
                (await panelManager.GetWorker(id), new WorkerDetailsViewModel { Id = id }).WithJobs(await panelManager.GetJobs()).WithAlert());

        [HttpPost]
        public async Task<IActionResult> AssignJob(int jobId, int workerId)
        {
            await panelManager.AssignJob(jobId, workerId);

            return RedirectToAction("Details", new { id = workerId });
        }

        [HttpPost]
        public async Task<IActionResult> SendOpinion(OpinionCreatorViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Details", workerViewModel);

            return await opinionManager.SendOpinion(mapper.Map<Opinion>(viewModel))
                ? (IActionResult)RedirectToAction("Details", new { id = workerViewModel.Id }).PushAlert("Opinion was sent") : this.ErrorPage();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWorker([FromQuery] int workerId)
            => await panelManager.DeleteWorker(workerId)
                ? (IActionResult)RedirectToAction("Index")
                : this.ErrorPage();
    }
}