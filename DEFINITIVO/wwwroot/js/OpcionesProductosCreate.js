/* Variables que muestro con la información del Model */
function funcion(){
    let imagenes = document.getElementsByClassName('carousel-item');
    let fuente = imagenes[0].src;
    let link = fuente + "data:image/gif;base64,";
    imagenes[0].src = link;
}