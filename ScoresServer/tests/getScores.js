const app = require('../index.js');

module.exports = function () {
	return {
		data_scoreOrder: () => [
			[{ name: 'BBB', score: 20 },
			{ name: 'AAA', score: 30 },
			{ name: 'CCC', score: 10 }]
		],
		data_topTen: () => [
			[{ name: 'AAA', score: 110 },
			{ name: 'BBB', score: 100 },
			{ name: 'CCC', score: 90 },
			{ name: 'DDD', score: 80 },
			{ name: 'EEE', score: 70 },
			{ name: 'FFF', score: 60 },
			{ name: 'GGG', score: 50 },
			{ name: 'HHH', score: 40 },
			{ name: 'III', score: 30 },
			{ name: 'JJJ', score: 20 },
			{ name: 'KKK', score: 10 }]
		],

		test_scoreOrder: (assert, data) => {
			assert.assertEquals(JSON.stringify([
				{ name: 'AAA', score: 30 },
				{ name: 'BBB', score: 20 },
				{ name: 'CCC', score: 10 },
			]), JSON.stringify(app.sortScores(data)));
		},
		test_topTen: (assert, data) => {
			assert.assertEquals(10, app.sortScores(data).length);
		}
	};
};