let url = 'https://api.zippopotam.us/ES/';
let appendOption;
let escribir;
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
    escribir.addEventListener('input', function () {
        let seleccion = this.value;
        if (seleccion.length > 4) {
            url += `${seleccion}`;

            fetch(url).then(response).then(show);

            function response(datos) {
                return datos.json();
            }
            function show(datos) {
                console.log('creando contenido');
                crearContenido(datos);
            }
        }
    })
}

function crearContenido(datos) {
    appendOption = document.getElementById('poblacion');
    let estado = document.getElementById('comunidadAutonoma');
    crearPoblaciones(datos);
    //poblacion.innerText = calcularNombrePoblacion(JSON.stringify(datos.places[0]));
    estado.innerText = datos.places[0].state;
    estado.value = datos.places[0].state;
}
function crearPoblaciones(datos) {
    for (i = 0; i < datos.places.length; i++) {
        let option = document.createElement('option');
        option.innerText = calcularNombrePoblacion(JSON.stringify(datos.places[i]));
        appendOption.appendChild(option);
    }
}

function calcularNombrePoblacion(datos) {
    let longitud = datos.length;
    var primerCorte = datos.slice(15, longitud);
    let posicion = primerCorte.search(',');
    let segundoCorte = primerCorte.slice(0, (posicion - 1));
    return segundoCorte;
}
