/* Variables */
let imagenSpinner;
let bodySpinner;
let aClicarSpinner;
let aClicar2Spinner;
let divPadreSpinner;
let spinnerSpinner;
let textoSpinner = document.createElement('p');
let textoVelocidad = document.createElement('p');
textoVelocidad.classList.add('texto-temporal-velocidad');

/* Empieza JavaScript */
window.addEventListener('load', function () {
    llenarVariablesSpinner();
    aClicarSpinner.addEventListener('click', generarSpinner);
    try {
        aClicar2Spinner.addEventListener('click', generarSpinner);
    }
    catch(error){ }
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
    textoVelocidad.innerText = '(P.D: la velocidad se debe a que es nuestro primer prototipo. Para la versión 2.0 irá como una bala... ¡Prometido!)';
    divPadreSpinner.appendChild(textoSpinner);
    divPadreSpinner.appendChild(imagenSpinner);
    bodySpinner.appendChild(divPadreSpinner);
    textoSpinner.appendChild(textoVelocidad);
    fondoPaginaEliminar.remove();
    window.scrollTo(0,0);
    setTimeout(function(){
        textoSpinner.classList.add('texto-spinner-opaco');
    },100)
    setTimeout(function() {
        sleep(4000);
    },500)
}

function sleep(ms){
    var now = Date.now();
    var end = now + ms;
    while(Date.now() < end){
    }
}

function getText() {
    numero = Math.floor(Math.random() * 5)+1;
    switch (numero) {
        case 1:
            return '¿Sabías que, debido al COVID-19, se espera que la cifra de desempleados en España ascienda hasta un 24,7%?'
            break;
        case 2:
            return 'Heldu nace desde y para el desempleado. La idea surgió estando ambos fundadores en paro, conociendo las dificultades que supone no tener ingresos.'
            break;
        case 3:
            return 'En Mayo de 2020 Zara, Stradivarius y Uterque anunciaron "Special Prices" con rebajas de hasta el 50% en las dos primeras y del 30%, en el caso de la tercera.'
            break;
        case 4:
            return '¿Sabías que una cultura de responsabilidad social crea un concepción positiva de la marca, atrae más público y fideliza futuros clientes?  Fuente: Investopedia.'
            break;
        case 5:
            return 'Heldu surge con el objetivo de generar un impacto positivo y una situación Win-to-Win. Por un lado la persona desempleada pueda tenere acceso a un consumo común. Y por otro lado, los comercios puedan llegar a usuarios que de otra manera no podrían.'
            break;
        default:
            return "¿Sabías que en mayo han quedado desempleadas más de 3000 personas en Euskadi?"
            break;

    }
}