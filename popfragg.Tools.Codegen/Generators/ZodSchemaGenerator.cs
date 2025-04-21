using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace popfragg.Tools.Codegen.Generators
{
    public static class ZodSchemaGenerator
    {
        public static void Generate(string inputPath, string outputPath)
        {
            var csFiles = Directory.GetFiles(inputPath, "*.cs", SearchOption.AllDirectories);

            foreach (var file in csFiles)
            {
                var lines = File.ReadAllLines(file);
                var className = Path.GetFileNameWithoutExtension(file);
                var schemaName = ToCamelCase(className.Replace("Request", "")) + "Schema";

                var sb = new StringBuilder();
                sb.AppendLine("import { z } from 'zod';");
                sb.AppendLine();
                sb.AppendLine($"export const {schemaName} = z.object({{");

                foreach (var line in lines)
                {
                    var match = Regex.Match(line, @"public (required\s+)?([\w<>\[\]?]+)\s+(\w+)\s*{");

                    if (!match.Success) continue;

                    var type = match.Groups[2].Value.Trim();
                    var prop = ToCamelCase(match.Groups[3].Value);
                    var isOptional = type.Contains("?") || line.Contains("= default") || line.Contains("= null");

                    string zodType = GetZodType(type);

                    sb.AppendLine($"  {prop}: {(isOptional ? zodType + ".optional()" : zodType)},");
                }

                sb.AppendLine("});");
                sb.AppendLine();
                sb.AppendLine($"export type {className} = z.infer<typeof {schemaName}>;");

                var outputFile = Path.Combine(outputPath, $"{ToKebabCase(className)}.ts");
                File.WriteAllText(outputFile, sb.ToString());
            }
        }

        private static string GetZodType(string type)
        {
            type = type.Replace("?", "");

            return type switch
            {
                "string" => "z.string()",
                "int" or "long" or "float" or "double" or "decimal" => "z.number()",
                "bool" => "z.boolean()",
                "DateTime" or "DateOnly" or "TimeOnly" => "z.string().datetime()",
                _ when type.StartsWith("List<string") => "z.array(z.string())",
                _ when type.StartsWith("List<") => $"z.array({ToCamelCase(ExtractGeneric(type))}Schema)",
                _ when type.StartsWith("Dictionary<string,string>") => "z.record(z.string())",
                _ when type.EndsWith("Request") => $"{ToCamelCase(type)}Schema",
                _ => "z.any()"
            };
        }

        private static string ExtractGeneric(string type)
        {
            var match = Regex.Match(type, @"<(.+)>");
            return match.Success ? match.Groups[1].Value : "any";
        }

        private static string ToCamelCase(string input)
            => char.ToLowerInvariant(input[0]) + input.Substring(1);

        private static string ToKebabCase(string input)
            => Regex.Replace(input, "(?<!^)([A-Z])", "-$1").ToLower();
    }
}
