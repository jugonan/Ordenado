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
    anclasOcultar = document.getElementsByClassName('anclas-ocultar');
    anclaCondiciones = anclas[0];
    anclaVendedor = anclas[1];
    anclaOpiniones = anclas[2];
    condiciones = document.getElementById('condiciones-producto-detalles');
    vendedor = document.getElementById('vendedor-producto-detalles');
    opiniones = document.getElementById('opiniones-producto-detalles');
    anclaCondiciones.addEventListener('click', function () {
        addDisplayNone();
        condiciones.classList.remove('d-none');
    })
    anclaVendedor.addEventListener('click', function () {
        addDisplayNone();
        vendedor.classList.remove('d-none');
    })
    anclaOpiniones.addEventListener('click', function () {
        addDisplayNone();
        opiniones.classList.remove('d-none');
    })
}

function addDisplayNone() {
    for (i = 0; i < anclasOcultar.length; i++) {
        anclasOcultar[i].classList.add('d-none');
    }
}