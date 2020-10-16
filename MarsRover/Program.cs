using MarsRover.Log;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                                           .AddSingleton<IConsole, ConsoleActions>()
                                           .BuildServiceProvider();

            Console.WriteLine("Welcome to Mars Rover Project");
            Console.WriteLine("\n");
            Console.WriteLine("Please type your inputs and hit 'Enter' for each.");
            Console.WriteLine("** If you want to move Rovers , please type 'start' as a new input value and hit 'Enter'.");
            Console.WriteLine("** If you want to close application , please type 'exit' as a new input value and hit 'Enter'.\n");
            Console.WriteLine("Inputs :\n");
            //initializing class
            Processes processes = new Processes(serviceProvider);

            //start process
            processes.Init();

            Console.WriteLine("\nPress 'Enter' to exit the process..."); 

            //To exit process with only Enter key.
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            { }

        }
    }
}
