window.addEventListener('load', ponerBonitasCards)


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
    rellenarEstrellas();
    rellenarVisitas();
    let descripciones = Array.from(document.getElementsByClassName('texto-oferta'));
    for (let i = 0; i < descripciones.length; i++) {
        if (descripciones[i].innerText.length > 40) {
            let original = descripciones[i].innerText;
            let acotado = original.substring(0, 40);
            console.log(acotado);
            let nuevo = acotado + '...';
            descripciones[i].innerHTML = nuevo;
        }
    }
}

function rellenarEstrellas() {
    let divEstrellas = Array.from(document.getElementsByClassName('media-de-producto'));
    divEstrellas.forEach(estrella => {
        let random = calcularRandom(3,5);
        let estrellas = estrella.children;
        for (i = 0; i < random; i++) {
            estrellas[i].classList.add('checked');
        }
    })
}

function rellenarVisitas() {
    let visitas = Array.from(document.getElementsByClassName('visitas-a-producto'));
    visitas.forEach(visita => {
        let random = calcularRandom(22, 89);
        let imagen = visita.innerHTML;
        visita.innerHTML = imagen + random;
    })
}

function calcularRandom(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}




