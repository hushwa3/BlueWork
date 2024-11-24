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

        if (roleInput) {
            roleInput.value = selectedRole;
        }

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

    // Role selection logic
    optionCards.forEach(card => {
        card.addEventListener('click', () => {
            optionCards.forEach(c => c.classList.remove('selected'));
            card.classList.add('selected');
            roleInput.value = card.dataset.type;
            console.log(`Role selected: ${roleInput.value}`);
        });
    });

    // Form submission
    if (form) {
        form.addEventListener('submit', function (e) {
            e.preventDefault();

            if (!roleInput.value) {
                alert('Please select a role.');
                return;
            }

            const formData = new FormData(form);
            console.log('Form data:', Object.fromEntries(formData));

            fetch(form.action, { method: 'POST', body: formData })
                .then(response => {
                    if (!response.ok) {
                        console.error('Response not OK:', response);
                        return response.text().then(text => { throw new Error(text); });
                    }
                    return response.json();
                })
                .then(data => {
                    console.log('Server response:', data);
                    if (data.success) {
                        alert(data.message || 'Registration successful!');
                        location.reload();
                    } else {
                        console.error('Server-side error:', data);
                        alert(data.message || 'An error occurred during registration.');
                    }
                })
                .catch(error => {
                    console.error('Fetch error:', error);
                    alert(`An error occurred: ${error.message}`);
                });
        });
    } else {
        console.error('Form not found.');
    }
});


document.addEventListener('DOMContentLoaded', function () {
    const loginForm = document.getElementById('loginForm');

    if (loginForm) {
        loginForm.addEventListener('submit', function (e) {
            e.preventDefault(); // Prevent the default form submission behavior

            // Create a FormData object for the form
            const formData = new FormData(loginForm);

            // Log form data (debugging purpose)
            console.log('Form data:', Object.fromEntries(formData));

            // Fetch API to handle login
            fetch(loginForm.action, {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                }
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
                    return response.json();
                })
                
                .catch(error => {
                    console.error('Error during login:', error);
                    alert('An internal error occurred while processing your login.');
                });
        });
    } else {
        console.error('Login form not found.');
    }
});






