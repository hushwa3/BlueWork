// Get all the required DOM elements
const loginBtn = document.getElementById('loginBtn');
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
const actionButton = document.getElementById('actionButton');
const actionBtn = document.getElementById('actionBtn');
const createBtn = document.getElementById('createAccountBtn');
const nextSkills = document.getElementById('next-skills');
const nextScope = document.getElementById('next-scope');
const skills = document.getElementById('skills');
const scope = document.getElementById('scope');

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
    if (pop1) pop1.style.display = 'none';
    if (pop2) pop2.style.display = 'none';
    if (pop3) pop3.style.display = 'none';
    if (headlineCard) headlineCard.style.display = 'none';
    if (skills) skills.style.display = 'none';
    if (scope) scope.style.display = 'none';
    backdrop.style.display = 'none';
};

// Handle click outside popups
document.addEventListener('click', (event) => {
    const isClickOutside = ![pop1, pop2, pop3, headlineCard, skills, scope].some(
        (element) => element && element.contains(event.target)
    );

    if (isClickOutside) {
        closeAllPopups();
    }
});

// Stop propagation of clicks inside popups
[pop1, pop2, pop3, headlineCard, skills, scope].forEach((popup) => {
    if (popup) {
        popup.addEventListener('click', (event) => {
            event.stopPropagation();
        });
    }
});

// Show pop2 when "Join" button is clicked
if (loginBtn) {
    $(loginBtn).on('click', function (event) {
        event.stopPropagation();
        closeAllPopups();
        if (pop2) {
            $(pop3).fadeOut(500); 
            $(pop2).fadeIn(500); 
            $(backdrop).fadeIn(500); 
        }
    });
}


if (loginBtn1) {
    $(loginBtn1).on('click', function (event) {
        event.stopPropagation();
        closeAllPopups();
        if (pop2) {
            $(pop3).fadeOut(500);
            $(pop2).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}

// Handle "Sign In" buttons
if (signIn1) {
    signIn1.addEventListener('click', () => {
        closeAllPopups();
        if (pop2) pop2.style.display = 'block';
        backdrop.style.display = 'block';
    });
}

// Handle "Register" button
if (register) {
    $(register).on('click', function (event) {
        event.stopPropagation();
        closeAllPopups();
        if (pop1) {
            $(pop1).fadeIn(500);
        }
    });
}

if (register1) {
    $(register1).on('click', function (event) {

        event.stopPropagation();
        if (pop1) {
            $(pop2).fadeOut(500);
            $(pop1).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}

// Handle card selection for account type
cards.forEach((card) => {
    card.addEventListener('click', () => {
        // Remove the 'selected' class from all cards
        cards.forEach((c) => c.classList.remove('selected'));

        // Add 'selected' class to the clicked card
        card.classList.add('selected');
        selectedType = card.dataset.type;

        // Update button visibility and text
        if (createBtn) createBtn.style.display = 'none';
        if (actionBtn) {
            actionBtn.style.display = 'block';
            actionBtn.textContent =
                selectedType === 'client' ? 'Join as a Client' : 'Apply as a Worker';
        }
    });
});

// Show the headline card when "Add Post" button is clicked
if (addPost) {
    $(addPost).on('click', function (event) {
        event.stopPropagation();
        if (headlineCard) {
            $(headlineCard).fadeIn(500);
            $(backdrop).fadeIn(500);
        } 
    });
}


// Handle the next buttons
if (nextSkills) {
    $(nextSkills).on('click', function (event) {
        event.stopPropagation();
        if (skills) {
            $(headlineCard).fadeOut(100);
            $(skills).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}


if (nextScope) {
    $(nextScope).on('click', function (event) {
        event.stopPropagation();
        if (scope) {
            $(headlineCard).fadeOut(100);
            $(skills).fadeOut(100);
            $(scope).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}


// Handle actionButton click to show pop3
if (actionBtn) {
    $(actionBtn).on('click', function (event) {
        event.stopPropagation();
        if (pop3) {
            $(pop1).fadeOut(100);
            $(pop3).fadeIn(100);
            $(backdrop).fadeIn(500);
        }
    });
}
 
