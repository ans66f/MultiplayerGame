CREATE TABLE user_connection (
	username varchar(20) PRIMARY KEY,
	password varchar(50)
);

CREATE TABLE user_stats (
	username varchar(20),
	nbOfGames int,
	timePlayed int,
	nbOfKills int,
	totalScore int,
	bulletsShot int,
	PRIMARY KEY (username),
	FOREIGN KEY (username) REFERENCES user_connection(username)
);

CREATE TABLE game_scores (
	gameID int NOT NULL AUTO_INCREMENT,
	username varchar(20),
	game_score int,
	PRIMARY KEY (gameID),
	FOREIGN KEY (username) REFERENCES user_connection(username)
);