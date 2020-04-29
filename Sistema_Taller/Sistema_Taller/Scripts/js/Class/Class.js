
export class Caso {
    constructor(id, usuario, cliente, nCaso, estado, cdetalle) {
        this.IdCaso = id || 0;
        this.IdUsuario = usuario;
        this.IdCliente = cliente;
        this.NumeroCaso = nCaso;
        this.IdEstadoCaso = estado;
        this.CasoDetalle = typeof cdetalle == "object" ? cdetalle : undefined

    }
    set IdCa(id) {
        this.IdCaso = id;
    }
    get IdCa() {
        return this.IdCaso;
    }
    set Usuario(usuario) {
        this.IdUsuario = usuario;
    }
    get Usuario() {
        return this.IdUsuario;
    }
    set Cliente(cliente) {
        this.IdCliente = cliente;
    }
    get Cliente() {
        return this.IdCliente;
    }
    set NumCaso(numero) {
        this.NumeroCaso = numero;
    }
    get NumCaso() {
        return this.NumeroCaso;
    }
    set Estado(estado) {
        this.IdEstadoCaso = estado;
    }
    get Estado() {
        return this.IdEstadoCaso;
    }
    set ObjCasoDetalle(detalle) {
        this.CasoDetalle = typeof cdetalle == "object" ? cdetalle : null
    }
    get ObjCasoDetalle() {
        return this.CasoDetalle;
    }

}
export class CasoDetalle extends Caso {

    set IdCd(id) {
        this.IdCasoDetalle = id;
    }
    get IdCd() {
        return this.IdCasoDetalle;
    }
    set Observacion(detalle) {
        this.Detalle = detalle == "" ? 'null' : detalle;
    }
    get Observacion() {
        return this.Detalle;
    }
    set Solucion(diagnostico) {
        this.Diagnostico = diagnostico == "" ? 'null' : diagnostico;;
    }
    get Solucion() {
        return this.Diagnostico;
    }
    set Id_Articulo(idArticulo) {
        this.IdArticulo = idArticulo;
    }
    get Id_Articulo() {
        return this.IdArticulo;
    }
    set ObjArticulo(articulo) {
        this.Articulo = (typeof articulo === "object" ? articulo : null);
    }
    get ObjArticulo() {
        return this.Articulo;
    }

}
export class Articulo {
    constructor(id, nombre, codigo, modelo, idMarca, idCategoria, serie) {
        this.IdArticulo = id || 0;
        this.Nombre = nombre || "";
        this.Codigo = codigo || "";
        this.Modelo = modelo || "";
        this.IdMarca = idMarca || 0;
        this.IdCategoria = idCategoria || 0;
        this.Serie = serie || "";
    }
    set Id(id) {
        this.IdArticulo = id;
    }
    get Id() {
        return this.IdArticulo;
    }
    set Nombre_(nombre) {
        this.Nombre = nombre || "";
    }
    get Nombre_() {
        return this.Nombre;
    }
    set Codigo_(codigo) {
        this.Codigo = codigo == "" ? 'null' : codigo;
    }
    get Codigo_() {
        return this.Codigo;
    }
    set Modelo_(modelo) {
        this.Modelo = modelo == "" ? 'null' : modelo;
    }
    get Modelo_() {
        return this.Modelo;
    }
    set Marca(idMarca) {
        this.IdMarca = idMarca;
    }
    get Marca() {
        return this.IdMarca;
    }
    set Categoria(idCategoria) {
        this.IdCategoria = idCategoria;
    }
    get Categoria() {
        return this.IdCategoria;
    }
    set Serie_(serie) {
        this.Serie = serie == "" ? 'null' : serie;;
    }
    get Serie_() {
        return this.Serie;
    }
}