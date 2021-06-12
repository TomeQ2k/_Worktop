using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Services;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Common.Enums;
using Worktop.Core.Common.Helpers;
using Worktop.WebApp.ViewModels;

namespace Worktop.WebApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksManager tasksManager;
        private readonly IReadOnlyPanelManager panelManager;
        private readonly IMapper mapper;

        public TasksController(ITasksManager tasksManager, IReadOnlyPanelManager panelManager, IMapper mapper)
        {
            this.tasksManager = tasksManager;
            this.panelManager = panelManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
            => View(TasksViewModel.Create(await tasksManager.GetCurrentUserTasks(), await panelManager.GetAllWorkers()).WithAlert());

        [HttpGet]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> Worker(int workerId)
            => View(TasksViewModel.Create(await tasksManager.GetUserTasks(workerId), await panelManager.GetAllWorkers())
                    .SetReadOnly()
                    .SelectWorker(workerId)
                    .WithAlert());

        [HttpGet]
        public IActionResult ArrangeTask() => View(new EditTaskViewModel());

        [HttpGet]
        public async Task<IActionResult> EditTask(int id)
        {
            var task = await tasksManager.GetTask(id);

            return task != null ? (IActionResult)View(mapper.Map<EditTaskViewModel>(task)) : this.ErrorPage();
        }

        [HttpPost]
        public async Task<IActionResult> ArrangeTask(EditTaskViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            return await tasksManager.ArrangeTask(viewModel.Description, viewModel.DateDeadline)
                ? (IActionResult)RedirectToAction("Index", "Tasks").PushAlert("Task was created")
                : View(viewModel.WithAlert("Arranging task failed"));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(EditTaskViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("EditTask", viewModel);

            return await tasksManager.UpdateTask(viewModel.Id, viewModel.Description, viewModel.DateDeadline)
                ? (IActionResult)RedirectToAction("Index", "Tasks").PushAlert("Task was updated")
                : View(viewModel.WithAlert("Updating task failed"));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(int taskId)
            => await tasksManager.DeleteTask(taskId)
                ? (IActionResult)RedirectToAction("Index", "Tasks").PushAlert("Task was deleted")
                : this.ErrorPage();

        [HttpPost]
        public async Task<IActionResult> ChangeProgressStatus(int taskId, TaskProgress progress)
            => await tasksManager.ChangeProgressStatus(taskId, progress)
                ? (IActionResult)RedirectToAction("Index", "Tasks")
                : this.ErrorPage();
    }
}