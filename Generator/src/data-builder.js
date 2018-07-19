function getGenericArgumentsString(genericTypesCount) {

    const temporaryList = [];

    for (let index = 0; index < genericTypesCount; index++) {
        temporaryList.push(`T${index + 1}`);
    }

    temporaryList.push('TOut');

    const result = temporaryList.join(', ');

    return result;
}

function getGenericParametersDeclarationString(genericTypesCount) {

    const temporaryList = [];

    for (let index = 0; index < genericTypesCount; index++) {
        temporaryList.push(`T${index + 1} arg${index + 1}`);
    }

    const result = temporaryList.join(', ');

    return result;

}

function getGenericParametersInvocationString(genericTypesCount) {

    const temporaryList = [];

    for (let index = 0; index < genericTypesCount; index++) {
        temporaryList.push(`arg${index + 1}`);
    }

    const result = temporaryList.join(', ');

    return result;

}

function getValueTypeFuncGenericParametersString(genericTypesCount) {

    const temporaryList = [];

    for (let index = 0; index < genericTypesCount; index++) {
        temporaryList.push(`int`);
    }

    temporaryList.push(`int`);

    const result = temporaryList.join(', ');

    return result;

}

function getReferenceTypeFuncGenericParametersString(genericTypesCount) {

    const temporaryList = [];

    for (let index = 0; index < genericTypesCount; index++) {
        temporaryList.push(`object`);
    }

    temporaryList.push(`int`);

    const result = temporaryList.join(', ');

    return result;

}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function getFirstRunParametersString(genericTypesCount) {

    const temporaryList = [];

    for (let index = 0; index < genericTypesCount; index++) {
        temporaryList.push(index);
    }

    const result = temporaryList.join(', ');

    return result;

}

function getSecondRunParametersString(genericTypesCount) {

    const temporaryList = [];

    for (let index = 0; index < genericTypesCount; index++) {
        temporaryList.push(index + 10);
    }

    const result = temporaryList.join(', ');

    return result;

}

function getReferenceTypeParametersString(genericTypesCount) {

    const temporaryList = [];

    for (let index = 0; index < genericTypesCount; index++) {
        temporaryList.push('null');
    }

    const result = temporaryList.join(', ');

    return result;

}

/**
 * Prepares all the data required to generate a memoizer C# class with Mustache.
 * @param {number} genericArgumentsCount Number of generic arguments of the C# class generated.
 */
exports.buildClassData = function (genericTypesCount) {

    return {

        genericArguments: getGenericArgumentsString(genericTypesCount),
        genericParametersDeclaration: getGenericParametersDeclarationString(genericTypesCount),
        genericParametersInvocation: getGenericParametersInvocationString(genericTypesCount),
        genericTypesCount

    };

}

exports.buildTestData = function (genericTypesCount) {

    return {

        referenceTypeFuncGenericParameters: getReferenceTypeFuncGenericParametersString(genericTypesCount),
        valueTypeFuncGenericParameters: getValueTypeFuncGenericParametersString(genericTypesCount),
        firstRunParameters: getFirstRunParametersString(genericTypesCount),
        secondRunParameters: getSecondRunParametersString(genericTypesCount),
        referenceTypeParameters: getReferenceTypeParametersString(genericTypesCount),
        genericTypesCount

    };

}