function toggleReturnDate(show) {
   const returnDateGroup = document.getElementById('returnDateGroup');
   returnDateGroup.style.display = show ? 'block' : 'none';
}
document.addEventListener('DOMContentLoaded', function () {
   toggleReturnDate(document.getElementById('two_way').checked);
});