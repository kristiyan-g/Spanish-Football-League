CREATE SCHEMA IF NOT EXISTS spanish_football_league;

CREATE TABLE IF NOT EXISTS spanish_football_league.teams 
(
    team_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    weight FLOAT NOT NULL,
    color VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS spanish_football_league.matches 
(
    match_id SERIAL PRIMARY KEY,
    home_team_id INT NOT NULL REFERENCES spanish_football_league.teams(team_id),
    away_team_id INT NOT NULL REFERENCES spanish_football_league.teams(team_id),
    home_team_odd DECIMAL(3, 2) NOT NULL,
    away_team_odd DECIMAL(3, 2) NOT NULL,
    created_date TIMESTAMP WITH TIME ZONE NOT NULL
);

CREATE TABLE IF NOT EXISTS spanish_football_league.results 
(
    result_id SERIAL PRIMARY KEY,
    home_team_id INT NOT NULL REFERENCES spanish_football_league.teams(team_id),
    away_team_id INT NOT NULL REFERENCES spanish_football_league.teams(team_id),
    score VARCHAR(10),
    created_date TIMESTAMP WITH TIME ZONE NOT NULL
);

CREATE TABLE IF NOT EXISTS spanish_football_league.winners
(
    winner_id SERIAL PRIMARY KEY,
    season_id INT NOT NULL,
    winner_team_id INT NOT NULL REFERENCES spanish_football_league.teams(team_id),
    expected_win_percentage FLOAT NOT NULL,
    created_date TIMESTAMP WITH TIME ZONE NOT NULL
)
