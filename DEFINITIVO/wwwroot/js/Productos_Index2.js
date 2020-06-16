//window.addEventListener('load', ponerBonitasCards)


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


let locationPC = document.getElementById('inputModalPostalCode');
let myModal = document.getElementById('myModal');
let cruzSalir = document.getElementById('cruzSalirModal');
let btnConfirmar = document.getElementById('botonConfirmar');
let btnCancelar = document.getElementById('botonCancelar');
let inputModalPostalCode = document.getElementById('inputPostalCode');
let modifyLocation = document.getElementById('modificarCiudad');
let modalRoot = document.getElementById('modal-root');


function tryLocation() {
    let postalCodeLS = JSON.parse(localStorage.getItem("location"));
    if (postalCodeLS == null || postalCodeLS == "") {
        myModal.style.display = "block";
        $("#myModal").modal();
        GestionarModal();
    }
}
modifyLocation.addEventListener('click', GestionarModal);

function GestionarModal() {
    btnConfirmar.disabled = true;
    inputModalPostalCode.addEventListener('input', function () {
        this.autofocus;
        let postalCode = this.value;
        if (postalCode.length === 5) {
            localStorage.setItem("location", JSON.stringify(postalCode));
            btnConfirmar.disabled = false;
        } else {
            btnConfirmar.disabled = true;
        }
    })
    btnCancelar.addEventListener('click', CancelarModal);
    cruzSalir.addEventListener('click', CancelarModal);
    modalRoot.addEventListener('click', CancelarModal);
    function CancelarModal() {
        localStorage.setItem("location", JSON.stringify(locationPC.innerText));
    }
}

function ponerBonitasCards() {
    let descripciones = Array.from(document.getElementsByClassName('item-box-blog-text'));
    for (let i = 10; i < descripciones.length; i++) {
        if (descripciones[i].innerText.length > 100) {
            let original = descripciones[i].innerText;
            let acotado = original.substring(0, 100);
            console.log(acotado);
            let nuevo = acotado + '...';
            descripciones[i].innerHTML = nuevo;
        }
    }
}




