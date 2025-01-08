INSERT INTO spanish_football_league.teams (name, weight, color)
SELECT * FROM (VALUES
    ('Real Madrid', 0, 'Red'),
    ('FC Barcelona', 0, 'Blue'),
    ('Atletico Madrid', 0, 'Green'),
    ('Sevilla FC', 0, 'Yellow'),
    ('Real Betis', 0, 'Pink'),
    ('Valencia CF', 0, 'Purple'),
    ('Athletic Bilbao', 0, 'Orange'),
    ('Real Sociedad', 0, 'Brown'),
    ('Villarreal CF', 0, 'Black'),
    ('Getafe CF', 0, 'White'),
    ('Espanyol', 0, 'Gray'),
    ('Levante UD', 0, 'Lime'),
    ('Granada CF', 0, 'Cyan'),
    ('Celta Vigo', 0, 'Magenta'),
    ('Mallorca', 0, 'Silver'),
    ('Alavés', 0, 'Teal'),
    ('Eibar', 0, 'Olive'),
    ('Elche CF', 0, 'Gold'),
    ('Rayo Vallecano', 0, 'Indigo'),
    ('Real Valladolid', 0, 'Lavender')
) AS new_data (name, weight, color)
WHERE NOT EXISTS (
    SELECT 1 FROM spanish_football_league.teams t
    WHERE t.name = new_data.name
);
