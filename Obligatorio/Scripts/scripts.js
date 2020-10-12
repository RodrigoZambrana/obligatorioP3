function inicio() {
    $("#registrar").click(verificarPasswords);
}

function verificarPasswords() {

    document.getElementById("error").classList.add("ocultar");
    document.getElementById("ok").classList.add("ocultar");

    // Ontenemos los valores de los campos de contraseñas 
    pass1 = document.getElementById('pass1');
    pass2 = document.getElementById('pass2');



    // Verificamos si las constraseñas no coinciden 
    if (pass1.value != pass2.value) {

        // Si las constraseñas no coinciden mostramos un mensaje 
        document.getElementById("ok").classList.add("ocultar");
        document.getElementById("error").classList.add("mostrar");

        // Desabilitamos el botón de login 
        document.getElementById("registrarse").disabled = true;

        return false;
    } else {

        // Si las contraseñas coinciden ocultamos el mensaje de error
        document.getElementById("error").classList.remove("mostrar");
        // Mostramos un mensaje mencionando que las Contraseñas coinciden 
        document.getElementById("ok").classList.remove("ocultar");

        //Habilitamos el boton de login
        document.getElementById("registrarse").disabled = false;

        //// Refrescamos la página (Simulación de envío del formulario) 
        //setTimeout(function () {
        //    location.reload();
        //}, 3000);

        return true;
    }

}

    function tipoProyecto() {
        var mivalor = document.getElementById('tipoProyecto').value;

        if (mivalor == "Cooperativo") {

            document.getElementById('divExperiencia').hide();;
            document.getElementById('divCantIntegrantes').show();

        }
        if (mivalor == "Personal") {
            document.getElementById('divExperiencia').show();;
            document.getElementById('divCantIntegrantes').hide();

        }
        
    }
