let opciones = document.getElementById('cupones-en-perfil');
let comprados = document.getElementsByClassName('seccion-comprados');
let disponibles = document.getElementsByClassName('seccion-disponibles');



opciones.addEventListener('click', function () {
    let cupones = document.getElementsByClassName('seccion-cupones');
    for (i = 0; i < cupones.length; i++) {
        if (cupones[i].classList.contains('d-none') !== true) {
            cupones[i].classList.add('d-none');
        }
    }

    let opcionSeleccionada = opciones.options[opciones.selectedIndex].value;
    switch (opcionSeleccionada) {
        case "1":
            for (i = 0; i < disponibles.length; i++) {
                disponibles[i].classList.toggle('d-none');
            }
            break;

        case "2":
            for (i = 0; i < comprados.length; i++) {
                comprados[i].classList.toggle('d-none');
            }
            break;
    }
})