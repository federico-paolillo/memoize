const Mustache = require('mustache');
const Builder = require('./data-builder');
const Utils = require('util');
const FS = require('fs');

const readFileAsync = Utils.promisify(FS.readFile);

const classTemplatePath = 'src/templates/Memoizer_X.mustache';
const testTemplatePath = 'src/templates/Memoizer_X_Test.mustache';

/**
 * Asynchronously renders a Memoizer class with the number of parameters specified.
 * @param {number} genericTypesCount Number of generic arguments of the C# class generated.
 */
exports.renderMemoizerClassAsync = async function (genericTypesCount) {

    const template = await readFileAsync(classTemplatePath, 'utf8');
    const data = Builder.buildClassData(genericTypesCount);

    const result = Mustache.render(template, data);

    return result;

}

/**
 * Asynchronously renders a Memoizer test class with the number of parameters specified.
 * @param {number} genericTypesCount Number of generic arguments of the C# class generated.
 */
exports.renderMemoizerTestAsync = async function (genericTypesCount) {

    const template = await readFileAsync(testTemplatePath, 'utf8');
    const data = Builder.buildTestData(genericTypesCount);

    const result = Mustache.render(template, data);

    return result;

}