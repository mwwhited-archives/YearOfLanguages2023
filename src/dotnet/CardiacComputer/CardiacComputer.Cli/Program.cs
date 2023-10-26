using static CardiacComputer.Cli.Opcodes;

namespace CardiacComputer.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        // https://www.cs.drexel.edu/~bls96/museum/cardiac.html

        var processor = CountNumbersWithEnding();

        foreach (var result in processor.Execute())
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Out: \t{result}");
        }

        Console.ForegroundColor = ConsoleColor.DarkGreen;
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

    public static CardiacProcessor CountNumbersWithEnding() => new CardiacProcessor(
        20,
        (CLA, 19), // set accumulator to value from 19
        (STO, 18), // store value to 18 

        (CLA, 18), // set accumulator to value from 19
        (ADD, 01), // add value from 1
        (STO, 18), // store value to 18 

        (CLA, 17), // set accumulator to value from 19
        (ADD, 01), // add value from 1
        (STO, 17), // store value to 18 
        (OUT, 17), // output value in 18

        (CLA, 18), // set accumulator to value from 19
        (TAC, 21), // if accumulator is < 0 then jump
        (HRS, 0)   // halt and reset
    )
    .Set(1, 1) // seed data 1 as 1
    .Set(17, 0) // seed data 17 as 0
    .Set(19, -4) // see data 19 as -4
    ;
}
