let anclas,
    anclasOcultar,
    condiciones,
    vendedor,
    opiniones,
    anclaCondiciones,
    anclaVendedor,
    anclaOpiniones;

window.addEventListener('DOMContentLoaded', function () {
    llenarVariables();
})

function llenarVariables() {
    anclas = document.getElementsByClassName('anclas-opciones');
    anclasOcultar = document.getElementsByClassName('anchor-texto-opciones');
    anclaCondiciones = anclas[0];
    anclaVendedor = anclas[1];
    anclaOpiniones = anclas[2];
    condiciones = document.getElementById('condiciones-producto-detalles');
    vendedor = document.getElementById('vendedor-producto-detalles');
    opiniones = document.getElementById('opiniones-producto-detalles');
    anclaCondiciones.addEventListener('click', function () {
        addOpacity();
        condiciones.classList.add('opacidad-cero');
    })
    anclaVendedor.addEventListener('click', function () {
        addOpacity();
        vendedor.classList.add('opacidad-cero');
    })
    anclaOpiniones.addEventListener('click', function () {
        addOpacity();
        opiniones.classList.add('opacidad-cero');
    })
}

function addOpacity() {
    for (i = 0; i < anclasOcultar.length; i++) {
        anclasOcultar[i].classList.remove('opacidad-cero');
    }
}