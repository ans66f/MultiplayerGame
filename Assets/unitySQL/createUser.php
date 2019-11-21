<?php
	// This program creates a user with given username and password
	$sqlconnection = mysqli_connect("localhost", "root", "", "multiplayer_game");
	if (mysqli_connect_errno()) {
		echo "failed to connect".mysqli_connect_error();
	}
	
	$user = mysqli_real_escape_string($sqlconnection, $_POST['username']);
	$pass = mysqli_real_escape_string($sqlconnection, $_POST['password']);
	
	if (isset($user) && isset($pass)) {
		$query = "INSERT INTO user_connection (username, password) VALUES ('$user', '$pass')";
		$result = mysqli_query($sqlconnection, $query);
		if (!$result) {
			echo "User could not be created";
		} else {
			echo "User created";
		}
	}
?>