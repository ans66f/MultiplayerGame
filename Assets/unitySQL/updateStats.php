<?php
	$sqlconnection = new mysqli("localhost", "root", "", "multiplayer_game");
	if (!$sqlconnection) {
		die("failed to connect".mysqli_connect_error());
	}
	$user = mysqli_real_escape_string($sqlconnection, $_POST['username']);
	$nbOfGames = mysqli_real_escape_string($sqlconnection, $_POST['nbOfGames']);
	$timePlayed = mysqli_real_escape_string($sqlconnection, $_POST['timePlayed']);
	$nbOfKills = mysqli_real_escape_string($sqlconnection, $_POST['nbOfKills']);
	$totalScore = mysqli_real_escape_string($sqlconnection, $_POST['totalScore']);
	$bulletsShot = mysqli_real_escape_string($sqlconnection, $_POST['bulletsShot']);
	
	$query = "SELECT nbOfGames, timePlayed, nbOfKills, totalScore, bulletsShot FROM user_stats WHERE username = '$user'";
	$result = mysqli_query($sqlconnection, $query);
	
	if (mysqli_num_rows($result) == 1) {
		$row = mysqli_fetch_assoc($result);
		
		$nbOfGames += $row['nbOfGames'];
		$timePlayed += $row['timePlayed'];
		$nbOfKills += $row['nbOfKills'];
		$totalScore += $row['totalScore'];
		$bulletsShot += $row['bulletsShot'];
		
		$query = "UPDATE user_stats SET nbOfGames = $nbOfGames, timePlayed = $timePlayed, nbOfKills = $nbOfKills, totalScore = $totalScore, bulletsShot = $bulletsShot WHERE username = '$user'";
		$result = mysqli_query($sqlconnection, $query);
		
		if (!$result) {
			echo "Could not update stats";
		} else {
			echo "User stats updated";
		}
	} else {
		echo "Error while getting current user stats";
	}
?>