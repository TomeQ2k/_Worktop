using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Enums;
using Worktop.Core.Extensions;
using Worktop.Core.Services.Interfaces;
using Worktop.ViewModels;

namespace Worktop.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksManager tasksManager;
        private readonly IMapper mapper;

        public TasksController(ITasksManager tasksManager, IMapper mapper)
        {
            this.tasksManager = tasksManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(new TasksViewModel(await tasksManager.GetUserTasks()).WithAlert());

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