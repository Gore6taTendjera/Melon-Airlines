document.addEventListener('DOMContentLoaded', function () {
   const paymentRadios = document.querySelectorAll('input[name="payment-method"]');
   const paymentDetails = document.querySelectorAll('.payment-details');

   paymentRadios.forEach(radio => {
      radio.addEventListener('change', function () {
         paymentDetails.forEach(detail => detail.classList.remove('active'));
         document.getElementById(this.value).classList.add('active');
      });
   });
});