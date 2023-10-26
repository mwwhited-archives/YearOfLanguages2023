using System.Text;
using static CardiacComputer.Cli.Opcodes;

namespace CardiacComputer.Cli;

public class CardiacProcessor
{
    public CardiacProcessor(
        int offset,
        params Instruction[] instructions
        ) : this(offset, instructions.AsEnumerable())
    {
    }
    public CardiacProcessor(
        int offset,
        IEnumerable<Instruction> instructions
        )
    {
        Set(offset, instructions.Select(instr => (int)instr).ToArray());
        if (offset > 0)
            this[0] = (Instruction)(JMP, offset);
    }

    public CardiacProcessor Set(int offset, params int[] values)
    {
        foreach (var item in values.Select((value, address) => (value, address)))
            this[item.address + offset] = item.value;
        return this;
    }

    private readonly int?[] _memory = new int?[100];
    private int _programCounter = 0;
    private int _accumulator = 0;
    private int _steps = 0;

    public IReadOnlyCollection<int?> Memory => _memory;
    public int ProgramCounter => _programCounter;
    public int Accumulator => _accumulator;
    public int Steps => _steps;

    public int this[int addr]
    {
        get => _memory[addr % _memory.Length] ?? 0;
        set => _memory[addr % _memory.Length] = value;
    }

    public static IEnumerable<int> getInputs()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Enter a number? ");
            int value;
            while (!int.TryParse(Console.ReadLine(), out value)) ;

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"read={value:000} ");

            yield return value;
        }
    }
    private int getInput(IEnumerator<int> input)
    {
        if (!input.MoveNext()) throw new ApplicationException("No input found");
        var current = input.Current;
        return current;
    }

    private void storeTo(int input, int addr)
    {
        _memory[addr] = input;
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"#{addr:00}={input:000} ");
    }

    private Instruction getInstruction() => _memory[_programCounter % _memory.Length] ?? 0;

    public IEnumerable<int> Execute() => Execute(getInputs());

    public IEnumerable<int> Execute(IEnumerable<int> inputs)
    {
        _programCounter = 0;
        _accumulator = 0;
        var input = inputs.GetEnumerator();
        return Execute(input);
    }

    public IEnumerable<int> Execute(IEnumerator<int> input)
    {
        while (true)
        {
            var instruction = getInstruction();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{_programCounter:00}@{(int)instruction:0000}:{(instruction.Opcode, instruction.Operand)} [{_accumulator}] ");

            _programCounter++;
            _steps++;

            switch (instruction.Opcode)
            {
                // The INP instruction reads a single card from the input and stores the contents of that card into the memory location identified by the operand address.
                // (MEM[a] ← INPUT)
                case INP:
                    storeTo(getInput(input), instruction.Operand);
                    break;

                // This instruction causes the contents of the memory location specified by the operand address to be loaded into the accumulator.
                // (ACC ← MEM[a])
                case CLA:
                    _accumulator = this[instruction.Operand];
                    break;

                // The ADD instruction takes the contents of the accumulator, adds it to the contents of the memory location identified by the operand address and stores the sum into the accumulator.
                // (ACC ← ACC + MEM[a])
                case ADD:
                    _accumulator += this[instruction.Operand];
                    break;

                //The TAC instruction is the CARDIAC's only conditional branch instruction. It tests the accumulator, and if the accumulator is negative, then the PC is loaded with the operand address. Otherwise, the PC is not modified and the program continues with the instruction following the TAC.
                //(If ACC < 0, PC ← a)
                case TAC:
                    if (_accumulator < 0)
                        _programCounter = instruction.Operand;
                    break;

                case TACoB:
                    if (_accumulator < 0)
                        _programCounter -= instruction.Operand;
                    break;

                case TACoF:
                    if (_accumulator < 0)
                        _programCounter += instruction.Operand;
                    break;

                //This instruction causes the accumulator to be shifted to the left by some number of digits and then back to the right some number of digits. The amounts by which it is shifted are shown above in the encoding for the SFT instruction.
                //(ACC ← (ACC × 10^l) / 10^r)
                case SFT:
                    _accumulator = (int)((_accumulator * Math.Pow(10, instruction.Operand / 10)) / Math.Pow(10, instruction.Operand % 10));
                    break;

                // The OUT instruction takes the contents of the memory location specified by the operand address and writes them out to an output card.
                // (OUTPUT ← MEM[a])
                case OUT:
                    yield return this[instruction.Operand];
                    break;

                // This is the inverse of the CLA instruction. The accumulator is copied to the memory location given by the operand address.
                // (MEM[a] ← ACC)
                case STO:
                    storeTo(_accumulator, instruction.Operand);
                    break;

                // In the SUB instruction the contents of the memory location identified by the operand address is subtracted from the contents of the accumulator and the difference is stored in the accumulator.
                // (ACC ← ACC − MEM[a])
                case SUB:
                    _accumulator -= this[instruction.Operand];
                    break;

                // The JMP instruction first copies the PC into the operand part of the instruction at address 99. So if the CARDIAC is executing a JMP instruction stored in memory location 42, then the value 843 will be stored in location 99. Then the operand address is copied into the PC, causing the next instruction to be executed to be the one at the operand address.
                // (MEM[99] ← 800 + PC; PC ← a)
                case JMP:
                    this[_memory.Length - 1] = new Instruction(JMP, _programCounter % _memory.Length);
                    _programCounter = instruction.Operand;
                    break;

                // The HRS instruction halts the CARDIAC and puts the operand address into the PC.
                // (PC ← a; HALT)
                case HRS:
                    _programCounter = instruction.Operand;
                    yield break;

                default:
                    throw new ApplicationException("Invalid instruction");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"-> {_programCounter:00}:[{_accumulator}] ");
        }
    }

    public override string ToString() =>
         new StringBuilder()
            .AppendLine($"{nameof(ProgramCounter)}: {ProgramCounter:00}")
            .AppendLine($"{nameof(Accumulator)}: {Accumulator:0000}")
            .AppendLine($"{nameof(Steps)}: {Steps:0000}")
            .AppendLine($"{nameof(Memory)}: {string.Join(";", Memory)}")
            //.AppendLine($"Decoded: {string.Join(";", _memory.Select(i => i.HasValue ? (Instruction)i : null))}")
            .ToString();
}
