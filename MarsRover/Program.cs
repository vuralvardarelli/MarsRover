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

            //initializing class
            Processes processes = new Processes(console);

            //start process
            string message= processes.Init();

            console.WritelineToConsole($"\n{message}\n");


            console.WritelineToConsole("\nPress 'Enter' to exit the process..."); 

            //To exit process with only Enter key.
            while (console.ReadKey().Key != ConsoleKey.Enter)
            { }

        }
    }
}
