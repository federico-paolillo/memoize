using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Memoization;

[Generator]
public sealed class MemoizerGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var sourceSb = new StringBuilder();

        sourceSb.AppendLine("using System;");
        
        sourceSb.AppendLine("namespace Memoization;");
        
        sourceSb.AppendLine(@"public static class Memoizer {
            public static void Memoize<T1, TOut>(Func<T1, TOut> fn) {
            }
        }");
        
        context.AddSource("memoizer.g.cs", SourceText.From(sourceSb.ToString(), Encoding.UTF8));
    }
}