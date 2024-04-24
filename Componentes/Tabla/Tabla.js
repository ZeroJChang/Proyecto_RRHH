$(function() {
    if (sessionStorage.getItem('employeesLoaded') === null) {
      // Llenar la tabla con datos de la API solo si no se ha cargado antes
      $.ajax({
        url: 'https://localhost:7282/Planilla',
        type: 'GET',
        success: function(data) {
          // Iterar sobre los datos de los empleados y agregar filas a la tabla
          $.each(data, function(index, empleado) {
            $('#employee-table-body').append(
              '<tr>' +
              '<td>' + empleado.nombre + '</td>' +
              '<td>' + empleado.nit + '</td>' +
              '<td>' + empleado.dpi + '</td>' +
              '<td>' + empleado.salarioBase + '</td>' +
              '<td>' + empleado.bono + '</td>' +
              '<td>' + empleado.fechaInicioTrabajo + '</td>' +
              '<td>' + (empleado.fechaBaja ? empleado.fechaBaja : '') + '</td>' +
              '<td>' + empleado.direccion + '</td>' +
              '<td>' + empleado.area + '</td>' +
              '<td>' + empleado.puesto + '</td>' +
              '</tr>'
            );
          });
          // Marcar como cargados los empleados en la sesión
          sessionStorage.setItem('employeesLoaded', true);
        }
      });
    }
  });
  