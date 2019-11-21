<?php
	$sqlconnection = mysqli_connect("localhost", "root", "", "multiplayer_game");
	if (mysqli_connect_errno()) {
		echo "failed to connect".mysqli_connect_error();
	}
	
	$user = mysqli_real_escape_string($sqlconnection, $_POST['username']);
	if (isset($user)) {
		$query = "INSERT INTO user_stats VALUES ('$user', 0, 0, 0, 0, 0)";
		$result = mysqli_query($sqlconnection, $query);
		if (!$result) {
			echo "User stats could not be created";
		} else {
			echo "User stats created";
		}
	}
?>