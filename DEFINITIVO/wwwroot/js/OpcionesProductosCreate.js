/* Variables que muestro con la información del Model */
let condiciones;
function opcionesProductosFN() {
    funcion2();

    let reserva = document.getElementById('ulReserva');
    let entrega = document.getElementById('ulEntrega');
    let horario = document.getElementById('ulHorario');
    let recogida = document.getElementById('ulRecogida');
    let otros = document.getElementById('ulOtros');
    let imagenes = document.getElementsByClassName('carousel-item');
    let thumbnails = document.getElementsByClassName('img-thumbnail');
    condiciones = document.getElementById('condicionesModal').value;
    console.log(condiciones);
}
/*
<p id="condicionesModal" class="d-none" value="@Model.Condiciones"></p>
    <p id="descripcionModal" class="d-none" value="@Model.Descripcion"></p>
    <p id="fechaModal" class="d-none" value="@Model.FechaValidez"></p>
    <p id="tituloModal" class="d-none" value="@Model.Titulo"></p>

*/