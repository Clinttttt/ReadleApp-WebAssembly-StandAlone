document.addEventListener("DOMContentLoaded", function () {
    const swiper = new Swiper('.swiper', {
        direction: 'horizontal',   // can be 'horizontal' or 'vertical'
        loop: true,

        pagination: {
            el: '.swiper-pagination',
        },

        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },

        scrollbar: {
            el: '.swiper-scrollbar',
        },
    });
});
