using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using UnityEditor;

namespace NoodleEater.Toolset.EnumGenerator 
{

    public class EnumGenerationSystem
    {
        private readonly CodeNamespace _codeNamespace = new CodeNamespace();
        private readonly CodeTypeDeclaration _codeTypeDeclaration = new CodeTypeDeclaration();
        private CodeCompileUnit _codeCompileUnit = new CodeCompileUnit();

        public string Namespace
        {
            get => _codeNamespace.Name;
            set => _codeNamespace.Name = value;
        }

        public string EnumName
        {
            get => _codeTypeDeclaration.Name;
            set => _codeTypeDeclaration.Name = value;
        }

        public string FileDirectory { get; set; }

        public string[] Values { get; set; }

        public EnumGenerationSystem()
        {
            _codeTypeDeclaration.IsEnum = true;
            _codeNamespace.Types.Add(_codeTypeDeclaration);
            _codeCompileUnit.Namespaces.Add(_codeNamespace);
        }
        
        public void SetEnumValues() {
            foreach (var enumValue in Values)
            {
                CodeMemberField field = new CodeMemberField(EnumName, enumValue);
                _codeTypeDeclaration.Members.Add(field);
            }
        }

        public void GenerateEnum()
        {
            SetEnumValues();
            
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions
            {
                BracingStyle = "C"
            };

            string filePath = FileDirectory + $"/{EnumName}.cs";

            if (!Directory.Exists(FileDirectory))
            {
                Directory.CreateDirectory(FileDirectory);
            }

            if (!File.Exists(filePath))
            {
                FileStream fileStream = File.Create(filePath);
                fileStream.Close();
            }

            using (StreamWriter sourceWriter = new StreamWriter(filePath))
            {
                provider.GenerateCodeFromCompileUnit(_codeCompileUnit, sourceWriter, options);
                AssetDatabase.Refresh();
            }
        }
    }
}