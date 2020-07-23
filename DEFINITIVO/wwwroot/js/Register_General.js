let storage;
let isCliente;
let isNegocio;

/* Empiezo todo con el ContentLoaded porque si no se rompe */
window.addEventListener('DOMContentLoaded', function () {
    getStorage();
});


function getStorage() {
    storage = localStorage.getItem('rol');
    isCliente = document.getElementById('input-isCliente');
    isNegocio = document.getElementById('input-isVendedor');
    if (storage === 'negocio') {
        let borrar = isCliente.parentElement;
        borrar.remove();
        isNegocio.value = true;
    } else {
        let borrar = isNegocio.parentElement;
        borrar.remove();
        isCliente.value = true;
    }
}