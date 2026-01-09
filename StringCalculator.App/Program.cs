// See https://aka.ms/new-console-template for more information
using StringCalculator.Core;
using System.Text;

Console.WriteLine("Enter expression (press ENTER twice to finish):");

var inputBuilder = new StringBuilder();

while (true)
{
    var line = Console.ReadLine();

    if (string.IsNullOrEmpty(line))
        break;

    inputBuilder.AppendLine(line);
}

//Sanitize input
var input = inputBuilder
    .ToString()
    .Replace("\r\n", "\n") // normalize Windows line endings
    .TrimEnd('\n'); //remove trailing newline
    


var calculator = new Calculator();

try
{
    var result = calculator.Add(input);
    Console.WriteLine($"Result: {result}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
