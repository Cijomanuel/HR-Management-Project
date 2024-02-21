$('#login-form').submit(function () {
	if (!$('#login-form').valid()) {
		return false;
	}
	var $input = $(this).find("input[name=Password]");
	if ($input.val() != "") {
		$input.val(encrypt($('#Password').val(), $('#RandomSeed').val()));
	}
	let submitButton = $('.submitbutton')[0];
	$('.submitbutton').attr("data-kt-indicator", "on");
	submitButton.disabled = true;
	setTimeout(function () {
		$('.submitbutton').removeAttr('data-kt-indicator'); submitButton.disabled = false;
	}, 80000);
});
$(document).ready(function () {
	alert('hi')
	var newKey = parseInt($('#Newkey').val());
	setTimeout(Reloadkey, newKey);
	function Reloadkey() {
		window.location.replace(window.location.href);
	}
});