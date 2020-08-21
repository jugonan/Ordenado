/*Parte de cálculo del % de descuento */
//let imagenesCarousel;
//let imagenesThumbNail;
//let numeroImagen;

//let precioInicial = document.getElementById('precio-inicial').innerText;
//let precioFinal = document.getElementById('precio-final').innerText;
//let resultado = document.getElementById('resultado-descuento');
//let numero = Math.ceil(100 - ((100 * precioFinal) / precioInicial))
//resultado.innerText = (numero) + ('% de descuento');

///* Creo un objeto tipo producto que después almacenaré en el LocalStorage
// * Al ser un objeto, desde el carrito ya podemos leer toda la información
// * necesaria para despúes procesar la transacción*/

//var Producto = {
//    Titulo: document.getElementById('activeProductTitle').innerText,
//    Precio: precioFinal,
//    Id: document.getElementById('boton-agregar-carrito').name
//};
//let productosBuy = [];
//let productosFav = [];
//productosBuy = actualizarCarrito(productosBuy);
//productosFav = actualizarFavorito(productosFav);
//let botonCarrito = document.getElementById('boton-agregar-carrito');
//let botonFavorito = document.getElementById('boton-agregar-favorito');
//let imagenFavorito = document.getElementById('imagen-favorito');

//botonCarrito.addEventListener('click', function () {
//    if (!comprobarCarrito(Producto)) {
//        addCarrito(Producto)
//    }
//})
//botonFavorito.addEventListener('click', function () {
//    if (comprobarFavorito(Producto)) {
//        removeFavorito()
//    }
//    else {
//        addFavorito(Producto)
//    }
//})

//function comprobarCarrito(Producto) {
//    if (productosBuy !== null) {
//        if (productosBuy.length > 0) {
//            for (i = 0; i < productosBuy.length; i++) {
//                if (productosBuy[i].Id === Producto.Id) {
//                    return true;
//                }
//            }
//        }
//        else {
//            return false;
//        }
//    }
//    else {
//        return false;
//    }
//};
//function comprobarFavorito(Producto) {
//    if (productosFav !== null) {
//        if (productosFav.length > 0) {
//            for (i = 0; i < productosFav.length; i++) {
//                if (productosFav[i].Id === Producto.Id) {
//                    return true;
//                }
//            }
//        }
//        else {
//            return false;
//        }
//    }
//    else {
//        return false;
//    }
//};
//function addCarrito(Producto) {
//    productosBuy.push(Producto);
//    localStorage.setItem('carrito', JSON.stringify(productosBuy));
//    actualizarCarrito(productosBuy);
//}
//function removeFavorito() {
//    let longitud = productosFav.length
//    for (i = 0; i < longitud; i++) {
//        if (productosFav[i].Id === Producto.Id) {
//            productosFav.splice(i, 1);
//        }
//    }
//    localStorage.setItem('favorito', JSON.stringify(productosFav));
//    botonFavorito.innerText = "Añadir a favoritos";
//    botonFavorito.style.backgroundColor = "white";
//    botonFavorito.style.color = "rgb(178, 73, 80)";
//}
//function addFavorito(Producto) {
//    productosFav.push(Producto);
//    localStorage.setItem('favorito', JSON.stringify(productosFav));
//    actualizarFavorito(productosFav);
//    botonFavorito.innerText = "Quitar de favoritos";
//    botonFavorito.style.backgroundColor = "rgb(178, 73, 80)";
//    botonFavorito.style.color = "white";
//}
//function actualizarCarrito(productosBuy) {
//    productosBuy = JSON.parse(localStorage.getItem("carrito"));
//    if (productosBuy === null) {
//        productosBuy = [];
//    }
//    return productosBuy;
//}
//function actualizarFavorito(productosFav) {
//    productosFav = JSON.parse(localStorage.getItem("favorito"));
//    if (productosFav === null) {
//        productosFav = [];
//    }
//    return productosFav;
//}

///*Parte del Modal de Reviews*/

//let estrellasModal = document.getElementById('div-estrellas-modal').children;
//let auxiliar = 0;
//for (i = estrellasModal.length; i > 0; i--) {
//    estrellasModal[auxiliar].value = (i);
//    estrellasModal[auxiliar].addEventListener('click', function () {
//        let valoracion = document.getElementById('incluir-valoracion');
//        valoracion.value = event.currentTarget.value;
//        console.log(valoracion);
//    });
//    auxiliar += 1;
//};

///* Rellenar las estrellas de los comentarios*/
//window.addEventListener('DOMContentLoaded', function () {
//    let divEstrellas = document.getElementsByClassName('stars-comentarios');
//    for (j = 0; j < divEstrellas.length; j++) {
//        let estrellasComentarios = divEstrellas[j].children;
//        for (i = 0; i < divEstrellas[j].title; i++) {
//            estrellasComentarios[i].classList.add('checked');
//        }
//    }
//    let divMedia = document.getElementsByClassName('stars-fijas');
//    for (j = 0; j < divMedia.length; j++) {
//        let estrellasComentarios = divMedia[j].children;
//        for (i = 0; i < divMedia[j].title; i++) {
//            estrellasComentarios[i].classList.add('checked');
//        }
//    }
//});

///* Parte para activar el carousel en base al click de las imágenes en thumbnail*/

//function cambiarCarousel() {
//    imagenesCarousel = document.getElementsByClassName('carousel-item');
//    let thumbNails = document.querySelector('#div-de-thumbNails').children;
//    for (i = 0; i < thumbNails.length; i++) {
//        let thumbNail = thumbNails[i];
//        thumbNail.addEventListener('click', function (event) {
//            numeroImagen = event.target.title - 1;
//            let activo = document.getElementsByClassName('active');
//            activo[0].classList.remove('active');
//            imagenesCarousel[numeroImagen].classList.toggle('active');
//        })
//    }

//}

let imagenes;
window.addEventListener('DOMContentLoaded', function () {
    imagenes = document.getElementsByClassName('img-thumbnail-carousel');
    for (let i = 0; i < imagenes.length; i++) {
        imagenes[i].addEventListener('error', function () {
            imagenes[i].classList.add('d-none');
        });
    }
})