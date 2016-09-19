using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ConsoleApp3
{
    public class Program
    {
        public static void Main(string[] args)
        {

        }
        //public static string data { get; set; }
        //public static void Main(string[] args)
        //{
        //    var currentNamespace = System.Reflection.Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace;
        //    IList<string> classNames = new List<string>();
        //    var list = Directory.EnumerateFiles(Path.GetDirectoryName(@"Entities\")).Where(x => Path.GetExtension(x) == ".cs");
        //    var text = list.Select(x => CSharpSyntaxTree.ParseText(File.ReadAllText(x))).Cast<CSharpSyntaxTree>();
        //    foreach (CSharpSyntaxTree syntaxTree in text)
        //    {
        //        var root = (CompilationUnitSyntax)syntaxTree.GetRoot();
        //        var sd = syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
        //        foreach (var item in sd)
        //        {
        //            classNames.Add(item.Identifier.ToString());
        //        }
        //    }
        //    CreateInterface(classNames, currentNamespace);
        //    CreateClass(classNames, currentNamespace);
        //}

        //public static void CreateInterface(IList<string> classNames, string currentNamespace)
        //{

        //    var workspace = new AdhocWorkspace();
        //    var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
        //    var usingSystemDirectives = generator.NamespaceImportDeclaration("System");
        //    //var usingSystemGenricDirectives = generator.NamespaceImportDeclaration("System.Generic");
        //    //var usingEntities = generator.NamespaceImportDeclaration("Entities");
        //    var IRepositoryAsynInterfaceType = generator.IdentifierName("IRepositoryAsync");
        //    foreach (var className in classNames)
        //    {
        //        var interfaceDeclaration = generator.InterfaceDeclaration("I" + className + "Repository", typeParameters: null,
        //                              accessibility: Accessibility.Public,
        //                              interfaceTypes: new SyntaxNode[] { IRepositoryAsynInterfaceType },
        //                              members: null);
        //        var namespaceDeclaration = generator.NamespaceDeclaration(currentNamespace, interfaceDeclaration);
        //        var newNode = generator.CompilationUnit(usingSystemDirectives/*, usingSystemGenricDirectives, usingEntities*/, namespaceDeclaration).
        //                      NormalizeWhitespace();
        //        data = newNode.ToString();
        //        var path = Path.GetFullPath("Generated");
        //        var logPath = Path.GetFullPath("Generated\\" + "I" + className + "Repository" + ".cs");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        File.SetAttributes(path, FileAttributes.Normal);
        //        var logFile = File.Create(logPath);
        //        var logWriter = new System.IO.StreamWriter(logFile);
        //        logWriter.WriteLine(data);
        //        logWriter.Dispose();
        //    }
        //}

        //public static void CreateClass(IList<string> classNames, string currentNamespace)
        //{
        //    var workspace = new AdhocWorkspace();
        //    var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
        //    var usingSystemDirectives = generator.NamespaceImportDeclaration("System");
        //    //var usingSystemGenricDirectives = generator.NamespaceImportDeclaration("System.Generic");
        //    //var usingEntities = generator.NamespaceImportDeclaration("EntityProjectName");
        //    var IDataContextType = generator.IdentifierName("IDataContext");
        //    var IUnitofWorkType = generator.IdentifierName("IUnitofWork");
        //    var constructorParameters = new SyntaxNode[] {
        //                                    generator.ParameterDeclaration("context", IDataContextType),

        //                    generator.ParameterDeclaration("unitofWork",IUnitofWorkType) };
        //    foreach (var className in classNames)
        //    {
        //        var constructor = generator.ConstructorDeclaration(className,
        //                                 constructorParameters, Accessibility.Public,
        //                                 statements: null);
        //        var members = new SyntaxNode[] { constructor };
        //        var IRepositoryInterfaceType = generator.IdentifierName("I" + className + "Repository");
        //        var classDeclaration = generator.ClassDeclaration(className + "Repository", typeParameters: null,
        //                                  accessibility: Accessibility.Public,
        //                                  interfaceTypes: new SyntaxNode[] { IRepositoryInterfaceType },
        //                                  members: members);
        //        var namespaceDeclaration = generator.NamespaceDeclaration(currentNamespace, classDeclaration);
        //        var newNode = generator.CompilationUnit(usingSystemDirectives/*, usingSystemGenricDirectives, usingEntities*/, namespaceDeclaration).
        //                      NormalizeWhitespace();
        //        data = newNode.ToString();
        //        var targetFolder = @"../ConsoleApp3/";
        //        var path = Path.GetFullPath(targetFolder + @"Generated/");
        //        var logPath = Path.GetFullPath(path + "\\" + className + "Repository" + ".cs");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        File.SetAttributes(path, FileAttributes.Normal);
        //        var logFile = File.Create(logPath);
        //        var logWriter = new System.IO.StreamWriter(logFile);
        //        logWriter.WriteLine(data);
        //        logWriter.Dispose();
        //    }
        //}
    }
}
