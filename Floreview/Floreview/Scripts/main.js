document.addEventListener('DOMContentLoaded', function () {
    var navigation = document.querySelector('nav');

    window.onscroll = function (e) {
        var scrollOffset = document.documentElement.scrollTop || document.body.scrollTop;

        if (scrollOffset > 0) {
            navigation.classList.add('scrolled');
        }
        else {
            navigation.classList.remove('scrolled');
        }
    }
});