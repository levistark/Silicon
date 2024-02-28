
/* 
This function handles the load of the user's last theme setting.
It uses localStorage to load the last saved color class (lastBodyClass) from the body-tag
*/
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

/*
This function toggles between dark and light mode on click at the round switch button on the header.
It adds/removes a predefined class name 'dark' to the body-tags classlist. 
Then it saves the user's last setting to localStorage. 
*/
document.querySelector('#switch-mode').addEventListener('click', (e) => {
    if (document.body.classList.contains('dark')) 
        document.body.classList.remove('dark')
    else 
        document.body.classList.add('dark')
    
    localStorage.setItem('lastBodyClass', document.body.classList[0])
})

/*
This function hides/shows the the flyout mobile navigation bar.
*/
function toggleNavBar() {

    let sidebar = document.querySelector('.mobile-nav-wrapper')
    console.log(1)

    if (sidebar.classList.contains('d-none')) {
        sidebar.classList.remove('d-none')
    } else {
        sidebar.classList.add('d-none')
    }
}


