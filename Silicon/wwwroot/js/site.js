/* 
This function handles the load of the user's last theme setting.
It uses localStorage to load the last saved color class (lastBodyClass) from the body-tag
*/
document.addEventListener('DOMContentLoaded', () => {
    if (localStorage.getItem('lastBodyClass') === 'dark') {
        htmlElement.setAttribute('data-bs-theme', 'dark');
        document.body.classList.add('dark')
        document.querySelector('#switch-mode').checked = true;
        document.querySelector('#mobile-switch-mode').checked = true;
    }
    else {
        htmlElement.setAttribute('data-bs-theme', 'light');
        document.body.classList.remove('dark')
        document.querySelector('#switch-mode').checked = false;
        document.querySelector('#mobile-switch-mode').checked = false;
    }

    if (sessionStorage.getItem('lastScrollPosition') !== null) {
        window.scrollTo({ top: sessionStorage.getItem('lastScrollPosition'), behavior: "instant" })
        sessionStorage.removeItem('lastScrollPosition');
    } 
})


/*
This function toggles between dark and light mode on click at the round switch button on the header.
It adds/removes a predefined class name 'dark' to the body-tags classlist. 
Then it saves the user's last setting to localStorage. 
*/
document.querySelector('#switch-mode').addEventListener('click', (e) => {
    if (document.body.classList.contains('dark')) {
        document.body.classList.remove('dark')
        htmlElement.setAttribute('data-bs-theme', 'light');
        document.querySelector('#switch-mode').checked = false;
        document.querySelector('#mobile-switch-mode').checked = false;

    }
    else {
        htmlElement.setAttribute('data-bs-theme', 'dark');
        document.body.classList.add('dark')
        document.querySelector('#switch-mode').checked = true;
        document.querySelector('#mobile-switch-mode').checked = true;
    }
    
    localStorage.setItem('lastBodyClass', document.body.classList[0])
})

// Select the HTML element
var htmlElement = document.querySelector('html');

// Get the value of the 'bs-data-theme' attribute
var theme = htmlElement.getAttribute('bs-data-theme');

document.querySelector('#mobile-switch-mode').addEventListener('click', (e) => {
    if (document.body.classList.contains('dark')) {
        document.body.classList.remove('dark')
        htmlElement.setAttribute('data-bs-theme', 'light');
        document.querySelector('#switch-mode').checked = false;
        document.querySelector('#mobile-switch-mode').checked = false;

    }
    else {
        document.body.classList.add('dark')
        htmlElement.setAttribute('data-bs-theme', 'dark');
        document.querySelector('#switch-mode').checked = true;
        document.querySelector('#mobile-switch-mode').checked = true;
    }

    localStorage.setItem('lastBodyClass', document.body.classList[0])
})

/*
This function hides/shows the the flyout mobile navigation bar on click.
*/
async function toggleNavBar() {

    let mobileNav = document.querySelector('.mobile-nav-wrapper')
    let backDrop = document.querySelector('.mobile-nav-backdrop')
    let sideBar = document.querySelector('.mobile-nav')

    if (mobileNav.classList.contains('hidenav') || mobileNav.classList.contains('d-none')) {
        await mobileNav.classList.add('fadeIn')
        await sideBar.classList.remove('flyOutAnimation')
        await mobileNav.classList.remove('d-none')
        await mobileNav.classList.remove('hidenav')
        await sideBar.classList.add('flyInAnimation')

    } else {
        await mobileNav.classList.remove('fadeIn')
        await mobileNav.classList.add('hidenav')
        await sideBar.classList.add('flyOutAnimation')
        await sideBar.classList.remove('flyInAnimation')
    }
}



