// Get the grid element
const grid = document.getElementById('myGrid');

// Define the class to toggle
const selectedClass = 'g-table__row_selected';

// Add an event listener to the grid
grid.addEventListener('click', (event) => {
    // Check if the clicked element is a grid row
    if (event.target.closest('fluent-data-grid-row')) {
        // Get the row element
        const row = event.target.closest('fluent-data-grid-row');

        // Toggle the selected class
        row.classList.toggle(selectedClass);
    }
});
