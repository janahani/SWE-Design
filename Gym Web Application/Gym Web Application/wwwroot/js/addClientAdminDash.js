window.addEventListener('DOMContentLoaded', (event) => {
    clearInputFields();
});

function clearInputFields() {
var inputFields = document.querySelectorAll('.add-user-input');
inputFields.forEach(function(input) {
input.value = '';
});
}