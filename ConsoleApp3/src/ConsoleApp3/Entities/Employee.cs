using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ConsoleApp3
{
    public class Employee
    {
        public int EmpId { get; set; }

        public void MethodFromGoogle()
        {
            var list = Directory.EnumerateFiles(Path.GetDirectoryName(@"D:\HyperExamples\ConsoleApp3\src\ConsoleApp3\Entities\"));
            var exts = Directory.EnumerateFiles(Path.GetDirectoryName(@"D:\HyperExamples\ConsoleApp3\src\ConsoleApp3\Entities\")).Where(x => Path.GetExtension(x) == ".cs");
            var text = exts.Select(x => CSharpSyntaxTree.ParseText(File.ReadAllText(x))).Cast<CSharpSyntaxTree>();
            foreach (CSharpSyntaxTree syntaxTree in text)
            {
                var root = (CompilationUnitSyntax)syntaxTree.GetRoot();
                var sd = syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();

                foreach (var item in sd)
                {
                    var fullClassName = item.Identifier.ToString();
                }
                var k = sd.Where(x => x.BaseList != null && x.BaseList.Types.Any(y => y.Type is Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax));
                //        && ( (Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax)y.Type ).Identifier.Text == "Module"));
                // Get all class declarations in each file that derive from Module
                foreach (ClassDeclarationSyntax classDeclaration in syntaxTree.GetRoot()
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .Where(x => x.BaseList != null && x.BaseList.Types
                        .Any(y => y.Type is IdentifierNameSyntax
                            && ( (IdentifierNameSyntax)y.Type ).Identifier.Text == "Module")))
                {
                    // Output the same namespace as the class
                    SyntaxNode namespaceNode = classDeclaration.Parent;
                    while (namespaceNode != null && !( namespaceNode is NamespaceDeclarationSyntax ))
                    {
                        namespaceNode = namespaceNode.Parent;
                    }
                    if (namespaceNode != null)
                    {
                        Console.WriteLine("namespace " + ( (NamespaceDeclarationSyntax)namespaceNode ).Name.ToString() + Environment.NewLine + "{");
                    }

                    // Output the extensions class
                    Console.WriteLine("    public static class " + classDeclaration.Identifier.Text + "PipelineExtensions" + Environment.NewLine + "    {");

                    // Get all non-static public constructors
                    foreach (ConstructorDeclarationSyntax constructor in classDeclaration.Members
                        .OfType<ConstructorDeclarationSyntax>()
                        .Where(x => x.Modifiers.Count == 1 && x.Modifiers[0].Text == "public"))
                    {
                        // Output the static constructor method
                        Console.WriteLine("        public static IPipeline " + classDeclaration.Identifier.Text + constructor.ParameterList.ToString().Insert(1, "this IPipeline pipeline, ") + Environment.NewLine + "        {");

                        // Create and add the module
                        Console.WriteLine("            return pipeline.AddModule(new " + classDeclaration.Identifier.Text + "(" + string.Join(", ", constructor.ParameterList.Parameters.Select(x => x.Identifier.Text)) + "));");

                        // Close method
                        Console.WriteLine("        }");
                    }

                    // Close extensions class
                    Console.WriteLine("    }");

                    // Close namespace
                    if (namespaceNode != null)
                    {
                        Console.WriteLine("}");
                    }
                }
            }
        }
    }
}
