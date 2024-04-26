var time = document.getElementById('time');
var button = document.getElementById('button');
var error = document.getElementById('error');

time.onchange = () => {
    if (time.value.includes('.') || time.value.includes(',')) {
        button.disabled = true;
        error.innerHTML = "Preencha o campo apenas com números inteiros."
        error.style.display = 'block';
        return
    } else {
        button.disabled = false;
        error.style.display = 'none';
    }

    if (isNaN(time.value)) {
        button.disabled = true;
        error.innerHTML = "Preencha o campo apenas com números."
        error.style.display = 'block';
        return;
    } else {
        button.disabled = false;
        error.style.display = 'none';
    }

    if (parseInt(time.value) < 10 || parseInt(time.value) > 180) {
        button.disabled = true;
        error.innerHTML = "Tempo de prova mínimo ou máximo excedido, tente um valor entre 10 a 180 minutos."
        error.style.display = 'block';
        return;
    } else {
        button.disabled = false;
        error.style.display = 'none';
        return;
    }
}
