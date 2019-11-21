<?php
	$sqlconnection = mysqli_connect("localhost", "root", "", "multiplayer_game");
	if (mysqli_connect_errno()) {
		echo "failed to connect".mysqli_connect_error();
	}
	
	$user = mysqli_real_escape_string($sqlconnection, $_POST['username']);
	$pass = mysqli_real_escape_string($sqlconnection, $_POST['password']);
	
	if (isset($user) && isset($pass)) {
		$query = "SELECT password FROM user_connection WHERE username = '$user'";
		$result = mysqli_query($sqlconnection, $query);
		
		if (mysqli_num_rows($result) == 1) {
			$row = mysqli_fetch_assoc($result);
			$testpass = $row['password'];
			if ($testpass == $pass) {
				echo "Password ok";
			} else {
				echo "Wrong password";
			}	
		} elseif (mysqli_num_rows($result) == 0) {
			echo "No such username ('$user')";
			
		} else {
			echo "Error";
		}
	}
?>