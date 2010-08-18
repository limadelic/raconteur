using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CSharp;

namespace Raconteur
{
    public class RaconteurGenerator
    {
        readonly Project project;

        public RaconteurGenerator(Project project) { this.project = project; }

        public void GenerateCSharpTestFile(FeatureFile featureFile, TextWriter outputWriter)
        {
            var codeProvider = new CSharpCodeProvider();
            GenerateTestFile(featureFile, codeProvider, outputWriter);
        }

        void GenerateTestFile(FeatureFile featureFile, CodeDomProvider codeProvider, TextWriter outputWriter) { using (var reader = new StreamReader(featureFile.GetFullPath(project))) GenerateTestFile(featureFile, codeProvider, reader, outputWriter); }

        class HackedWriter : TextWriter
        {
            readonly TextWriter innerWriter;
            bool trimSpaces;

            public HackedWriter(TextWriter innerWriter) { this.innerWriter = innerWriter; }

            public override void Write(char[] buffer, int index, int count) { Write(new string(buffer, index, count)); }

            public override void Write(char value) { Write(value.ToString()); }

            public override void Write(string value)
            {
                if (trimSpaces)
                {
                    value = value.TrimStart(' ', '\t');
                    if (value == string.Empty) return;
                    trimSpaces = false;
                }

                innerWriter.Write(value);
            }

            public override Encoding Encoding { get { return innerWriter.Encoding; } }

            static readonly Regex indentNextRe = new Regex(@"^[\s\/\']*#indentnext (?<ind>\d+)\s*$");

            public override void WriteLine(string text)
            {
                var match = indentNextRe.Match(text);
                if (match.Success)
                {
                    Write(new string(' ', int.Parse(match.Groups["ind"].Value)));
                    trimSpaces = true;
                    return;
                }

                base.WriteLine(text);
            }

            public override string ToString() { return innerWriter.ToString(); }

            public override void Flush() { innerWriter.Flush(); }

            protected override void Dispose(bool disposing) { if (disposing) innerWriter.Dispose(); }
        }


        public void GenerateTestFile(FeatureFile featureFile, CodeDomProvider codeProvider, TextReader inputReader,
            TextWriter outputWriter)
        {
            outputWriter = new HackedWriter(outputWriter);

            var codeDomHelper = new CodeDomHelper(codeProvider);

            var codeNamespace = GenerateTestFileCode(featureFile, inputReader, codeProvider, codeDomHelper);
            var options = new CodeGeneratorOptions {BracingStyle = "C"};

            codeProvider.GenerateCodeFromNamespace(codeNamespace, outputWriter, options);

            outputWriter.Flush();
        }

        public CodeNamespace GenerateTestFileCode(FeatureFile featureFile, TextReader inputReader,
            CodeDomProvider codeProvider, CodeDomHelper codeDomHelper)
        {
            return null;
        }

        public Version CurrentVersion { get { return Assembly.GetExecutingAssembly().GetName().Version; } }

        static readonly Regex VersionRe = new Regex(@"Raconteur Version:\s*(?<ver>\d+\.\d+\.\d+\.\d+)");

        public Version GeneratedFileVersion(string FilePath)
        {
            try
            {
                if (!File.Exists(FilePath)) return null;

                using (var reader = new StreamReader(FilePath)) return GeneratedFileVersion(reader);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        Version GeneratedFileVersion(StreamReader Reader)
        {
            try
            {
                const int maxLinesToScan = 10;

                var lineNo = 0;
                string line;
                while ((line = Reader.ReadLine()) != null && lineNo < maxLinesToScan)
                {
                    var match = VersionRe.Match(line);
                    if (match.Success) return new Version(match.Groups["ver"].Value);

                    lineNo++;
                }
                return null;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}