import { xhr } from "./XHR.js";

export  function crearTabla(idTabla = null, url = null, redirect = null, ...parametros ) {
    return new Promise((resolve, reject) => {

        if (idTabla != null && url != null && redirect != null && parametros.length != 0) {
            if (typeof idTabla == "string" && typeof url == "string" && typeof redirect == "string" &&
                typeof parametros == "object") {
                resolve([idTabla, url, redirect, parametros]);
            }
            reject("Parametros no corresponde a los tipos esperados")
        }
        reject("Debe completar los parametros")
    })

}
export  function Parametros(data = null, name = null, autoWidth = false, callback = () => null) {
    this.data = data || null;
    this.name = name || "";
    this.autoWidth = autoWidth || false;
    this.render = callback(data) || null;

}
export  function rowSelect(tbody, table, modal) {

    $(tbody).on("click", ".show", function () {
        let data = table.row($(this).parents("tr")).data();
        $(modal).modal();
        for (let i in data) {            
            $("#" + i).val(data[i])
        }
    });
}
export  function rowSelectEdit(tbody, table, url) {
    $(tbody).on("click", ".edit", function () {
        let data = table.row($(this).parents("tr")).data();
        location.href = url + "Editar/?id=" + Object.values(data)[0];
    });
}

//Cambiar rowSelectDelete
export function rowSelectDelete(tbody, table, url, modal, form) {
    $(tbody).on("click", ".delete", function () {
        let data = table.row($(this).parents("tr")).data();
        $(modal).modal();
        form.elements[0].value = Object.values(data)[0];
    });

}

