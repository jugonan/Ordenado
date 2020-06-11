/* Variables */
let body;
let aClicar;
let divPadre;
let spinner;
let texto = document.createElement('p');
texto.innerText = '¿Sabías que España prevee tener una tasa de paro de hasta un 24,7%?';

/* Empieza JavaScript */
window.addEventListener('load', function () {
    llenarVariablesSpinner();
    aClicar.addEventListener('click', generarSpinner)
})

function llenarVariablesSpinner() {
    body = document.getElementById('mainDiv');
    aClicar = document.getElementById('navbar-logo');
    divPadre = document.createElement('div');
    imagen = document.createElement('img');
    imagen.src = 'https://i.pinimg.com/originals/78/e8/26/78e826ca1b9351214dfdd5e47f7e2024.gif';
}

function generarSpinner() {
    divPadre.classList.add('div-spinner');
    imagen.classList.add('imagen-spinner');
    texto.classList.add('texto-spinner');
    divPadre.appendChild(texto);
    divPadre.appendChild(imagen);
    body.appendChild(divPadre);
    setTimeout(function () {
        texto.classList.add('texto-spinner-opaco');
    },500)
}
