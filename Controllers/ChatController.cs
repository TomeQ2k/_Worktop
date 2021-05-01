using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Extensions;
using Worktop.Core.Params;
using Worktop.Core.Services.Interfaces;
using Worktop.Core.SignalR;
using Worktop.Models.Domain;
using Worktop.ViewModels;
using Worktop.ViewModels.Partials;

namespace Worktop.Controllers
{
    public class ChatController : Controller
    {
        private readonly IMessenger messenger;
        private readonly IChatRoomsManager chatRoomsManager;
        private readonly IMapper mapper;

        public ChatController(IMessenger messenger, IChatRoomsManager chatRoomsManager, IMapper mapper)
        {
            this.messenger = messenger;
            this.chatRoomsManager = chatRoomsManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index() => View(new ChatViewModel());

        [HttpGet]
        public async Task<IActionResult> Global()
        {
            ViewBag.CurrentUserName = HttpContext.User.Identity.Name;

            return View(new GlobalChatViewModel(await messenger.GetMessages(new FetchMessagesParams())).WithAlert());
        }

        [HttpGet]
        public async Task<IActionResult> FetchMessages([FromQuery] int pageNumber = 1)
        {
            var messages = await messenger.GetMessages(new FetchMessagesParams().CurrentPage(pageNumber) as FetchMessagesParams);

            return Ok(new { messages, totalPages = messages.TotalPages });
        }

        [HttpGet]
        public async Task<IActionResult> Rooms()
            => View(new RoomsViewModel(await chatRoomsManager.GetRooms()).WithAlert());

        [HttpGet]
        public async Task<IActionResult> Room(string roomId, string password = null)
        {
            RoomsChatHub.SetRoomId(roomId);

            ViewBag.CurrentUserName = HttpContext.User.Identity.Name;
            ViewBag.RoomId = roomId;

            var chatRoom = await chatRoomsManager.JoinRoom(roomId, password: password);

            return chatRoom != null ? (IActionResult)View(new RoomViewModel(chatRoom)) : RedirectToAction("Rooms");
        }

        [HttpGet]
        public IActionResult CreateRoom() => View(new EditRoomViewModel());

        [HttpGet]
        public IActionResult EditRoom(string id)
        {
            var room = chatRoomsManager.GetRoom(id);

            return room != null ? (IActionResult)View(mapper.Map<EditRoomViewModel>(room)) : this.ErrorPage();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message, string roomId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await messenger.Send(message.Text, HttpContext.User.Identity.Name, roomId) != null
                ? (IActionResult)Ok() : BadRequest();
        }

        [HttpPost]
        public IActionResult JoinRoom(RoomCardViewModel viewModel)
            => chatRoomsManager.JoinRoom(viewModel.Room?.Id, viewModel.Password) == null
                ? (IActionResult)RedirectToAction("Rooms")
                : RedirectToAction("Room", new { roomId = viewModel.Room.Id, password = viewModel.Password });

        [HttpPost]
        public async Task<IActionResult> LeaveRoom(string roomId)
            => await chatRoomsManager.LeaveRoom(roomId)
                ? (IActionResult)RedirectToAction("Rooms") : this.ErrorPage();

        [HttpPost]
        public async Task<IActionResult> CreateRoom(EditRoomViewModel viewModel)
            => await chatRoomsManager.CreateRoom(mapper.Map<ChatRoom>(viewModel))
                ? (IActionResult)RedirectToAction("Rooms").PushAlert($"Room has been created")
                : this.ErrorPage();

        [HttpPost]
        public async Task<IActionResult> UpdateRoom(EditRoomViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("EditRoom", viewModel);

            return await chatRoomsManager.UpdateRoom(mapper.Map<ChatRoom>(viewModel))
                ? (IActionResult)RedirectToAction("Rooms").PushAlert($"Room #{viewModel.Id} has been updated")
                : this.ErrorPage();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoom(string id)
            => await chatRoomsManager.DeleteRoom(id)
                ? (IActionResult)RedirectToAction("Rooms").PushAlert($"Room #{id} has been deleted")
                : this.ErrorPage();
    }
}