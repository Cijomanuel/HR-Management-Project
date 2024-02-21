var errorMessage = 'Username and Password can not be same.';
var errorElement = $('span[data-valmsg-for="SamePassword"]');

function CheckSameUsernamePassword() {
	if ($('#UserName').val() !== $('#Password').val()) {
		errorElement.addClass('field-validation-valid').removeClass('field-validation-error').text('');
	} else {
		errorElement.addClass('field-validation-error').removeClass('field-validation-valid').text(errorMessage);
	}
}

$('#UserName').change(CheckSameUsernamePassword);
$('#Password').change(CheckSameUsernamePassword);

$('#confirm-form').submit(function () {
	if (!$('#confirm-form').valid()) {
		return false;
	}

	if ($('#UserName').val() !== $('#Password').val()) {
		errorElement.addClass('field-validation-valid').removeClass('field-validation-error').text('');
	} else {
		errorElement.addClass('field-validation-error').removeClass('field-validation-valid').text(errorMessage);
		return false;
	}

	if ($('#UserName').val() != "") {
		$('#UserName').val(encrypt($('#UserName').val(), $('#RandomSeed').val()));
	}
	if ($('#Password').val() != "") {
		$('#Password').val(encrypt($('#Password').val(), $('#RandomSeed').val()));
		$('#ConfirmPassword').val($('#Password').val())
	}

	let submitButton = $('.submitbutton')[0];
	$('.submitbutton').attr("data-kt-indicator", "on");
	submitButton.disabled = true;
	setTimeout(function () {
		$('.submitbutton').removeAttr('data-kt-indicator'); submitButton.disabled = false;
	}, 80000);
});