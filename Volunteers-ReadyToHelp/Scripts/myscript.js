$(function () {
    var scrolled_val;
    $(window).scroll(function () {
        scrolled_val = $(document).scrollTop().valueOf();
        //console.log(scrolled_val + ' = scroll value');
        if (scrolled_val > 50) {
            $('header nav').addClass('inbody');
        }
        else {
            $('header nav').removeClass('inbody');
        }
    });

    //Automatic carousel indicator
    var SlideNumber = $('#featured .item').length;
    for (var i = 0; i < SlideNumber; i++) {
        var indicator;
        if (i == 0) {
           indicator = '<li data-target="#featured" class="active" data-slide-to="' + i + '"></li>';
        }
        else {
           indicator = '<li data-target="#featured" data-slide-to="' + i + '"></li>';
        }
        
        $('#featured ol').append(indicator);
    }

    //Adjust height automaticaly
    var windowheight = $(window).height();
    $('.full-screen').css('height', windowheight);
    $('#featured .item img').each(function () {
        var imgSrc = $(this).attr('src');
        $(this).parent().css({ 'background-image': 'url(' + imgSrc + ')' });
        $(this).remove();
    });
    $(window).resize(function () {
        windowheight = $(window).height();
        $('.full-screen').css('height', windowheight);
    });


    var eventHeight = $('.recent-events .row').height();
    $('.recent-events .join-now').css('height', eventHeight);

    $('.carousel').carousel({
        interval: false
    });


    // init Isotope
    var $container = $('.work-items').isotope('layout');

    // filter items on button click
    $('.work-filter').on('click', 'a', function (e) {
        e.preventDefault();
        var filterValue = $(this).attr('data-filter');
        $container.isotope({ filter: filterValue });

        $('.work-filter li').removeClass('active');
        $(this).closest('li').addClass('active');
    });

    var paragraphheight = $('.service p').height();
    $('.service-image').css('height', paragraphheight);


    $("#datepicker").datepicker({
        changeMonth: true,
        changeYear: true
    });

    //CKEDITOR.replace('description');

    

});