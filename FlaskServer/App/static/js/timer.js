const button = document.getElementById('button');
const form = document.getElementById('form');
const timerElement = document.getElementById('timer');

button.onclick = () => {
    button.disabled = true;
    form.submit()
}

function Timer() {
    let time = timerElement.innerHTML;
    let hours = parseInt(time.split(':')[0]);
    let minutes = parseInt(time.split(':')[1]);
    let seconds = parseInt(time.split(':')[2]);

    if (hours == 0 && minutes == 0 && seconds == 0) {
        form.submit();
        return;
    }

    if (seconds == 0) {
        if (minutes == 0) {
            hours -= 1;
            minutes = 59;
        } else {
            minutes -= 1;
        }
        seconds = 59;
    } else {
        seconds -= 1;
    }

    timerElement.innerHTML = `${hours < 10 ? '0' + hours : hours}:${minutes < 10 ? '0' + minutes : minutes}:${seconds < 10 ? '0' + seconds : seconds}`;
}

setInterval(Timer, 1000);