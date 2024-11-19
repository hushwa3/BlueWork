// Get all the required DOM elements
const joinBtn = document.getElementById('joinBtn');
const pop1 = document.getElementById('pop1');
const pop2 = document.getElementById('pop2');
const pop3 = document.getElementById('pop3');
const continue1 = document.getElementById('continue1');
const signIn1 = document.getElementById('sign-in1');
const signIn2 = document.getElementById('sign-in2');
const signIn3 = document.getElementById('sign-in3');
const joinNow1 = document.getElementById('join-now1');
const addPost = document.getElementById('add-post');
const headlineCard = document.getElementById('headline-card');
const cards = document.querySelectorAll('.option-card');
const actionButton = document.getElementById('actionButton');
const actionBtn = document.getElementById('actionBtn');
const createBtn = document.getElementById('createAccountBtn');
const nextSkills = document.getElementById('next-skills');
const skills = document.getElementById('skills');

// Add backdrop for popups
const backdrop = document.createElement('div');
backdrop.classList.add('backdrop');
document.body.appendChild(backdrop);

let selectedType = null;

// Helper function to close all popups
const closeAllPopups = () => {
    if (pop1) pop1.style.display = 'none';
    if (pop2) pop2.style.display = 'none';
    if (pop3) pop3.style.display = 'none';
    if (headlineCard) headlineCard.style.display = 'none';
    if (skills) skills.style.display = 'none';
    backdrop.style.display = 'none';
};

// Handle click outside popups
document.addEventListener('click', (event) => {
    const isClickOutside = ![pop1, pop2, pop3, joinBtn, headlineCard, addPost].some(
        (element) => element && element.contains(event.target)
    );

    if (isClickOutside) {
        closeAllPopups();
    }
});

// Stop propagation of clicks inside popups
[pop1, pop2, pop3, headlineCard].forEach((popup) => {
    if (popup) {
        popup.addEventListener('click', (event) => {
            event.stopPropagation();
        });
    }
});

// Show pop1 when "Join" button is clicked
if (joinBtn) {
    joinBtn.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (pop1) pop1.style.display = 'block';
        backdrop.style.display = 'block';
    });
}

// Handle "Sign In" buttons
if (signIn1) {
    signIn1.addEventListener('click', () => {
        closeAllPopups();
        if (pop3) pop3.style.display = 'block';
        backdrop.style.display = 'block';
    });
}

if (signIn2) {
    signIn2.addEventListener('click', () => {
        closeAllPopups();
        if (pop3) pop3.style.display = 'block';
        backdrop.style.display = 'block';
    });
}

if (signIn3) {
    signIn3.addEventListener('click', (event) => {
        event.stopPropagation();
        if (pop3) pop3.style.display = pop3.style.display === 'none' || pop3.style.display === '' ? 'block' : 'none';
        if (pop2) pop2.style.display = 'none';
        if (pop1) pop1.style.display = 'none';
        backdrop.style.display = pop3.style.display === 'block' ? 'block' : 'none';
    });
}

// Handle "Join Now" button
if (joinNow1) {
    joinNow1.addEventListener('click', () => {
        closeAllPopups();
        if (pop1) pop1.style.display = 'block';
        backdrop.style.display = 'block';
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

        // Update form action dynamically
        if (actionButton) {
            actionButton.setAttribute(
                'asp-action',
                selectedType === 'client' ? 'Client_Profile' : 'WorkerProfile_Setup'
            );
        }
    });
});

// Handle account type submission
if (actionBtn) {
    actionBtn.addEventListener('click', () => {
        if (selectedType === 'client') {
            window.location.href = '/Home/Client_Profile';
        } else if (selectedType === 'worker') {
            window.location.href = '/Home/WorkerProfile_Setup';
        }
    });
}

// Show the headline card when "Add Post" button is clicked
if (addPost) {
    addPost.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (headlineCard) {
            headlineCard.style.display = 'block';
            backdrop.style.display = 'block';
        }
    });
}

if (nextSkills) {
    nextSkills.addEventListener('click', (event) => {
        event.stopPropagation();
        closeAllPopups();
        if (skills) {
            headlineCard.style.display = 'none';
            skills.style.display = 'block';
            backdrop.style.display = 'block';
        }
    });
}