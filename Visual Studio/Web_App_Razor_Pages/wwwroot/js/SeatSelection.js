function changeFloor() {
   var selectedFloor = document.querySelector('input[name="floor"]:checked');
   if (selectedFloor && selectedFloor.value == 1) {
      document.querySelector('.floor1').style.display = 'block';
      document.querySelector('.floor2').style.display = 'none';
   } else if (selectedFloor && selectedFloor.value == 2) {
      document.querySelector('.floor1').style.display = 'none';
      document.querySelector('.floor2').style.display = 'block';
   }
}


//document.addEventListener("DOMContentLoaded", function () {
//   const seats = document.querySelectorAll("input[type='radio'][name='selectedSeat']");
//   seats.forEach(seat => {
//      seat.addEventListener("change", function () {
//         document.getElementById("seatId").value = this.value;
//      });
//   });
//});


function selectSeat(seatRow, seatColumn) {
   document.getElementById("seatRowConfirm").value = seatRow;
   document.getElementById("seatColumnConfirm").value = seatColumn;

   document.getElementById("seatRowCalculatePrice").value = seatRow;
   document.getElementById("seatColumnCalculatePrice").value = seatColumn;

}


document.addEventListener('DOMContentLoaded', function () {
   var radioButtons = document.getElementsByName('seatSelection');

   radioButtons.forEach(function (radio) {
      radio.addEventListener('change', function () {
         var section2 = document.getElementById('section2');
         var sectionSelectSeatType = document.getElementById('section-select-seatType');
         if (this.value === 'selectNow') {
            section2.style.display = 'block';
            sectionSelectSeatType.style.display = 'none';
         } else {
            sectionSelectSeatType.style.display = 'block';
            section2.style.display = 'none';
         }
      });
   });
});

function updateSeatClass(radio) {
   document.getElementById('selectedSeatClassCalculatePrice').value = radio.value;
}

function choseRandomOrNo(radio) {
   document.getElementById('seatSelectionCalculatePrice').value = radio.value;
}


document.addEventListener('DOMContentLoaded', function () {
   let seatRowColumn = document.getElementById('seatRowColumn').textContent;
   let seatPrice = document.getElementById('seatPrice').textContent;

   let confirmForm = document.getElementById('confirmForm');


   if (seatRowColumn == '' || !seatRowColumn || seatRowColumn == 0 || seatPrice == 0 || !seatPrice) {
      confirmForm.classList.add('hidden');
   } else {
      confirmForm.classList.remove('hidden');
   }
});



document.addEventListener('DOMContentLoaded', function () {
   let seatRowCalculatePrice = document.getElementById("seatRowCalculatePrice").value;
   let seatRowCalculatePrice2 = document.getElementById("seatRowCalculatePrice").textContent;

   console.log(seatRowCalculatePrice);
   console.log(seatRowCalculatePrice2);


   let seatRowColumn = document.getElementById('seatRowColumn').textContent;

   seatRowCalculatePrice = seatRowColumn;

   console.log(seatRowCalculatePrice);
   console.log(seatRowCalculatePrice2);

});