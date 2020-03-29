
export const xhr = async (url, options, callback) => {
    let request = await fetch(url, options)
    if (request.ok) {
        let data = await request.json()
        await callback(data)
    } else{
        throw new Error("Error de conexion")
    }
}
