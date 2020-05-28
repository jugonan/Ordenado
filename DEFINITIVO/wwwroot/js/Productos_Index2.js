/*
let ancla;
let form;
*/
document.addEventListener('scroll', function () {
    let divCategorias = document.getElementById('divCategorias');
    divCategorias.classList.remove('show');
    let divDetallesHeldu = document.getElementById('divDetallesHeldu');
    divDetallesHeldu.classList.remove('show');
    let divBuscador = document.getElementById('divBuscador');
    divBuscador.classList.remove('show');
    if (window.scrollY > 300) {
        tryLocation();
    }
})


let locationPC = document.getElementById('inputModalPostalCode').value;
let myModal = document.getElementById('myModal');
let cruzSalir = document.getElementById('cruzSalirModal');
let btnConfirmar = document.getElementById('botonConfirmar');
let btnCancelar = document.getElementById('botonCancelar');
let inputModalPostalCode = document.getElementById('inputPostalCode');
let modifyLocation = document.getElementById('modificarCiudad');

let postalCodeLS = JSON.parse(localStorage.getItem("location"));

function tryLocation() {
    if (postalCodeLS == null || postalCodeLS == "") {
        myModal.style.display = "block";
        $("#myModal").modal();
        GestionarModal();
    }
}
modifyLocation.addEventListener('click', GestionarModal());

function GestionarModal() {
    inputModalPostalCode.addEventListener('input', CambiarCP());
    btnCancelar.addEventListener('click', CancelarModal());
    cruzSalir.addEventListener('click', CancelarModal());
    function CancelarModal() {
        localStorage.setItem("location", JSON.stringify(locationPC));
    }
}

function CambiarCP() {
    let postalCode = this.value;
    if (postalCode.length === 5) {
        localStorage.setItem("location", JSON.stringify(postalCode));
        btnConfirmar.disabled = "false";
    }
}




