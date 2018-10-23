// Import and initialize modules
const express = require('express');
const bodyParser = require('body-parser');
const fs = require('fs');

// Setup the express routing instance
const app = express();

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

// Returns whether the provided name matches the pattern of 3 alphabetic characters
const validName = (name) => /^[A-Z]{3}$/.test(name);
// Returns whether the provided score is a number above zero
const validScore = (score) => !isNaN(score) && Number(score) > 0;
// Reads the contents of the disc and parses the JSON data into a JSON object
const loadScores = () => JSON.parse(fs.readFileSync(__dirname + '/scores.json')).scores;
// Writes the JSON object containing a scores array to the disk
const saveScores = (scores) => {
	let scoreObj = { scores: scores };
	fs.writeFile(__dirname + '/scores.json', JSON.stringify(scoreObj, null, 1), (err) => {
		if (err) throw err;
		console.log('Scores written to disk (scores.json).');
	});
};
// Sorts the scores from largest to smallest and returns the top ten as a JSON array
const sortScores = (scores) => scores.sort((a, b) => b.score - a.score).slice(0, 10);
// Accepts a name and score, validates them and adds the record to the data store
const addScore = (name, score) => {
	if (!validName(name)) return false;
	if (!validScore(score)) return false;

	let scores = loadScores();
	scores.push({
		name: name,
		score: score
	});
	saveScores(scores);
	console.log(`=> Score of ${score} added for '${name}'`);
	return true;
};

// GET route that returns the top ten scores
app.get('/', (req, res) => {
	res.json(sortScores(loadScores()));
});

// POST route that accepts multi-part form data containing a name and score
app.post('/', (req, res) => {
	if (req.body === undefined) {
		res.json({ success: false });
		return;
	}
	let name = req.body.name;
	let score = req.body.score;
	res.json({ success: addScore(name, score) });
});

// Starts the server
app.listen(11000, () => {
	console.log('Breakout scores server listening on port 11000');
});

// Export functionality (used for testing)
module.exports.sortScores = sortScores;
module.exports.loadScores = loadScores;
module.exports.validName = validName;
module.exports.validScore = validScore;
module.exports.addScore = addScore;
