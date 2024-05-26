// Function to filter employees based on search input
function filterEmployees() {
    // Get the value of the search input
    var searchText = document.querySelector('.table-search-bar').value.toLowerCase();

    // Get all table rows
    var rows = document.querySelectorAll('tbody tr');

    // Loop through each row
    rows.forEach(function(row) {
        // Get the employee name column
        var nameColumn = row.querySelector('td:nth-child(2)');
        if (nameColumn) {
            var name = nameColumn.textContent.toLowerCase();
            // Check if the employee name contains the search text
            if (name.includes(searchText)) {
                // Show the row if it matches the search text
                row.style.display = '';
            } else {
                // Hide the row if it doesn't match the search text
                row.style.display = 'none';
            }
        }
    });
}

// Attach event listener to search input
document.querySelector('.table-search-bar').addEventListener('input', filterEmployees);
