#r "System.Text.Encoding.dll"
#r "System.Threading.Tasks.dll"

using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;


Program p = new Program();
p.GetValue();

public class Program
{
    public string data { get; set; }

    public void GetValue()
    {
        var currentNamespace = Directory.GetCurrentDirectory().Split('\\').Last();
        IList<string> classNames = new List<string>();
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
        Console.WriteLine("namespace is " + currentNamespace);
        Console.WriteLine("Creating Interfaces");
        CreateInterface(classNames, currentNamespace);
        Console.WriteLine("Creating Classes");
        CreateClass(classNames, currentNamespace);
        Console.WriteLine("Completed");
    }

    public void CreateInterface(IList<string> classNames, string currentNamespace)
    {
        var workspace = new AdhocWorkspace();
        var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
        var usingSystemDirectives = generator.NamespaceImportDeclaration("System");
        var usingEntities = generator.NamespaceImportDeclaration("ConsoleApp3");
        var IRepositoryAsynInterfaceType = generator.IdentifierName("IRepositoryAsync");
        foreach (var className in classNames)
        {
            var interfaceDeclaration = generator.InterfaceDeclaration("I" + className + "Repository", typeParameters: null,
                                  accessibility: Accessibility.Public,
                                  interfaceTypes: new SyntaxNode[] { IRepositoryAsynInterfaceType },
                                  members: null);
            var namespaceDeclaration = generator.NamespaceDeclaration(currentNamespace, interfaceDeclaration);
            var newNode = generator.CompilationUnit(usingSystemDirectives, usingEntities, namespaceDeclaration).
                          NormalizeWhitespace();
            data = newNode.ToString();
            CreateFile(data, className, "interface");
        }
    }

    public void CreateClass(IList<string> classNames, string currentNamespace)
    {
        var workspace = new AdhocWorkspace();
        var generator = SyntaxGenerator.GetGenerator(workspace, LanguageNames.CSharp);
        var usingSystemDirectives = generator.NamespaceImportDeclaration("System");
        var usingEntities = generator.NamespaceImportDeclaration("ConsoleApp3");
        var IDataContextType = generator.IdentifierName("IDataContext");
        var IUnitofWorkType = generator.IdentifierName("IUnitofWork");
        var constructorParameters = new SyntaxNode[] {
                                            generator.ParameterDeclaration("context", IDataContextType),

                            generator.ParameterDeclaration("unitofWork",IUnitofWorkType) };
        foreach (var className in classNames)
        {
            var constructor = generator.ConstructorDeclaration(className,
                                     constructorParameters, Accessibility.Public,
                                     statements: null);
            var members = new SyntaxNode[] { constructor };
            var IRepositoryInterfaceType = generator.IdentifierName("I" + className + "Repository");
            var classDeclaration = generator.ClassDeclaration(className + "Repository", typeParameters: null,
                                      accessibility: Accessibility.Public,
                                      interfaceTypes: new SyntaxNode[] { IRepositoryInterfaceType },
                                      members: members);
            var namespaceDeclaration = generator.NamespaceDeclaration(currentNamespace, classDeclaration);
            var newNode = generator.CompilationUnit(usingSystemDirectives, usingEntities, namespaceDeclaration).
                          NormalizeWhitespace();
            data = newNode.ToString();
            CreateFile(data, className, "class");
        }
    }

    public void CreateFile(string data, string className, string type)
    {
        string path;
        if (type == "class")
        {
            path = Path.GetFullPath("Repositories");
        }
        else
        {
            path = Path.GetFullPath("Interfaces");
        }
        var logPath = Path.GetFullPath(path + "\\" + "I" + className + "Repository" + ".cs");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.SetAttributes(path, FileAttributes.Normal);
        var logFile = File.Create(logPath);
        var logWriter = new System.IO.StreamWriter(logFile);
        logWriter.WriteLine(data);
        logWriter.Dispose();
    }
}