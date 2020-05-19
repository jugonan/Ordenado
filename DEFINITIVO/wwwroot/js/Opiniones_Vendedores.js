document.addEventListener('DOMContentLoaded', function () {
    comenzarJS();
})

function comenzarJS() {
/* Obtengo los valores de media del vendedor y de cada uno de sus productos */
    obtenerMedias();    
};

function obtenerMedias() {
    let contenedorMediaUsuario = document.querySelector('#row-de-prueba');
    let mediaUsuario = contenedorMediaUsuario.title;
    let cantidadReviews = document.getElementsByClassName('valoracion-producto-individual');
    for (i = 0; i < cantidadReviews.length; i++) {
        rellenarEstrellas(cantidadReviews[i]);
    }
}

function rellenarEstrellas(review) {
    let valoracion = review.title;
    let estrellas = review.children;
    for (j = 0; j < valoracion; j++) {
        estrellas[j].classList.add('checked');
    }
};