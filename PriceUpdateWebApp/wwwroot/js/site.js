$(function(){
    $('#environments').on('change', function () {
        window.location.href = $('#environments option:selected').data('url');
    })

    function adjustMinHeight() {
        $('.hero').css('min-height', 'calc(100vh - ' + ($('#sticky-wrapper').height() + 10)+'px)')
        $('main[role=main]').css('min-height', 'calc(100vh - ' + ($('#sticky-wrapper').height() + $('footer.footer').height() + 20) +'px)')
    }

    $(window).resize(function () { adjustMinHeight })
    adjustMinHeight();
});