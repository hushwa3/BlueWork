﻿const loginBtn = document.getElementById('loginBtn');
const loginBtn1 = document.getElementById('loginBtn1');
const pop1 = document.getElementById('pop1');
const pop2 = document.getElementById('pop2');
const pop3 = document.getElementById('pop3'); 
const signUp = document.getElementById('sign-up');
const signIn1 = document.getElementById('sign-in1');
const register = document.getElementById('register'); 
const register1 = document.getElementById('register1'); 
const addPost = document.getElementById('add-post');
const headlineCard = document.getElementById('headline-card');
const cards = document.querySelectorAll('.option-card');
const roleInput = document.getElementById('roleInput');
const actionButton = document.getElementById('actionButton');
const actionBtn = document.getElementById('actionBtn');
const createBtn = document.getElementById('createAccountBtn');
const nextSkills = document.getElementById('next-skills');
const nextScope = document.getElementById('next-scope');
const nextBudget = document.getElementById('next-budget');
const nextDetails = document.getElementById('next-details');
const skills = document.getElementById('skills');
const scope = document.getElementById('scope');
const details = document.getElementById('details');
const budget = document.getElementById('budget');
const laststep = document.getElementById('laststep');

// Add backdrop for popups
const backdrop = document.createElement('div');
backdrop.classList.add('backdrop');
document.body.appendChild(backdrop);

// Set initial styles for the backdrop
backdrop.style.display = 'none';
backdrop.style.position = 'fixed';
backdrop.style.top = 0;
backdrop.style.left = 0;
backdrop.style.width = '100%';
backdrop.style.height = '100%';
backdrop.style.background = 'rgba(0, 0, 0, 0.5)';
backdrop.style.zIndex = 10;

// Helper function to close all popups
const closeAllPopups = () => {
    [pop2, headlineCard, skills, scope, budget, details, laststep].forEach((popup) => {
        if (popup) popup.style.display = 'none';
    });
    backdrop.style.display = 'none';
};

// Handle click outside popups
document.addEventListener('click', (event) => {
    const isClickOutside = ![pop1, pop2, pop3, headlineCard, skills, scope, budget, details, laststep].some(
        (element) => element && element.contains(event.target)
    );

    if (isClickOutside) {
        closeAllPopups();
    }
});

// Stop propagation of clicks inside popups
[pop1, pop2, pop3, headlineCard, skills, scope, budget, details, laststep].forEach((popup) => {
    if (popup) {
        popup.addEventListener('click', (event) => {
            event.stopPropagation();
        });
    }
});

// Show pop2 when "Login" button is clicked
[loginBtn, loginBtn1].forEach((btn) => {
    if (btn) {
        btn.addEventListener('click', (event) => {
            event.stopPropagation();
            closeAllPopups();
            if (pop2) {
                $(pop3).fadeOut(500);
                $(pop2).fadeIn(500);
                $(backdrop).fadeIn(500);
            }
        });
    }
});

// Handle "Register" button (Main button to open registration form popup)
if (register) {
    register.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups(); 
        if (pop3) {
            $(pop1).fadeIn(500); 
            $(pop3).fadeOut(500); 
            $(backdrop).fadeIn(500); 
        }
    });
}

// Handle secondary "Register" button (for switching to the registration popup)
if (register1) {
    register1.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (pop3) {
            $(pop2).fadeOut(500); 
            $(pop1).fadeIn(500); 
            $(backdrop).fadeIn(500); 
        }
    });
}

cards.forEach((card) => {
    card.addEventListener('click', () => {
        cards.forEach((c) => c.classList.remove('selected'));
        card.classList.add('selected');

        const selectedRole = card.dataset.type;


        if (createBtn) createBtn.style.display = 'none'; 
        if (actionBtn) {
            actionBtn.style.display = 'block';
            actionBtn.textContent =
                selectedRole === 'client' ? 'Join as a Client' : 'Apply as a Worker';
          
        }
    });
});

// Handle actionButton click to show pop3
if (actionBtn) {
    actionBtn.addEventListener('click', (event) => {
        event.stopPropagation();
        if (pop3) {
            $(pop1).fadeOut(100);
            $(pop3).fadeIn(500);
        }
    });
}

// Show the headline card when "Add Post" button is clicked
if (addPost) {
    addPost.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (headlineCard) {
            $(headlineCard).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}

// Handle the next buttons
if (nextSkills) {
    nextSkills.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (skills) {
            $(headlineCard).fadeOut(100);
            $(skills).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}

if (nextScope) {
    nextScope.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (scope) {
            $(skills).fadeOut(100);
            $(scope).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}

if (nextBudget) {
    nextBudget.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (budget) {
            $(scope).fadeOut(100);
            $(budget).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}
if (nextDetails) {
    nextDetails.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (budget) {
            $(budget).fadeOut(100);
            $(laststep).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}






