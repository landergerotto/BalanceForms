const button = document.getElementById('button');
const form = document.getElementById('form');
const time = document.getElementById('timer');
const target = new Date(new Date().getTime() + parseInt(minutes) * 60_000);

button.onclick = () => {
    button.disabled = true;
    form.submit()
}

function Timer() {
    const now = new Date();
    
    if (now >= target) {
        form.submit();
        return;
    }
    
    time.innerHTML = new Date(target - now).toISOString().substring(11, 19);
    setTimeout(Timer, 10);
}

Timer()