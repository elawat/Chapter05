﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static System.Console;
using static System.Diagnostics.Process;

namespace Ch05_BuildingStrings
{
    class Recorder
    {
        static Stopwatch timer = new Stopwatch();
        static long bytesPhysicalBefore = 0;
        static long bytesVirtualBefore = 0;
        public static void Start()
        {
            GC.Collect(); //garbage collector type
            GC.WaitForPendingFinalizers();
            GC.Collect();
            bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
            bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;
            timer.Restart();
        }
        public static void Stop()
        {
            timer.Stop();
            long bytesPhysicalAfter = GetCurrentProcess().WorkingSet64;
            long bytesVirtualAfter = GetCurrentProcess().VirtualMemorySize64;
            WriteLine("Stopped recording.");
            WriteLine($"{bytesPhysicalAfter - bytesPhysicalBefore:N0} physical bytes used.");
            WriteLine($"{bytesVirtualAfter - bytesVirtualBefore:N0} virtual bytes used.");
            WriteLine($"{timer.Elapsed} time span ellapsed.");
            WriteLine($"{timer.ElapsedMilliseconds:N0} total milliseconds ellapsed.");

        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Enumerable.Range(1, 10000).ToArray();
            Recorder.Start();
            WriteLine("Using string");
            string s = "";
            for(int i = 0; i < numbers.Length; i++)
            {
                s += numbers[i] + ", ";
            }
            Recorder.Stop();
            Recorder.Start();
            WriteLine("Using StringBuilder");
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < numbers.Length; i++)
            {
                builder.Append(numbers[i]);
                builder.Append(", ");
            }
            Recorder.Stop();
            ReadLine();

        }
    }
}
