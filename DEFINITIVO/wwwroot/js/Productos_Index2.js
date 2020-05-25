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


function tryLocation() {
    let locationPC = document.getElementById('inputModalPostalCode').value;
    let myModal = document.getElementById('myModal');
    postalCodeLS = JSON.parse(localStorage.getItem("location"));

    if (postalCodeLS == null || postalCodeLS == "") {
        myModal.style.display = "block";
        $("#myModal").modal();
    }

    let btnCancelar = document.getElementById('botonCancelar');
    btnCancelar.addEventListener('click', function () {
        localStorage.setItem("location", JSON.stringify(locationPC));
    })

    let inputModalPostalCode = document.getElementById('inputPostalCode');
    inputModalPostalCode.addEventListener('input', function () {
        let postalCode = this.value;
        if (postalCode.length === 5) {
            localStorage.setItem("location", JSON.stringify(postalCode));
        }
    })
} 

let modifyLocation = document.getElementById('modificarCiudad');
modifyLocation.addEventListener('click', function () {
    let inputModalPostalCode = document.getElementById('inputPostalCode');
    inputModalPostalCode.addEventListener('input', function () {
        let postalCode = this.value;
        if (postalCode.length === 5) {
            localStorage.setItem("location", JSON.stringify(postalCode));
        }
    })
})
    




