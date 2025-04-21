


$(document).ready(function () {
    $("#lblMenu").text("Datos personales");
    $("#secDataMedic").hide();
    //OBTENER Almacenes
    jQuery.ajax({
        /*url: $.MisUrls.url._ObtenerUsuarios +"id=" + $("#txbId").val(),*/
        url: '/Users/obtenerUsuario/' + $("#txbId").val() ,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var datos = data.resultado;
            if (datos != null) {
                
                //var dateObject = new Date(Date(datos[0].FechaNac));

                //var day = ("0" + dateObject.getDate()).slice(-2);
                //var month = ("0" + (dateObject.getMonth() +1)).slice(-2);
                //var year = dateObject.getUTCFullYear();

                $("#txbNombre").val(datos[0].Nombres);
                $("#txbApellidoP").val(datos[0].ApellidoP);
                $("#txbApellidoM").val(datos[0].ApellidoM);
                $("#txbfechaNac").val(datos[0].FechaNac);

            }

        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {
            //$("#cboAlmacen").LoadingOverlay("show");
            //$("#cboAlmacenOut").LoadingOverlay("show");
        },
    });
});

function DataMedic() {
    if ($("#txbNombre").val() == "" ||
        $("#txbApellidoP").val() == "" ||
        $("#txbApellidoM").val() == "" ||
        $("#txbfechaNac").val() == "") {
        swal("Mensaje", "Favor de llenar primero los datos", "warning")
        return
    }
    $("#lblMenu").text("Datos médicos");
    $("#secPersonalData").hide();
    $("#secDataMedic").show();

    jQuery.ajax({
        /*url: $.MisUrls.url._ObtenerUsuarios +"id=" + $("#txbId").val(),*/
        url: '/Users/obtenerDatosMedicos/' + $("#txbId").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var datos = data.resultado;
            if (datos != null) {

                $("#txbSangre").val(datos[0].TipoSangre);
                $("#selAlergias").val(datos[0].Alergias);
                $("#selMedicas").val(datos[0].AlergiasMedicamento);
                $("#txbEnfermedades").val(datos[0].Enfermedades);
                $("#selCirugias").val(datos[0].Cirugias);
                $("#selTomaMed").val(datos[0].TomaMedicamento);
                $("#txbMedicamento").val(datos[0].Medicamentos);

            }

        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {
            //$("#cboAlmacen").LoadingOverlay("show");
            //$("#cboAlmacenOut").LoadingOverlay("show");
        },
    });




}