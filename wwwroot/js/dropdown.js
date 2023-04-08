(function () {
    'use strict';

    // Fetch all the dropdowns
    var dropdowns = document.querySelectorAll('.dropdown:not(.is-hoverable)');

    // Loop through the dropdowns and hide them
    dropdowns.forEach(function (el) {
        el.classList.remove('is-active');
    });

    // Add a click event listener to each dropdown button
    dropdowns.forEach(function (el) {
        var dropdownButton = el.querySelector('.dropdown-trigger');
        dropdownButton.addEventListener('click', function (event) {
            event.stopPropagation();
            el.classList.toggle('is-active');
        });
    });

    // Close the dropdown menu if the user clicks outside of it
    document.addEventListener('click', function (event) {
        dropdowns.forEach(function (el) {
            el.classList.remove('is-active');
        });
    });

})();
