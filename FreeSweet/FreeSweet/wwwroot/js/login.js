let menuList = document.getElementById("menuList");

// Toggle the 'open' class to animate max-height changes
function menu() {
    menuList.classList.toggle('open');
}

// LOGIN TABS
$(function () {
    var tab = $('.tabs h3 a');
    tab.on('click', function (event) {
        event.preventDefault();
        tab.removeClass('active');
        $(this).addClass('active');
        tab_content = $(this).attr('href');
        $('div[id$="tab-content"]').removeClass('active');
        $(tab_content).addClass('active');
    });
});

// SLIDESHOW
$(function () {
    $('#slideshow > div:gt(0)').hide();
    setInterval(function () {
        $('#slideshow > div:first')
            .fadeOut(1000)
            .next()
            .fadeIn(1000)
            .end()
            .appendTo('#slideshow');
    }, 3850);
});

// CUSTOM JQUERY FUNCTION FOR SWAPPING CLASSES
(function ($) {
    'use strict';
    $.fn.swapClass = function (remove, add) {
        this.removeClass(remove).addClass(add);
        return this;
    };
}(jQuery));

// SHOW/HIDE PANEL ROUTINE (needs better methods)
// I'll optimize when time permits.
$(function () {
    $('.agree,.forgot, #toggle-terms, .log-in, .sign-up').on('click', function (event) {
        event.preventDefault();
        var terms = $('.terms'),
            recovery = $('.recovery'),
            close = $('#toggle-terms'),
            arrow = $('.tabs-content .fa');
        if ($(this).hasClass('agree') || $(this).hasClass('log-in') || ($(this).is('#toggle-terms')) && terms.hasClass('open')) {
            if (terms.hasClass('open')) {
                terms.swapClass('open', 'closed');
                close.swapClass('open', 'closed');
                arrow.swapClass('active', 'inactive');
            } else {
                if ($(this).hasClass('log-in')) {
                    return;
                }
                terms.swapClass('closed', 'open').scrollTop(0);
                close.swapClass('closed', 'open');
                arrow.swapClass('inactive', 'active');
            }
        }
        else if ($(this).hasClass('forgot') || $(this).hasClass('sign-up') || $(this).is('#toggle-terms')) {
            if (recovery.hasClass('open')) {
                recovery.swapClass('open', 'closed');
                close.swapClass('open', 'closed');
                arrow.swapClass('active', 'inactive');
            } else {
                if ($(this).hasClass('sign-up')) {
                    return;
                }
                recovery.swapClass('closed', 'open');
                close.swapClass('closed', 'open');
                arrow.swapClass('inactive', 'active');
            }
        }
    });
});

// DISPLAY MSSG
$(function () {
    $('.recovery .button').on('click', function (event) {
        event.preventDefault();
        $('.recovery .mssg').addClass('animate');
        setTimeout(function () {
            $('.recovery').swapClass('open', 'closed');
            $('#toggle-terms').swapClass('open', 'closed');
            $('.tabs-content .fa').swapClass('active', 'inactive');
            $('.recovery .mssg').removeClass('animate');
        }, 2500);
    });
});

// DISABLE SUBMIT FOR DEMO
$(function () {
    $('.button').on('click', function (event) {
        $(this).stop();
        event.preventDefault();
        return false;
    });
});

$(function () {
    // Step 1: Email submission
    $('#step1-submit').on('click', function (event) {
        event.preventDefault();
        $('#recovery-step1').hide();
        $('#recovery-step2').show();
    });

    // Step 2: Verification code submission
    $('#step2-submit').on('click', function (event) {
        event.preventDefault();
        $('#recovery-step2').hide();
        $('#recovery-step3').show();
    });

    // Step 3: Password reset submission
    $('#step3-submit').on('click', function (event) {
        event.preventDefault();
        // Here you would typically send the new password to your server
        $('.recovery').hide();
        $('.mssg').text('Your password has been successfully reset!').addClass('animate');
        setTimeout(function () {
            $('.recovery').swapClass('open', 'closed');
            $('#toggle-terms').swapClass('open', 'closed');
            $('.tabs-content .fa').swapClass('active', 'inactive');
            $('.mssg').removeClass('animate');
        }, 2500);
    });
});