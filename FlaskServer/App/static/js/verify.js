var time = document.getElementById('time');
var button = document.getElementById('button');
var error = document.getElementById('error');

time.onchange = () => {
    if (isNaN(time.value))
    {
        button.disabled = true;
        error.style.display = 'block';
        return
    }
    button.disabled = false;
    error.style.display = 'none';
}
