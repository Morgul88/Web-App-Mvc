const formErrorHandler = (element, validationResult) => {
    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

    if (validationResult) {
        element.classList.remove('input-valdation-error')
        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid')
        spanElement.innerHTML = '';
    } else {
        element.classList.add('input-valdation-error')
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid')
        spanElement.innerHTML = element.dataset.valRequired;
    }

}

const compareValidator = (element, compareValue) => {
    
}

const textValidator = (element, minLength = 2) => {
    
    if (element.value.length >= minLength) {
        console.log("du är inne i if satsen")
        formErrorHandler(element, true)
    }
    else {
        formErrorHandler(element, false)
    }
    
}
  
const emailValidator = (element) => {
    const regEx = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/

    formErrorHandler(element, regEx.test(element.value))
}

const passwordValidator = (element) => {
    if (element.dataset.valEqualtoOther !== undefined) {
        
        let password = document.getElementsByName(element.dataset.valEqualtoOther.replace('*', 'Form'))[0].value
        console.log(password)
        if (element.value === password)
            formErrorHandler(element, true)
        else
            formErrorHandler(element, false)
    }
    else {
        
        const regEx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.#$!%*?&^])[A-Za-z\d@.#$!%*?&]{8,15}$/
        formErrorHandler(element, regEx.test(element.value))
    }
   
}

const checkBoxValidator = (element) => {
    if (element.checked) {
        formErrorHandler(element, true)
    } else {
        formErrorHandler(element, false)
    }
       
}

let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (element) => {
                checkBoxValidator(element.target)
            })
        }
        else {

            input.addEventListener('keyup', (element) => {

                switch (element.target.type) {

                    case 'text':
                        textValidator(element.target)
                        break;
                    case 'email':
                        emailValidator(element.target)
                        break;
                    case 'password':
                        passwordValidator(element.target)
                        break;
                }
            })
        }
    }
})