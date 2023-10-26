using System.Net;
using static CardiacComputer.Cli.Opcodes;

namespace CardiacComputer.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        // https://www.cs.drexel.edu/~bls96/museum/cardiac.html

        var processor = AddNumbers();

        // var processor = new CardiacProcessor(CountNumbersWithLoop());

        //processor[0] = 817;

        foreach (var result in processor.Execute())
            Console.WriteLine($"Out: \t{result}");

        Console.WriteLine("fin!");
        Console.WriteLine(processor);
    }

    public static CardiacProcessor AddNumbers() => new(
        17,
        (INP, 34),
        (INP, 35),
        (CLA, 34),
        (ADD, 35),
        (STO, 36),
        (OUT, 36),
        (HRS, 0)
    );


    public static IEnumerable<(int addr, Opcodes opcode, int operand)> CountNumbers() => new[]
    {
        (00, JMP, 20), // Jump to beginning

        (01, default, 1), // store 1 // TODO: there is no direct store

        (20, CLA, 01), // set counter to 0
        (21, STO, 03), // store counter
        (22, OUT, 03), // output counter
        (23, ADD, 01), // add 1 //add increment function
        (24, STO, 03), // store counter
        (25, OUT, 03), // output counter
        (26, ADD, 01), // add 1 //add increment function
        (27, STO, 03), // store counter
        (28, OUT, 03), // output counter
        (29, ADD, 01), // add 1 //add increment function
        (30, STO, 03), // store counter
        (31, OUT, 03), // output counter
        (32, ADD, 01), // add 1 //add increment function
        (33, STO, 03), // store counter
        (34, OUT, 03), // output counter
        (35, HRS, 20),  // Halt and Reset
    };

    public static IEnumerable<(int addr, Opcodes opcode, int operand)> CountNumbersWithLoop() => new[]
    {
        (00, JMP, 21), // Jump to beginning

        (01, default, 1), // store 1 // TODO: there is no direct store

        (21, CLA, 01), // set counter to 0
        (22, STO, 03), // store counter
        (23, OUT, 03), // output counter
        (24, ADD, 01), // add 1 //add increment function
        (25, JMP, 22), // store counter
    };
}
