//const switchBackground = () => {
//    var wrapper = document.getElementById("second-wrapper");
//    wrapper.classList.toggle("dark-background");

//}

document.addEventListener('DOMContentLoaded', function() {
    let sw = document.querySelector('#switch-mode')

    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/SiteSettings/changetheme?theme=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
                else {
                    console.log("Någonting gick fel")
                }
            })
    })
})