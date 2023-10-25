using System.Net;
using static CardiacComputer.Cli.Instructions;

namespace CardiacComputer.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        // https://www.cs.drexel.edu/~bls96/museum/cardiac.html
        // Console.WriteLine("Hello, World!");

        var processor = new CardiacProcessor(AddNumbers());

        foreach (var result in processor.Execute())
            Console.WriteLine($"Out: \t{result}");


        foreach (var result in processor.Execute())
            Console.WriteLine($"Out: \t{result}");

        Console.WriteLine("fin!");
        Console.WriteLine(processor);
    }

    public static IEnumerable<(int addr, Instructions opcode, int operand)> AddNumbers() => new[]
    {
        (00, JMP, 17), // Jump to beginning
        (17, INP, 34), // Read in "A"
        (18, INP, 35), // Read in "B"
        (19, CLA, 34), // Set accumulator to "A"
        (20, ADD, 35), // ADD "B"
        (21, STO, 36), // STO "S"
        (22, OUT, 36), // OUT "S"
        (23, HRS, 17),  // Halt and Reset
    };
}
