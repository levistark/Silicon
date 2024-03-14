const addressLengthValidator = (value, minLenght, maxLength) => {
    if (value.length >= minLenght && value.length <= maxLength)
        return true

    return false
}

const postalCodeValidator = (postalcode) => {
    return /^[\w\s]{6}$/.test(postalcode)
}

const addressFormErrorHandler = (e, validationResult) => {
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
let addressFormFields = {
    addressLine1: true,
    addressLine2: true,
    postalCode: true,
    city: true,
};


const addressFormTextValidator = (e) => {

    switch (e.target.name) {
        case "AccountDetails.AddressForm.AddressLine1":
            addressFormFields.addressLine1 = addressLengthValidator(e.target.value, 1, 50)
            addressFormErrorHandler(e, addressLengthValidator(e.target.value, 1, 50))
            break;

        case "AccountDetails.AddressForm.AddressLine2":
            addressFormFields.addressLine2 = addressLengthValidator(e.target.value, 1, 50)
            addressFormErrorHandler(e, addressLengthValidator(e.target.value, 1, 50))
            break;

        case "AccountDetails.AddressForm.City":
            addressFormFields.City = addressLengthValidator(e.target.value, 2, 50)
            addressFormErrorHandler(e, addressLengthValidator(e.target.value, 2, 50))
            break;
    }

    validateAddressForm();
}

const addressFormPostalCodeValidator = (e) => {
    addressFormErrorHandler(e, postalCodeValidator(e.target.value))

    addressFormFields.postalCode = postalCodeValidator(e.target.value)
    validateAddressForm();
}

function validateAddressForm() {
    let fieldValues = []

    const submitBtn = document.querySelector('#addressInfoSubmitBtn')

    for (let fieldKey in addressFormFields) {
        let fieldValue = addressFormFields[fieldKey];
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
                addressFormPostalCodeValidator(e)
            }
            else {
                addressFormTextValidator(e)
            }
        })
    }
})

/*
Validate the form on page load
*/
document.addEventListener("DOMContentLoaded", () => {
    validateAddressForm();
})
