const emailValidator = (email) => {
    return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)
}

const passwordValidator = (password) => {
    return /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,25}$/.test(password)
}

const formErrorHandler = (e, validationResult) => {
    let errorElement = document.querySelector('#signInErrorBox')

    if (validationResult) {
        errorElement.classList.remove('signInErrorBox')
        errorElement.classList.add('d-none')
    }
    else {
        errorElement.classList.add('signInErrorBox')
        errorElement.classList.remove('d-none')
    }
}

let inputs = document.querySelectorAll('input')

let formFields = {
    email: false,
    password: false,
};


const formEmailValidator = (e) => {
    formErrorHandler(e, emailValidator(e.target.value))

    formFields.email = emailValidator(e.target.value)
    validateForm();
}

const formPasswordValidator = (e) => {
    formErrorHandler(e, passwordValidator(e.target.value))

    formFields.password = passwordValidator(e.target.value)
    validateForm();
}

function validateForm() {
    let fieldValues = []
    const submitBtn = document.querySelector('#signInFormBtn')

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
    document.getElementById("signInEmail").value = "";
    validateForm();
})


/* 
Adding event-listeners for all inputs on the site
*/
inputs.forEach(input => {

    if (input.dataset.val === 'true') {
        input.addEventListener('keyup', (e) => {
            switch (e.target.type) {

                case 'email':
                    formEmailValidator(e)
                    break;

                case 'password':
                    formPasswordValidator(e)
                    break;
            }
        })
    }
})



