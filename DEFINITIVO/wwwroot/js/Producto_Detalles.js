/* Variables que voy a utilizar durante el programa */
let imagenesCarousel,
    imagenesThumbnail,
    anclasClickUsuario,
    condicionesProducto,

/*Desde aquí son para gestionar las opciones y mostrar precios*/
    cantOpciones,
    radio1,
    radio2,
    radio3,
    precioInicialOpcion1,
    precioInicialOpcion2,
    precioInicialOpcion3,
    precioFinalOpcion1,
    precioFinalOpcion2,
    precioFinalOpcion3,
    precioInicialProducto,
    precioFinalProducto,
    opcionElegida;


window.addEventListener('DOMContentLoaded', function () {
    llenarVariables();
    addEvents();
    TEMPORALquitarMinutosDeFecha();
    TEMPORALlPrecioTotal();
    TEMPORALAddCantidadVisitas();
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

    /*Desde aquí son para gestionar las opciones y mostrar precios*/
    cantOpciones = document.getElementById('cantOpciones').value;
    radio1 = document.getElementById('opcion-producto-1');
    precioInicialOpcion1 = document.getElementById('precioInicialOpcion1');
    precioFinalOpcion1 = document.getElementById('precioFinalOpcion1');
    precioInicialProducto = document.getElementById('precioInicialProducto');
    precioFinalProducto = document.getElementById('precioFinalProducto');

    if (cantOpciones > 1) {
        radio2 = document.getElementById('opcion-producto-2');
        precioInicialOpcion2 = document.getElementById('precioInicialOpcion2');
        precioFinalOpcion2 = document.getElementById('precioFinalOpcion2');
    }
    if (cantOpciones > 2) {
        radio3 = document.getElementById('opcion-producto-3');
        precioInicialOpcion3 = document.getElementById('precioInicialOpcion3');
        precioFinalOpcion3 = document.getElementById('precioFinalOpcion3');
    }
    opcionElegida = document.getElementById('opcionElegida');
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

    radio1.addEventListener('click', function () {
        mostrarprecios(precioInicialOpcion1.value, precioFinalOpcion1.value, 1);
    });
    if (radio2 != null) {
        radio2.addEventListener('click', function () {
            mostrarprecios(precioInicialOpcion2.value, precioFinalOpcion2.value, 2);
        });
    }
    if (radio3 != null) {
        radio3.addEventListener('click', function () {
            mostrarprecios(precioInicialOpcion3.value, precioFinalOpcion3.value, 3);
        });
    }
}

function ocultarTodasCondiciones() {
    for (let i = 0; i < condicionesProducto.length; i++) {
        condicionesProducto[i].classList.add('d-none');
    }
}

/* Esta así hasta que tengamos opciones por producto, ahí desaparecerá */
function TEMPORALlPrecioTotal() {
    /*let precioTotal = document.getElementById('precio-compra');*/
    let precioFinal = document.getElementById('precio-final-producto').innerText;
    let precioInicial = document.getElementById('precio-inicial-producto').innerText;
    let click = document.getElementById('opcion-producto-1').addEventListener('click', function () {
        /*precioTotal.innerText = `Total: ${precioFinal}€`;*/
        let placesDescuento = document.getElementsByClassName('descuento-opciones');
        let placeDescuento = placesDescuento[0];
        let cantidadDescuento = 100 - (precioFinal * 100 / precioInicial);
        placeDescuento.innerText = `${cantidadDescuento.toFixed(0)}%`;
    })
}

function TEMPORALquitarMinutosDeFecha() {
    let fecha = document.getElementById('fecha-validez');
    let textoFecha = fecha.innerText.toString();
    let posicion = textoFecha.indexOf('00');
    fecha.innerText = textoFecha.substring(0, posicion);
}

function TEMPORALAddCantidadVisitas() {
    let visitas = document.getElementById('cantidad-temporal-visitas');
    let random = Math.floor((Math.random() * 200) + 73);
    visitas.innerText = `+${random}`;
}

function mostrarprecios(precioInicial, precioFinal, opc) {
    precioInicialProducto.innerText = precioInicial;
    precioFinalProducto.innerText = precioFinal;
    opcionElegida.value = opc;
}