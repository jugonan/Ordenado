window.addEventListener('DOMContentLoaded', function () {

    /*Escribe la información que va a meter sobre el producto en la muestra de la derecha */
    let tituloIntroducido = document.getElementById('titulo-introducido');
    tituloIntroducido.addEventListener('input', function () {
        let tituloDemo = document.getElementById('titulo-demo');
        tituloDemo.innerText = tituloIntroducido.value;
    });

    let descripcionIntroducida = document.getElementById('descripcion-introducida');
    descripcionIntroducida.addEventListener('input', function () {
        let descripcionDemo = document.getElementById('descripcion-demo');
        descripcionDemo.innerText = descripcionIntroducida.value;
    });

    let fechaIntroducida = document.getElementById('fecha-introducida');
    fechaIntroducida.addEventListener('input', function () {
        let fechaDemo = document.getElementById('fecha-demo');
        fechaDemo.innerText = fechaIntroducida.value;
    });

    let imagenIntroducida = document.getElementById('imagen-introducida');
    imagenIntroducida.addEventListener('input', function () {
        let imagenDemo = document.getElementById('imagen-demo');
        imagenDemo.src = imagenIntroducida.value;
    });


    let precioInicialIntroducido = document.getElementById('precioInicial-introducido');
    precioInicialIntroducido.addEventListener('input', function () {
        let precioInicialDemo = document.getElementById('precioInicial-demo');
        precioInicialDemo.innerText = precioInicialIntroducido.value;
    });

    let precioFinalIntroducido = document.getElementById('precioFinal-introducido');
    precioFinalIntroducido.addEventListener('input', function () {
        let precioFinalDemo = document.getElementById('precioFinal-demo');
        precioFinalDemo.innerText = precioFinalIntroducido.value;

        let descuento = document.getElementById('descuento-demo');
        let numero = Math.ceil(100 - ((100 * precioFinalIntroducido.value) / precioInicialIntroducido.value));
        descuento.innerText = (numero) + '% de descuento';
    });

})