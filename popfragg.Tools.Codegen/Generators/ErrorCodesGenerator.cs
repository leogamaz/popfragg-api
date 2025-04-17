using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace popfragg.Tools.Codegen.Generators
{
    public static class ErrorCodesGenerator
    {
        public static void Generate()
        {
            var solutionRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;

            var inputPath = Path.Combine(solutionRoot, "popfragg.Common", "Exceptions", "ErrorCodes.cs");
            var outputPath = Path.Combine(solutionRoot, "popfragg.Tools.Codegen", "Outputs", "error-codes.ts");

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"❌ ErrorCodes.cs não encontrado em: {inputPath}");
                return;
            }

            var lines = File.ReadAllLines(inputPath);
            var constRegex = new Regex(@"public const string (\w+)\s*=\s*""([^""]+)"";", RegexOptions.Compiled);

            var entries = new List<(string Name, string Value)>();

            foreach (var line in lines)
            {
                var match = constRegex.Match(line);
                if (match.Success)
                {
                    var name = match.Groups[1].Value;
                    var value = match.Groups[2].Value;
                    entries.Add((name, value));
                }
            }

            if (entries.Count == 0)
            {
                Console.WriteLine("Nenhum código encontrado.");
                return;
            }

            // Geração do arquivo TS
            var tsLines = new List<string>
            {
                "export const ErrorCodes = {"
            };

            tsLines.AddRange(entries.Select(e => $"  {e.Name}: '{e.Value}',"));

            tsLines.Add("} as const;");
            tsLines.Add("");
            tsLines.Add("export type ErrorCode = typeof ErrorCodes[keyof typeof ErrorCodes];");

            File.WriteAllLines(outputPath, tsLines);

            Console.WriteLine($"Arquivo gerado com sucesso: {outputPath}");
        }
    }
}
