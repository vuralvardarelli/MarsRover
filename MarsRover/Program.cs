using MarsRover.Log;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {

            //To implement Dependency Injection on .Net Core Console App
            ServiceProvider serviceProvider = new ServiceCollection()
                                           .AddSingleton<IConsole, ConsoleActions>()
                                           .BuildServiceProvider();

            //To get console interface for actions
            IConsole console = serviceProvider.GetRequiredService<IConsole>();

            console.WritelineToConsole("Welcome to Mars Rover Project");
            console.WritelineToConsole("\n");
            console.WritelineToConsole("Please type your inputs and hit 'Enter' for each.");
            console.WritelineToConsole("** If you want to move Rovers , please type 'start' as a new input value and hit 'Enter'.");
            console.WritelineToConsole("** If you want to close application , please type 'exit' as a new input value and hit 'Enter'.\n");
            console.WritelineToConsole("Inputs :\n");

            //initializing class
            Processes processes = new Processes(console);

            //start process
            processes.Init();

            console.WritelineToConsole("\nPress 'Enter' to exit the process..."); 

            //To exit process with only Enter key.
            while (console.ReadKey().Key != ConsoleKey.Enter)
            { }

        }
    }
}
