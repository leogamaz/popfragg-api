using popfragg.Tools.Codegen.Generators;


Console.WriteLine("Gerando Schemas");
var input = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\popfragg.Domain\DTOS"));
var output = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\popfragg.Tools.Codegen\Outputs\schemas"));
Directory.CreateDirectory(output);

ZodSchemaGenerator.Generate(input, output);
Console.WriteLine("Schemas gerados em Outputs/schemas");

Console.WriteLine("Gerando Códigos de erro");
ErrorCodesGenerator.Generate();
Console.WriteLine("Códigos de erros Gerados com sucesso!");