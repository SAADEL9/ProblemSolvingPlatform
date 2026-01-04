SELECT Count(*) as TotalProblems FROM Probleme;
SELECT Title, Count(*) as Count FROM Probleme GROUP BY Title HAVING Count(*) > 1;
