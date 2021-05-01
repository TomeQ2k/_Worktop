window.onload = () => scrollToBottom();

const chat = document.getElementById('chat');
const textInput = document.getElementById('messageText');
const validationSpan = document.getElementById('validationSpan');

const senderName = currentUserName;
const roomId = currentRoomId;
const senderId = currentUserId;

const messagesQueue = [];

const MESSAGE_CARD_WIDTH = "55vw";
const MAX_MESSAGE_LENGTH = 500;

let currentPage = 1;
let totalPages = Number.MAX_SAFE_INTEGER;

function clearInputFields() {
    messagesQueue.push(textInput.value);
    textInput.value = '';
    validationSpan.innerHTML = '';
}

function sendMessage() {
    const text = messagesQueue.shift() || "";
    if (text.trim() === "") return;

    const message = new Message(text, senderName, senderId, roomId);

    sendMessageToHub(message, roomId);
}

function validationFailHandler() {
    const text = messagesQueue.shift() || '';
    let validationErrorMessage = '';

    if (text === '') {
        validationErrorMessage = 'Message cannot be empty';
    } else if (text.length > MAX_MESSAGE_LENGTH) {
        validationErrorMessage = `Message cannot be longer than ${MAX_MESSAGE_LENGTH} characters`;
    } else {
        validationErrorMessage = 'Error occurred';
    }

    validationSpan.innerHTML = validationErrorMessage;
}

function createMessageCard(message) {
    const messageCard = document.createElement('div');
    messageCard.className = message.senderId !== senderId ? "card border-dark mb-3 mx-auto" : "card border-info mb-3 mx-auto";
    messageCard.style.width = MESSAGE_CARD_WIDTH;

    const cardHeader = document.createElement('div');
    cardHeader.className = "card-header";
    cardHeader.innerHTML = message.senderName;

    const cardBody = document.createElement('div');
    cardBody.className = "card-body text-dark";

    const cardText = document.createElement('p');
    cardText.className = "card-text";
    cardText.innerHTML = message.text;
    const cardDateText = document.createElement('span');
    cardDateText.className = "text-info";
    cardDateText.innerHTML = new Date(message.dateSent).toLocaleDateString('pl-PL', { hour: 'numeric', minute: 'numeric', hour12: true });

    cardBody.appendChild(cardText);
    cardBody.appendChild(cardDateText);

    messageCard.appendChild(cardHeader);
    messageCard.appendChild(cardBody);

    return messageCard;
}

function appendMessage(message) {
    const messageCard = createMessageCard(message);
    chat.appendChild(messageCard);
    scrollToBottom();
}

function appendMessages(messages) {
    messages.reverse();

    for (const message of messages) {
        const messageCard = createMessageCard(message);
        chat.prepend(messageCard);
    }
}

function getMessages() {
    fetch('/Chat/FetchMessages?' + new URLSearchParams({ pageNumber: currentPage }))
        .then(response => response.json())
        .then(data => {
            totalPages = data.totalPages;
            appendMessages(data.messages);
        })
        .catch(error => console.error('Unable to fetch messages', error));
}

onScroll(() => {
    currentPage++;
    if (currentPage <= totalPages) {
        getMessages();
    }
});