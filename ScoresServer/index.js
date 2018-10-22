const express = require('express');
const bodyParser = require('body-parser');
const fs = require('fs');

const app = express();

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

const validName = (name) => /^[A-Z]{3}$/.test(name);
const validScore = (score) => !isNaN(score) && Number(score) > 0;
const loadScores = () => JSON.parse(fs.readFileSync(__dirname + '/scores.json')).scores;
const saveScores = (scores) => {
	let scoreObj = { scores: scores };
	fs.writeFile(__dirname + '/scores.json', JSON.stringify(scoreObj, null, 1), (err) => {
		if (err) throw err;
		console.log('Scores written to disk (scores.json).');
	});
};
const sortScores = (scores) => scores.slice(0, 10).sort((a, b) => b.score - a.score);
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

app.get('/', (req, res) => {
	res.json(sortScores(loadScores()));
});

app.post('/', (req, res) => {
	let name = req.name;
	let score = req.score;
	addScore(name, score);
});

app.listen(11000, () => {
	console.log('Breakout scores server listening on port 11000');
});

module.exports.sortScores = sortScores;
module.exports.loadScores = loadScores;
module.exports.validName = validName;
module.exports.validScore = validScore;
module.exports.addScore = addScore;