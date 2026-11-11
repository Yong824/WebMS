(function () {
    'use strict';

    // ─── 1. Open any modal via [data-modal-url] ───
    $(document).on('click', '[data-modal-url]', function (e) {
        e.preventDefault();
        const $btn = $(this);
        const url = $btn.data('modal-url');
        const target = $btn.data('modal-target');

        $.get(url)
            .done(function (html) {
                const $modal = $(target);
                $modal.find('.modal-content').html(html);
                // Re-parse unobtrusive validation on freshly loaded HTML
                $.validator.unobtrusive.parse($modal.find('form'));
                $modal.modal('show');
            })
            .fail(function (xhr) {
                console.error('Failed to load modal:', xhr);
                alert('Could not load the form.');
            });
    });

    // ─── 2. Submit any form with [data-ajax-form] ───
    $(document).on('submit', 'form[data-ajax-form]', function (e) {
        e.preventDefault();
        const $form = $(this);
        const $modal = $form.closest('.modal');

        $.post($form.attr('action'), $form.serialize())
            .done(function (response) {
                // Case A: Success JSON
                if (typeof response === 'object' && response.success) {
                    if ($modal.length > 0) {
                        $modal.modal('hide');
                    }

                    // Handle redirect if provided (e.g., from Hall creation)
                    if (response.redirectUrl) {
                        window.location.href = response.redirectUrl;
                        return;
                    }

                    if ($form.data('no-reload') !== true) {
                        location.reload();
                    }
                    return;
                }

                // Case B: Server returned a partial view with validation errors
                $modal.find('.modal-content').html(response);
                $.validator.unobtrusive.parse($modal.find('form'));
            })
            .fail(function (xhr) {
                if (xhr.status === 400 && xhr.responseJSON) {
                    alert('Validation failed. Please check your inputs.');
                    console.warn(xhr.responseJSON);
                } else {
                    alert('An error occurred while saving.');
                    console.error(xhr);
                }
            });
    });
})();