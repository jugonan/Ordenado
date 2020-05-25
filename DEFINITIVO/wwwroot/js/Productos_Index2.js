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
    tryLocation();
})

//if (document.readyState !== 'loading') {
//    tryLocation();
//} else {
//    document.addEventListener('DOMContentLoaded', function () {
//        tryLocation();
//    });
//}

function tryLocation() {
    let location = document.getElementById('location').value;
    let myModal = document.getElementById('myModal');
    postalCodeLS = JSON.parse(localStorage.getItem("location"));

    if (postalCodeLS == null || postalCodeLS == "") {
        let btnConfirmar = document.getElementById('botonConfirmar');
        myModal.style.display = "block";
        $("#myModal").modal();

        let inputModalPostalCode = document.getElementById('inputPostalCode');
        inputModalPostalCode.addEventListener('input', function () {
            let postalCode = this.value;
            if (postalCode.length === 5) {
                localStorage.setItem("location", JSON.stringify(postalCode));
            }
        })
    }
}



