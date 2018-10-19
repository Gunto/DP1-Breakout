var TestSuite = require('node-unit-test');
var testSuite = new TestSuite();

var result = testSuite
	.addFile(__dirname + '/tests/addScore.js')
	.addFile(__dirname + '/tests/getScores.js')
	.execute();

process.exit(result ? 0 : 1);