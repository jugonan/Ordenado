/* Creo el objeto de condición con todas las varibles como nulas para después añadirle
 * los arreglos con valores en su Value */
let condicion;
let clicksDescripciones = 0;
let clicksCondiciones = 0;
let crearProductoBTN;
let anclasClickUsuario;
let condicionesProducto;
let ulCondiciones;
let botonAddCondicion;
let botonDeleteCondicion;
let botonDescripcion;
let botonDescripcionEliminar


/* Primer evento para ejecutar toda la carga del JS cuando la página está cargada */
window.addEventListener('DOMContentLoaded', function () {
    /* Estas funciones se ejecutan en Productos/Create */
/* Declaro el UL sobre el que crearé el resto de condiciones */
    llenarVariables();
    unableEnterKey();
    obtenerTitulo();
    obtenerDescripcion();
    obtenerFecha();
    obtenerImagen();
    obtenerImagen2();
    obtenerImagen3();
    obtenerCondiciones();
    enableAnclasOpciones();
    crearProductoBTN = document.getElementById('boton-crear-producto');
    crearProductoBTN.addEventListener('click', crearVM);

});

/*Escribe la información que va a meter sobre el producto en la muestra de la derecha */
function unableEnterKey() {
    window.addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            window.alert('El botón "enter" está desactivado, utiliza el botón "Crear" por favor :)');
        }
    });
}

function obtenerTitulo() {
    let tituloIntroducido = document.getElementById('titulo-introducido');
    tituloIntroducido.addEventListener('input', function () {
        let tituloDemo = document.getElementById('titulo-producto');
        tituloDemo.innerText = tituloIntroducido.value;
    });
}

function llenarVariables() {
    ulCondiciones = document.getElementById('lista-condiciones');
    botonAddCondicion = document.getElementById('condicion-introducidaBtn');
    botonDeleteCondicion = document.getElementById('condicion-introducida-borrar');
    botonDescripcion = document.getElementById('descripcion-introducidaBtn');
    botonDescripcionEliminar = document.getElementById('descripcion-introducida-borrar');
}

function obtenerDescripcion() {
    comprobarClicksDescripcion();
    botonDescripcion.addEventListener('click', function () {
        clicksDescripciones++;
        comprobarClicksDescripcion();
        let descripcionIntroducida = document.getElementById('descripcion-introducida');
        let descripcionDemo = document.getElementById('descripcion-demo');
        descripcionDemo.innerText = descripcionIntroducida.value;
        descripcionIntroducida.value = '';
    });
    botonDescripcionEliminar.addEventListener('click', function () {
        clicksDescripciones--;
        comprobarClicksDescripcion();
        let descripcionDemo = document.getElementById('descripcion-demo');
        descripcionDemo.innerText = '';
    })
}

function comprobarClicksDescripcion() {
    if (clicksDescripciones === 0) {
        botonDescripcionEliminar.disabled = true;
    }
    else {
        botonDescripcionEliminar.disabled = false;
    }
}

function obtenerFecha() {
    let fechaIntroducida = document.getElementById('fecha-introducida');
    fechaIntroducida.addEventListener('input', function () {
        let fechaDemo = document.getElementById('fecha-demo');
        fechaDemo.innerText = `Válido hasta: ${fechaIntroducida.value}`;
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

function enableAnclasOpciones() {
    anclasClickUsuario = document.getElementsByClassName('anclas-opciones');
    condicionesProducto = document.getElementsByClassName('texto-ocultar ');
    /* Cada vez que hago click oculto todas las condiciones y luego muestro la seleccionada */
    for (let i = 0; i < anclasClickUsuario.length; i++) {
        anclasClickUsuario[i].addEventListener('click', function () {
            ocultarTodasCondiciones();
            condicionesProducto[i].classList.remove('d-none');
        })
    }
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

function ocultarTodasCondiciones() {
    for (let i = 0; i < condicionesProducto.length; i++) {
        condicionesProducto[i].classList.add('d-none');
    }
}

function obtenerCondiciones() {
    reviewClicks();
    botonAddCondicion.addEventListener('click', addCondicion);
    botonDeleteCondicion.addEventListener('click', deleteCodicion);
}

function addCondicion() {
    clicksCondiciones++;
    reviewClicks();
    if (clicksCondiciones < 8) {
        let inputCondicion = document.getElementById('condiciones-introducidas');
        let condicion = inputCondicion.value;
        let li = document.createElement('li');
        li.className = 'lista-condiciones';
        li.innerHTML = condicion;
        ulCondiciones.appendChild(li);
        inputCondicion.value = '';
    }
    else {
        showModal();
    }
}

function deleteCodicion() {
    clicksCondiciones--;
    ulCondiciones.removeChild(ulCondiciones.lastChild);
    reviewClicks();
}

function reviewClicks() {
    if (clicksCondiciones === 0) {
        botonDeleteCondicion.disabled = true;
    }
    else {
        botonDeleteCondicion.disabled = false;
    }
}

function showModal() {
    $('#myModal').modal('show');
}

function crearVM() {
    let descripcionIntroducida = document.getElementById('descripcion-introducida');
    let descripcionDemo = document.getElementById('descripcion-demo');
    descripcionIntroducida.value = descripcionDemo.innerText;
    let listas = document.getElementsByClassName('lista-condiciones');
    let condiciones = [];
    for (let i = 0; i < listas.length; i++) {
        condiciones.push(listas[i].innerText);
    }
    let condicionesSend = JSON.stringify(condiciones);
    let sender = document.getElementById('condiciones-sender');
    sender.value = condicionesSend;
}

