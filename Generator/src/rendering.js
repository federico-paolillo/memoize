const Mustache = require('mustache');
const Builder = require('./data-builder');
const Utils = require('util');
const FS = require('fs');

const readFileAsync = Utils.promisify(FS.readFile);

const templatePath = 'src/templates/Memoizer_X.mustache';

/**
 * Asynchronously renders a Memoizer class with the number of parameters specified.
 * @param {number} genericTypesCount Number of generic arguments of the C# class generated.
 */
exports.renderMemoizerClassAsync = async function (genericTypesCount) {

    const template = await readFileAsync(templatePath, 'utf8');
    const data = Builder.build(genericTypesCount);

    const result = Mustache.render(template, data);

    return result;

}