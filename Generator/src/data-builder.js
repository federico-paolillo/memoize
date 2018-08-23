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