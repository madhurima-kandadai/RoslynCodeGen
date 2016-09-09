using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ConsoleApp3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> classNames = new List<string>();
            var list = Directory.EnumerateFiles(Path.GetDirectoryName(@"Entities\")).Where(x => Path.GetExtension(x) == ".cs");
            var text = list.Select(x => CSharpSyntaxTree.ParseText(File.ReadAllText(x))).Cast<CSharpSyntaxTree>();
            foreach (CSharpSyntaxTree syntaxTree in text)
            {
                var root = (CompilationUnitSyntax)syntaxTree.GetRoot();
                var sd = syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
                foreach (var item in sd)
                {
                    classNames.Add(item.Identifier.ToString());
                }
            }

            foreach (var item in classNames)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        public static void CreateClass(string ClassName)
        {
            //SF Syntax Factory
            //SyntaxFactory SF;
            //CompilationUnitSyntax cu = SF.CompilationUnit();
//            CompilationUnitSyntax cu = SF.CompilationUnit()
//    .AddUsings(SF.UsingDirective(SF.IdentifierName("System")))
//    .AddUsings(SF.UsingDirective(SF.IdentifierName("System.Generic")))
//    ;
//            cu.AddUsings(SF.UsingDirective(SF.IdentifierName("System")));
//cu = cu.AddUsings(SF.UsingDirective(SF.IdentifierName("System")));
//                NamespaceDeclarationSyntax ns = SF.NamespaceDeclaration(SF.IdentifierName("MyNamespace"));
//            cu = cu.AddMembers(ns);
        }
    }
}
