using System.Net;
using static CardiacComputer.Cli.Instructions;

namespace CardiacComputer.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        // https://www.cs.drexel.edu/~bls96/museum/cardiac.html

        var processor = new CardiacProcessor(AddNumbers());

        //processor[0] = 817;

        foreach (var result in processor.Execute())
            Console.WriteLine($"Out: \t{result}");

        Console.WriteLine("fin!");
        Console.WriteLine(processor);
    }

    public static IEnumerable<(int addr, Instructions opcode, int operand)> AddNumbers() => new[]
    {
        (99, JMP, 17), // Jump to beginning
        (17, INP, 34), // Read in "A"
        (18, INP, 35), // Read in "B"
        (19, CLA, 34), // Set accumulator to "A"
        (20, ADD, 35), // ADD "B"
        (21, STO, 36), // STO "S"
        (22, OUT, 36), // OUT "S"
        (23, HRS, 17),  // Halt and Reset
    };

    public static IEnumerable<(int addr, Instructions opcode, int operand)> CountNumbers() => new[]
    {
        (00, JMP, 20), // Jump to beginning

        (01, default, 1), // store 1 // TODO: there is no direct store

        (20, CLA, 99), // set counter to 0
        (21, STO, 03), // store counter
        (22, OUT, 03), // output counter
        (23, ADD, 01), // add 1 //add increment function
        (25, STO, 03), // store counter
        (26, ADD, 01), // add 1 //add increment function
    };
}
