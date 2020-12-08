using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode
{
    public class Day08
    {
        public static void FindAccumulatorAtInfiniteLoop(string filename)
        {
            var program = LoadProgram(filename);

            try
            {
                var accumulator = program.Run();
            }
            catch (InfiniteLoopException e)
            {
                Console.WriteLine($"When the infinite loop starts, acc is {e.Accumulator}");
            }
        }

        public static void MutateToFindNormalExitAccumulator(string filename)
        {
            var program = LoadProgram(filename);

            int[] nopIndexes = FindJmpIndexes(program);

            int accumulator = 0;

            for (var i = 0; i < nopIndexes.Length && accumulator == 0; i++)
            {
                var nopIndex = nopIndexes[i];
                var saved = program[nopIndex];
                program[nopIndex] = new SimpleInstruction
                {
                    Operator = SimpleProgram.Nop,
                };
                try
                {
                    accumulator = program.Run();
                }
                catch (InfiniteLoopException e)
                {
                    // Not this one
                }
                program[nopIndex] = saved;
            }

            Console.WriteLine($"Accumulator value on normal program exit is {accumulator}");
        }

        private static int[] FindJmpIndexes(SimpleProgram program)
        {
            var indexes = new List<int>();
            for (var i=0; i<program.Count; i++)
                if (program[i].Operator == SimpleProgram.Jmp)
                    indexes.Add(i);
            return indexes.ToArray();
        }

        private static SimpleProgram LoadProgram(string filename)
        {
            var lines = Utility.ReadLinesFromFile(filename);

            var program = new SimpleProgram();

            foreach (var line in lines)
            {
                var parts = line.Split(" ");
                var instruction = new SimpleInstruction
                {
                    Operator = parts[0],
                    Operand = Convert.ToInt32(parts[1])
                };

                program.Add(instruction);
            }

            return program;
        }
    }

    public class InfiniteLoopException : Exception
    {
        public int Accumulator { get; }

        public InfiniteLoopException(int accumulator)
        {
            Accumulator = accumulator;
        }
    }

    public class SimpleProgram : List<SimpleInstruction>
    {
        public const string Jmp = "jmp";
        public const string Acc = "acc";
        public const string Nop = "nop";

        private static List<int> _history;

        public int Run()
        {
            _history = new List<int>();

            var pointer = 0;
            var accumulator = 0;

            var notDone = true;

            while (notDone)
            {
                if (_history.Contains(pointer)) throw new InfiniteLoopException(accumulator);

                var instruction = this[pointer];
                _history.Add(pointer);
                switch (instruction.Operator)
                {
                    case Nop:
                        pointer++;
                        break;
                    case Acc:
                        accumulator += instruction.Operand;
                        pointer++;
                        break;
                    case Jmp:
                        pointer += instruction.Operand;
                        break;
                }

                if (pointer == this.Count) notDone = false;
            }

            return accumulator;
        }
    }

    public class SimpleInstruction
    {
        public string Operator { get; set; }
        public int Operand { get; set; }
    }
}