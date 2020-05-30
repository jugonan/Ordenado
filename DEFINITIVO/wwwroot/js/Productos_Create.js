/* Declaro las variables que llenaré como lista para alimentar las <li> */
let listaHorarios = [];
let listaReservas = [];
let listaEntregas = [];
let listaRecogidas = [];
let listaOtros = [];
/* Creo el objeto de condición con todas las varibles como nulas para después añadirle
 * los arreglos con valores en su Value */
let condicion = {
    'Horario': null,
    'Reservas': null,
    'Entregas': null,
    'Recogidas': null,
    'Otros': null
};
let clicksHorarios = 0;
let clicksReservas = 0;
let clicksEntregas = 0;
let clicksRecogidas = 0;
let clicksOtros = 0;
let crearProductoBTN;

/* Primer evento para ejecutar toda la carga del JS cuando la página está cargada */
window.addEventListener('DOMContentLoaded', function () {
    /* Estas funciones se ejecutan en Productos/Create */
    obtenerTitulo();
    obtenerDescripcion();
    obtenerFecha();
    obtenerImagen();
    obtenerImagen2();
    obtenerImagen3();
    obtenerHorario();
    obtenerReserva();
    obtenerEntrega();
    obtenerRecogida();
    obtenerOtros();
    crearProductoBTN = document.getElementById('boton-crear-producto');
    crearProductoBTN.addEventListener('click', crearVM);

});

/*Escribe la información que va a meter sobre el producto en la muestra de la derecha */
function obtenerTitulo() {
    let tituloIntroducido = document.getElementById('titulo-introducido');
    tituloIntroducido.addEventListener('input', function () {
        let tituloDemo = document.getElementById('titulo-producto');
        tituloDemo.innerText = tituloIntroducido.value;
    });
}

function obtenerImagen() {
    var input = document.getElementById('imagen-introducida');
    input.addEventListener('change', cambiarImagenDemo)

    function cambiarImagenDemo() {
        var file = input.files[0]
        let imagenes = document.getElementsByClassName('carousel-item');
        let Blob = URL.createObjectURL(file);
        let imagenCarousel = imagenes[0].firstElementChild;
        imagenCarousel.src = Blob;
        let thumbnail = document.getElementsByClassName('img-thumbnail');
        thumbnail[0].src = Blob;
    }
}

function obtenerFecha() {
    let fechaIntroducida = document.getElementById('fecha-introducida');
    fechaIntroducida.addEventListener('input', function () {
        let fechaDemo = document.getElementById('fecha-demo');
        fechaDemo.innerText = fechaIntroducida.value;
    });
}

function obtenerImagen2() {
    var input = document.getElementById('imagen-introducida2');
    input.addEventListener('change', cambiarImagenDemo2)
    function cambiarImagenDemo2() {
        var file = input.files[0]
        let imagenes = document.getElementsByClassName('carousel-item');
        let Blob = URL.createObjectURL(file);
        let imagenCarousel = imagenes[1].firstElementChild;
        imagenCarousel.src = Blob;
        let thumbnail = document.getElementsByClassName('img-thumbnail');
        thumbnail[1].src = Blob;
        imagenes[0].classList.remove('active');
        imagenes[1].classList.add('active');
    }
}

function obtenerImagen3() {
    var input = document.getElementById('imagen-introducida3');
    input.addEventListener('change', cambiarImagenDemo3)
    function cambiarImagenDemo3() {
        var file = input.files[0]
        let imagenes = document.getElementsByClassName('carousel-item');
        let Blob = URL.createObjectURL(file);
        let imagenCarousel = imagenes[2].firstElementChild;
        imagenCarousel.src = Blob;
        let thumbnail = document.getElementsByClassName('img-thumbnail');
        thumbnail[2].src = Blob;
        imagenes[1].classList.remove('active');
        imagenes[2].classList.add('active');
    }
}

function obtenerHorario() {
    let botonHorario = document.getElementById('horario-introducidaBtn');
    botonHorario.addEventListener('click', function () {
        if (comprobarClicks('horario')) {
            let horarioIntroducido = document.getElementById('horario-introducida');
            /* Elimino el d-none del UL para que se pueda ver el bullet point */
            let ulHorario = document.getElementById('ul-horario');
            ulHorario.classList.remove('d-none');
            /* Añado a la lista declarada el valor que acaba de introducir el usuario */
            listaHorarios.push(horarioIntroducido.value);
            /* Traigo desde la lista lo que se ha guardado, lo imprimo en <li> y lo borro del input para que pueda
             * seguir añadiendo condicines*/
            for (i = listaHorarios.length - 1; i < listaHorarios.length; i++) {
                let li = document.createElement('li');
                li.innerText = listaHorarios[i];
                ulHorario.appendChild(li);
            }
            horarioIntroducido.value = '';
        }
        else {
            window.prompt('Ya has escrito suficiente');
        }
    });
    let botonHorarioEliminar = document.getElementById('horario-introducida-borrar');
    botonHorarioEliminar.addEventListener('click', function () {
        clicksHorarios = 0;
        let ulHorario = document.getElementById('ul-horario');
        while (ulHorario.firstChild) {
            ulHorario.removeChild(ulHorario.firstChild);
        }
        ulHorario.classList.add('d-none');
    })
}

function obtenerReserva() {
    let botonReserva = document.getElementById('reserva-introducidaBtn');
    botonReserva.addEventListener('click', function () {
        clicksReservas = 0;
        if (comprobarClicks('reserva')) {
            let reservaIntroducida = document.getElementById('reserva-introducida');
            /* Elimino el d-none del UL para que se pueda ver el bullet point */
            let ulReserva = document.getElementById('ul-reserva');
            ulReserva.classList.remove('d-none');
            /* Añado a la lista declarada el valor que acaba de introducir el usuario */
            listaReservas.push(reservaIntroducida.value);
            /* Traio desde la lista lo que se ha guardado, lo imprimo en <li> y lo borro del input para que pueda
             * seguir añadiendo condicines*/
            for (i = listaReservas.length - 1; i < listaReservas.length; i++) {
                let li = document.createElement('li');
                li.innerText = listaReservas[i];
                ulReserva.appendChild(li);
            }
            reservaIntroducida.value = '';
        }
        else {
            window.prompt('Ya has escrito suficiente');
        }
    });
    let botonReservaEliminar = document.getElementById('reserva-introducida-borrar');
    botonReservaEliminar.addEventListener('click', function () {
        let ulReserva = document.getElementById('ul-reserva');
        while (ulReserva.firstChild) {
            ulReserva.removeChild(ulReserva.firstChild);
        }
        ulReserva.classList.add('d-none');
    })
}

function obtenerEntrega() {
    let botonEntrega = document.getElementById('entrega-introducidaBtn');
    botonEntrega.addEventListener('click', function () {
        clicksEntregas = 0;
        if (comprobarClicks('entrega')) {
            let entregaIntroducida = document.getElementById('entrega-introducida');
            /* Elimino el d-none del UL para que se pueda ver el bullet point */
            let ulEntrega = document.getElementById('ul-entrega');
            ulEntrega.classList.remove('d-none');
            /* Añado a la lista declarada el valor que acaba de introducir el usuario */
            listaEntregas.push(entregaIntroducida.value);
            /* Traio desde la lista lo que se ha guardado, lo imprimo en <li> y lo borro del input para que pueda
             * seguir añadiendo condicines*/
            for (i = listaEntregas.length - 1; i < listaEntregas.length; i++) {
                let li = document.createElement('li');
                li.innerText = listaEntregas[i];
                ulEntrega.appendChild(li);
            }
            entregaIntroducida.value = '';
        }
        else {
            window.prompt('Ya has escrito suficiente');
        }
    });
    let botonEntregaEliminar = document.getElementById('entrega-introducida-borrar');
    botonEntregaEliminar.addEventListener('click', function () {
        let ulEntrega = document.getElementById('ul-entrega');
        while (ulEntrega.firstChild) {
            ulEntrega.removeChild(ulEntrega.firstChild);
        }
        ulEntrega.classList.add('d-none');
    })
}

function obtenerRecogida() {
    let botonRecogida = document.getElementById('recogida-introducidaBtn');
    botonRecogida.addEventListener('click', function () {
        clicksRecogidas = 0;
        if (comprobarClicks('recogida')) {
            let recogidaIntroducida = document.getElementById('recogida-introducida');
            /* Elimino el d-none del UL para que se pueda ver el bullet point */
            let ulRecogida = document.getElementById('ul-recogida');
            ulRecogida.classList.remove('d-none');
            /* Añado a la lista declarada el valor que acaba de introducir el usuario */
            listaRecogidas.push(recogidaIntroducida.value);
            /* Traigo desde la lista lo que se ha guardado, lo imprimo en <li> y lo borro del input para que pueda
             * seguir añadiendo condicines*/
            for (i = listaRecogidas.length - 1; i < listaRecogidas.length; i++) {
                let li = document.createElement('li');
                li.innerText = listaRecogidas[i];
                ulRecogida.appendChild(li);
            }
            recogidaIntroducida.value = '';
        }
        else {
            window.prompt('Ya has escrito suficiente');
        }
    });
    let botonRecogidaEliminar = document.getElementById('recogida-introducida-borrar');
    botonRecogidaEliminar.addEventListener('click', function () {
        let ulRecogida = document.getElementById('ul-recogida');
        while (ulRecogida.firstChild) {
            ulRecogida.removeChild(ulEntrega.firstChild);
        }
        ulRecogida.classList.add('d-none');
    })
}

function obtenerOtros() {
    let botonOtro = document.getElementById('otros-introducidoBtn');
    botonOtro.addEventListener('click', function () {
        clicksOtros
        if (comprobarClicks('otro')) {
            let otroIntroducido = document.getElementById('otros-introducido');
            /* Elimino el d-none del UL para que se pueda ver el bullet point */
            let ulOtro = document.getElementById('ul-otros');
            ulOtro.classList.remove('d-none');
            /* Añado a la lista declarada el valor que acaba de introducir el usuario */
            listaOtros.push(otroIntroducido.value);
            /* Traigo desde la lista lo que se ha guardado, lo imprimo en <li> y lo borro del input para que pueda
             * seguir añadiendo condicines*/
            for (i = listaOtros.length - 1; i < listaOtros.length; i++) {
                let li = document.createElement('li');
                li.innerText = listaOtros[i];
                ulOtro.appendChild(li);
            }
            otroIntroducido.value = '';
        }
        else {
            window.prompt('Ya has escrito suficiente');
        }
    });
    let botonOtrosEliminar = document.getElementById('otros-introducida-borrar');
    botonOtrosEliminar.addEventListener('click', function () {
        let ulOtros = document.getElementById('ul-otros');
        while (ulOtros.firstChild) {
            ulOtros.removeChild(ulOtros.firstChild);
        }
        ulOtros.classList.add('d-none');
    })
}

function obtenerPrecioInicial() {
    let precioInicialIntroducido = document.getElementById('precioInicial-introducido');
    precioInicialIntroducido.addEventListener('input', function () {
        let precioInicialDemo = document.getElementById('precioInicial-demo');
        precioInicialDemo.innerText = precioInicialIntroducido.value;
    });
}

function obtenerPrecioFinal() {

    let precioFinalIntroducido = document.getElementById('precioFinal-introducido');
    precioFinalIntroducido.addEventListener('input', function () {
        let precioFinalDemo = document.getElementById('precioFinal-demo');
        precioFinalDemo.innerText = precioFinalIntroducido.value;

        let descuento = document.getElementById('descuento-demo');
        let numero = Math.ceil(100 - ((100 * precioFinalIntroducido.value) / precioInicialIntroducido.value));
        descuento.innerText = (numero) + '% de descuento';
    });
}

function obtenerDescripcion() {
    let botonDescripcion = document.getElementById('descripcion-introducidaBtn');
    botonDescripcion.addEventListener('click', function () {
        let descripcionIntroducida = document.getElementById('descripcion-introducida');
        let descripcionDemo = document.getElementById('descripcion-demo');
        descripcionDemo.innerText = descripcionIntroducida.value;
        descripcionIntroducida.value = '';
    });
    let botonDescripcionEliminar = document.getElementById('descripcion-introducida-borrar');
    botonDescripcionEliminar.addEventListener('click', function () {

        let descripcionDemo = document.getElementById('descripcion-demo');
        descripcionDemo.innerText = '';
    })

}

function comprobarClicks(string) {
    switch (string) {
        case 'horario':
            clicksHorarios++;
            if (clicksHorarios > 3) {
                return false;
            }
            else {
                return true;
            }
            break;

        case 'reserva':
            clicksReservas++;
            if (clicksReservas > 3) {
                return false;
            }
            else {
                return true;
            }
            break;

        case 'entrega':
            clicksEntregas++;
            if (clicksEntregas > 2) {
                return false;
            }
            else {
                return true;
            }
            break;

        case 'recogida':
            clicksRecogidas++;
            if (clicksRecogidas > 2) {
                return false;
            }
            else {
                return true;
            }
            break;

        case 'otro':
            clicksOtros++;
            if (clicksOtros > 4) {
                return false;
            }
            else {
                return true;
            }
            break;
    }
}

function crearVM() {
    /* Al limpiar todos los inputs, se envían vacíos al controlador
     * con este método les devuelvo el valor para crear el VM*/
    let otroIntroducido = document.getElementById('otros-introducido');
    condicion.Horario = listaHorarios;
    condicion.Reservas = listaReservas;
    condicion.Entregas = listaEntregas;
    condicion.Recogidas = listaRecogidas;
    condicion.Otros = listaOtros;
    var json = JSON.stringify(condicion);
    otroIntroducido.value = json;
};

/* Parte de la vista de OpcionesProductos Create */
