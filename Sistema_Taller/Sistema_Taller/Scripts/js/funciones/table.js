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
export function Parametros(data = null, autoWidth = false, callback = () => null) {
    this.data = data || null;
    this.name = data || null;
    this.autoWidth = autoWidth || false;
    this.render = callback(data) || null;

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
export const rowEvent = (tbody, table, identifier, callback) => {
    $(tbody).on("click", identifier, function () {
        let data = table.row($(this).parents("tr")).data();
        callback(data)
    });
}
export function dataEdit(data = null) {
    let s = JSON.parse(sessionStorage.j)
    let nd = s.filter(x => JSON.stringify(x) !== JSON.stringify(data))
    sessionStorage.j = JSON.stringify(nd)
}
