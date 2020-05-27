/* Variables que muestro con la información del Model */
let ulReserva;
let ulHorario;
let ulEntrega;
let ulRecogida;
let ulOtros;
let btnAdd;
let btnCreate;

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
    descuento: '',
    unidades: ''
};

/* Arreglo en el que voy añadiendo las diferentes opciones de producto */
let opciones = [];

/* Variable auxiliar con la que controlo que no añada más de 3 productos */
let clicks = 0;

/* Función inicial que se ejecuta con 'DOMContentLoaded' desed CArritoLayout.js */
function funcion() {
    llenarVariablesModel();
    llenarListas();
}


function crearOpcion() {
    llenarVariablesOpcionProducto();
    opcion = {
        descripcion: descripcion.value,
        precioInicio: precioInicio.value,
        precioFin: precioFin.value,
        descuento: calcularDescuento(precioInicio.value, precioFin.value),
        unidades: unidades.value
    };
    /* Añado el objeto con los atributos con la información que acaba de introducir a la lista de objetos
     * que luego volcaré en las opciones productos*/
    opciones.push(opcion);
    revisarOpciones();
    vaciarVariablesOpcionProducto();
}

/* Por cada objeto guardado en la lista de objetos creo un input con el mismo nombre para que pueda ser utilizado
 * como radio. Le asigno la posición que tiene en el array como 'id' para luego utilizarlo*/
function revisarOpciones() {
    if (opciones.length !== 0) {
        for (i = 0; i < opciones.length; i++) {
            input = document.createElement('input');
            labelOpciones[i].innerText = opciones[i].descripcion;
            input.setAttribute('name', 'opcionesProducto');
            input.setAttribute('type', 'radio');
            input.setAttribute('id', `${i}`);
            input.addEventListener('click', cambiarPrecio);
            labelOpciones[i].appendChild(input);
        }
    }
}

/* Sumo 1 en clicks totales para que no exceda el límite, luego quito el d-none de los precios para que sean visibles
 * y utilizo el id antes asignado al input, que es igual a la posición que tiene el objeto en el arreglo de objetos para
 * extraer sus parámetros */
function cambiarPrecio() {
    clicks++;
    let euros = document.getElementsByClassName('simbolo-euro');
    for (i = 0; i < euros.length; i++) {
        euros[i].classList.remove('d-none');
    }
    let descuento = document.getElementById('descuento-demo');
    let precioInicial = document.getElementById('precioInicial-demo');
    let precioFinal = document.getElementById('precioFinal-demo');
    precioInicial.innerText = opciones[this.id].precioInicio;
    precioFinal.innerText = opciones[this.id].precioFin;
    descuento.innerText = opciones[this.id].descuento;
}

/* Calculo el porcentaje de descuento que tiene con los dos precios y lo redondeo a un int */
function calcularDescuento(precioInicio, precioFin) {
    let respuesta = 100 - (precioFin * 100 / precioInicio);
    return `%${respuesta.toFixed(0)}`;
}

/* Identifico las variables a rellenar con la información del producto en la vista y les añado
 * su información */
function llenarVariablesModel() {
    ulReserva = document.getElementById('ulReserva');
    ulHorario = document.getElementById('ulHorario');
    ulEntrega = document.getElementById('ulEntrega');
    ulRecogida = document.getElementById('ulRecogida');
    ulOtros = document.getElementById('ulOtros');
    labelOpciones = document.getElementsByClassName('radio-opciones');
    btnAdd = document.getElementById('btnAddOpcion');
    btnCreate = document.getElementById('btnCreate');
    btnAdd.addEventListener('click', crearOpcion);
    btnCreate.addEventListener('click', pasarVM);
}

/* Creo un <li> por cada objeto en el Model de producto */
function llenarListas() {
    if (ulReserva.hasChildNodes(ulReserva)) {
        mostrarHijos(ulReserva);
    }
    if (ulHorario.hasChildNodes(ulHorario)) {
        mostrarHijos(ulHorario);
    }
    if (ulEntrega.hasChildNodes(ulEntrega)) {
        mostrarHijos(ulEntrega);
    }
    if (ulRecogida.hasChildNodes(ulRecogida)) {
        mostrarHijos(ulRecogida);
    }
    if (ulOtros.hasChildNodes(ulOtros)) {
        mostrarHijos(ulOtros);
    }
}

/* Quito la clase d-none para que se muestren  */
function mostrarHijos(variable) {
    variable.classList.remove('d-none');
}

/* Identifico las variables que utilizaré para crear el objeto */
function llenarVariablesOpcionProducto() {
    descripcion = document.getElementById('descripcion-introducida');
    precioInicio = document.getElementById('precioInicial-introducido');
    precioFin = document.getElementById('precioFinal-introducido');
    unidades = document.getElementById('unidades-introducidas');
}

/* Limpio las variables para que pueda crear un nuevo objeto */
function vaciarVariablesOpcionProducto() {
    descripcion.value = '';
    precioInicio.value = '';
    precioFin.value = '';
    unidades.value = '';
}

function pasarVM() {
    let opcion1 = document.getElementById('opcionProducto1');
    let opcion2 = document.getElementById('opcionProducto2');
    let opcion3 = document.getElementById('opcionProducto3');
    opcion1.value = JSON.stringify(opciones[0]);
    opcion2.value = JSON.stringify(opciones[1]);
    opcion3.value = JSON.stringify(opciones[2]);
}
