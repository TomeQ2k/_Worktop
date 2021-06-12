const chatSection = document.getElementById('chat');

const SCROLL_TRIGGER_LEVEL = 50;

const scrollToBottom = () => {
    chatSection.scrollTo(0, chatSection.scrollHeight);
};

const onScroll = (callback) => {
    chatSection.addEventListener('scroll', () => {
        const scrollTop =  chatSection.scrollTop;

        if (Math.ceil(scrollTop) < SCROLL_TRIGGER_LEVEL) {
            callback();        
        }
    });
};