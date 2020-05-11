let url = 'https://api.zippopotam.us/ES/';
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
    escribir = document.getElementById('apiUno');
    escribir.addEventListener('input', function () {
        let seleccion = this.value;
        if (seleccion.length > 4) {
            url += `${seleccion}`;

            fetch(url).then(response).then(show);

            function response(datos) {
                return datos.json();
            }
            function show(datos) {
                crearContenido(datos);
            }
        }
    })
}

function crearContenido(datos) {
    let poblacion = document.createElement('p');
    let estado = document.createElement('p');
    let pais = document.createElement('p');
    poblacion.innerText = calcularNombrePoblacion(JSON.stringify(datos.places[0]));
    estado.innerText = datos.places[1].state;
    pais.innerText = datos.country;
    let poblacionAppend = document.getElementById('poblacion');
    let estadoAppend = document.getElementById('estado');
    let paisAppend = document.getElementById('pais');
    poblacionAppend.appendChild(poblacion);
    estadoAppend.appendChild(estado);
    paisAppend.appendChild(pais);
}

function calcularNombrePoblacion(datos) {
    let longitud = datos.length;
    var primerCorte = datos.slice(15, longitud);
    let posicion = primerCorte.search(',');
    let segundoCorte = primerCorte.slice(0, (posicion - 1));
    return segundoCorte;
}
