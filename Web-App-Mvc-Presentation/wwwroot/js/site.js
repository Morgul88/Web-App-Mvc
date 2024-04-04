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
function selectElement() {
    let element = document.querySelector('.select');
    console.log(element); // Loggar det valda elementet till konsolen
    // Gör något med det valda elementet här
    console.log("1")
}

// Anropa funktionen när dokumentet har laddats
document.addEventListener('DOMContentLoaded', function () {
    selectElement();
});

document.addEventListener('DOMContentLoaded', function () {
    select()
})

function select() {
    try {
        let select = document.querySelector('.select');
        let selected = select.querySelector('.selected');
        let selectOption = select.querySelector('.select-options'); 

        console.log("Select element:", select);
        console.log("Selected element:", selected);
        console.log("Select options element:", selectOption);

        selected.addEventListener('click', function () {
            selectOption.style.display = (selectOption.style.display == 'block') ? 'none' : 'block';
        });

        let options = selectOption.querySelectorAll('.option'); 
        console.log(options)
        options.forEach(function (option) {
            console.log("Adding click listener to option:", option);
            option.addEventListener('click', function () {
                console.log("3")
                selected.innerHTML = this.textContent;
                selectOption.style.display = 'none';
                let category = this.getAttribute('data-value')
                selected.setAttribute('data-value',category)
                updateCourseByFilter();
                console.log("2")
            })
            console.log("5")
        })
        console.log("5")
    } catch (error) {
        console.log(error);
    }
}

function updateCourseByFilter() {
    console.log("1")
    const category = document.querySelector('.select .selected').getAttribute('data-value') || 'all'
    console.log("Category value:", category);
    const url = `/courses?category=${encodeURIComponent(category)}`
    console.log(url)
    fetch(url) 
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.box-row').innerHTML = dom.querySelector('.box-row').innerHTML
        })

    
}