using System;
using System.Collections.Specialized;
using Generic_Stack;

namespace Lab1_Generic
{
    class Program
    {
        enum Options { Exit, Push, Pop, Peek, Swap, Show }

        static Stack<int> ts;

        static string GetInfo;

        static void Main(string[] args)
        {
            ts = new Stack<int>();

            ts.CollectionChanged += Ts_CollectionChanged;

            uint Option = 0;

            bool Repeat = true;

            while (Repeat)
            {
                if (GetInfo != null)
                    Console.WriteLine($"Info:\n{GetInfo}\n");

                Console.Write(
                    "Options:\n" +
                    $"{(uint)Options.Push} - Add an item\n" +
                    $"{(uint)Options.Pop} - Remove an item\n" +
                    $"{(uint)Options.Peek} - Show the head item\n" +
                    $"{(uint)Options.Swap} - Swap two latest items\n" +
                    $"{(uint)Options.Show} - Show the stack\n" +
                    $"{(uint)Options.Exit} - Exit\n\n" +
                    "Choose option: "
                );

                while (!uint.TryParse(Console.ReadLine(), out Option) || Option > 6)
                    Console.Write("Incorrect input!\n\nChoode option: ");
                Console.WriteLine();

                switch (Option)
                {
                    case (uint)Options.Push:

                        int NewItem = 0;

                        Console.Write("Enter item: ");
                        while (!int.TryParse(Console.ReadLine(), out NewItem))
                            Console.Write("Incorrect input!\nEnter item: ");

                        ts.Push(NewItem);

                        break;

                    case (uint)Options.Pop:

                        try 
                        {
                            Console.WriteLine($"Removed item is {ts.Pop()}");
                        }

                        catch (InvalidOperationException e) 
                        { 
                            Console.WriteLine(e.Message); 
                        }

                        break;

                    case (uint)Options.Peek:

                        try
                        {
                            Console.WriteLine($"Item is {ts.Peek()}");
                        }

                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;

                    case (uint)Options.Swap:

                        try
                        {
                            ts.Swap();
                        }

                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;

                    case (uint)Options.Show:

                        string Output = "Stack: ";

                        if (ts.Count == 0)
                            Output += "empty";

                        else
                            foreach (var item in ts)
                                Output += item + " ";

                        Console.WriteLine(Output);

                        break;

                    case (uint)Options.Exit:

                        Repeat = false;

                        break;
                }

                if (Repeat)
                {
                    Console.WriteLine();
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void Ts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GetInfo = "Stack changed latest action is ";

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    GetInfo += $"Add\nAdded item is {(int)e.NewItems[0]}\n";
                    break;

                case NotifyCollectionChangedAction.Remove:
                    GetInfo += $"Remove\nRemoved item is {(int)e.OldItems[0]}\n";
                    break;

                case NotifyCollectionChangedAction.Replace:
                    GetInfo += $"Replace\n" +
                        $"Replaced items are {(int)e.OldItems[0]} and {(int)e.OldItems[1]} " +
                        $"to {(int)e.NewItems[0]} and {(int)e.NewItems[1]}\n";
                    break;
            }

            GetInfo+=$"Amount of items: {(sender as Stack<int>).Count}";
        }
    }
}