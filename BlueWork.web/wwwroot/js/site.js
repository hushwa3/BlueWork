// Get all the required DOM elements
const loginBtn = document.getElementById('loginBtn');
const loginBtn1 = document.getElementById('loginBtn1');
const pop1 = document.getElementById('pop1');
const pop2 = document.getElementById('pop2');
const pop3 = document.getElementById('pop3'); // Registration form popup
const signUp = document.getElementById('sign-up');
const signIn1 = document.getElementById('sign-in1');
const register = document.getElementById('register'); // Main Register button
const register1 = document.getElementById('register1'); // Secondary Register button
const addPost = document.getElementById('add-post');
const headlineCard = document.getElementById('headline-card');
const cards = document.querySelectorAll('.option-card');
const roleInput = document.getElementById('roleInput');
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
    [pop1, pop2, pop3, headlineCard, skills, scope].forEach((popup) => {
        if (popup) popup.style.display = 'none';
    });
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

// Handle "Sign In" buttons
if (signIn1) {
    signIn1.addEventListener('click', () => {
        closeAllPopups();
        if (pop2) pop2.style.display = 'block';
        backdrop.style.display = 'block';
    });
}

// Handle "Register" button (Main button to open registration form popup)
if (register) {
    register.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups(); // Close any other popups
        if (pop3) {
            $(pop1).fadeIn(500); 
            $(pop3).fadeOut(500); // Show the registration form popup
            $(backdrop).fadeIn(500); // Show the backdrop
        }
    });
}

// Handle secondary "Register" button (for switching to the registration popup)
if (register1) {
    register1.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (pop3) {
            $(pop2).fadeOut(500); // Hide the login popup
            $(pop3).fadeIn(500); // Show the registration popup
            $(backdrop).fadeIn(500); // Show the backdrop
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

        // Get the selected role from the data-type attribute
        const selectedRole = card.dataset.type;

        // Update the hidden input field with the selected role
        if (roleInput) {
            roleInput.value = selectedRole;
        }

        // Update button visibility and text
        if (createBtn) createBtn.style.display = 'none'; // Hide the create button
        if (actionBtn) {
            actionBtn.style.display = 'block'; // Show the action button
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
            $(backdrop).fadeIn(500);
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
            $(headlineCard).fadeOut(100);
            $(skills).fadeOut(100);
            $(scope).fadeIn(500);
            $(backdrop).fadeIn(500);
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    const roleInput = document.getElementById('roleInput');
    const optionCards = document.querySelectorAll('.option-card');
    const form = document.getElementById('registrationForm');

    console.log('JavaScript loaded.');

    optionCards.forEach(card => {
        card.addEventListener('click', () => {
            optionCards.forEach(c => c.classList.remove('selected'));
            card.classList.add('selected');
            roleInput.value = card.dataset.type; // Update the hidden input
            console.log(`Role selected: ${roleInput.value}`);
        });
    });

    if (form) {
        form.addEventListener('submit', function (e) {
            e.preventDefault();

            if (!roleInput.value) {
                alert('Please select a role.');
                return;
            }

            const formData = new FormData(form);
            console.log('Form data:', Object.fromEntries(formData));

            fetch('/Account/Registration', {
                method: 'POST',
                body: formData
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(data => {
                    console.log('Server response:', data);
                    if (data.success) {
                        alert(data.message);
                        location.reload();
                    } else {
                        alert(data.message || 'An error occurred.');
                    }
                })
                .catch(error => {
                    console.error('Fetch error:', error);
                    alert('An internal error occurred.');
                });
        });
    } else {
        console.error('Form not found.');
    }
});







