// let menuList = document.getElementById("menuList").
// menuList.style.maxHeight = "0px"


// function menu() {
//     if (menuList.style.maxHeight == "0px") {
//         menuList.style.maxHeight = "300px"
//     }
//     else {
//         menuList.style.maxHeight = "0px"
//     }

// }
// menu();

let menuList = document.getElementById("menuList");

// Toggle the 'open' class to animate max-height changes
function menu() {
    menuList.classList.toggle('open');
}








const images = document.querySelectorAll('.slider-image');
let currentIndex = 0;

function logoutt() {
    localStorage.removeItem("users");
    window.location.href = "login.html";
}

function changeSlide() {
    images[currentIndex].classList.remove('active');
    currentIndex = (currentIndex + 1) % images.length;
    images[currentIndex].classList.add('active');
}

setInterval(changeSlide, 3000);


