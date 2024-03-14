
const passwordValidator = (password) => {
    return /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,25}$/.test(password)
}

const compareValidator = (value, compareValue) => {
    if (value === compareValue)
        return true

    return false
}

const formPasswordValidator = (e) => {
    formErrorHandler(e, passwordValidator(e.target.value))

    if (e.target.name == "Security.Form.CurrentPassword") {
        formFields.currentPassword = passwordValidator(e.target.value)
    }
    else {
        formFields.newPassword = passwordValidator(e.target.value)
    }
    validateForm();
}

const passwordConfirmValidator = (e, compareValue) => {
    formErrorHandler(e, compareValidator(e.target.value, compareValue))

    formFields.passwordConfirm = compareValidator(e.target.value, compareValue)
    validateForm();
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

        console.log(e.target.dataset.valRequired)
    }
}

let inputs = document.querySelectorAll('input')

let formFields = {
    currentPassword: false,
    newPassword: false,
    passwordConfirm: false,
};

function validateForm() {
    let fieldValues = []
    const submitBtn = document.querySelector('#securitySubmitBtn')

    for (let fieldKey in formFields) {
        let fieldValue = formFields[fieldKey];
        fieldValues.push(fieldValue);
    }

    if (fieldValues.includes(false)) {
        submitBtn.disabled = true
        submitBtn.classList.add('btn-disabled')
        submitBtn.classList.remove('btn-theme-s')
        return false
    } else {
        submitBtn.disabled = false
        submitBtn.classList.remove('btn-disabled')
        submitBtn.classList.add('btn-theme-s')
        return true
    }
}

/*
Validate the form on page load
*/
document.addEventListener("DOMContentLoaded", () => {
    validateForm();
})

/* 
Adding event-listeners for all inputs on the site
*/
inputs.forEach(input => {
    if (input.dataset.val === 'true') {
        input.addEventListener('keyup', (e) => {
            if (e.target.name === "Security.Form.PasswordConfirm") {
                let compareValue = document.querySelector('#newPassword').value
                if (compareValue.length !== 0) {
                    passwordConfirmValidator(e, compareValue)
                }
                else {
                    formPasswordValidator(e)
                }
            }
            else {
                formPasswordValidator(e)
            }
        })
    }
})