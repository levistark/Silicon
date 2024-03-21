let checkbox = false

document.getElementById("delete-checkbox").addEventListener('change', (e) => {
    checkboxFormValidator(e)
})

const checkboxFormValidator = (e) => {
    deleteErrorHandler(e, checkedValidator(e.target))
    checkbox = checkedValidator(e.target)
    handleDeleteBtn();
}

const checkedValidator = (element) => {
    if (element.checked)
        return true

    return false
}

const deleteErrorHandler = (e, validationResult) => {
    let spanElement = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)
    
    if (validationResult) {
        e.target.classList.remove('input-validation-error')
        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid')
    }
    else {
        e.target.classList.add('input-validation-error')
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid')
    }
}

function handleDeleteBtn() {
    const submitBtn = document.querySelector('#deleteAccountSubmitBtn')

    if (checkbox === false) {
        submitBtn.disabled = true
        submitBtn.classList.add('btn-disabled')
        submitBtn.classList.remove('btn-danger')
    } else {
        submitBtn.disabled = false
        submitBtn.classList.remove('btn-disabled')
        submitBtn.classList.add('btn-danger')
    }

    console.log(checkbox)
}



