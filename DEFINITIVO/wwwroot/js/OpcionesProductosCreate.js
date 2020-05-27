/* Variables que muestro con la información del Model */
let ulReserva;
let ulHorario;
let ulEntrega;
let ulRecogida;
let ulOtros;
let btnAdd;
/* Variables que leo cada vez que crea una opción */
let descripcion;
let precioInicio;
let precioFin;
let unidades;
/* Objecto del tipo de Opción producto. Cada vez que añada uno, creo uno nuevo y lo guardo en una lista */
let opcion = {
    descripcion: '',
    precioInicio: '',
    precioFin: '',
    unidades: ''
};
let opciones;

function funcion() {
    let imagenes = document.getElementsByClassName('carousel-item');
    let fuente = imagenes[0].src;
    let link = fuente + "data:image/gif;base64,";
    imagenes[0].src = link;
    /* Ahora lo saco desde una clase pero luego lo cogeré desde el Id */
    let botones = document.getElementsByClassName('btn-warning');
    btnAdd = botones[0];
    btnAdd.addEventListener('click', crearOpcion);
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

function crearOpcion() {
    descripcion = document.getElementById('descripcion-introducida');
    precioInicio = document.getElementById('precioInicial-introducido');
    precioFin = document.getElementById('precioFinal-introducido');
    unidades = document.getElementById('unidades-introducidas');
    opcion = {
        descripcion: descripcion.value,
        precioInicio: precioInicio.value,
        precioFin: precioFin.value,
        unidades: unidades.value
    };
    opciones.push(opcion);
    descripcion.value = '';
    precioInicio.value = '';
    precioFin.value = '';
    unidades.value = '';
}