var tokenKey = "accessToken";
$('#submitLogin').click(function (e) {
	e.preventDefault();
	var authRequest = {
		Name: $('#emailLogin').val(),
		Password: $('#passwordLogin').val()
	};

	$.ajax({
		type: 'POST',
		url: '/api/Authentication',
		data: JSON.stringify(authRequest),
		contentType: "application/json; charset=utf-8",
		dataType: "json"
	}).success(function (data) {
		$('.userName').text(data.username);
		$('.userInfo').css('display', 'block');
		$('.loginForm').css('display', 'none');
		// сохраняем в хранилище sessionStorage токен доступа
		sessionStorage.setItem(tokenKey, data.access_token);
		console.log(data.access_token);
	}).fail(function (data) {
		console.log(data);
	});
});

$('#logOut').click(function (e) {
	e.preventDefault();
	$('.loginForm').css('display', 'block');
	$('.userInfo').css('display', 'none');
	sessionStorage.removeItem(tokenKey);
});

$('#getDataByLogin').click(function (e) {
	e.preventDefault();
	$.ajax({
		type: 'GET',
		url: '/api/values/getlogin',
		beforeSend: function (xhr) {

			var token = sessionStorage.getItem(tokenKey);
			xhr.setRequestHeader("Authorization", "Bearer " + token);
		},
		success: function (data) {
			alert(data);
		},
		fail: function (data) {
			console.log(data);
		}
	});
});
$('#getDataByRole').click(function (e) {
	e.preventDefault();
	$.ajax({
		type: 'GET',
		url: '/api/values/getrole',
		beforeSend: function (xhr) {

			var token = sessionStorage.getItem(tokenKey);
			xhr.setRequestHeader("Authorization", "Bearer " + token);
		},
		success: function (data) {
			alert(data);
		},
		fail: function (data) {
			console.log(data);
		}
	});
});