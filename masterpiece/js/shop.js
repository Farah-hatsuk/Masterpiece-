let menuList = document.getElementById("menuList");

// Toggle the 'open' class to animate max-height changes
function menu() {
    menuList.classList.toggle('open');
}


let catagories = document.querySelectorAll(".shuffle li")
let boxes = document.querySelectorAll('.item .card');

function filers(category) {
    boxes.forEach((box) => {
            // Hide all boxes initially
        box.style.display = 'none';

    // Show boxes based on selected category
        if (category == "all" || box.dataset.category === category) {
            box.style.display = 'block';
        }

    })
}

catagories.forEach((classs) => {
    // Add event listener to each category
    classs.addEventListener("click", function() {
    // Update active class

        catagories.forEach((removes) => removes.classList.remove("active"));
        classs.classList.add("active")

        // Update active class
        let selectedCategory = classs.getAttribute('data-category');
    
            // Call the function to filter boxes
        filers(selectedCategory);
    })

})

filterCategory('all');