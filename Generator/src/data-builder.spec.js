const Builder = require('./data-builder');

describe('Builder.build', function () {

    it('Should return the correct rendering data', function () {

        const genericArgumentsExpected = 'T1, T2, T3, T4, TOut';
        const genericParametersDeclarationExpected = 'T1 arg1, T2 arg2, T3 arg3, T4 arg4';
        const genericParametersInvocationExpected = 'arg1, arg2, arg3, arg4';
        const genericTypesCountExpected = 4;

        const result = Builder.build(4);

        expect(result.genericArguments).to.equal(genericArgumentsExpected);
        expect(result.genericParametersDeclaration).to.equal(genericParametersDeclarationExpected);
        expect(result.genericParametersInvocation).to.equal(genericParametersInvocationExpected);
        expect(result.genericTypesCount).to.equal(genericTypesCountExpected);

    });

});