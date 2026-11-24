(function () {
    'use strict';

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
