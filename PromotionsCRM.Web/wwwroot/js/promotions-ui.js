// PromotionsCRM - small UI helpers. No external dependencies except Bootstrap's bundle.
(function () {
    'use strict';

    // 1. Marks the navigation link of the current section as active.
    //    "/Promotions/Details/5" -> the link to "/Promotions" gets the "active" class.
    function highlightCurrentNavLink() {
        var currentSection = window.location.pathname.split('/')[1].toLowerCase();

        document.querySelectorAll('.navbar .nav-link').forEach(function (link) {
            var linkSection = new URL(link.href, window.location.origin).pathname.split('/')[1].toLowerCase();

            if (currentSection && linkSection === currentSection) {
                link.classList.add('active');
                link.setAttribute('aria-current', 'page');
            }
        });
    }

    // 2. Closes success messages automatically after a few seconds.
    //    Error messages stay until the user closes them.
    function autoCloseSuccessAlerts() {
        var delayInMs = 6000;

        document.querySelectorAll('.alert-success.alert-dismissible').forEach(function (alertElement) {
            window.setTimeout(function () {
                if (window.bootstrap && bootstrap.Alert) {
                    bootstrap.Alert.getOrCreateInstance(alertElement).close();
                }
            }, delayInMs);
        });
    }

    // 3. Marks offers that end in 3 days or less, so they stand out.
    //    Expects: <span class="offer-days" data-days-left="2">...</span>
    function markUrgentOffers() {
        var urgentLimit = 3;

        document.querySelectorAll('.offer-days[data-days-left]').forEach(function (label) {
            var daysLeft = parseInt(label.getAttribute('data-days-left'), 10);

            if (!isNaN(daysLeft) && daysLeft <= urgentLimit) {
                label.classList.add('is-urgent');
            }
        });
    }

    document.addEventListener('DOMContentLoaded', function () {
        highlightCurrentNavLink();
        autoCloseSuccessAlerts();
        markUrgentOffers();
    });
})();
