// See https://aka.ms/new-console-template for more information

using ConsoleSudoku;
using Spectre.Console.Cli;

var app = new CommandApp<PlayCommand>();
return app.Run(args);