
import { xhr } from "../funciones/XHR.js"
import { obtenerId, llenarForm } from "../funciones/actionScripts.js"
import { crearTabla, Parametros, rowSelect } from "../funciones/table.js"


var table;
onload = () => {
    xhr("/Cliente/BuscarCliente/?id=" + obtenerId(location.href), null,
        (data) => {
            llenarForm(document.forms[0], data)

            crearTabla("#tablaNegocio", JSON.stringify(data.Negocio), "",
                new Parametros("NombreEmpresa", "NombreEmpresa", true),
                new Parametros("CedJuridica", "CedJuridica", true),
                new Parametros("Direccion", "Direccion", true),
                new Parametros("TelEmpresa", "TelEmpresa", true)
            ).then(x => {
                x[3].push({
                    "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>\
                                                   <a type='button' class='show'><i class= 'fa fa-eye'></i></a>",
                });
                sessionStorage.j = x[1]
                table = $(x[0]).DataTable({
                    "lengthChange": false,
                    "searching": false,
                    "data": JSON.parse(x[1]),
                    "columns": x[3]
                });
                rowSelect(x[0] + " tbody", table, "#negocioModal");
            })
        })

}
