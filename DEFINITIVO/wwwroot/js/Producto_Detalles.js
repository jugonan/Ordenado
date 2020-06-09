/* Variables que voy a utilizar durante el programa */
let imagenesCarousel,
    imagenesThumbnail,
    anclasClickUsuario,
    condicionesProducto;

window.addEventListener('DOMContentLoaded', function () {
    llenarVariables();
    addEvents();
    TEMPORALquitarMinutosDeFecha();
    TEMPORALlPrecioTotal();
});


function llenarVariables() {
    imagenesCarousel = document.getElementsByClassName('imagenes-carousel');
    anclasClickUsuario = document.getElementsByClassName('anclas-opciones');
    condicionesProducto = document.getElementsByClassName('texto-ocultar ');
    /* Esta variable es larga porque la convierto a un arreglo para aplicar forEach y tomar su parent element */
    imagenesThumbnail = [];
    let previoImagenesThumbnail = document.getElementsByClassName('normal-thumbnail-carousel');
    let arregloImagenesThumbnail = Array.from(previoImagenesThumbnail);
    arregloImagenesThumbnail.forEach(elemento => {
        imagenesThumbnail.push(elemento.firstElementChild);
    })
}

function addEvents() {
    for (let index = 0; index < imagenesCarousel.length; index++) {
        imagenesCarousel[index].addEventListener('error', function () {
            imagenesCarousel[index].parentElement.classList.add('d-none');
            imagenesCarousel[index].parentElement.classList.remove('carousel-item');
        })
    }
    for (let i = 0; i < imagenesThumbnail.length; i++) {
        imagenesThumbnail[i].addEventListener('error', function () {
            imagenesThumbnail[i].parentElement.classList.add('d-none');
        })
    }
    /* Cada vez que hago click oculto todas las condiciones y luego muestro la seleccionada */
    for (let i = 0; i < anclasClickUsuario.length; i++) {
        anclasClickUsuario[i].addEventListener('click', function () {
            ocultarTodasCondiciones();
            condicionesProducto[i].classList.remove('d-none');
        })
    }
}

function ocultarTodasCondiciones() {
    for (let i = 0; i < condicionesProducto.length; i++) {
        condicionesProducto[i].classList.add('d-none');
    }
}

/* Esta así hasta que tengamos opciones por producto, ahí desaparecerá */
function TEMPORALlPrecioTotal() {
    let precioTotal = document.getElementById('precio-compra');
    let precioFinal = document.getElementById('precio-final-producto').innerText;
    let precioInicial = document.getElementById('precio-inicial-producto').innerText;
    let click = document.getElementById('opcion-producto-1').addEventListener('click', function () {
        precioTotal.innerText = precioFinal;
        let ahorrado = document.getElementById('cantidad-ahorrada');
        let cantidadAhorrada = precioInicial - precioFinal;
        ahorrado.innerText = (`Has ahorrado ${cantidadAhorrada.toFixed(2)}€`);
        let placesDescuento = document.getElementsByClassName('descuento-opciones');
        let placeDescuento = placesDescuento[0];
        let cantidadDescuento = 100 - (precioFinal * 100 / precioInicial);
        placeDescuento.innerText = `desc: ${cantidadDescuento.toFixed(0)}%`;
    })
}

function TEMPORALquitarMinutosDeFecha() {
    let fecha = document.getElementById('fecha-validez');
    let textoFecha = fecha.innerText.toString();
    let posicion = textoFecha.indexOf(' ');
    fecha.innerText = textoFecha.substring(0, posicion);
}

