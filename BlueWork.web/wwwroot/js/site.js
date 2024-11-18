﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
let joinBtn = document.getElementById('joinBtn');
let pop1 = document.getElementById('pop1');
let continue1 = document.getElementById('continue1');
let pop2 = document.getElementById('pop2');
let pop3 = document.getElementById('pop3');
let signIn1 = document.getElementById('sign-in1');
let signIn2 = document.getElementById('sign-in2');
let signIn3 = document.getElementById('sign-in3');
let joinNow1 = document.getElementById('join-now1');

// Function to close all popups
const closeAllPopups = () => {
    pop1.style.display = 'none';
    pop2.style.display = 'none';
    pop3.style.display = 'none';
};

// Handle click outside popups
document.addEventListener('click', (event) => {
    // Check if click is outside all popups
    const isClickOutside = ![pop1, pop2, pop3, joinBtn].some(element =>
        element.contains(event.target)
    );

    if (isClickOutside) {
        closeAllPopups();
    }
});

// Stop propagation of clicks inside popups to prevent them from closing
[pop1, pop2, pop3].forEach(popup => {
    popup.addEventListener('click', (event) => {
        event.stopPropagation();
    });
});

joinBtn.addEventListener('click', (event) => {
    event.stopPropagation();
    pop3.style.display = 'none';
    pop1.style.display = 'block';
});


signIn1.addEventListener('click', () => {
    pop3.style.display = 'block';
    pop2.style.display = 'none';
    pop1.style.display = 'none';
});

signIn2.addEventListener('click', () => {
    pop3.style.display = 'block';
    pop2.style.display = 'none';
    pop1.style.display = 'none';
});
signIn3.addEventListener('click', (event) => {
    event.stopPropagation();
    pop3.style.display = pop3.style.display === 'none' || pop3.style.display === '' ? 'block' : 'none';
    // Hide other popups
    pop2.style.display = 'none';
    pop1.style.display = 'none';
});

joinNow1.addEventListener('click', () => {
    pop3.style.display = 'none';
    pop2.style.display = 'none';
    pop1.style.display = 'block';
});

const cards = document.querySelectorAll('.option-card');
const actionButton = document.getElementById('actionButton');
const actionBtn = document.getElementById('actionBtn');
const createBtn = document.getElementById('createAccountBtn');
let selectedType = null;

cards.forEach(card => {
    card.addEventListener('click', () => {
        // Remove the 'selected' class from all cards
        cards.forEach(c => c.classList.remove('selected'));

        // Add 'selected' class to the clicked card
        card.classList.add('selected');
        selectedType = card.dataset.type;

        // Hide 'Create Account' button and show 'actionBtn'
        createBtn.style.display = 'none';
        actionBtn.style.display = 'block';

        // Update the action button based on selection
        if (selectedType === 'client') {
            actionBtn.textContent = 'Join as a Client';
            actionButton.setAttribute('asp-action', 'Client_Profile');
        } else {
            actionBtn.textContent = 'Apply as a Worker';
            actionButton.setAttribute('asp-action', 'WorkerProfile_Setup');
        }
    });
});
actionBtn.addEventListener('click', () => {
    if (selectedType === 'client') {
        // Navigate to Client Profile page
        window.location.href = '/Home/Client_Profile';
    } else if (selectedType === 'worker') {
        // Navigate to Worker Profile Setup page
        window.location.href = '/Home/WorkerProfile_Setup';
    }
});

