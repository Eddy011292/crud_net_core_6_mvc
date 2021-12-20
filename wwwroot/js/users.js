$(document).ready(function () {

	var success = $('#success').val();
	var error = $('#error').val();
	

	if (success != "empty") {
		Swal.fire({
			icon: 'success',
			title: success,
			showConfirmButton: false,
			timer: 1500,
			allowOutsideClick: false
		});
	}

	if (error != "empty") {
		Swal.fire({
			icon: 'error',
			title: error,
			allowOutsideClick: false
		});
	}
});