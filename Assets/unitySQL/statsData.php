<?php
	$sqlconnection = new mysqli("localhost", "root", "", "multiplayer_game");
	if (!$sqlconnection) {
		die("failed to connect".mysqli_connect_error());
	}
	$user = mysqli_real_escape_string($sqlconnection, $_POST['username']);
	
	$query = "SELECT nbOfGames, timePlayed, nbOfKills, totalScore, bulletsShot FROM user_stats WHERE username = '$user'";
	$result = mysqli_query($sqlconnection, $query);
	
	if (mysqli_num_rows($result) == 1) {
		$row = mysqli_fetch_assoc($result);
		echo "username:".$user. "|nbOfGames:".$row['nbOfGames']."|timePlayed:".$row['timePlayed']."|nbOfKills:".$row['nbOfKills']."|totalScore:".$row['totalScore']."|bulletsShot:".$row['bulletsShot'].";";
	} else {
		echo "Error while getting user stats";
	}
?>