using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Memoization;

[Generator]
public sealed class MemoizerGenerator : ISourceGenerator
{
    private const int MAX_GENERIC_PARAMETERS = 1;

    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        GenerateMemoizerClasses(context);
        GenerateStaticMemoizerOverloads(context);
    }

    private static void GenerateMemoizerClasses(GeneratorExecutionContext context)
    {
        for (var numberOfParameters = 1; numberOfParameters < MAX_GENERIC_PARAMETERS + 1; numberOfParameters++)
        {
            GenerateMemoizerClass(context, numberOfParameters);
        }
    }

    private static void GenerateStaticMemoizerOverloads(GeneratorExecutionContext context)
    {
        for (var numberOfParameters = 1; numberOfParameters < MAX_GENERIC_PARAMETERS + 1; numberOfParameters++)
        {
            GenerateMemoizerOverload(context, numberOfParameters);
        }
    }

    private static void GenerateMemoizerClass(GeneratorExecutionContext context, int numberOfParameters)
    {
        var sourceSb = new StringBuilder();

        var genericParametersList = GenerateGenericParametersList(numberOfParameters);
        var genericArgumentsList = GenerateGenericArgumentsList(numberOfParameters);
        var genericFieldsList = GenerateGenericFieldsList(numberOfParameters);
        var genericFnInvocationArgumentsList = GenerateGenericFnInvocationArgumentsList(numberOfParameters);
        var fieldsResetCode = GenerateFieldsResetCode(numberOfParameters);
        var fieldsTestCode = GenerateFieldsTestCode(numberOfParameters);

        sourceSb.AppendLine("using System;");

        sourceSb.AppendLine("namespace Memoization;");

        sourceSb.AppendLine(@$"public sealed class Memoizer<{genericParametersList}, TOut> {{

            private readonly Func<{genericParametersList}, TOut> fn;

            private bool isNotMemoizing = true;

            private TOut lastResult = default;

            {genericFieldsList}

            internal Memoizer(Func<{genericParametersList}, TOut> fn) {{
            
                this.fn = fn ?? throw new ArgumentNullException(nameof(fn));
            
            }}

            public TOut Call({genericArgumentsList}) {{

                if (isNotMemoizing) {{

                    isNotMemoizing = false;
                
                }}

                var argsChanged = false;

                {fieldsTestCode}

                if (argsChanged) {{
                
                    var newResult = fn({genericFnInvocationArgumentsList});

                    lastResult = newResult;
                
                }}
                
                return lastResult;

            }}

            public static implicit operator Func<{genericParametersList}, TOut>(Memoizer<{genericParametersList}, TOut> memoizer) {{
                return memoizer.Call;
            }}

            public void Reset() {{
                
                isNotMemoizing = true;

                lastResult = default;
                
                {fieldsResetCode}
            }}

        }}");

        var sourceText = SourceText.From(sourceSb.ToString(), Encoding.UTF8);

        context.AddSource($"Memoizer.{numberOfParameters:D2}.class.g.cs", sourceText);
    }

    private static void GenerateMemoizerOverload(GeneratorExecutionContext context, int numberOfParameters)
    {
        var genericParametersList = GenerateGenericParametersList(numberOfParameters);

        var sourceSb = new StringBuilder();

        sourceSb.AppendLine("using System;");

        sourceSb.AppendLine("namespace Memoization;");

        sourceSb.AppendLine(@$"public static partial class Memoizer {{
        
            public static Memoizer<{genericParametersList}, TOut> Memoize<{genericParametersList}, TOut>(Func<{genericParametersList}, TOut> fn) {{

                return new Memoizer<{genericParametersList}, TOut>(fn);

            }}    
            
        }}");

        var sourceText = SourceText.From(sourceSb.ToString(), Encoding.UTF8);

        context.AddSource($"Memoizer.{numberOfParameters:D2}.static.g.cs", sourceText);
    }

    private static string GenerateGenericParametersList(int numberOfParameters)
    {
        var genericParametersList =
            string.Join(", ", Enumerable.Range(1, numberOfParameters).Select(pIndex => $"T{pIndex:D}"));

        return genericParametersList;
    }

    private static string GenerateGenericArgumentsList(int numberOfParameters)
    {
        var genericArgumentsList =
            string.Join(", ", Enumerable.Range(1, numberOfParameters).Select(pIndex => $"T{pIndex:D} param{pIndex:D}"));

        return genericArgumentsList;
    }

    private static string GenerateGenericFieldsList(int numberOfParameters)
    {
        var genericFieldsListSb = new StringBuilder();

        for (int fieldIndex = 1; fieldIndex < numberOfParameters + 1; fieldIndex++)
        {
            genericFieldsListSb.AppendLine($"private T{fieldIndex:D} param{fieldIndex:D} = default;");
        }

        return genericFieldsListSb.ToString();
    }

    private static string GenerateGenericFnInvocationArgumentsList(int numberOfParameters)
    {
        var fnArgumentsList =
            string.Join(", ", Enumerable.Range(1, numberOfParameters).Select(pIndex => $"param{pIndex:D}"));

        return fnArgumentsList;
    }

    private static string GenerateFieldsResetCode(int numberOfParameters)
    {
        var genericFieldsListSb = new StringBuilder();

        for (int fieldIndex = 1; fieldIndex < numberOfParameters + 1; fieldIndex++)
        {
            genericFieldsListSb.AppendLine($"param{fieldIndex:D} = default;");
        }

        return genericFieldsListSb.ToString();
    }

    private static string GenerateFieldsTestCode(int numberOfParameters)
    {
        var fieldsTestCode = new StringBuilder();

        for (int fieldIndex = 1; fieldIndex < numberOfParameters + 1; fieldIndex++)
        {
            fieldsTestCode.AppendLine(@"
                if (ParametersEquality.AreNotEqual(this.param1, param1)) {{
                    this.param1 = param1;
                    argsChanged = true;
                }}
            ");
        }

        return fieldsTestCode.ToString();
    }
}
