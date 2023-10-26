using static CardiacComputer.Cli.Opcodes;

namespace CardiacComputer.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        // https://www.cs.drexel.edu/~bls96/museum/cardiac.html

        var processor = CountNumbersWithLoop();

        foreach (var result in processor.Execute())
            Console.WriteLine($"Out: \t{result}");

        Console.WriteLine("fin!");
        Console.WriteLine(processor);
    }

    public static CardiacProcessor AddNumbers() => new(
        17,
        (INP, 34), // get input a
        (INP, 35), // get input b
        (CLA, 34), // set accumulator to a
        (ADD, 35), // add b to accumulator
        (STO, 36), // store to s
        (OUT, 36), // output s
        (HRS, 0)   // halt and reset
    );

    public static CardiacProcessor CountNumbers() => new CardiacProcessor(
        20, //set entry point offset
        (CLA, 01), //set accumulator to value in 1

        (STO, 03), // store accumulator to 3
        (OUT, 03), // output value in 3
        (ADD, 01), // increment 

        (STO, 03), // store accumulator to 3
        (OUT, 03), // output value in 3
        (ADD, 01), // increment 

        (STO, 03), // store accumulator to 3
        (OUT, 03), // output value in 3
        (ADD, 01), // increment 

        (STO, 03), // store accumulator to 3
        (OUT, 03), // output value in 3
        (ADD, 01), // increment 

        (STO, 03), // store accumulator to 3
        (OUT, 03), // output value in 3

        (HRS, 0)   // halt and reset
    ).Set(1, 1);   // set data value 1 to 1

    public static CardiacProcessor CountNumbersWithLoop() => new CardiacProcessor(
        20,
        (CLA, 01), //set accumulator to value in 1
                   
        (STO, 03), // store accumulator to 3
        (OUT, 03), // output value in 3
        (ADD, 01), // increment 
                   
        (JMP, 21)  // halt and reset
    ).Set(1, 1);   // set data value 1 to 1
}
