var time = document.getElementById('time');
var button = document.getElementById('button');
var error = document.getElementById('error');

time.onchange = () => {
    if (isNaN(time.value)) {
        button.disabled = true;
        error.innerHTML = "Preencha o campo apenas com números."
        error.style.display = 'block';
        return
    } else {
        button.disabled = false;
        error.style.display = 'none';
    }

    if (parseInt(time.value) > 180) {
        button.disabled = true;
        error.innerHTML = "Tempo de prova máximo de 180 minutos excedido."
        error.style.display = 'block';
        return
    } else {
        button.disabled = false;
        error.style.display = 'none';
        return
    }
}
