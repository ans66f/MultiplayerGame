<?php
	// 
	
	$sqlconnection = mysqli_connect("localhost", "root", "", "multiplayer_game");
	if (mysqli_connect_errno()) {
		echo "failed to connect".mysqli_connect_error();
	}
	
	$user = mysqli_real_escape_string($sqlconnection, $_POST['username']);
	$score = mysqli_real_escape_string($sqlconnection, $_POST['score']);
	
	if (isset($user) && isset($score)) {
		$query = "INSERT INTO game_scores (username, game_score) VALUES ('$user', $score)";
		$result = mysqli_query($sqlconnection, $query);
		if (!$result) {
			echo "Game score could not be saved";
		} else {
			echo "Game score saved";
		}
	}
?>