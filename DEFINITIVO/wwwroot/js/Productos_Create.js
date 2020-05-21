/* Declaro las variables que llenaré como lista para alimentar las <li> */
const listaHorarios = [];

window.addEventListener('DOMContentLoaded', function () {

    obtenerTitulo();
    obtenerImagen();
    obtenerFecha();
    obtenerHorario();
    /*obtenerDescripcion();
    obtenerPrecioInicial();
    obtenerPrecioFinal(); */
});

/*Escribe la información que va a meter sobre el producto en la muestra de la derecha */
function obtenerTitulo() {
    let tituloIntroducido = document.getElementById('titulo-introducido');
    tituloIntroducido.addEventListener('input', function () {
        let tituloDemo = document.getElementById('titulo-producto');
        tituloDemo.innerText = tituloIntroducido.value;
    });
}

function obtenerDescripcion() {
    let descripcionIntroducida = document.getElementById('descripcion-introducida');
    descripcionIntroducida.addEventListener('input', function () {
        let descripcionDemo = document.getElementById('descripcion-demo');
        descripcionDemo.innerText = descripcionIntroducida.value;
    });
}

function obtenerFecha() {
    let fechaIntroducida = document.getElementById('fecha-introducida');
    fechaIntroducida.addEventListener('input', function () {
        let fechaDemo = document.getElementById('fecha-demo');
        fechaDemo.innerText = fechaIntroducida.value;
    });
}

function obtenerHorario() {
    let botonHorario = document.getElementById('horario-introducidaBtn');
    botonHorario.addEventListener('click', function () {
        let horarioIntroducido = document.getElementById('horario-introducida');
        /* Elimino el d-none del UL para que se pueda ver el bullet point */
        let ulHorario = document.getElementById('ul-horario');
        ulHorario.classList.remove('d-none');
        /* Añado a la lista declarada el valor que acaba de introducir el usuario */
        listaHorarios.push(horarioIntroducido.value);
        /* Traigo desde la lista lo que se ha guardado, lo imprimo en <li> y lo borro del input para que pueda
         * seguir añadiendo condicines*/
        for (i = listaHorarios.length-1; i < listaHorarios.length; i++) {
            let li = document.createElement('li');
            li.innerText = listaHorarios[i];
            ulHorario.appendChild(li);
        }
        horarioIntroducido.value = '';
    });
}

function obtenerImagen() {
    let imagenIntroducida = document.getElementById('imagen-introducida');
    imagenIntroducida.addEventListener('input', function () {
        let imagenDemo = document.getElementById('imagen-demo');
        imagenDemo.src = imagenIntroducida.value;
    });
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