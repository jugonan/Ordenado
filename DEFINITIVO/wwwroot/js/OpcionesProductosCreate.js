/* Variables que muestro con la información del Model */
let ulReserva;
let ulHorario;
let ulEntrega;
let ulRecogida;
let ulOtros;

function funcion() {
    let imagenes = document.getElementsByClassName('carousel-item');
    let fuente = imagenes[0].src;
    let link = fuente + "data:image/gif;base64,";
    imagenes[0].src = link;
    ulReserva = document.getElementById('ulReserva');
    ulHorario = document.getElementById('ulHorario');
    ulEntrega = document.getElementById('ulEntrega');
    ulRecogida = document.getElementById('ulRecogida');
    ulOtros = document.getElementById('ulOtros');

    if (ulReserva.hasChildNodes(ulReserva)) {
        mostrarHijos(ulReserva);
    }
    if (ulHorario.hasChildNodes(ulHorario)){
        mostrarHijos(ulHorario);
    }
    if (ulEntrega.hasChildNodes(ulEntrega)){
        mostrarHijos(ulEntrega);
    }
    if (ulRecogida.hasChildNodes(ulRecogida)){
        mostrarHijos(ulRecogida);
    }
    if (ulOtros.hasChildNodes(ulOtros)){
        mostrarHijos(ulOtros);
    }
}

function mostrarHijos(variable) {
    variable.classList.remove('d-none');
}