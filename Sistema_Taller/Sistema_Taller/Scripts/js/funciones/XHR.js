
export const xhr = async (url, options, callback) => {
    let request = await fetch(url, options)
    if (request.ok) {
        let data = await request.json()
        await callback(data)
    } else{
        throw new Error("Error de conexion")
    }
}

export const objRequest = (obj, array) => {
    
    if (array == undefined) {
        return obj;
    }
    obj.Empresa = JSON.parse(array)
    return obj;
}
