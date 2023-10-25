using System.Text;

namespace CardiacComputer.Cli;

public class CardiacProcessor
{
    public CardiacProcessor(
        params (int index, Instructions instruction, int operand)[] instructions
        ) : this(Enumerable.Range(0, 100).Select(idx => (instructions.FirstOrDefault(i => i.index == idx) switch
        {
            (int, Instructions, int) v => (v.instruction, v.operand)
        })))
    {
    }
    //public CardiacProcessor(
    //    params (int index, int instruction)[] instructions
    //    ) : this(Enumerable.Range(0, 100).Select(idx => (instructions.FirstOrDefault(i => i.index == idx) switch
    //    {
    //        (int index, int instr) v => v.instruction
    //    })))
    //{
    //}

    //public CardiacProcessor(
    //    params (Instructions instruction, int address)[] instructions
    //    ) : this(instructions.AsEnumerable())
    //{
    //}
    //public CardiacProcessor(
    //    params int[] instructions
    //    ) : this(instructions.AsEnumerable())
    //{
    //}
    public CardiacProcessor(
        IEnumerable<(Instructions instruction, int address)> instructions
        ) : this(instructions.Select(item => (
            (int)item.instruction) * 100 +
            ((item.address >= 0 && item.address < 100) ? item.address : throw new ApplicationException("Invalid instruction address(s) detected"))
            ))
    {
    }
    private CardiacProcessor(
        IEnumerable<int> instructions
        )
    {
        if (instructions.Any(instruction => instruction < 0 || instruction > 999))
            throw new ApplicationException("Invalid instruction(s) detected");

        var program = instructions.ToArray();
        Array.Copy(program, _memory, Math.Min(program.Length, _memory.Length));
        //_memory[0] = 001;
    }

    private readonly int[] _memory = new int[100];
    //private readonly int[] _program;
    private int _programCounter = 0;
    private int _accumulator = 0;

    public IReadOnlyCollection<int> Memory => _memory;
    public int ProgramCounter => _programCounter;
    public int Accumulator => _accumulator;


    public static IEnumerable<int> getInputs()
    {
        while (true)
        {
            Console.Write("Enter a number? ");
            int value;
            while (!int.TryParse(Console.ReadLine(), out value)) ;
            yield return value;
        }
    }
    public IEnumerable<int> Execute() => Execute(getInputs());

    public IEnumerable<int> Execute(IEnumerable<int> inputs)
    {
        _programCounter = 0; // _memory.Length - 1;
        _accumulator = 0;

        var input = inputs.GetEnumerator();

        int getInput()
        {
            if (!input.MoveNext()) return -99999;
            var current = input.Current;
            return current;
        }

        void storeTo(int input, int addr)
        {
            while (input < -999) input += 1000;
            while (input > 999) input -= 1000;
            _memory[addr] = input;
            //Console.WriteLine($"#{addr}={input}");
        }

        int getMemory(int addr) => _memory[addr % _memory.Length];

        (Instructions instruction, int address, int raw) getInstruction() => _memory[_programCounter % _memory.Length] switch
        {
            int instr => ((Instructions)(instr / 100), instr % 100, instr)
        };

        while (true)
        {
            //TODO: maybe these should be an error instead

            _programCounter %= _memory.Length;
            _accumulator %= 10000;

            var (instruction, address, instructionRegister) = getInstruction();

            Console.WriteLine($"{_programCounter:00}@{instructionRegister:000}:{(instruction, address)}");

            _programCounter++;

            switch (instruction)
            {
                // The INP instruction reads a single card from the input and stores the contents of that card into the memory location identified by the operand address.
                // (MEM[a] ← INPUT)
                case Instructions.INP:
                    storeTo(getInput(), address);
                    break;

                // This instruction causes the contents of the memory location specified by the operand address to be loaded into the accumulator.
                // (ACC ← MEM[a])
                case Instructions.CLA:
                    _accumulator = getMemory(address);
                    break;

                // The ADD instruction takes the contents of the accumulator, adds it to the contents of the memory location identified by the operand address and stores the sum into the accumulator.
                // (ACC ← ACC + MEM[a])
                case Instructions.ADD:
                    _accumulator += getMemory(address);
                    break;

                //The TAC instruction is the CARDIAC's only conditional branch instruction. It tests the accumulator, and if the accumulator is negative, then the PC is loaded with the operand address. Otherwise, the PC is not modified and the program continues with the instruction following the TAC.
                //(If ACC < 0, PC ← a)
                case Instructions.TAC:
                    if (_accumulator < 0)
                        _programCounter = address;
                    break;

                //This instruction causes the accumulator to be shifted to the left by some number of digits and then back to the right some number of digits. The amounts by which it is shifted are shown above in the encoding for the SFT instruction.
                //(ACC ← (ACC × 10^l) / 10^r)
                case Instructions.SFT:
                    _accumulator = (int)((_accumulator * Math.Pow(10, address / 10)) / Math.Pow(10, address % 10));
                    break;

                // The OUT instruction takes the contents of the memory location specified by the operand address and writes them out to an output card.
                // (OUTPUT ← MEM[a])
                case Instructions.OUT:
                    yield return getMemory(address);
                    break;

                // This is the inverse of the CLA instruction. The accumulator is copied to the memory location given by the operand address.
                // (MEM[a] ← ACC)
                case Instructions.STO:
                    storeTo(_accumulator, address);
                    break;

                // In the SUB instruction the contents of the memory location identified by the operand address is subtracted from the contents of the accumulator and the difference is stored in the accumulator.
                // (ACC ← ACC − MEM[a])
                case Instructions.SUB:
                    _accumulator -= getMemory(address);
                    break;

                // The JMP instruction first copies the PC into the operand part of the instruction at address 99. So if the CARDIAC is executing a JMP instruction stored in memory location 42, then the value 843 will be stored in location 99. Then the operand address is copied into the PC, causing the next instruction to be executed to be the one at the operand address.
                // (MEM[99] ← 800 + PC; PC ← a)
                case Instructions.JMP:
                    storeTo(((int)Instructions.JMP) * 100 + (_programCounter % 100), 99);
                    _programCounter = address;
                    break;

                // The HRS instruction halts the CARDIAC and puts the operand address into the PC.
                // (PC ← a; HALT)
                case Instructions.HRS:
                    _programCounter = address;
                    yield break;
            }
        }
    }

    public override string ToString() =>
         new StringBuilder()
            .AppendLine($"{nameof(ProgramCounter)}: {ProgramCounter:00}")
            .AppendLine($"{nameof(Accumulator)}: {Accumulator:0000}")
            .AppendLine($"{nameof(Memory)}: {string.Join(";", Memory.Select(v => v.ToString("000")))}")
            .ToString();
}
