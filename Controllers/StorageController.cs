using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Enums;
using Worktop.Core.Extensions;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;
using Worktop.ViewModels;
using Worktop.ViewModels.Components;
using Worktop.ViewModels.Partials;

namespace Worktop.Controllers
{
    public class StorageController : Controller
    {
        private readonly IStorageManager storageManager;
        private readonly IDirectoryManager directoryManager;
        private readonly IMimeMappingService mimeMappingService;

        private static StorageViewModel storageViewModel;
        private static DirectoryViewModel directoryViewModel;
        private static EditDirectoryViewModel editDirectoryViewModel;

        public StorageController(IStorageManager storageManager, IDirectoryManager directoryManager, IMimeMappingService mimeMappingService)
        {
            this.storageManager = storageManager;
            this.directoryManager = directoryManager;
            this.mimeMappingService = mimeMappingService;
        }

        [HttpGet]
        public IActionResult Index() => View(new StorageIndexViewModel());

        [HttpGet]
        public async Task<IActionResult> Public([FromQuery] StorageSortType sortType = StorageSortType.DateDescending)
            => View(storageViewModel = (StorageViewModel)new StorageViewModel(await storageManager.ListFiles(sortType: sortType), await storageManager.ListDirectories()).WithError().WithAlert());

        [HttpGet]
        public async Task<IActionResult> Private([FromQuery] StorageSortType sortType = StorageSortType.DateDescending)
            => View(storageViewModel = (StorageViewModel)new StorageViewModel(await storageManager.ListFiles(isPrivate: true, sortType: sortType),
                await storageManager.ListDirectories(isPrivate: true)).WithError().WithAlert());

        [HttpGet]
        public async Task<IActionResult> Directory(string id, [FromQuery] bool isPrivate = false, StorageSortType sortType = StorageSortType.DateDescending)
        {
            directoryViewModel = ((DirectoryViewModel)new DirectoryViewModel(await storageManager.ListDirectory(id, sortType: sortType))
                .WithError().WithAlert()).SetPrivate(isPrivate);

            return directoryViewModel.Directory != null
                ? (IActionResult)View(directoryViewModel)
                : this.AccessDeniedPage();
        }

        [HttpGet]
        public async Task<IActionResult> PreviousDirectory([FromQuery] string directoryId, bool isPrivate = false)
        {
            var previousDirectory = await storageManager.ListPreviousDirectory(directoryId);

            return previousDirectory != null ? (IActionResult)RedirectToAction("Directory", new { id = previousDirectory.Id, isPrivate = isPrivate })
                : RedirectToAction(!isPrivate ? "Public" : "Private");
        }

        [HttpGet]
        public async Task<IActionResult> EditDirectory([FromQuery] string directoryId)
            => View(editDirectoryViewModel = (EditDirectoryViewModel)new EditDirectoryViewModel(await directoryManager.GetDirectory(directoryId)).WithError());

        [HttpPost]
        public async Task<IActionResult> UploadFiles([FromForm] UploadFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return viewModel.DirectoryId == null ?
                    (IActionResult)View(!viewModel.IsPrivate ? "Public" : "Private", storageViewModel) : View("Directory", directoryViewModel.SetPrivate(viewModel.IsPrivate));

            return await storageManager.UploadFiles(viewModel.Files, directoryId: viewModel.DirectoryId, isPrivate: viewModel.IsPrivate)
               ? (viewModel.DirectoryId == null ? (IActionResult)RedirectToAction(!viewModel.IsPrivate ? "Public" : "Private")
                    : RedirectToAction("Directory", new { id = viewModel.DirectoryId, isPrivate = viewModel.IsPrivate })).PushAlert("File has been uploaded")
               : (viewModel.DirectoryId == null ? (IActionResult)RedirectToAction(!viewModel.IsPrivate ? "Public" : "Private")
                    : RedirectToAction("Directory", new { id = viewModel.DirectoryId, isPrivate = viewModel.IsPrivate }));
        }

        [HttpPost]
        public async Task<IActionResult> DownloadFile([FromQuery] string fileId)
        {
            var downloadContent = await storageManager.DownloadFile(fileId);

            return downloadContent != null
                ? File(downloadContent.File, mimeMappingService.MapContentType(downloadContent), downloadContent.FileName)
                : (IActionResult)this.ErrorPage();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFile([FromQuery] string fileId, string directoryId, bool isPrivate = false)
         => await storageManager.DeleteFile(fileId)
             ? (directoryId == null ? (IActionResult)RedirectToAction(!isPrivate ? "Public" : "Private")
                 : RedirectToAction("Directory", new { id = directoryId, isPrivate = isPrivate })).PushAlert("File has been deleted")
             : (directoryId == null ? (IActionResult)RedirectToAction(!isPrivate ? "Public" : "Private") : RedirectToAction("Directory", new { id = directoryId, isPrivate = isPrivate }));

        [HttpPost]
        public async Task<IActionResult> CreateDirectory(DirectoryFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return viewModel.DirectoryId == null ?
                    (IActionResult)View(!viewModel.IsPrivate ? "Public" : "Private", storageViewModel) : View("Directory", directoryViewModel.SetPrivate(viewModel.IsPrivate));

            return await directoryManager.CreateDirectory(viewModel.DirectoryName, viewModel.DirectoryPath, isPrivate: viewModel.IsPrivate, parentDirectoryId: viewModel.DirectoryId) != null
                    ? (viewModel.DirectoryId == null ? (IActionResult)RedirectToAction(!viewModel.IsPrivate ? "Public" : "Private")
                        : RedirectToAction("Directory", new { id = viewModel.DirectoryId, isPrivate = viewModel.IsPrivate })).PushAlert("Directory has been created")
                    : (viewModel.DirectoryId == null ? (IActionResult)RedirectToAction(!viewModel.IsPrivate ? "Public" : "Private", storageViewModel)
                        : RedirectToAction("Directory", new { id = viewModel.DirectoryId, isPrivate = viewModel.IsPrivate }));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDirectory(Directory directory)
        {
            if (!ModelState.IsValid)
                return View("EditDirectory", editDirectoryViewModel);

            return await directoryManager.UpdateDirectory(directory.Id, directory.Name)
                ? (directory.ParentDirectoryId == null ? (IActionResult)RedirectToAction(directory.UserId == null ? "Public" : "Private")
                    : RedirectToAction("Directory", new { id = directory.Id, isPrivate = directory.UserId != null })).PushAlert("Directory has been updated")
                : (directory.ParentDirectoryId == null ? RedirectToAction(directory.UserId == null ? "Public" : "Private")
                    : RedirectToAction("Directory", new { id = directory.Id, isPrivate = directory.UserId != null }));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDirectory([FromQuery] string directoryId, bool isPrivate = false, bool hasParentDirectory = false)
        {
            var parentDirectoryId = await directoryManager.DeleteDirectory(directoryId);

            return parentDirectoryId != null ? (!hasParentDirectory ? (IActionResult)RedirectToAction(!isPrivate ? "Public" : "Private")
                    : RedirectToAction("Directory", new { id = parentDirectoryId, isPrivate = isPrivate })).PushAlert("Directory has been deleted")
                : (!hasParentDirectory ? (IActionResult)RedirectToAction(!isPrivate ? "Public" : "Private")
                    : RedirectToAction("Directory", new { id = parentDirectoryId, isPrivate = isPrivate }));
        }

        [HttpPost]
        public IActionResult SortFiles(StoragePartialViewModel viewModel)
        {
            if (viewModel.DirectoryId != null)
                return RedirectToAction("Directory", new { id = viewModel.DirectoryId, isPrivate = viewModel.IsPrivate, sortType = viewModel.SortType });
            else if (!viewModel.IsPrivate)
                return RedirectToAction("Public", new { sortType = viewModel.SortType });
            else if (viewModel.IsPrivate)
                return RedirectToAction("Private", new { sortType = viewModel.SortType });
            else
                return this.ErrorPage();
        }
    }
}