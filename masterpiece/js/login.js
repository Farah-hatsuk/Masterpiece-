let menuList = document.getElementById("menuList");

// Toggle the 'open' class to animate max-height changes
function menu() {
    menuList.classList.toggle('open');
}


document.addEventListener("DOMContentLoaded", () => {
    const signInBtn = document.getElementById("signIn");
    const signUpBtn = document.getElementById("signUp");
    const fistForm = document.getElementById("form1");
    const secondForm = document.getElementById("form2");
    const container = document.querySelector(".container");

    signInBtn.addEventListener("click", () => {
        container.classList.remove("right-panel-active");
    });

    signUpBtn.addEventListener("click", () => {
        container.classList.add("right-panel-active");
    });

    fistForm.addEventListener("submit", (e) => e.preventDefault());
    secondForm.addEventListener("submit", (e) => e.preventDefault());
});





// Regular Expression Patterns
const usernamePattern = /^[a-zA-Z0-9_]{3,15}$/;
const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$/;
const passwordPattern = /^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$/;

// Sign Up Inputs
const signupUsername = document.getElementById('signup-username');
const signupEmail = document.getElementById('signup-email');
const signupPassword = document.getElementById('signup-password');
const signupConfirmPassword = document.getElementById('signup-confirm-password');

// Sign Up Errors
const signupUsernameError = document.getElementById('signup-username-error');
const signupEmailError = document.getElementById('signup-email-error');
const signupPasswordError = document.getElementById('signup-password-error');
const signupConfirmPasswordError = document.getElementById('signup-confirm-password-error');

// Sign In Inputs
const signinEmail = document.getElementById('signin-email');
const signinPassword = document.getElementById('signin-password');

// Sign In Errors
const signinEmailError = document.getElementById('signin-email-error');
const signinPasswordError = document.getElementById('signin-password-error');

// Real-Time Validation
signupUsername.addEventListener('input', () => {
    if (!usernamePattern.test(signupUsername.value)) {
        signupUsernameError.style.display = 'block';
    } else {
        signupUsernameError.style.display = 'none';
    }
});

signupEmail.addEventListener('input', () => {
    if (!emailPattern.test(signupEmail.value)) {
        signupEmailError.style.display = 'block';
    } else {
        signupEmailError.style.display = 'none';
    }
});

signupPassword.addEventListener('input', () => {
    if (!passwordPattern.test(signupPassword.value)) {
        signupPasswordError.style.display = 'block';
    } else {
        signupPasswordError.style.display = 'none';
    }
});

signupConfirmPassword.addEventListener('input', () => {
    if (signupConfirmPassword.value !== signupPassword.value) {
        signupConfirmPasswordError.style.display = 'block';
    } else {
        signupConfirmPasswordError.style.display = 'none';
    }
});

signinEmail.addEventListener('input', () => {
    if (!emailPattern.test(signinEmail.value)) {
        signinEmailError.style.display = 'block';
    } else {
        signinEmailError.style.display = 'none';
    }
});

signinPassword.addEventListener('input', () => {
    if (!passwordPattern.test(signinPassword.value)) {
        signinPasswordError.style.display = 'block';
    } else {
        signinPasswordError.style.display = 'none';
    }
});


