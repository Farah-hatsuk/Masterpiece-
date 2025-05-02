let menuList = document.getElementById("menuList");

// Toggle the 'open' class to animate max-height changes
function menu() {
    menuList.classList.toggle('open');
}

//$(document).ready(function () {
//    $('.slider').slick({
//        dots: true, // Show dots navigation
//        arrows: true, // Show next/prev arrows
//        infinite: true, // Loop slides
//        speed: 800, // Animation speed
//        slidesToShow: 1, // Number of slides to show
//        slidesToScroll: 1, // Number of slides to scroll
//        autoplay: true, // Enable autoplay
//        autoplaySpeed: 2000, // Autoplay speed in ms
//    });
//});

let size = document.getElementById("size")

size.addEventListener("change", function () {
    sizes(size)
})

function sizes(size) {
    if (size.value == "mini") {
        document.getElementById("price").innerHTML= "5 jd"
    }

    if (size.value=="small"){
        document.getElementById("price").innerHTML= "15 jd"
    }

    if (size.value=="medium"){
        document.getElementById("price").innerHTML= "20 jd"
    }

    if (size.value=="large"){
        document.getElementById("price").innerHTML="30 jd"
    }
}