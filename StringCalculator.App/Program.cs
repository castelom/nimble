// See https://aka.ms/new-console-template for more information
using StringCalculator.Core;

Console.WriteLine("Enter expression:");
var input = Console.ReadLine();

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
