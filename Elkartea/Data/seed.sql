CREATE TABLE users (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  username TEXT NOT NULL,
  password TEXT NOT NULL,
  role TEXT NOT NULL
);

INSERT INTO users (username, password, role)
VALUES ('admin', 'admin123', 'admin'),
       ('usuario', 'user123', 'user');
