// admin-app.js

/* Theme (Admin) ------------------------------------------------------------------------- */
$(document).on('click', '#themeToggle', function () {
    const current = document.documentElement.getAttribute('data-theme');
    setTheme(current === 'dark' ? 'light' : 'dark');
});

function setTheme(theme) {
    document.documentElement.setAttribute('data-theme', theme);
    // Persist in a cookie so the server can render the right theme on first paint
    document.cookie = `theme=${theme}; path=/; max-age=31536000; SameSite=Lax`;
}

/* Sibebar (Admin) ------------------------------------------------------------------------- */
$('#sidebarToggle').on('click', function () {
    $('body').toggleClass('sidebar-collapsed'); // hide
});

$(function () {
    const path = location.pathname.toLowerCase();

    $('.submenu .nav-link').each(function () {
        if (this.pathname.toLowerCase() === path) {
            $(this).addClass('active');
            $(this).closest('.collapse').addClass('show');
        }
    });

    // top-level (no submenu) active state
    $('.sidebar-nav > .nav > .nav-item > .nav-link').each(function () {
        const href = $(this).attr('href');
        if (!href || href.startsWith('#')) return;

        if (this.pathname && this.pathname.toLowerCase() === path) {
            $(this).addClass('active');
        }
    });
});