const Builder = require('./data-builder');

describe('Builder.buildClassData', function () {

    it('Should return the correct rendering data', function () {

        const genericArgumentsExpected = 'T1, T2, T3, T4, TOut';
        const genericParametersDeclarationExpected = 'T1 arg1, T2 arg2, T3 arg3, T4 arg4';
        const genericParametersInvocationExpected = 'arg1, arg2, arg3, arg4';
        const genericTypesCountExpected = 4;

        const result = Builder.buildClassData(4);

        expect(result.genericArguments).to.equal(genericArgumentsExpected);
        expect(result.genericParametersDeclaration).to.equal(genericParametersDeclarationExpected);
        expect(result.genericParametersInvocation).to.equal(genericParametersInvocationExpected);
        expect(result.genericTypesCount).to.equal(genericTypesCountExpected);

    });

});

describe('Builder.buildTestData', function () {

    it('Should return the correct rendering data', function () {

        const valueTypeFuncGenericParametersExpected = 'int, int, int, int, int';
        const referenceTypeFuncGenericParametersExpected = 'object, object, object, object, int';
        const firstRunParametersExpected = '0, 1, 2, 3';
        const secondRunParametersExpected = '10, 11, 12, 13';
        const referenceTypeParametersExpected = 'null, null, null, null';
        const genericTypesCountExpected = 4;

        const result = Builder.buildTestData(4);

        expect(result.valueTypeFuncGenericParameters).to.equal(valueTypeFuncGenericParametersExpected);
        expect(result.referenceTypeFuncGenericParameters).to.equal(referenceTypeFuncGenericParametersExpected);
        expect(result.firstRunParameters).to.equal(firstRunParametersExpected);
        expect(result.secondRunParameters).to.equal(secondRunParametersExpected);
        expect(result.referenceTypeParameters).to.equal(referenceTypeParametersExpected);
        expect(result.genericTypesCount).to.equal(genericTypesCountExpected);

    });

});