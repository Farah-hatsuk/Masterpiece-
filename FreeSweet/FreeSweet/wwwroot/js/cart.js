  
let menuList = document.getElementById("menuList");

// Toggle the 'open' class to animate max-height changes
function menu() {
    menuList.classList.toggle('open');
}

  
  
  /* Set rates + misc */
  const taxRate = 0.05;
  const shippingRate = 2.00;

  /* Recalculate cart */
function recalculateCart() {
   
      let subtotal = 0;

      $('.product').each(function () {
          subtotal += parseFloat($(this).find('.product-line-price').text());
      });

      const tax = subtotal * taxRate;
      const shipping = subtotal > 0 ? shippingRate : 0;
      const total = subtotal + tax + shipping;

      $('#cart-subtotal').text(subtotal.toFixed(2));
      $('#cart-tax').text(tax.toFixed(2));
      $('#cart-shipping').text(shipping.toFixed(2));
      $('#cart-total').text(total.toFixed(2));
  }

  /* Update quantity */
$('.product-quantity input').on('change', function () {

      const $productRow = $(this).closest('.product');
      const price = parseFloat($productRow.find('.product-price1').text());
      const quantity = parseInt($(this).val());
      const linePrice = price * quantity;

      $productRow.find('.product-line-price').text(linePrice.toFixed(2)+ " JD");
      recalculateCart();
  });

  /* Remove product */
  $('.remove-product').on('click', function () {
      $(this).closest('.product').remove();
      recalculateCart();
  });