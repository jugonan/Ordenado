/*Declaro las variables que voy a ir rellenando y borrando cada vez que introduce el CP*/
let url = 'https://api.zippopotam.us/ES/';
let appendOption;
let escribir;
let estado;
let urlBase;
let format;
let apiKey;
let iban;
let btnValidar;
let errorSpan;


if (document.readyState !== 'loading') {
    console.log('document is already ready, just execute code here');
    myInitCode();
    validateIBAN();
} else {
    document.addEventListener('DOMContentLoaded', function () {
        console.log('document was not ready, place code here');
        myInitCode();
        validateIBAN();
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
        if (seleccion.length === 5q) {
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
    let estado = document.getElementById('comunidadAutonoma');
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


//Validación del IBAN
//https://bankcodesapi.com/users/api-iban/
//API Key: 07b1ca15082a6d485c93fecad6055dbe

function validateIBAN() {

    urlBase = "https://bankcodesapi.com/iban/";
    format = "json";
    apiKey = "9fc53b3db09ca830488d19546a4fc2a1";
    iban = "";

    btnValidar = document.getElementById('btnValidar');
    iban = document.getElementById('inputIBAN');
    errorSpan = document.getElementById('spanIBAN');
    iban.addEventListener('change', function () {
        if (iban.length == 24) {
            btnValidar.disabled = false;
        }
    });


    btnValidar.addEventListener('click', function (e) {
        e.preventDefault();

        btnValidar.disabled = true;
        let ibanNumber = iban.value;
        let urlFull = (urlBase + format + "/" + apiKey + "/" + ibanNumber);

        fetch(urlFull)
            .then(function (response) {
                return response.json();
            })
            .then(function (myJson) {
                console.log(myJson);
            });

        let respuesta = JSON.parse(response);
        if (respuesta.result.validation.iban_validity == "Valid") {
            btnValidar.style("background-color: green; color: white;");
            btnValidar.innerText("IBAN OK");
        }
        else {
            btnValidar.disabled = false;
            errorSpan.style("background-color: darkgray; color: red;");
            errorSpan.innerText("IBAN Incorrecto");
        }
            
    });

}


