const mailbox = document.getElementById('mailbox');

const MAIL_CARD_WIDTH = "75vw";
const MAIL_BUTTON_HEIGHT = "40px";

function createMailCard(mail) {
    const mailCard = document.createElement('div');
    mailCard.className = 'card align-self-center mb-3 border-dark';
    mailCard.style.width = MAIL_CARD_WIDTH;

    const cardHeader = document.createElement('div');
    cardHeader.className = "card-header text-center";
    const headerStrong = document.createElement('strong');
    headerStrong.innerHTML = mail.subject;
    cardHeader.appendChild(headerStrong);

    const cardBody = document.createElement('div');
    cardBody.className = "card-body d-flex flex-row justify-content-around";

    const emailText = document.createElement('p');
    emailText.innerHTML = 'From: ';
    emailText.style.width = '20%';
    const emailSpan = document.createElement('span');
    emailSpan.className = 'text-muted';
    const emailStrong = document.createElement('strong');
    emailStrong.innerHTML = mail.fromAddress;
    emailSpan.appendChild(emailStrong);
    emailText.appendChild(emailSpan);

    const centerDiv = document.createElement('div');
    centerDiv.className = 'd-flex flex-row justify-content-center';
    centerDiv.style.width = '50%';

    const favoriteButton = document.createElement('a');
    favoriteButton.href = `/Mailbox/ToggleFavorite/${mail.id}`;
    favoriteButton.className = 'btn btn-info mr-2';
    favoriteButton.style.height = MAIL_BUTTON_HEIGHT;
    favoriteButton.innerHTML = 'Favorite';

    const deleteButton = document.createElement('a');
    deleteButton.href = `/Mailbox/DeleteMail/${mail.id}`;
    deleteButton.className = 'btn btn-danger';
    deleteButton.style.height = MAIL_BUTTON_HEIGHT;
    deleteButton.innerHTML = 'Delete';

    centerDiv.appendChild(favoriteButton);
    centerDiv.appendChild(deleteButton);

    const dateText = document.createElement('p');
    dateText.className = 'float-right';
    dateText.innerHTML = 'Sent: ';
    const dateStrong = document.createElement('strong');
    dateStrong.innerHTML = new Date(mail.dateSent).toLocaleDateString('pl-PL', { hour: 'numeric', minute: 'numeric', hour12: true });
    const dateSpan = document.createElement('span');
    dateSpan.className = 'text-muted';
    dateSpan.appendChild(dateStrong);
    dateText.appendChild(dateSpan);

    cardBody.appendChild(emailText);
    cardBody.appendChild(centerDiv);
    cardBody.appendChild(dateText);

    const contentSection = document.createElement('section');
    contentSection.className = 'container';
    const contentHeader = document.createElement('p');
    contentHeader.className = 'card-text';
    const contentStrong = document.createElement('strong')
    contentStrong.innerHTML = 'Content:';
    contentHeader.appendChild(contentStrong);
    const contentText = document.createElement('p');
    contentText.innerHTML = mail.content;
    contentSection.appendChild(contentHeader);
    contentSection.appendChild(contentText);

    mailCard.appendChild(cardHeader);
    mailCard.appendChild(cardBody);
    mailCard.appendChild(contentSection);

    return mailCard;
}

function appendMail(mail) {
    const mailCard = createMailCard(mail);
    mailbox.prepend(mailCard);
}