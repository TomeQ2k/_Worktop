const connection = new signalR.HubConnectionBuilder()
    .withUrl(ROOMSCHAT_HUB_ROUTE)
    .build();

connection.on(SIGNALR_ACTIONS.MESSAGE_RECEIVED, appendMessage);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

const sendMessageToHub = (message, roomId) => {
    connection.invoke(SIGNALR_INVOKES.SEND_MESSAGE, message, roomId);
};