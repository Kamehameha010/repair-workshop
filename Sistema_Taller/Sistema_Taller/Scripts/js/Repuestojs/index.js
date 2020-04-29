
import { xhr } from "../funciones/XHR.js"
import { crearTabla, rowEvent, Parametros } from "../funciones/table.js"

crearTabla("#tableInventario", "/Repuesto/Stock", "/Repuesto/",
    new Parametros("codigo", "codigo", true),
    new Parametros("descripcion", "descripcion", true),
    new Parametros("cantidad", "cantidad", true),
    new Parametros("Proveedor", "Proveedor", true)
).then(data => {
    
    data[3].push({
        "defaultContent": "<a type='button' class='edit'><span class='fa fa-edit '></span></a>\
                                                   <a type='button' class='delete '><span class='fa fa-trash '></span></a>\
                                                   <a type='button' class='show '><i class= 'fa fa-eye'></i></a>",

    });
    let table = $(data[0]).DataTable({
        "processing": true,
        "serverSide": true,
        responsive: true,
        "ajax": {
            "url": data[1],
            "type": "POST",
            "datatype": "json"
        },
        "cache": true,
        "pageLength": 10,
        "filter": true,
        "responsivePriority": 1,
        "data": null,
        "columns": data[3]


    });
    table.on('responsive-resize', function (e, datatable, columns) {
        var count = columns.reduce(function (a, b) {
            return b === false ? a + 1 : a;
        }, 0);

        console.log(count + ' column(s) are hidden');
    });

    //view
    rowEvent(data[0] + " tbody", table, ".show", x => {
        $("#visualizar").modal()
        for (let i in x.data()) {
            $("#" + i).html(x.data()[i])
        }
    })

    //edit
    rowEvent(data[0] + " tbody", table, ".edit", x => {
        location.href = data[2] + "Editar/?id=" + Object.values(x.data())[0];
    })

    //delete
    rowEvent(data[0] + " tbody", table, ".delete", x => {
        $("#Eliminar").modal()
        document.forms[0].elements[0] = Object.values(x.data())[0];
    })

    document.forms[0].addEventListener("submit", e => {
        e.preventDefault();


    })
});