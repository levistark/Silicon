
document.addEventListener('DOMContentLoaded', () => {
    if (localStorage.getItem('lastBodyClass') === 'dark') {
        document.body.classList.add('dark')
        document.querySelector('#switch-mode').checked = true;
    }
    else {
        document.body.classList.remove('dark')
        document.querySelector('#switch-mode').checked = false;
    }
})

document.querySelector('#switch-mode').addEventListener('click', (e) => {
    if (document.body.classList.contains('dark')) 
        document.body.classList.remove('dark')
    else 
        document.body.classList.add('dark')
    
    localStorage.setItem('lastBodyClass', document.body.classList[0])
})


