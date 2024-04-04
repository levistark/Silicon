const subscribeForm = document.getElementById("subscribeForm");

subscribeForm.addEventListener('submit', (e) => {
    sessionStorage.setItem('lastScrollPosition', window.scrollY)
})

document.getElementById("homeSignUpEmail").addEventListener('keyup', (e) => {
    formValidator(e);
})

const emailValidator = (email) => {
    return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)
}

const formValidator = (e) => {
    formErrorHandler(e, emailValidator(e.target.value))

    validateForm(emailValidator(e.target.value));
}

const formErrorHandler = (e, validationResult) => {
    let spanElement = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)

    if (validationResult) {
        e.target.classList.remove('input-validation-error')
        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid')
        spanElement.innerHTML = ''
    }
    else {
        e.target.classList.add('input-validation-error')
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid')
        spanElement.innerHTML = e.target.dataset.valRequired
    }
}
const submitBtn = document.querySelector('#homeSignUpFormBtn')

function validateForm(isValid) {

    if (isValid) {
        submitBtn.disabled = false
        submitBtn.classList.remove('btn-disabled')
        submitBtn.classList.add('btn-theme')
        return true
    } else {
        submitBtn.disabled = true
        submitBtn.classList.add('btn-disabled')
        submitBtn.classList.remove('btn-theme')
        return false
    }
}

