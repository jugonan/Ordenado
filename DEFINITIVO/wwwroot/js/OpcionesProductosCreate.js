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
let labelOpciones;
/* Objecto del tipo de Opción producto. Cada vez que añada uno, creo uno nuevo y lo guardo en una lista */
let opcion = {
    descripcion: '',
    precioInicio: '',
    precioFin: '',
    unidades: ''
};
/* Arreglo en el que voy añadiendo las diferentes opciones de producto */
let opciones = [];
/* Variable auxiliar con la que controlo que no añada más de 3 productos */
let clicks = 0;


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
    labelOpciones = document.getElementsByClassName('radio-opciones');

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
    revisarOpciones();

    descripcion.value = '';
    precioInicio.value = '';
    precioFin.value = '';
    unidades.value = '';
}

function revisarOpciones() {
    if (opciones.length !== 0) {
        for (i = 0; i < opciones.length; i++) {
            input = document.createElement('input');
            labelOpciones[i].innerText = opciones[i].descripcion;
            input.setAttribute('name', 'opcionesProducto');
            input.setAttribute('type', 'radio');
            input.setAttribute('id',`${i}`);
            input.addEventListener('click', cambiarPrecio);
            labelOpciones[i].appendChild(input);
        }
    }
}

function cambiarPrecio() {
    /* Quito el 'd-none' a los símbolos de € */
    let euros = document.getElementsByClassName('simbolo-euro');
    for (i = 0; i < euros.length; i++) {
        euros[i].classList.remove('d-none');
    }
    let precioInicial = document.getElementById('precioInicial-demo');
    let precioFinal = document.getElementById('precioFinal-demo');
    precioInicial.innerText = opciones[this.id].precioInicio;
    precioFinal.innerText = opciones[this.id].precioFin;
}