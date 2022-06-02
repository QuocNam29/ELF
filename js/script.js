
jQuery(document).ready(function ($) {
	
	"use strict";
	
//------- Notifications Dropdowns
  $('.top-area > .setting-area > li').on("click",function(){
	$(this).siblings().children('div').removeClass('active');
	$(this).children('div').addClass('active');
	return false;
  });
//------- remove class active on body
  $("body *").not('.top-area > .setting-area > li').on("click", function() {
	$(".top-area > .setting-area > li > div").removeClass('active');		
 });
	

//--- user setting dropdown on topbar	
$('.user-img').on('click', function() {
	$('.user-setting').toggleClass("active");
	return false;
});	
	
//--- side message box	
$('.friendz-list > li, .chat-users > li').on('click', function() {
	$('.chat-box').addClass("show");
	return false;
});	
	$('.close-mesage').on('click', function() {
		$('.chat-box').removeClass("show");
		return false;
	});	
	
//------ scrollbar plugin
	if ($.isFunction($.fn.perfectScrollbar)) {
		$('.dropdowns, .twiter-feed, .invition, .followers, .chatting-area, .peoples, #people-list, .chat-list > ul, .message-list, .chat-users, .left-menu').perfectScrollbar();
	}

/*--- socials menu scritp ---*/	
	$('.trigger').on("click", function() {
	    $(this).parent(".menu").toggleClass("active");
	  });
	
/*--- emojies show on text area ---*/	
	$('.add-smiles > span').on("click", function() {
	    $(this).parent().siblings(".smiles-bunch").toggleClass("active");
	  });

// delete notifications
$('.notification-box > ul li > i.del').on("click", function(){
    $(this).parent().slideUp();
	return false;
  }); 	

/*--- socials menu scritp ---*/	
	$('.f-page > figure i').on("click", function() {
	    $(".drop").toggleClass("active");
	  });

//===== Search Filter =====//
	(function ($) {
	// custom css expression for a case-insensitive contains()
	jQuery.expr[':'].Contains = function(a,i,m){
	  return (a.textContent || a.innerText || "").toUpperCase().indexOf(m[3].toUpperCase())>=0;
	};

	function listFilter(searchDir, list) { 
	  var form = $("<form>").attr({"class":"filterform","action":"#"}),
	  input = $("<input>").attr({"class":"filterinput","type":"text","placeholder":"Search Contacts..."});
	  $(form).append(input).appendTo(searchDir);

	  $(input)
	  .change( function () {
		var filter = $(this).val();
		if(filter) {
		  $(list).find("li:not(:Contains(" + filter + "))").slideUp();
		  $(list).find("li:Contains(" + filter + ")").slideDown();
		} else {
		  $(list).find("li").slideDown();
		}
		return false;
	  })
	  .keyup( function () {
		$(this).change();
	  });
	}

//search friends widget
	$(function () {
	  listFilter($("#searchDir"), $("#people-list"));
	});
	}(jQuery));	

//progress line for page loader
	$('body').show();
	NProgress.start();
	setTimeout(function() { NProgress.done(); $('.fade').removeClass('out'); }, 2000);
	
//--- bootstrap tooltip	
	$(function () {
	  $('[data-toggle="tooltip"]').tooltip();
	});
	
// Sticky Sidebar & header
	if($(window).width() < 769) {
		jQuery(".sidebar").children().removeClass("stick-widget");
	}

	if ($.isFunction($.fn.stick_in_parent)) {
		$('.stick-widget').stick_in_parent({
			parent: '#page-contents',
			offset_top: 60,
		});

		
		$('.stick').stick_in_parent({
		    parent: 'body',
            offset_top: 0,
		});
		
	}
	
/*--- topbar setting dropdown ---*/	
	$(".we-page-setting").on("click", function() {
	    $(".wesetting-dropdown").toggleClass("active");
	  });	
	  
/*--- topbar toogle setting dropdown ---*/	
$('#nightmode').on('change', function() {
    if ($(this).is(':checked')) {
        // Show popup window
        $(".theme-layout").addClass('black');	
    }
	else {
        $(".theme-layout").removeClass("black");
    }
});

//chosen select plugin
if ($.isFunction($.fn.chosen)) {
	$("select").chosen();
}

//----- add item plus minus button
if ($.isFunction($.fn.userincr)) {
	$(".manual-adjust").userincr({
		buttonlabels:{'dec':'-','inc':'+'},
	}).data({'min':0,'max':20,'step':1});
}	
	
if ($.isFunction($.fn.loadMoreResults)) {	
	$('.loadMore').loadMoreResults({
		displayedItems: 3,
		showItems: 1,
		button: {
		  'class': 'btn-load-more',
		  'text': 'Load More'
		}
	});	
}
	//===== owl carousel  =====//
	if ($.isFunction($.fn.owlCarousel)) {
		$('.sponsor-logo').owlCarousel({
			items: 6,
			loop: true,
			margin: 30,
			autoplay: true,
			autoplayTimeout: 1500,
			smartSpeed: 1000,
			autoplayHoverPause: true,
			nav: false,
			dots: false,
			responsiveClass:true,
				responsive:{
					0:{
						items:3,
					},
					600:{
						items:3,

					},
					1000:{
						items:6,
					}
				}

		});
	}
	
// slick carousel for detail page
	if ($.isFunction($.fn.slick)) {
	$('.slider-for-gold').slick({
		slidesToShow: 1,
		slidesToScroll: 1,
		arrows: false,
		slide: 'li',
		fade: false,
		asNavFor: '.slider-nav-gold'
	});
	
	$('.slider-nav-gold').slick({
		slidesToShow: 3,
		slidesToScroll: 1,
		asNavFor: '.slider-for-gold',
		dots: false,
		arrows: true,
		slide: 'li',
		vertical: true,
		centerMode: true,
		centerPadding: '0',
		focusOnSelect: true,
		responsive: [
		{
			breakpoint: 768,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 1,
				infinite: true,
				vertical: false,
				centerMode: true,
				dots: false,
				arrows: false
			}
		},
		{
			breakpoint: 641,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 1,
				infinite: true,
				vertical: true,
				centerMode: true,
				dots: false,
				arrows: false
			}
		},
		{
			breakpoint: 420,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 1,
				infinite: true,
				vertical: false,
				centerMode: true,
				dots: false,
				arrows: false
			}
		}	
		]
	});
}
	
//---- responsive header
	
$(function() {

	//	create the menus
	$('#menu').mmenu();
	$('#shoppingbag').mmenu({
		navbar: {
			title: 'General Setting'
		},
		offCanvas: {
			position: 'right'
		}
	});

	//	fire the plugin
	$('.mh-head.first').mhead({
		scroll: {
			hide: 200
		}
		
	});
	$('.mh-head.second').mhead({
		scroll: false
	});

	
});		

//**** Slide Panel Toggle ***//
	  $("span.main-menu").on("click", function(){
	     $(".side-panel").addClass('active');
		  $(".theme-layout").addClass('active');
		  return false;
	  });

	  $('.theme-layout').on("click",function(){
		  $(this).removeClass('active');
	     $(".side-panel").removeClass('active');
		  
	     
	  });

	  
// login & register form
	$('button.signup').on("click", function(){
		$('.login-reg-bg').addClass('show');
		return false;
	  });
	  
	  $('.already-have').on("click", function(){
		$('.login-reg-bg').removeClass('show');
		return false;
	  });
	
//----- count down timer		
	if ($.isFunction($.fn.downCount)) {
		$('.countdown').downCount({
			date: '11/12/2018 12:00:00',
			offset: +10
		});
	}
	
/** Post a Comment **/
	jQuery(".post-comt-box1 textarea").on("keydown", function (event) {

		if (event.keyCode == 13) {

			jQuery(this).blur();
			jQuery('#submit_cmt').focus().click();
		}
	});

	jQuery(".post-comt-box textarea").on("keydown", function (event) {
		var value = "";
		var avt = "";
		var txtMaBD = "";
		var txtMaND = "";
		var URL = "";
		var BD_cmt = "";
		if (event.keyCode == 13) {
			var comment = jQuery(this).val();

			$('#BD_cmt')
				.keypress(function () {
					BD_cmt = $(this).val();

				})
				.keypress();
			


				$('#MaBD_cmt')
					.keypress(function () {
						txtMaBD = $(this).val();

					})
					.keypress();

				$('#MaND_cmt')
					.keypress(function () {
						txtMaND = $(this).val();

					})
					.keypress();

				$('#URL_CMT')
					.keypress(function () {
						URL = $(this).val();

					})
					.keypress();
			if (BD_cmt == "2") {
				var today = new Date();
				var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
				var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
				var dateTime = date + ' ' + time;
				//count Questions
				var binhLuanBDSP = {};
				binhLuanBDSP.noiDung = comment;
				binhLuanBDSP.maND = txtMaND;
				binhLuanBDSP.maBDSP = txtMaBD;
				binhLuanBDSP.ngayBL = dateTime;
				binhLuanBDSP.trangThai = "Hien";

				$.ajax({
					type: "POST",
					url: URL,
					data: JSON.stringify(binhLuanBDSP),
					contentType: 'application/json; charset=utf-8',
					dataType: 'json',
					success: function (r) {

					}
				});
			}
			else if (BD_cmt == "1") {
				var today = new Date();
				var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
				var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
				var dateTime = date + ' ' + time;
				//count Questions
				var binhLuanBDTT = {};
				binhLuanBDTT.noiDung = comment;
				binhLuanBDTT.maND = txtMaND;
				binhLuanBDTT.maBDTT = txtMaBD;
				binhLuanBDTT.ngayBL = dateTime;
				binhLuanBDTT.trangThai = "Hien";

				$.ajax({
					type: "POST",
					url: URL,
					data: JSON.stringify(binhLuanBDTT),
					contentType: 'application/json; charset=utf-8',
					dataType: 'json',
					success: function (r) {

					}
				});
            }
			$('#name_cmt')
				.keypress(function () {
					value = $(this).val();

				})
				.keypress();
			$('#avt_cmt')
				.keypress(function () {
					avt = $(this).val();

				})
				.keypress();


			var parent = jQuery(".showmore").parent("li");
			var comment_HTML = '	<li><div class="comet-avatar"><img src="' + avt + '" alt=""></div><div class="we-comment"><div class="coment-head"><h5><a href="time-line.html" title="">' + value + '</a></h5><a class="we-reply" href="#" title="Reply"><i class="fa fa-reply"></i></a></div><p>' + comment + '</p></div></li>';
			$(comment_HTML).insertBefore(parent);
			jQuery(this).val('');
		}
	}); 
	/*jQuery(".post-comt-box textarea").on("keydown", function (event) {

		if (event.keyCode == 13) {
			var comment = jQuery(this).val();
			var parent = jQuery(".showmore").parent("li");
			var comment_HTML = '	<li><div class="comet-avatar"><img src="images/resources/comet-1.jpg" alt=""></div><div class="we-comment"><div class="coment-head"><h5><a href="time-line.html" title="">Jason borne</a></h5><span>1 year ago</span><a class="we-reply" href="#" title="Reply"><i class="fa fa-reply"></i></a></div><p>' + comment + '</p></div></li>';
			$(comment_HTML).insertBefore(parent);
			jQuery(this).val('');
		}
	}); */

	jQuery(".newpst-input textarea").on("click", function (event) {

			jQuery(this).blur();
		jQuery('#New-box').focus().click();
		
	}); 
	
//inbox page 	
//***** Message Star *****//  
    $('.message-list > li > span.star-this').on("click", function(){
    	$(this).toggleClass('starred');
    });


//***** Message Important *****//
    $('.message-list > li > span.make-important').on("click", function(){
    	$(this).toggleClass('important-done');
    });

    

// Listen for click on toggle checkbox
	$('#select_all').on("click", function(event) {
	  if(this.checked) {
	      // Iterate each checkbox
	      $('input:checkbox.select-message').each(function() {
	          this.checked = true;
	      });
	  }
	  else {
	    $('input:checkbox.select-message').each(function() {
	          this.checked = false;
	      });
	  }
	});


	$(".delete-email").on("click",function(){
		$(".message-list .select-message").each(function(){
			  if(this.checked) {
			  	$(this).parent().slideUp();
			  }
		});
	});

// change background color on hover
	$('.category-box').hover(function () {
		$(this).addClass('selected');
		$(this).parent().siblings().children('.category-box').removeClass('selected');
	});
	
	
//------- offcanvas menu 

	const menu = document.querySelector('#toggle');  
	const menuItems = document.querySelector('#overlay');  
	const menuContainer = document.querySelector('.menu-container');  
	const menuIcon = document.querySelector('.canvas-menu i');  

	function toggleMenu(e) {
		menuItems.classList.toggle('open');
		menuContainer.classList.toggle('full-menu');
		menuIcon.classList.toggle('fa-bars');
		menuIcon.classList.add('fa-times');
		e.preventDefault();
	}

	if( menu ) {
		menu.addEventListener('click', toggleMenu, false);	
	}
	
// Responsive nav dropdowns
	$('.offcanvas-menu li.menu-item-has-children > a').on('click', function () {
		$(this).parent().siblings().children('ul').slideUp();
		$(this).parent().siblings().removeClass('active');
		$(this).parent().children('ul').slideToggle();
		$(this).parent().toggleClass('active');
		return false;
	});	
	


});//document ready end
var MaBDSP_TraoDoi = "";

//-------------------------------------------------------------
document.querySelectorAll('.truck-button').forEach(button => {
	button.addEventListener('click', e => {

		e.preventDefault();
		MaBDSP_TraoDoi = $(".truck-button").attr('data-id');
		alert(MaBDSP_TraoDoi);

		let box = button.querySelector('.box'),
			truck = button.querySelector('.truck');
		var MaND_TraoDoi = "";
		var URL_TraoDoi = "";
		var maNDKhac_TraoDoi = "";

		$('#MaND_TraoDoi')
			.keypress(function () {
				MaND_TraoDoi = $(this).val();

			})
			.keypress();

		$('#URL_TraoDoi')
			.keypress(function () {
				URL_TraoDoi = $(this).val();

			})
			.keypress();

		
		$('#maNDKhac_TraoDoi')
			.keypress(function () {
				maNDKhac_TraoDoi = $(this).val();

			})
			.keypress();

		var URLDelete_TraoDoi = "";


		$('#URLDelete_TraoDoi')
			.keypress(function () {
				URLDelete_TraoDoi = $(this).val();

			})
			.keypress();


		if (!button.classList.contains('done')) {
			
			
			if (!button.classList.contains('animation')) {
				
				var today = new Date();
				var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
				var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
				var dateTime = date + ' ' + time;

				var traoDoi = {};
				traoDoi.maND = MaND_TraoDoi;
				traoDoi.maNDKhac = maNDKhac_TraoDoi;
				traoDoi.maBDSP = MaBDSP_TraoDoi;
				traoDoi.ngayDangKyTD = dateTime;
				traoDoi.trangThai = "Đăng ký";

				$.ajax({
					type: "POST",
					url: URL_TraoDoi,
					data: JSON.stringify(traoDoi),
					contentType: 'application/json; charset=utf-8',
					dataType: 'json',
					success: function (r) {

					}
				});

				button.classList.add('animation');

				gsap.to(button, {
					'--box-s': 1,
					'--box-o': 1,
					duration: .3,
					delay: .5
				});

				gsap.to(box, {
					x: 0,
					duration: .4,
					delay: .7
				});

				gsap.to(button, {
					'--hx': -5,
					'--bx': 50,
					duration: .18,
					delay: .92
				});

				gsap.to(box, {
					y: 0,
					duration: .1,
					delay: 1.15
				});

				gsap.set(button, {
					'--truck-y': 0,
					'--truck-y-n': -26
				});

				gsap.to(button, {
					'--truck-y': 1,
					'--truck-y-n': -25,
					duration: .2,
					delay: 1.25,
					onComplete() {
						gsap.timeline({
							onComplete() {
								button.classList.add('done');
							}
						}).to(truck, {
							x: 0,
							duration: .4
						}).to(truck, {
							x: 40,
							duration: 1
						}).to(truck, {
							x: 20,
							duration: .6
						}).to(truck, {
							x: 96,
							duration: .4
						});
						gsap.to(button, {
							'--progress': 1,
							duration: 2.4,
							ease: "power2.in"
						});
					}
				});

			}

		} else {
			button.classList.remove('animation', 'done');
			
			var URLDelete_TraoDoi = "";
			

			$('#URLDelete_TraoDoi')
				.keypress(function () {
					URLDelete_TraoDoi = $(this).val();

				})
				.keypress();

			var traoDoi = {};
			traoDoi.maND = MaND_TraoDoi;
			traoDoi.maBDSP = MaBDSP_TraoDoi;
			

			$.ajax({
				type: "POST",
				url: URLDelete_TraoDoi,
				data: JSON.stringify(traoDoi),
				contentType: 'application/json; charset=utf-8',
				dataType: 'json',
				success: function (r) {

				}
			});

			gsap.set(truck, {
				x: 4
			});
			gsap.set(button, {
				'--progress': 0,
				'--hx': 0,
				'--bx': 0,
				'--box-s': .5,
				'--box-o': 0,
				'--truck-y': 0,
				'--truck-y-n': -26
			});
			gsap.set(box, {
				x: -24,
				y: -6
			});
		}

	});
});

//--------------------------------------------------------
document.querySelectorAll('.truck-button_1').forEach(button => {
	button.addEventListener('click', e => {
		e.preventDefault();

		let box = button.querySelector('.box'),
			truck = button.querySelector('.truck');
		var MaND_TraoDoi = "";
		var URL_TraoDoi = "";
		var MaBDSP_TraoDoi = "";
		var maNDKhac_TraoDoi = "";

		$('#MaND_TraoDoi')
			.keypress(function () {
				MaND_TraoDoi = $(this).val();

			})
			.keypress();

		$('#URL_TraoDoi')
			.keypress(function () {
				URL_TraoDoi = $(this).val();

			})
			.keypress();

		$('#MaBDSP_TraoDoi')
			.keypress(function () {
				MaBDSP_TraoDoi = $(this).val();

			})
			.keypress();

		$('#maNDKhac_TraoDoi')
			.keypress(function () {
				maNDKhac_TraoDoi = $(this).val();

			})
			.keypress();

		var URLDelete_TraoDoi = "";


		$('#URLDelete_TraoDoi')
			.keypress(function () {
				URLDelete_TraoDoi = $(this).val();

			})
			.keypress();

		if (!button.classList.contains('hihi')) {
			button.classList.add('hihi');

			var traoDoi = {};
			traoDoi.maND = MaND_TraoDoi;
			traoDoi.maBDSP = MaBDSP_TraoDoi;


			$.ajax({
				type: "POST",
				url: URLDelete_TraoDoi,
				data: JSON.stringify(traoDoi),
				contentType: 'application/json; charset=utf-8',
				dataType: 'json',
				success: function (r) {

				}
			});
			gsap.set(truck, {
				x: 4
			});
			gsap.set(button, {
				'--progress': 0,
				'--hx': 0,
				'--bx': 0,
				'--box-s': .5,
				'--box-o': 0,
				'--truck-y': 0,
				'--truck-y-n': -26
			});
			gsap.set(box, {
				x: -24,
				y: -6
			});
		}
		else {
			if (!button.classList.contains('done')) {

				if (!button.classList.contains('animation')) {


					button.classList.add('animation');

					var today = new Date();
					var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
					var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
					var dateTime = date + ' ' + time;

					var traoDoi = {};
					traoDoi.maND = MaND_TraoDoi;
					traoDoi.maNDKhac = maNDKhac_TraoDoi;
					traoDoi.maBDSP = MaBDSP_TraoDoi;
					traoDoi.ngayDangKyTD = dateTime;
					traoDoi.trangThai = "Đăng ký";

					$.ajax({
						type: "POST",
						url: URL_TraoDoi,
						data: JSON.stringify(traoDoi),
						contentType: 'application/json; charset=utf-8',
						dataType: 'json',
						success: function (r) {

						}
					});

					gsap.to(button, {
						'--box-s': 1,
						'--box-o': 1,
						duration: .3,
						delay: .5
					});

					gsap.to(box, {
						x: 0,
						duration: .4,
						delay: .7
					});

					gsap.to(button, {
						'--hx': -5,
						'--bx': 50,
						duration: .18,
						delay: .92
					});

					gsap.to(box, {
						y: 0,
						duration: .1,
						delay: 1.15
					});

					gsap.set(button, {
						'--truck-y': 0,
						'--truck-y-n': -26
					});

					gsap.to(button, {
						'--truck-y': 1,
						'--truck-y-n': -25,
						duration: .2,
						delay: 1.25,
						onComplete() {
							gsap.timeline({
								onComplete() {
									button.classList.add('done');
								}
							}).to(truck, {
								x: 0,
								duration: .4
							}).to(truck, {
								x: 40,
								duration: 1
							}).to(truck, {
								x: 20,
								duration: .6
							}).to(truck, {
								x: 96,
								duration: .4
							});
							gsap.to(button, {
								'--progress': 1,
								duration: 2.4,
								ease: "power2.in"
							});
						}
					});

				}

			}

			else {
				button.classList.remove('animation', 'done');

				var traoDoi = {};
				traoDoi.maND = MaND_TraoDoi;
				traoDoi.maBDSP = MaBDSP_TraoDoi;


				$.ajax({
					type: "POST",
					url: URLDelete_TraoDoi,
					data: JSON.stringify(traoDoi),
					contentType: 'application/json; charset=utf-8',
					dataType: 'json',
					success: function (r) {

					}
				});

				gsap.set(truck, {
					x: 4
				});
				gsap.set(button, {
					'--progress': 0,
					'--hx': 0,
					'--bx': 0,
					'--box-s': .5,
					'--box-o': 0,
					'--truck-y': 0,
					'--truck-y-n': -26
				});
				gsap.set(box, {
					x: -24,
					y: -6
				});
			}
		}

	});
});





