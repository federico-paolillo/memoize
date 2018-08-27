function getGenericArgumentsString(genericTypesCount) {

    return [
        ...Array(genericTypesCount).fill().map((_, i) => `T${i + 1}`),
        'TOut'
    ].join(', ');

}

function getGenericParametersDeclarationString(genericTypesCount) {
    return Array(genericTypesCount).fill().map((_, i) => `T${i + 1} arg${i + 1}`).join(', ');
}

function getGenericParametersInvocationString(genericTypesCount) {
    return Array(genericTypesCount).fill().map((_, i) => `arg${i + 1}`).join(', ');
}

function getValueTypeFuncGenericParametersString(genericTypesCount) {
    return Array(genericTypesCount + 1).fill('int').join(', ');
}

function getReferenceTypeFuncGenericParametersString(genericTypesCount) {

    return [
        ...Array(genericTypesCount).fill('object'),
        'int'
    ].join(', ');

}

function getFirstRunParametersString(genericTypesCount) {
    return Array(genericTypesCount).fill().map((_, i) => i).join(', ');
}

function getSecondRunParametersString(genericTypesCount) {
    return Array(genericTypesCount).fill().map((_, i) => 10 + i).join(', ');
}

function getReferenceTypeParametersString(genericTypesCount) {
    return Array(genericTypesCount).fill('null').join(', ');
}

function lmao(pattern, repetitions) {
    return Array(repetitions).fill(pattern).join(', ');
}

function getItIsAnysSequence(genericTypesCount) {
    return Array(genericTypesCount).fill('It.IsAny<string>()').join(', ');
}

function getStringTypeParametersString(genericTypesCount) {
    return Array(genericTypesCount).fill('string').join(', ');
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

/**
 * Prepares all the data required to generate a memoizer test C# class with Mustache.
 * @param {number} genericArgumentsCount Number of generic arguments of the C# class generated.
 */
exports.buildTestData = function (genericTypesCount) {

    return {

        referenceTypeFuncGenericParameters: getReferenceTypeFuncGenericParametersString(genericTypesCount),
        valueTypeFuncGenericParameters: getValueTypeFuncGenericParametersString(genericTypesCount),
        firstRunParameters: getFirstRunParametersString(genericTypesCount),
        secondRunParameters: getSecondRunParametersString(genericTypesCount),
        referenceTypeParameters: getReferenceTypeParametersString(genericTypesCount),
        stringTypeParameters: getStringTypeParametersString(genericTypesCount + 1),
        itIsAnysSequence: getItIsAnysSequence(genericTypesCount),
        equalityComparerCallsCount: genericTypesCount * 2,
        ayYyy: lmao("\"ayYyy\"", genericTypesCount),
        ayYYyy: lmao("\"ayYYyy\"", genericTypesCount),
        ayYYYy: lmao("\"ayYYYy\"", genericTypesCount),
        genericTypesCount

    };

}