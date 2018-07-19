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

/**
 * Prepares all the data required to generate a memoizer C# class with Mustache.
 * @param {number} genericArgumentsCount Number of generic arguments of the C# class generated.
 */
exports.build = function (genericTypesCount) {

    return {

        genericArguments: getGenericArgumentsString(genericTypesCount),
        genericParametersDeclaration: getGenericParametersDeclarationString(genericTypesCount),
        genericParametersInvocation: getGenericParametersInvocationString(genericTypesCount),
        genericTypesCount

    }

}