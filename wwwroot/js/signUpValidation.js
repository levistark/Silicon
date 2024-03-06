
const lengthValidator = (value, minLenght, maxLength) => {
    if (value.length >= minLenght && value.length <= maxLength)
        return true

    return false
}

const compareValidator = (value, compareValue) => {
    if (value === compareValue)
        return true

    return false
}

const checkedValidator = (element) => {
    if (element.checked)
        return true

    return false
}

const emailValidator = (email) => {
    return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)
}

const passwordValidator = (password) => {
    return /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,25}$/.test(password)
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

let forms = document.querySelectorAll('form')
let inputs = document.querySelectorAll('input')

let signUpFormFields = {
    firstName: false,
    lastName: false,
    email: false,
    password: false,
    passwordConfirm: false,
    termsAndConditions: false,
};

const signUpTextValidator = (e) => {
    formErrorHandler(e, lengthValidator(e.target.value, 2, 20))

    // Check what type of text-field it is
    if (e.target.name === "Form.FirstName") {
        signUpFormFields.firstName = lengthValidator(e.target.value, 2, 20)
        validateSignUpForm();
    }

    else {
        signUpFormFields.lastName = lengthValidator(e.target.value, 2, 20)
        validateSignUpForm();
    }
}

const signUpEmailValidator = (e) => {
    formErrorHandler(e, emailValidator(e.target.value))

    signUpFormFields.email = emailValidator(e.target.value)
    validateSignUpForm();
} 

const signUpPasswordValidator = (e) => {
    formErrorHandler(e, passwordValidator(e.target.value))

    signUpFormFields.password = passwordValidator(e.target.value)
    validateSignUpForm();
}

const signUpPasswordConfirmValidator = (e, compareValue) => {
    formErrorHandler(e, compareValidator(e.target.value, compareValue))

    signUpFormFields.passwordConfirm = compareValidator(e.target.value, compareValue)
    validateSignUpForm();
}

const checkboxFormValidator = (e) => {
    formErrorHandler(e, checkedValidator(e.target))
}

function validateSignUpForm() {
    let fieldValues = []
    const submitBtn = document.querySelector('#signUpFormBtn')

    for (let fieldKey in signUpFormFields) {
        let fieldValue = signUpFormFields[fieldKey];
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
Adding event-listeners for all inputs on the site
*/
inputs.forEach(input => {
    
    if (input.dataset.val === 'true') {
        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {

                checkboxFormValidator(e)

                // Checking if the checkbox is valid
                signUpFormFields.termsAndConditions = checkedValidator(e.target)

                // Checking if all the fields in the form are valid, then handling submit button state
                validateSignUpForm();

                
            })
        }
        else {
            input.addEventListener('keyup', (e) => {
                switch (e.target.type) {

                    case 'text':
                        signUpTextValidator(e)
                        break;

                    case 'email':
                        signUpEmailValidator(e)
                        break;

                    case 'password':

                        if (e.target.name === "Form.PasswordConfirm") {
                            let compareValue = document.querySelector('#Form_Password').value

                            if (compareValue.length !== 0) {
                                signUpPasswordConfirmValidator(e, compareValue)
                            }
                            else {
                                signUpPasswordValidator(e)
                            }
                        }
                        else {
                            signUpPasswordValidator(e)
                        }
                        break;
                }
            })
        }
    }
})



