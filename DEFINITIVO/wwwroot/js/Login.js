window.addEventListener('DOMContentLoaded', function () {
    let accesoExterno = document.getElementById('parrafo-superior-el').children;
    for (i = 0; i < accesoExterno.length; i++) {
        switch (accesoExterno[i].value) {
            case 'Google':
                accesoExterno[i].classList.remove('btn-primary');
                accesoExterno[i].classList.add('btn-warning');
                break;

            case 'Facebook':
                accesoExterno[i].classList.remove('btn-primary');
                accesoExterno[i].classList.add('btn-info');
                break;
        };
    };
})