module.exports = function (wallaby) {

    return {

        name: 'Memoizer C# classes generator',
        testFramework: 'mocha',
        files: [
            'src/**/*.js',
            '!src/**/*.spec.js',
        ],
        tests: [
            'src/**/*.spec.js'
        ],
        env: {
            type: 'node',
            runner: 'node'
        },
        setup: function () {
            global.expect = require('chai').expect;
        },
    }

};