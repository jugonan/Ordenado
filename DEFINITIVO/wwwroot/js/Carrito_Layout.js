//document.addEventListener('DOMContentLoaded', iniciarJS());

//function iniciarJS() {
//    funcion();
//    opcionesProductosFN();
//    cambiarCarousel();
//    actualizarNumerosFavorito();
//}


let imagenCarrito = document.getElementById('imagen-carrito');
let carritoLleno = document.getElementById('carrito-lleno');
let carritoVacio = document.getElementById('carrito-vacio');
let divCarrito = document.getElementById('carrito');
let anclas = document.getElementsByClassName('borrar-producto');
let numeroCarrito = document.getElementById('numero-productos-carrito');
let productos = [];
let btnVaciar = document.getElementById('vaciar-carrito');
let cabezaCarrito = document.querySelector('#carrito-lleno tbody');
/* Si AUX está como false, el click es de cierre, si está como true, es de apertura
*/
let aux = true;

imagenCarrito.addEventListener('click', function () {
    aux = comprobarClick();
    if (aux) {
        productos = obtenerLocalStorage(productos);
        if (productos.length > 0) {
            carritoLleno.classList.remove('d-none');
        }
        else {
            carritoVacio.classList.remove('d-none');
        }
    }
    else {
        limpiarCarrito()
    }
    actualizarNumerosCarrito();
})

btnVaciar.addEventListener('click', function () {
    productos = [];
    localStorage.setItem('carrito', JSON.stringify(productos));
    limpiarCarrito();
})

function limpiarCarrito() {
    carritoLleno.classList.add('d-none');
    carritoVacio.classList.add('d-none');
    if (productos.length > 0) {
        vaciarCarrito()
    }
    aux = false;
}

function activarBorrado() {
    let productoEliminar;
    for (i = 0; i < anclas.length; i++) {
        anclas[i].addEventListener('click', function (e) {
            if (e.target.classList.contains('borrar-producto')) {
                productoEliminar = e.target;
                e.target.parentElement.parentElement.remove();
                eliminarLocalStorage(productoEliminar);
            }
        })
    }
}

function comprobarClick() {
    if (carritoLleno.classList.contains('d-none') && carritoVacio.classList.contains('d-none')) {
        aux = true;
    }
    else {
        aux = false;
    }
    return aux;
}

function ocultarCarrito() {
    carritoLleno.classList.add('d-none');
    carritoVacio.classList.add('d-none');
    if (productos.length > 0) {
        vaciarCarrito()
    }
}

function obtenerLocalStorage(productos) {
    productos = JSON.parse(localStorage.getItem('carrito'));
    if (productos.length !== 0 || productos !== null) {
        crearProductos(productos);
        activarBorrado();
    }
    else {
        productos = [];
    }
    return productos;
}

function crearProductos(productos) {
    for (i = 0; i < productos.length; i++) {
        const fila = document.createElement('tr');
        fila.innerHTML = `
                        <td>${productos[i].Titulo}</td>
                        <td>${productos[i].Precio}</td>
                        <td><a href="#" class="borrar-producto" id="${productos[i].Id}">X</a></td>
                        `;
        cabezaCarrito.appendChild(fila);
    }
}

function vaciarCarrito() {
    while (cabezaCarrito.firstChild) {
        cabezaCarrito.removeChild(cabezaCarrito.firstChild);
    }
}

function eliminarLocalStorage(productoEliminar) {
    productos = JSON.parse(localStorage.getItem('carrito'));
    productos.forEach(function (item, index) {
        if (item.Id === productoEliminar.id) {
            productos.splice(index, 1);
            localStorage.setItem('carrito', JSON.stringify(productos));
        }
        if (productos.length === 0) {
            limpiarCarrito();
        }
    });
}

function actualizarNumerosCarrito() {
    productos = JSON.parse(localStorage.getItem('carrito'));
    if (productos === null) {
        numeroCarrito.innerHTML = 0;
    }
    else {
        if (productos.length === 0) {
            numeroCarrito.innerHTML = 0;
        }
        else {
            numeroCarrito.innerHTML = productos.length;
        }
    }
};

/*Parte de favoritos*/
let imagenFavorito2 = document.getElementById('imagen-favorito');
let favoritoLleno = document.getElementById('favorito-lleno');
let favoritoVacio = document.getElementById('favorito-vacio');
let divFaorito = document.getElementById('favorito');
let anclas2 = document.getElementsByClassName('borrar-favorito');
let favoritos = [];
let btnVaciar2 = document.getElementById('vaciar-favorito');
let cabezaFavorito = document.querySelector('#favorito-lleno tbody');

let aux2 = true;

imagenFavorito2.addEventListener('click', function () {
    aux2 = comprobarClick2();
    if (aux2) {
        favoritos = obtenerLocalStorage2(favoritos);
        if (favoritos.length > 0) {
            favoritoLleno.classList.remove('d-none');
        }
        else {
            favoritoVacio.classList.remove('d-none');
        }
    }
    else {
        limpiarFavorito()
    }
})

btnVaciar2.addEventListener('click', function () {
    favoritos = [];
    localStorage.setItem('favorito', JSON.stringify(favoritos));
    limpiarFavorito();
})

function limpiarFavorito() {
    favoritoLleno.classList.add('d-none');
    favoritoVacio.classList.add('d-none');
    if (favoritos.length > 0) {
        vaciarFavorito()
    }
    aux2 = false;
}

function activarBorrado2() {
    let favoritoEliminar;
    for (i = 0; i < anclas2.length; i++) {
        anclas2[i].addEventListener('click', function (e) {
            if (e.target.classList.contains('borrar-favorito')) {
                favoritoEliminar = e.target;
                e.target.parentElement.parentElement.remove();
                eliminarLocalStorage2(favoritoEliminar);
            }
        })
    }
}

function comprobarClick2() {
    if (favoritoLleno.classList.contains('d-none') && favoritoVacio.classList.contains('d-none')) {
        aux2 = true;
    }
    else {
        aux2 = false;
    }
    return aux2;
}

function ocultarFavorito() {
    favoritoLleno.classList.add('d-none');
    favoritoVacio.classList.add('d-none');
    if (favoritos.length > 0) {
        vaciarFavorito()
    }
}

function obtenerLocalStorage2(favoritos) {
    favoritos = JSON.parse(localStorage.getItem('favorito'));
    if (favoritos.length !== 0 || favoritos !== null) {
        crearFavoritos(favoritos);
        activarBorrado2();
    }
    else {
        favoritos = [];
    }
    return favoritos;
}

function crearFavoritos(favoritos) {
    for (i = 0; i < favoritos.length; i++) {
        const fila2 = document.createElement('tr');
        fila2.innerHTML = `
                        <td>${favoritos[i].Titulo}</td>
                        <td>${favoritos[i].Precio}</td>
                        <td><a href="#" class="borrar-favorito" id="${favoritos[i].Id}">X</a></td>
                        `;
        cabezaFavorito.appendChild(fila2);
    }
}

function vaciarFavorito() {
    while (cabezaFavorito.firstChild) {
        cabezaFavorito.removeChild(cabezaFavorito.firstChild);
    }
}

function eliminarLocalStorage2(favoritoEliminar) {
    favoritos = JSON.parse(localStorage.getItem('favorito'));
    favoritos.forEach(function (item, index) {
        if (item.Id === favoritoEliminar.id) {
            favoritos.splice(index, 1);
            localStorage.setItem('favorito', JSON.stringify(favoritos));
        }
        if (favoritos.length === 0) {
            limpiarFavorito();
        }
    });
}