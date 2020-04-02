import { xhr } from "./XHR.js";

const cantidad = (formElements) => {
    var count = 0;
    formElements.forEach(x => {
        if (x != "") {
            count++;
        }
    })
    return count;
}
export function crearTabla(idTabla = null, url = null, redirect = null, ...parametros ) {
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
export function Parametros(data = null, name = null, autoWidth = false, callback = () => null) {
    this.data = data || null;
    this.name = name || "";
    this.autoWidth = autoWidth || false;
    this.render = callback(data) || null;

}
export function rowSelect(tbody, table, modal) {

    $(tbody).on("click", ".show", function () {
        let data = table.row($(this).parents("tr")).data();
        $(modal).modal();
        for (let i in data) {            
            $("#" + i).val(data[i])
        }
    });
}
export function rowSelectEdit(tbody, table, url) {
    $(tbody).on("click", ".edit", function () {
        let data = table.row($(this).parents("tr")).data();
        location.href = url + "Editar/?id=" + Object.values(data)[0];
    });
}
export function rowSelectDelete(tbody, table, url, modal, form) {
    $(tbody).on("click", ".delete", function () {
        let data = table.row($(this).parents("tr")).data();
        $(modal).modal();
        form.elements[0].value = Object.values(data)[0];
    });

}
export function agregarFila(formElements){
    return new Promise((resolve, reject) => {
		if(cantidad(formElements)>0){
			resolve(Object.fromEntries(formElements))
		}
		reject("Debe llenar los campo en el formulario")
	})
}
export const datosFila = async (x) => {
    
    if (sessionStorage.j == undefined) {
        sessionStorage.j = await JSON.stringify([])
    }
    let p = JSON.parse(sessionStorage.j)
    p.push(x)
    sessionStorage.j = await JSON.stringify(p)
}
export const dataTable = (table, options) => {
    $(table).DataTable(options);
}
