$(function() {

  var siteSticky = function() {
		$(".js-sticky-header").sticky({topSpacing:0});
	};
	siteSticky();

	var siteMenuClone = function() {

		$('.js-clone-nav').each(function() {
			var $this = $(this);
			$this.clone().attr('class', 'site-nav-wrap').appendTo('.site-mobile-menu-body');
		});


		setTimeout(function() {
			
			var counter = 0;
      $('.site-mobile-menu .has-children').each(function(){
        var $this = $(this);
        
        $this.prepend('<span class="arrow-collapse collapsed">');

        $this.find('.arrow-collapse').attr({
          'data-toggle' : 'collapse',
          'data-target' : '#collapseItem' + counter,
        });

        $this.find('> ul').attr({
          'class' : 'collapse',
          'id' : 'collapseItem' + counter,
        });

        counter++;

      });

    }, 1000);

		$('body').on('click', '.arrow-collapse', function(e) {
      var $this = $(this);
      if ( $this.closest('li').find('.collapse').hasClass('show') ) {
        $this.removeClass('active');
      } else {
        $this.addClass('active');
      }
      e.preventDefault();  
      
    });  

		//$(window).resize(function() {
		//	var $this = $(this),
		//		w = $this.width();

		//	if ( w > 768 ) {
		//		if ( $('body').hasClass('offcanvas-menu') ) {
		//			$('body').removeClass('offcanvas-menu');
		//		}
		//	}
		//})

		//$('body').on('click', '.js-menu-toggle', function(e) {
		//	var $this = $(this);
		//	e.preventDefault();

		//	if ( $('body').hasClass('offcanvas-menu') ) {
		//		$('body').removeClass('offcanvas-menu');
		//		$this.removeClass('active');
		//	} else {
		//		$('body').addClass('offcanvas-menu');
		//		$this.addClass('active');
		//	}
		//}) 

		// click outisde offcanvas
		$(document).mouseup(function(e) {
	    var container = $(".site-mobile-menu");
	    if (!container.is(e.target) && container.has(e.target).length === 0) {
	      if ( $('body').hasClass('offcanvas-menu') ) {
					$('body').removeClass('offcanvas-menu');
				}
	    }
		});
	}; 
	siteMenuClone();

});

function getCookie(name) {
	let matches = document.cookie.match(new RegExp(
		"(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
	));
	return matches ? decodeURIComponent(matches[1]) : undefined;
}

function showLoadingSpinner() {
	$('.spinner-container').show();
}

function hideLoadingSpinner() {
	$('.spinner-container').hide();
}

function showpop(msg, title) {
	toastr.options = {
		"closeButton": false,
		"debug": false,
		"newestOnTop": false,
		"progressBar": true,
		"positionClass": "toast-top-left",
		"preventDuplicates": true,
		"onclick": null,
		"showDuration": "300",
		"hideDuration": "1000",
		"timeOut": "120000",
		"extendedTimeOut": "1000",
		"showEasing": "swing",
		"hideEasing": "linear",
		"showMethod": "fadeIn",
		"hideMethod": "fadeOut",
	}
	toastr.success(msg, title);
	toastr.warning(msg, title);
	toastr.info(msg, title);
	toastr.error(msg, title);
	return false;
}
