using static CardiacComputer.Cli.Opcodes;

namespace CardiacComputer.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        // https://www.cs.drexel.edu/~bls96/museum/cardiac.html

        var processor = CountNumbersTo();

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

        (CLA, 17), // set accumulator to value from 17
        (ADD, 01), // add value from 1
        (STO, 17), // store value to 17 
        (OUT, 17), // output value in 17
        (ADD, 19), // add value from 1
        (TAC, 20), // if accumulator is < 0 then jump

        (HRS, 0)   // halt and reset
    )
    .Set(1, 1) // seed data 1 as 1
    .Set(17, 0) // seed data 17 as 0
    .Set(19, -4) // see data 19 as -4
    ;

    public static CardiacProcessor CountNumbersWithEndingLong() => new CardiacProcessor(
        20,
        (CLA, 19), // set accumulator to value from 19
        (STO, 18), // store value to 18 

        (CLA, 18), // set accumulator to value from 18
        (ADD, 01), // add value from 1
        (STO, 18), // store value to 18 

        (CLA, 17), // set accumulator to value from 17
        (ADD, 01), // add value from 1
        (STO, 17), // store value to 18 
        (OUT, 17), // output value in 18

        (CLA, 18), // set accumulator to value from 18
        (TAC, 21), // if accumulator is < 0 then jump
        (HRS, 0)   // halt and reset
    )
    .Set(1, 1) // seed data 1 as 1
    .Set(17, 0) // seed data 17 as 0
    .Set(19, -4) // see data 19 as -4
    ;

    const int _inc = 1;
    const int _zero = 2;
    const int _counter = 3;
    const int _result = 4;

    public static CardiacProcessor CountNumbersTo() => new CardiacProcessor(
        20,

        /* 20 */(INP, _counter), // read value into counter (5)
        /* 21 */(CLA, _zero),    // set accumulator 0
        /* 22 */(STO, _result),  // set result to 0
        /* 23 */(SUB, _counter), // negate counter 
        /* 24 */(STO, _counter), // store counter 

        /* 25 */(CLA, _result), // set accumulator to value from 4
        /* 26 */(ADD, _inc), // add value from 1
        /* 27 */(STO, _result), // store value to 4 
        /* 28 */(ADD, _counter), // add value from 1
        /* 29 */(TACoB, 5), // if accumulator is < 0 then jump back 5

        /* 30 */(OUT, _result), // output value in 4

        /* 31 */(HRS, 0)   // halt and reset
    )
    .Set(_inc, 1) // seed data 1 as 1
    .Set(_zero, 0) // seed data 2 as 0
    ;
}
