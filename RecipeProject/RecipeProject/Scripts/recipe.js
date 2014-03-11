$(function () {
    'use strict';
    var $success;

    $success = $('.alert-box');
    $success.on('click', '.close', function (event) {
        event.preventDefault();
        $success.fadeOut('400', function () {
            $(this).remove();
        });
    });
});