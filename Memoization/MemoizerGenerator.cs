using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Memoization;

[Generator]
public sealed class MemoizerGenerator : ISourceGenerator
{
    private const int MAX_GENERIC_PARAMETERS = 16;

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
        
    }
    
    private static void GenerateStaticMemoizerOverloads(GeneratorExecutionContext context)
    {
        for (var numberOfParameters = 1; numberOfParameters < MAX_GENERIC_PARAMETERS + 1; numberOfParameters++)
        {
            GenerateMemoizerOverload(context, numberOfParameters);
        }
    }

    private static void GenerateMemoizerOverload(GeneratorExecutionContext context, int numberOfParameters)
    {
        var genericParametersList =
            string.Join(", ", Enumerable.Range(1, numberOfParameters).Select(pIndex => $"T{pIndex:D}"));

        var sourceSb = new StringBuilder();

        sourceSb.AppendLine("using System;");

        sourceSb.AppendLine("namespace Memoization;");

        sourceSb.AppendLine(@$"public static partial class Memoizer {{
        
            public static void Memoize<{genericParametersList}, TOut>(Func<{genericParametersList}, TOut> fn) {{

                

            }}    
            
        }}");

        var sourceText = SourceText.From(sourceSb.ToString(), Encoding.UTF8);

        context.AddSource($"Memoizer.{numberOfParameters:D2}.g.cs", sourceText);
    }
}