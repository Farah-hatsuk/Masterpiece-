// Get references to elements
const editButton = document.getElementById("editButton");
const inputs = document.querySelectorAll("input, select");
const profileImageInput = document.getElementById("profileImageInput");
const userImage = document.getElementById("userImage");
let isEditing = false;

// On page load, populate profile data if a user is logged in
// window.addEventListener("load", () => {
//     const loggedInUser = JSON.parse(sessionStorage.getItem("loggedInUser"));

//     if (loggedInUser) {
//         document.getElementById("userFullName").textContent = `${loggedInUser.fname} ${loggedInUser.lname}`;
//         document.getElementById("userEmail").textContent = loggedInUser.email;
//         userImage.src = loggedInUser.profileImage || "../Images/img/user (1).png";

//         document.getElementById("firstName").value = loggedInUser.fname;
//         document.getElementById("lastName").value = loggedInUser.lname;
//         document.getElementById("email").value = loggedInUser.email;
//         document.getElementById("gender").value = loggedInUser.gender || "";
//         document.getElementById("birthday").value = loggedInUser.birthday || "";
//     } else {
//         alert("No logged-in user found. Redirecting to login page...");
//         window.location.href = "login.html"; 
//     }
// });

// Event listener for edit/save button
editButton.addEventListener("click", () => {
    toggleEditing();
});

// Event listener for profile image upload
profileImageInput.addEventListener("change", function (event) {
    const file = event.target.files[0];

    if (!file) {
        alert("No file selected. Please select an image.");
        return;
    }

    if (!file.type.startsWith("image/")) {
        alert("Invalid file type. Please select an image.");
        return;
    }

    const reader = new FileReader();
    reader.onload = function () {
        userImage.src = reader.result;
    };
    reader.readAsDataURL(file);
});

// Toggle editing mode
function toggleEditing() {
    if (isEditing) {
        if (validateAndSaveData()) {
            isEditing = false;
            inputs.forEach(input => input.disabled = true);
            editButton.textContent = "Edit";
            
        }
    } else {
        isEditing = true;
        inputs.forEach(input => input.disabled = false);
        editButton.textContent = "Save";
        editButton.style.padding = "10px";
    }
}

// Validate and save data
function validateAndSaveData() {
    const updatedEmail = document.getElementById("email").value;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(updatedEmail)) {
        alert("Please enter a valid email address.");
        return false; // Prevent saving if email is invalid
    }

    const updatedUser = {
        ...JSON.parse(sessionStorage.getItem("loggedInUser")),
        fname: document.getElementById("firstName").value,
        lname: document.getElementById("lastName").value,
        email: updatedEmail,
        gender: document.getElementById("gender").value,
        birthday: document.getElementById("birthday").value,
        profileImage: userImage.src,
    };

    sessionStorage.setItem("loggedInUser", JSON.stringify(updatedUser));
    const users = JSON.parse(localStorage.getItem("users")) || [];
    const userIndex = users.findIndex(user => user.email === updatedUser.email);

    if (userIndex !== -1) {
        users[userIndex] = updatedUser;
    }
    localStorage.setItem("users", JSON.stringify(users));

    document.getElementById("userFullName").textContent = `${updatedUser.fname} ${updatedUser.lname}`;
    document.getElementById("userEmail").textContent = updatedUser.email;

    alert("Profile updated successfully!");
    return true;
}
const resetPasswordButton = document.getElementById("resetPasswordButton");
const resetPasswordModal = document.getElementById("resetPasswordModal");
const closeModal = document.getElementById("closeModal");
const savePasswordButton = document.getElementById("savePasswordButton");

// Show reset password modal
resetPasswordButton.addEventListener("click", () => {
    resetPasswordModal.style.display = "block";
});

// Close the modal
closeModal.addEventListener("click", () => {
    resetPasswordModal.style.display = "none";
});

// Save new password
savePasswordButton.addEventListener("click", () => {
    const currentPassword = document.getElementById("currentPassword").value;
    const newPassword = document.getElementById("newPassword").value;
    const confirmPassword = document.getElementById("confirmPassword").value;

    const loggedInUser = JSON.parse(sessionStorage.getItem("loggedInUser"));
    const users = JSON.parse(localStorage.getItem("users")) || [];

    // Check if the current password matches
    if (loggedInUser.password !== currentPassword) {
        alert("Current password is incorrect.");
        return;
    }

    // Check if new password matches confirmation
    if (newPassword !== confirmPassword) {
        alert("New password and confirm password do not match.");
        return;
    }

    if (newPassword.length < 6) {
        alert("Password must be at least 6 characters long.");
        return;
    }

    // Update password in sessionStorage and localStorage
    // loggedInUser.password = newPassword;
    // sessionStorage.setItem("loggedInUser", JSON.stringify(loggedInUser));

    // const userIndex = users.findIndex(user => user.email === loggedInUser.email);
    // if (userIndex !== -1) {
    //     users[userIndex].password = newPassword;
    // }
    // localStorage.setItem("users", JSON.stringify(users));

    // alert("Password updated successfully!");
    // resetPasswordModal.style.display = "none";
});

// Close the modal when clicking outside the modal content
window.addEventListener("click", event => {
    if (event.target === resetPasswordModal) {
        resetPasswordModal.style.display = "none";
    }
});




// const HistoryOfOrder = document.getElementById("HistoryOfOrder");

// HistoryOfOrder.addEventListener("click", () => {
//     HistoryOfOrder.style.display = "block";
// });


// Get the button and modal elements
// Get the button and modal elements
// Get the button and modal elements
const historyButton = document.getElementById("historyButton"); // Button to open the modal
const historyModal = document.getElementById("historyModal"); // The modal container
const closeHistoryModal = document.getElementById("closeHistoryModal"); // Close button inside the modal

// Show the modal when the "History Of Order" button is clicked
historyButton.addEventListener("click", () => {
  historyModal.style.display = "block"; // Make the modal visible
});

// Close the modal when the close button is clicked
closeHistoryModal.addEventListener("click", () => {
  historyModal.style.display = "none"; // Hide the modal
});

// Close the modal when clicking outside the modal content
window.addEventListener("click", (event) => {
  if (event.target === historyModal) {
    historyModal.style.display = "none"; // Hide the modal
  }
});
