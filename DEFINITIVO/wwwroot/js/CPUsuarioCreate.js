/*Declaro las variables que voy a ir rellenando y borrando cada vez que introduce el CP*/ 
let url = 'https://api.zippopotam.us/ES/';
let appendOption;
let escribir;
let estado;

if (document.readyState !== 'loading') {
    console.log('document is already ready, just execute code here');
    myInitCode();
} else {
    document.addEventListener('DOMContentLoaded', function () {
        console.log('document was not ready, place code here');
        myInitCode();
    });
}


function myInitCode() {
    escribir = document.getElementById('codigoPostal');
    escribir.addEventListener('click', function () {
        /*Le añado una función de click para que limpie cada vez que introduce o modifica el CP*/
        if (escribir.value.length === 5) {
            limpiarCampos();
        }
    });
    escribir.addEventListener('input', function () {
        let seleccion = this.value;
        if (seleccion.length === 5) {
            /*Se activa cuando el CP es 5 y modifica la url original añadiendo el CP introducido al final como KEY */
            let urlBusqueda = (url + `${seleccion}`);

            fetch(urlBusqueda)
                .then(datos => datos.json())
                .then(datos => crearContenido(datos))
        }
    })
}

function crearContenido(datos) {
    appendOption = document.getElementById('poblacion');
    estado = document.getElementById('comunidadAutonoma');
    crearPoblaciones(datos);
    estado.innerText = datos.places[0].state;
    estado.value = datos.places[0].state;
}
function crearPoblaciones(datos) {
    for (i = 0; i < datos.places.length; i++) {
        /* Por cada Población en el CP introducido genera un optio con el nombre como InnerText y value */
        let option = document.createElement('option');
        option.innerText = datos.places[i]["place name"];
        appendOption.appendChild(option);
    }
}

function limpiarCampos() {
    escribir.value = '';
    estado.value = '';
    estado.innerText = '';
    while (appendOption.hasChildNodes()) {
        appendOption.removeChild(appendOption.firstChild);
    }
}