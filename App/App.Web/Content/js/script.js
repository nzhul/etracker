"use strict"; // start of use strict

$(document).ready(function () {
	popup_login_init();
	Authenticate();
	ExtendingValidator();
})

//Login popup
function popup_login_init() {
	$(".top-login").on("click", function () {
		$(".login-popup").addClass("open");
	})
	$(".login-popup .close-button").on("click", function () {
		$(".login-popup").removeClass("open");
	})
}

function Authenticate() {
	var loginButton = $('.login-content a#login-button');
	loginButton.on('click', function () {

		var token = $('.login-content input[name=__RequestVerificationToken]').val();
		var email = $('.login-content input[name=email]').val();
		var password = $('.login-content input[name=password]').val();
		var statusLabel = $('.login-content span#login-status');

		if (email && password) {
			var data = {};
			data.Email = email;
			data.password = password;
			data.__RequestVerificationToken = token;

			var request = $.ajax({
				url: "/account/loginajax",
				type: 'POST',
				data: data,
				beforeSend: function () {
					statusLabel.css('display', 'block');
				}
			});

			request.done(function (response) {
				if (response.Status === 'Failure') {
					console.log('Login failed for some reason. Invalid email or non existing user');

					// Display error message
					statusLabel.removeClass();
					statusLabel.addClass('label label-danger');
					statusLabel.text('Login failed!');

				}

				if (response.Status === 'Success') {

					console.log('Successful login.');
					// Display success message and after 1-2 seconds delay -> close the login dialog

					statusLabel.removeClass();
					statusLabel.addClass('label label-success');
					statusLabel.text('Successful login!');

					setTimeout(function () {
						$(".login-popup").removeClass("open");
						location.reload();
					}, 1000);
				}

				if (response.Status === 'Locked') {
					console.log('Account is locked');

					// Display message for locked account: "Your account is temporary locked. Please contact the website administrator"
					statusLabel.removeClass();
					statusLabel.addClass('label label-danger');
					statusLabel.text('Заключен акаунт!');
				}

				if (response.Status === 'RequiresVerification') {
					console.log('The account requires email verification');
					// Display send activation email again button
				}
			});

			request.fail(function (jqXHR, textStatus) {
				console.log(jqXHR);
				console.log(textStatus);
			})
		}
	});
}

function ExtendingValidator() {
	// extend jquery range validator to work for required checkboxes
	var defaultRangeValidator = $.validator.methods.range;
	$.validator.methods.range = function (value, element, param) {
		if (element.type === 'checkbox') {
			// if it's a checkbox return true if it is checked
			return element.checked;
		} else {
			// otherwise run the default validation function
			return defaultRangeValidator.call(this, value, element, param);
		}
	}
}
