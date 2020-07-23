let clickNegocio;

window.addEventListener('DOMContentLoaded', function () {
    clickNegocio = document.getElementById('anchor-isVendedor');
    clickNegocio.addEventListener('click', function (event) {
        let cliente = 'negocio';
        localStorage.setItem('rol', cliente);
    });
})
