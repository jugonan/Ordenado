/* Variables */
let imagenSpinner;
let bodySpinner;
let aClicarSpinner;
let aClicar2Spinner;
let divPadreSpinner;
let spinnerSpinner;
let textoSpinner = document.createElement('p');

/* Empieza JavaScript */
window.addEventListener('load', function () {
    llenarVariablesSpinner();
    bodySpinner.addEventListener('click', generarSpinner);
    aClicarSpinner.addEventListener('click', generarSpinner);
    aClicar2Spinner.addEventListener('click', generarSpinner);
})

function llenarVariablesSpinner() {
    bodySpinner = document.getElementById('mainDiv');
    aClicarSpinner = document.getElementById('navbar-logo');
    aClicar2Spinner = document.getElementById('continuarNav');
    divPadreSpinner = document.createElement('div');
    imagenSpinner = document.createElement('img');
    imagenSpinner.src = 'https://i.pinimg.com/originals/78/e8/26/78e826ca1b9351214dfdd5e47f7e2024.gif';
}

function generarSpinner() {
    let fondoPaginaEliminar = document.getElementById('mainContainerLayout');
    divPadreSpinner.classList.add('div-spinner');
    imagenSpinner.classList.add('imagen-spinner');
    textoSpinner.classList.add('texto-spinner');
    textoSpinner.innerText = getText();
    divPadreSpinner.appendChild(textoSpinner);
    divPadreSpinner.appendChild(imagenSpinner);
    bodySpinner.appendChild(divPadreSpinner);
    setTimeout(function () {
        textoSpinner.classList.add('texto-spinner-opaco');
        fondoPaginaEliminar.remove();
    }, 500)
}


function getText() {
    let numero = Math.floor(Math.random() * 5);
    switch (numero) {
        case 1:
            return '¿Sabías que, debido al COVID-19, se espera que la cifra de desempleados en España ascienda hasta un 24.7%?'
            break;
        case 2:
            return 'Heldu nace desde y para el desempleado. La idea surgió estando ambos fundadores en paro, conociendo las dificultades que supone no tener ingresos.'
            break;
        case 3:
            return 'En Mayo de 2020 Zara, Stradivarius y Uterque anunciaron <Special Prices> y rebajas de hasta el 50% en las dos primeras y del 30%, en el caso de la tercera.'
            break;
        case 4:
            return '¿Sabías que una cultura de responsabilidad civil crea un concepción positiva de la marca, atrae más público y fideliza futuros clientes? Fuente: investopedia.'
            break;
        case 5:
            return 'Heldu surge con el objetivo de generar un impacto positivo y una situación Win-to-Win en la que la persona desempleada pueda tenere acceso a un consumo común y el negocio pueda llegar a usuarios que de otra manera no podría.'
            break;

    }
}