$(document).ready(function () {
    agregarFiltroTabla();

     


});

var someStringValue = '@(ViewBag.Ejemplares)';

var idMateriales = [];
var nombreMateriales = [];
var descripcionesMateriales = [];

var idEjemplaresSeleccionados = [];
var clavesEjemplaresSeleccionados = [];


for (var i = 0; i < lista.length; i++) {
    if (!idMateriales.includes(lista[i]['Material']['IdMaterial'])) {

        idMateriales.push(lista[i]['Material']['IdMaterial']);

        nombreMateriales.push(lista[i]['Material']['Nombre']);

        descripcionesMateriales.push(lista[i]['Material']['Descripcion']);

        var materialDropDown = '<li>' + lista[i]['Material']['Nombre'] + '</li>';
        $('#ddListaMateriales').append(materialDropDown);

    }
}

$("#ddListaMateriales li").click(function () {
    var nombreMaterial = $(this).html();
    var indexMaterial = $(this).index();
    $('#lbDescripcionMaterial').html(descripcionesMateriales[indexMaterial]);
    var idMaterial = idMateriales[indexMaterial];
    presentarTablaEjemplares(nombreMaterial);
    llenarTablaEjemplares(idMaterial);

});

function presentarTablaEjemplares(nombreMaterial) {

    $('#contenedorTablaEjemplares').removeAttr('hidden');
    $('#textNombreMaterial').html(nombreMaterial.toUpperCase());
}


function llenarTablaEjemplares(idMaterial) {
    var registroActual = '';

    var index = 0;

    for (var i = 0; i < lista.length; i++) {

        if (lista[i]['Material']['IdMaterial'] == idMaterial) {

            registroActual = registroActual + '<tr><td>' + lista[i]['IdEjemplar'] + '</td>';
            registroActual = registroActual + '<td>' + lista[i]['ClaveEjemplar'] + '</td>';
            registroActual = registroActual + '<td><input type="checkbox" class="form-check-input" id="chb' + index + '"></div></td></tr>';
            index++;
        }
    }
    registroActual = registroActual + '';
    $('#tblTbodyEjemplares').html(registroActual);
}

$('#btnAgregarEjmeplares').click(function () {
    llenarListaClavesEjemplares();
});

function llenarListaClavesEjemplares() {

    var idTemporales = [];
    var clavesTemporales = [];
    var clavesDuplicadas = [];
    

    $('#tblTbodyEjemplares tr').each(function (i) {
        

        if ($('#chb' + i).is(":checked")) {
            
            if (!idEjemplaresSeleccionados.includes($(this).find("td:eq(0)").html())) {
                idTemporales.push($(this).find("td:eq(0)").html());
                clavesTemporales.push($(this).find("td:eq(1)").html());
            } else {

                clavesDuplicadas.push($(this).find("td:eq(1)").html());


            }
        }

        $()
    });

    var clavesHtml = '';

    for (var i = 0; i < clavesTemporales.length; i++) {

        clavesHtml = clavesHtml + '<option>' + clavesTemporales[i] + '</option>';
    }

    idEjemplaresSeleccionados = idEjemplaresSeleccionados.concat(idTemporales);
    clavesEjemplaresSeleccionados = clavesEjemplaresSeleccionados.concat(clavesTemporales);

    $('#listaClavesEjemplares').append(clavesHtml);
    if (clavesDuplicadas.length > 0) {
        var textoClavesDuplicadas = '';
        for (var i = 0; i < clavesDuplicadas.length; i++) {
            textoClavesDuplicadas = textoClavesDuplicadas + '<h4 style="color:dodgerblue">' + clavesDuplicadas[i] + '</h4>';
        }
        mostrarMensaje('LOS SIGUIENTES EJEMPLARES YA HAN SIDO AGREGADOS A LA LISTA', textoClavesDuplicadas);
    }

    if (clavesTemporales.length > 0) {
        mostrarMensajeModificacionEjemplares(1);
    }

}

function mostrarMensaje(titulo, mensaje) {

    $('#modalMensajeTitulo').html(titulo.toUpperCase());
    $('#modalMensajeTexto').html(mensaje);
    $('#modalMensaje').modal('show');
}

function mostrarMensajeModificacionEjemplares(validacion) {


    if (validacion == 1) {

        $('#labelElementosAgregados').html('Nuevos ejemplares agregados a la lista');

    } else if (validacion == 2) {

        $('#labelElementosAgregados').html('Ejemplares eliminados');

    } else if (validacion == 3) {

        $('#labelElementosAgregados').html('');
    }

}

$('#contenedorPrincipal2').hover(function () {
    mostrarMensajeModificacionEjemplares(3);
});


$('#btnEliminarEjemplar').click(function () {

    var index = $("#listaClavesEjemplares").prop('selectedIndex');

    if (index != -1) {

        idEjemplaresSeleccionados.splice(index, 1);
        clavesEjemplaresSeleccionados.splice(index, 1);

        $('#listaClavesEjemplares').html('');
        var clavesHtml = '';

        for (var i = 0; i < clavesEjemplaresSeleccionados.length; i++) {

            clavesHtml = clavesHtml + '<option>' + clavesEjemplaresSeleccionados[i] + '</option>';
        }


        $('#listaClavesEjemplares').append(clavesHtml);

        mostrarMensajeModificacionEjemplares(2);
    }



});





function agregarFiltroTabla() {

    $("#txtFiltrarAlumno").on("keyup", function () {

        var value = $(this).val().toLowerCase();
        $("#tblTbodyAlumno tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });


    $("#txtFiltrarLaboratorista").on("keyup", function () {

        var value = $(this).val().toLowerCase();
        $("#tblTbodyLaboratorista tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#txtFiltrarEjemplar").on("keyup", function () {

        var value = $(this).val().toLowerCase();
        $("#tblTbodyEjemplares tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });


}

$('#tblAlumno tr').click(function () {

    $("#txtIdAlumno").val($(this).find("td:eq(0)").html());
    $('#modalAlumnos').modal('hide');
});

$('#tblLaboratorista tr').click(function () {

    $("#txtIdLaboratorista").val($(this).find("td:eq(0)").html());
    $("#txtIdLaboratorio").val($(this).find("td:eq(6)").html());
    $('#modalLaboratoristas').modal('hide');

});


function validarFormulario() {
    var validacion = 0;
    //0 MAL 1 BIEN 2 FECHA 3 LISTA
    var today = new Date();
    var fechaActual = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

    fechaActual = Date.parse(fechaActual);

    if (($('#txtIdLaboratorio').val() != '') &&
        $('#txtIdLaboratorista').val() != '' &&
        $('#txtIdAlumno').val() != '' &&
        $('#txtFechaLimite').val() != '') {

        var fechaLimite = $('#txtFechaLimite').val();

        fechaLimite = Date.parse(fechaLimite);

        if (idEjemplaresSeleccionados.length > 0) {


            if (fechaActual < fechaLimite) {

                validacion = 1;

            } else {
                validacion = 2;
            }

        } else {
            validacion = 3;

        }


    }

    return validacion;

}

$('#btnRealizarPréstamo').click(function () {
    var validacion = validarFormulario();

    if (validacion == 1) {
        realizarPrestamo();

    } else if (validacion == 2) {
        mostrarMensaje('FECHA NO PERMITIDA', 'La fecha de devolución debe ser mayor día de hoy.');
    } else if (validacion == 3) {
        mostrarMensaje('NO SE SELECCIONARON EJEMPLARES', 'Seleccione un material y a continuación elija los ejemplares que serán prestados.');
    } else if (validacion == 0) {
        mostrarMensaje('CAMPOS VACÍOS', 'Es necesario que todos los campos del formulario sean llenados.');
    }

});

