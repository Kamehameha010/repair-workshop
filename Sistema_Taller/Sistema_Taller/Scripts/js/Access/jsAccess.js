import { xhr } from "../funciones/XHR.js"

document.forms[0].addEventListener("submit", (e) => {
    e.preventDefault();
    xhr(location.pathname, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(Object.fromEntries(new FormData(document.forms[0])))
    }).then(data => {
        if (data == "1") {
            location.replace("/Home/")
        } else {
            $(".no-valid").fadeIn("slow", 0)
        }
    }).catch(error => { $(".conn-error").fadeIn("slow", 0) })
})