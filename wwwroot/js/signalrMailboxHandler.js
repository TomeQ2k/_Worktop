const connection = new signalR.HubConnectionBuilder()
    .withUrl(MAILBOX_HUB_ROUTE)
    .build();

connection.on(SIGNALR_ACTIONS.MAIL_RECEIVED, appendMail);

connection.start()
    .catch(error => {
        console.error(error.message);
    });