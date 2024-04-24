document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('form');
    const timerElement = document.getElementById('timer');

    function Timer() {
        let time = timerElement.innerHTML;
        let minutes = parseInt(time.split(':')[0]);
        let seconds = parseInt(time.split(':')[1]);

        if (seconds == 0) {
            minutes -= 1;
            seconds = 59;
        } else {
            seconds -= 1;
        }

        if (minutes == 0 && seconds == 0) {
            form.submit();
            return;
        }

        timerElement.innerHTML = `${minutes}:${seconds < 10 ? '0' + seconds : seconds}`;
    }

    setInterval(Timer, 1000);
});