// Initiate GET request (AJAX-supported)
$(document).on('click', '[data-get]', e => {
    e.preventDefault();
    const url = e.target.dataset.get;
    location = url || location;
});

// Back Button (AJAX-supported)
$(document).on('click', '[data-back]', e => {
    e.preventDefault();
    const url = document.referrer;
    location = url || '/';
})

// Trim input
$('[data-trim]').on('change', e => {
    e.target.value = e.target.value.trim();
})