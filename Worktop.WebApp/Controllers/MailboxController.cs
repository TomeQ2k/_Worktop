using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Helpers;
using Worktop.Core.Application.Params;
using Worktop.Core.Application.Services;
using Worktop.Core.Application.SignalR;
using Worktop.Core.Application.SignalR.Hubs;
using Worktop.Core.Domain.Entities;
using Worktop.WebApp.ViewModels;

namespace Worktop.WebApp.Controllers
{
    public class MailBoxController : Controller
    {
        private readonly IMailboxService mailboxService;
        private readonly IMapper mapper;
        private readonly IHubContext<MailboxHub> hubContext;
        private readonly IConnectionManager connectionManager;

        public MailBoxController(IMailboxService mailboxService, IMapper mapper, IHubContext<MailboxHub> hubContext, IConnectionManager connectionManager)
        {
            this.mailboxService = mailboxService;
            this.mapper = mapper;
            this.hubContext = hubContext;
            this.connectionManager = connectionManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
            => View(new MailboxViewModel(await mailboxService.GetMails(new GetMailsParams().CurrentPage(pageNumber) as GetMailsParams)).WithAlert());

        [HttpPost]
        public async Task<IActionResult> FilterMails(MailboxViewModel viewModel, [FromQuery] int pageNumber = 1)
           => View("Index", viewModel.FilterMails(await mailboxService.FilterMails(GetMailsParams.Build
           (
               viewModel.Subject,
               viewModel.OnlyFavorites,
               viewModel.SortType
           ).CurrentPage(pageNumber) as GetMailsParams)));

        [HttpGet]
        public async Task<IActionResult> NewMail() => View(new MailViewModel(await mailboxService.FetchEmailAddresses()).WithError());

        [HttpPost]
        public async Task<IActionResult> SendMail(MailViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("NewMail", viewModel);

            var mail = await mailboxService.SendMail(mapper.Map<Mail>(viewModel));

            if (mail != null)
            {
                string connectionId = await connectionManager.GetConnectionId(mail.ReceiverId);

                if (!string.IsNullOrEmpty(connectionId))
                    await hubContext.Clients.Client(await connectionManager.GetConnectionId(mail.ReceiverId))?
                        .SendAsync(SignalrActions.MAIL_RECEIVED,
                            new
                            {
                                id = mail.Id,
                                subject = mail.Subject,
                                content = mail.Content,
                                fromAddress = mail.FromAddress,
                                dateSent = mail.DateSent
                            });

                return RedirectToAction("Index").PushAlert("Mail sent");
            }

            return RedirectToAction("NewMail");
        }

        [HttpGet]
        public async Task<IActionResult> ToggleFavorite(int id, [FromQuery] int pageNumber)
            => await mailboxService.ToggleFavorite(id)
                ? (IActionResult)RedirectToAction("Index", new { pageNumber })
                : this.ErrorPage();

        [HttpGet]
        public async Task<IActionResult> DeleteMail(int id, [FromQuery] int pageNumber)
            => await mailboxService.DeleteMail(id)
                ? (IActionResult)RedirectToAction("Index", new { pageNumber }).PushAlert("Mail deleted")
                : this.ErrorPage();
    }
}