const button = document.getElementById('button');
const form = document.getElementById('form');
const time = document.getElementById('timer');
var target = new Date(new Date().getTime() + parseInt(minutes) * 60_000);

if (sessionStorage.getItem('time') != null)
    target = new Date(sessionStorage.getItem('time'));
sessionStorage.setItem('time', target)

button.onclick = () => {
    button.disabled = true;
    sessionStorage.removeItem('time')
    form.submit()
}

function Timer() {
    const now = new Date();
    
    if (now >= target) {
        button.disabled = true;
        sessionStorage.removeItem('time')
        form.submit();
        return;
    }
    time.innerHTML = new Date(target - now).toISOString().substring(11, 19);
    setTimeout(Timer, 10);
}

Timer()