
addPuntitos('titulo-producto', 14);
addPuntitos('descripcion-producto', 30);

function addPuntitos(idElemento, longitud) {
    let campos = document.getElementsByClassName(idElemento);
    for (let i = 0; i < campos.length; i++) {
        if (campos[i].innerText.length > longitud) {
            let string = campos[i].innerText;
            let descripcion = string.substring(0, longitud);
            campos[i].innerText = descripcion + '...';
        }
    }
}


