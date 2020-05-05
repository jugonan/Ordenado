/* Empiezo todo con el ContentLoaded porque si no se rompe */
window.addEventListener('DOMContentLoaded', function () {
    let opcionComprar = document.getElementById('opcion-comprar');
    let opcionVender = document.getElementById('opcion-vender');
    let camposRegistro = document.getElementById('campos-registro');
    let botonesAutenticacion = document.getElementById('botones-autenticacion-registro');
    let cuadroUsuario = document.getElementById('div-de-usuario');
    let cuadroVendedor = document.getElementById('div-de-vendedor');

    /* Event listeners para mostrar una interfaz u otra */
    opcionVender.addEventListener('click', function () {
        setTimeout(function () {
            opcionVender.parentElement.classList.add('d-none');
            camposRegistro.classList.remove('d-none');
            cuadroUsuario.classList.add('d-none');
        }, 500);
    })
    opcionComprar.addEventListener('click', function () {
        setTimeout(function () {
            opcionComprar.parentElement.classList.add('d-none');
            camposRegistro.classList.remove('d-none');
            botonesAutenticacion.classList.remove('d-none');
            cuadroVendedor.classList.add('d-none');
        }, 500);

    })

    let accesoExterno = document.getElementById('parrafo-superior-er').children;
    for (i = 0; i < accesoExterno.length; i++) {
        switch (accesoExterno[i].value) {
            case 'Google':
                accesoExterno[i].classList.remove('btn-primary');
                accesoExterno[i].classList.add('btn-warning');
                break;

            case 'Facebook':
                accesoExterno[i].classList.remove('btn-primary');
                accesoExterno[i].classList.add('btn-info');
                break;
        };
    };
});
