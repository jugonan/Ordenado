let ancla;
let form;
document.addEventListener('scroll', function () {
    let divCategorias = document.getElementById('divCategorias');
    divCategorias.classList.remove('show');
    let divDetallesHeldu = document.getElementById('divDetallesHeldu');
    divDetallesHeldu.classList.remove('show');
    let divBuscador = document.getElementById('divBuscador');
    divBuscador.classList.remove('show');
})

function funcion() {
    alimentarVariables();
    addEvent();
}

function alimentarVariables() {
    ancla = document.getElementById('anchor-miperfil');
    form = ancla.nextSibling.nextSibling;
}
function addEvent() {
    ancla.addEventListener('mouseover', function () {
        form.classList.remove('d-none');
    })
}
