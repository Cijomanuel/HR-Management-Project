$('#reset-form').submit(function () {
	if (!$('#reset-form').valid()) {
		return false;
	}
	if ($('#Password').val() != "") {
		$('#Password').val(encrypt($('#Password').val(), $('#RandomSeed').val()));
		$('#ConfirmPassword').val($('#Password').val());
	}
	let submitButton = $('.submitbutton')[0];
	$('.submitbutton').attr("data-kt-indicator", "on");
	submitButton.disabled = true;
	setTimeout(function () {
		$('.submitbutton').removeAttr('data-kt-indicator'); submitButton.disabled = false;
	}, 80000);
});