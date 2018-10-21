const app = require('../index.js');

module.exports = function () {
	return {
		data_validName: () => ['YES', 'ABC', 'XYZ'],
		data_invalidName: () => ['Noo', '112', '4BC', '!@#', 'AB'],
		data_validScore: () => [1, 100, 200, '1', '100', '200'],
		data_invalidScore: () => [0, -1, 'one hundred'],
		data_addScore: () => [{ name: 'AAA', score: 10 }],

		test_validName: (assert, data) => {
			assert.assertTrue(app.validName(data));
		},
		test_invalidName: (assert, data) => {
			assert.assertFalse(app.validName(data));
		},
		test_validScore: (assert, data) => {
			assert.assertTrue(app.validScore(data));
		},
		test_invalidScore: (assert, data) => {
			assert.assertFalse(app.validScore(data));
		},
		test_addScore: (assert, data) => {
			assert.assertTrue(app.addScore(data.name, data.score));
		}
	};
};