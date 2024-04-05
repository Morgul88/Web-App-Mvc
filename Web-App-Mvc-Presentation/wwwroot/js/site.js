

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

document.addEventListener('DOMContentLoaded', function () {
    select()
    searchQuery()
})

function select() {
    try {
        let select = document.querySelector('.select');
        let selected = select.querySelector('.selected');
        let selectOption = select.querySelector('.select-options'); 

        

        selected.addEventListener('click', function () {
            selectOption.style.display = (selectOption.style.display == 'block') ? 'none' : 'block';
        });

        let options = selectOption.querySelectorAll('.option'); 
        
        options.forEach(function (option) {
            
            option.addEventListener('click', function () {
                
                selected.innerHTML = this.textContent;
                selectOption.style.display = 'none';
                let category = this.getAttribute('data-value')
                selected.setAttribute('data-value',category)
                updateCourseByFilter();
                
            })
            
        })
        
    } catch (error) {
        console.log(error);
    }
}
function searchQuery() {
    try {
        document.querySelector('#searchQuery').addEventListener('keyup', function () {
            
            updateCourseByFilter()
        })
    } catch {

    }
}
function updateCourseByFilter() {
    console.log("1")
    const category = document.querySelector('.select .selected').getAttribute('data-value') || 'all'
    console.log(category)
    const searchQuery = document.querySelector('#searchQuery').value
    console.log(searchQuery)
    console.log("Category value:", category);
    const url = `/courses?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchQuery)}`
    console.log(url)
    fetch(url) 
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.box-row').innerHTML = dom.querySelector('.box-row').innerHTML
        })

    
}