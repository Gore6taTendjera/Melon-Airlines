function showSectionDashboard(sectionIndex) {
   // Hide all sections
   var sections = document.querySelectorAll('.section');
   sections.forEach(function (section) {
      section.style.display = 'none';
   });

   // Show the selected section
   var selectedSection = document.getElementById('section' + sectionIndex);
   if (selectedSection) {
      selectedSection.style.display = 'block';
   }
}


var userData = {
   Name: "John",
   MiddleName: "Doe",
   Surname: "Smith",
   Gender: "Male",
   BirthDate: "1990-01-01",
   BirthPlace: "New York",
   Username: "johnsmith",
   Password: "********",
   Email: "john@example.com",
   Nationality: "American"
};

function setUserData() {
   var fullName = userData.Name + " " + (userData.MiddleName || "") + " " + userData.Surname;
   document.getElementById('fullName').innerText = fullName.trim() || '-';

   var birthdatePlaceNationality = userData.BirthDate + ", " + userData.BirthPlace + ", " + userData.Nationality;
   document.getElementById('birthdatePlaceNationality').innerText = birthdatePlaceNationality.trim() || '-';

   document.getElementById('email').innerText = userData.Email || '-';
   document.getElementById('username').innerText = userData.Username || '-';
   document.getElementById('password').innerText = userData.Password || '-';
}

setUserData();