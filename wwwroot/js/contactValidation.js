const lengthValidator = (value, minLenght, maxLength) => {
    if (value.length >= minLenght && value.length <= maxLength)
        return true

    return false
}

const emailValidator = (email) => {
    return /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)
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
        if (e.target.name != "AccountDetails.BasicInfoForm.PhoneNumber" && e.target.name != "AccountDetails.BasicInfoForm.Bio") {
            e.target.classList.add('input-validation-error')
            spanElement.classList.add('field-validation-error')
            spanElement.classList.remove('field-validation-valid')
            spanElement.innerHTML = e.target.dataset.valRequired
        }
    }
}

let inputs = document.querySelectorAll('input')

let formFields = {
    firstName: true,
    lastName: true,
    email: true,
    tel: true,
    bio: true,
};



const formTextValidator = (e) => {
    formErrorHandler(e, lengthValidator(e.target.value, 2, 45))

    switch (e.target.type) {
        case "text":
            if (e.target.name == "AccountDetails.BasicInfoForm.FirstName") {
                formFields.firstName = lengthValidator(e.target.value, 2, 25)
            }
            else {
                formFields.lastName = lengthValidator(e.target.value, 2, 35)
            }
            break;
        case "tel":
            formFields.tel = lengthValidator(e.target.value, 1, 35)

            break;
        case "textarea":
            formFields.bio = lengthValidator(e.target.value, 1, 300)
            break;
    }
    validateForm();

}

const formEmailValidator = (e) => {
    formErrorHandler(e, emailValidator(e.target.value))

    formFields.email = emailValidator(e.target.value)
    validateForm();
}

function validateForm() {
    let fieldValues = []

    const submitBtn = document.querySelector('#basicInfoSubmitBtn')

    for (let fieldKey in formFields) {
        let fieldValue = formFields[fieldKey];
        fieldValues.push(fieldValue);
    }

    if (fieldValues.includes(false)) {
        submitBtn.disabled = true
        submitBtn.classList.add('btn-disabled')
        submitBtn.classList.remove('btn-theme')

        return false
    } else {
        submitBtn.disabled = false
        submitBtn.classList.remove('btn-disabled')
        submitBtn.classList.add('btn-theme')
        return true
    }

}

/* 
Adding event-listeners for all inputs on the site
*/
inputs.forEach(input => {

    if (input.dataset.val === 'true') {

        input.addEventListener('keyup', (e) => {
            if (e.target.type == "email") {
                formEmailValidator(e)
            }
            else {
                formTextValidator(e)
            }
        })
    }
})

document.getElementById("basicInfoBio").addEventListener('keyup', (e) => {
    formTextValidator(e)
})


/*
Validate the form on page load
*/
document.addEventListener("DOMContentLoaded", () => {
    validateForm();
})
