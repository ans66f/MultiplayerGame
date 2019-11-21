<?php
	// This program gets top 5 scores for a game
	$sqlconnection = new mysqli("localhost", "root", "", "multiplayer_game");
	if (!$sqlconnection) {
		die("failed to connect".mysqli_connect_error());
	}
	
	$query = "SELECT username, game_score FROM game_scores ORDER BY game_score DESC LIMIT 5";
	$result = mysqli_query($sqlconnection, $query);
	
	if (mysqli_num_rows($result) > 0) {
		while ($row = mysqli_fetch_assoc($result)) {
			echo "username:".$row['username']. "|score:".$row['game_score'].";";
		}
	}
?>