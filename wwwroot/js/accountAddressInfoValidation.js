﻿const lengthValidator = (value, minLenght, maxLength) => {
    if (value.length >= minLenght && value.length <= maxLength)
        return true

    return false
}

const postalCodeValidator = (postalcode) => {
    return /^[\w\s]{6}$/.test(postalcode)
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
    addressLine1: true,
    addressLine2: true,
    postalCode: true,
    city: true,
};


const formTextValidator = (e) => {

    switch (e.target.name) {
        case "AccountDetails.AddressForm.AddressLine1":
            formFields.addressLine1 = lengthValidator(e.target.value, 1, 50)
            formErrorHandler(e, lengthValidator(e.target.value, 1, 50))
            break;

        case "AccountDetails.AddressForm.AddressLine2":
            formFields.addressLine2 = lengthValidator(e.target.value, 1, 50)
            formErrorHandler(e, lengthValidator(e.target.value, 1, 50))
            break;

        case "AccountDetails.AddressForm.City":
            formFields.City = lengthValidator(e.target.value, 2, 50)
            formErrorHandler(e, lengthValidator(e.target.value, 2, 50))
            break;
    }

    validateForm();
}

const formPostalCodeValidator = (e) => {
    formErrorHandler(e, postalCodeValidator(e.target.value))

    formFields.postalCode = postalCodeValidator(e.target.value)
    validateForm();
}

function validateForm() {
    let fieldValues = []

    const submitBtn = document.querySelector('#addressInfoSubmitBtn')

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
            if (e.target.Name == "AccountDetails.AddressForm.PostalCode") {
                formPostalCodeValidator(e)
            }
            else {
                formTextValidator(e)
            }
        })
    }
})

/*
Validate the form on page load
*/
document.addEventListener("DOMContentLoaded", () => {
    validateForm();
})
