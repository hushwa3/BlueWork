// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

let joinBtn = document.getElementById('joinBtn');
let pop1 = document.getElementById('pop1');
let continue1 = document.getElementById('continue1');
let pop2 = document.getElementById('pop2');
let pop3 = document.getElementById('pop3');
let signIn = document.getElementById('sign-in');

joinBtn.addEventListener('click', () => {
    pop1.style.display = pop1.style.display === 'none' || pop1.style.display === '' ? 'block' : 'none';
});

continue1.addEventListener('click', () => {
    pop2.style.display = 'block';
    pop1.style.display = 'none';
});

signIn.addEventListener('click', () => {
    pop3.style.display = 'block';
    pop2.style.display = 'none';
    pop1.style.display = 'none';
});

signIn.addEventListener('click', () => {
    pop3.style.display = 'block';
    pop2.style.display = 'none';
    pop1.style.display = 'none';
});
