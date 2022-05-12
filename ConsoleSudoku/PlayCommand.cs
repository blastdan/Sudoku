using Spectre.Console;
using Spectre.Console.Cli;
using SudokuDomain;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleSudoku
{
    internal class PlayCommand : Command<PlayCommand.Settings>
    {
        private struct CellSelection : IEqualityComparer<CellSelection>
        {
            public RowIdentifier Row { get; set; }
            public ColumnIdentifier Column { get; set; }

            public CellSelection(RowIdentifier row, ColumnIdentifier column)
            {
                this.Row = row;
                this.Column = column;
            }

            public bool Equals(CellSelection x, CellSelection y)
            {
                return x.Row == y.Row && x.Column == y.Column;
            }

            public int GetHashCode([DisallowNull] CellSelection obj)
            {
                return obj.Row.GetHashCode() ^ obj.Column.GetHashCode();
            }

            public override string? ToString()
            {
                return (this.Row, this.Column) switch
                {
                    (RowIdentifier.None, ColumnIdentifier.None) => "No cell selected",
                    (RowIdentifier.None, _) => $"Column {this.Column}",
                    (_, ColumnIdentifier.None) => $"Row {this.Row}",
                    (_, _) => $"Column {this.Column}"
                };
            }
        }

        public sealed class Settings : CommandSettings
        {
            [Description("Start a game of Sudoku")]
            [CommandArgument(0, "[play]")]
            public string? SearchPath { get; init; }
        }

        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            var gridDisplay = GenerateEmptyTable();
            var liveDisplay = AnsiConsole.Live(gridDisplay);
            var sudokuGrid = new SudokuGrid();

            AnsiConsole.Status()
                .Spinner(Spinner.Known.Hearts)
                .Start("Starting the Game!", ctx =>
                {
                    Thread.Sleep(2000);
                });

            sudokuGrid.GridUpdates.Subscribe(update =>
            {
                liveDisplay.Start(ctx =>
                {
                    var styledValue = new Panel(new Markup($"[bold]{(int)update.Value}[/]"));
                    gridDisplay.Rows.Update((int)update.RowId - 1, (int)update.ColumnId - 1, styledValue);
                    ctx.Refresh();
                });
            });

            var RowColumnSelection = GenerateCellSelectionPrompt();

            while (!sudokuGrid.Completed)
            {
                var column = AnsiConsole.Prompt(new TextPrompt<ColumnIdentifier>("What Column 1-9?")
                                        .PromptStyle("bold")
                                        .Validate(c =>
                                        {
                                            return c switch
                                            {
                                                0 => ValidationResult.Error("Must be 1-9"),
                                                _ => ValidationResult.Success(),
                                            };
                                        }));

                //var selection = AnsiConsole.Prompt(RowColumnSelection);
                //var column = AnsiConsole.Ask<ColumnIdentifier>("What column 1-9?");
                var row = AnsiConsole.Ask<RowIdentifier>("What row 1-9?");
                var value = AnsiConsole.Ask<CellRange>("What value 1-9?");

                sudokuGrid.WriteValue(row, column, value);
            }

            return 0;
        }

        private static SelectionPrompt<CellSelection> GenerateCellSelectionPrompt()
        {
            return new SelectionPrompt<CellSelection>()
                            .Title("Select which cell to enter a value for")
                            .MoreChoicesText("[grey](Move up and down to select a row)[/]")
                            .AddChoiceGroup(new CellSelection(RowIdentifier.R1, ColumnIdentifier.None),
                            new CellSelection(RowIdentifier.R1, ColumnIdentifier.C1),
                            new CellSelection(RowIdentifier.R1, ColumnIdentifier.C2),
                            new CellSelection(RowIdentifier.R1, ColumnIdentifier.C3),
                            new CellSelection(RowIdentifier.R1, ColumnIdentifier.C4),
                            new CellSelection(RowIdentifier.R1, ColumnIdentifier.C5),
                            new CellSelection(RowIdentifier.R1, ColumnIdentifier.C6),
                            new CellSelection(RowIdentifier.R1, ColumnIdentifier.C7),
                            new CellSelection(RowIdentifier.R1, ColumnIdentifier.C8),
                            new CellSelection(RowIdentifier.R1, ColumnIdentifier.C9))
                            .AddChoiceGroup(new CellSelection(RowIdentifier.R2, ColumnIdentifier.None),
                            new CellSelection(RowIdentifier.R2, ColumnIdentifier.C1),
                            new CellSelection(RowIdentifier.R2, ColumnIdentifier.C2),
                            new CellSelection(RowIdentifier.R2, ColumnIdentifier.C3),
                            new CellSelection(RowIdentifier.R2, ColumnIdentifier.C4),
                            new CellSelection(RowIdentifier.R2, ColumnIdentifier.C5),
                            new CellSelection(RowIdentifier.R2, ColumnIdentifier.C6),
                            new CellSelection(RowIdentifier.R2, ColumnIdentifier.C7),
                            new CellSelection(RowIdentifier.R2, ColumnIdentifier.C8),
                            new CellSelection(RowIdentifier.R2, ColumnIdentifier.C9))
                            .AddChoiceGroup(new CellSelection(RowIdentifier.R3, ColumnIdentifier.None),
                            new CellSelection(RowIdentifier.R3, ColumnIdentifier.C1),
                            new CellSelection(RowIdentifier.R3, ColumnIdentifier.C2),
                            new CellSelection(RowIdentifier.R3, ColumnIdentifier.C3),
                            new CellSelection(RowIdentifier.R3, ColumnIdentifier.C4),
                            new CellSelection(RowIdentifier.R3, ColumnIdentifier.C5),
                            new CellSelection(RowIdentifier.R3, ColumnIdentifier.C6),
                            new CellSelection(RowIdentifier.R3, ColumnIdentifier.C7),
                            new CellSelection(RowIdentifier.R3, ColumnIdentifier.C8),
                            new CellSelection(RowIdentifier.R3, ColumnIdentifier.C9))
                            .AddChoiceGroup(new CellSelection(RowIdentifier.R4, ColumnIdentifier.None),
                            new CellSelection(RowIdentifier.R4, ColumnIdentifier.C1),
                            new CellSelection(RowIdentifier.R4, ColumnIdentifier.C2),
                            new CellSelection(RowIdentifier.R4, ColumnIdentifier.C3),
                            new CellSelection(RowIdentifier.R4, ColumnIdentifier.C4),
                            new CellSelection(RowIdentifier.R4, ColumnIdentifier.C5),
                            new CellSelection(RowIdentifier.R4, ColumnIdentifier.C6),
                            new CellSelection(RowIdentifier.R4, ColumnIdentifier.C7),
                            new CellSelection(RowIdentifier.R4, ColumnIdentifier.C8),
                            new CellSelection(RowIdentifier.R4, ColumnIdentifier.C9))
                            .AddChoiceGroup(new CellSelection(RowIdentifier.R5, ColumnIdentifier.None),
                            new CellSelection(RowIdentifier.R5, ColumnIdentifier.C1),
                            new CellSelection(RowIdentifier.R5, ColumnIdentifier.C2),
                            new CellSelection(RowIdentifier.R5, ColumnIdentifier.C3),
                            new CellSelection(RowIdentifier.R5, ColumnIdentifier.C4),
                            new CellSelection(RowIdentifier.R5, ColumnIdentifier.C5),
                            new CellSelection(RowIdentifier.R5, ColumnIdentifier.C6),
                            new CellSelection(RowIdentifier.R5, ColumnIdentifier.C7),
                            new CellSelection(RowIdentifier.R5, ColumnIdentifier.C8),
                            new CellSelection(RowIdentifier.R5, ColumnIdentifier.C9))
                            .AddChoiceGroup(new CellSelection(RowIdentifier.R6, ColumnIdentifier.None),
                            new CellSelection(RowIdentifier.R6, ColumnIdentifier.C1),
                            new CellSelection(RowIdentifier.R6, ColumnIdentifier.C2),
                            new CellSelection(RowIdentifier.R6, ColumnIdentifier.C3),
                            new CellSelection(RowIdentifier.R6, ColumnIdentifier.C4),
                            new CellSelection(RowIdentifier.R6, ColumnIdentifier.C5),
                            new CellSelection(RowIdentifier.R6, ColumnIdentifier.C6),
                            new CellSelection(RowIdentifier.R6, ColumnIdentifier.C7),
                            new CellSelection(RowIdentifier.R6, ColumnIdentifier.C8),
                            new CellSelection(RowIdentifier.R6, ColumnIdentifier.C9))
                            .AddChoiceGroup(new CellSelection(RowIdentifier.R7, ColumnIdentifier.None),
                            new CellSelection(RowIdentifier.R7, ColumnIdentifier.C1),
                            new CellSelection(RowIdentifier.R7, ColumnIdentifier.C2),
                            new CellSelection(RowIdentifier.R7, ColumnIdentifier.C3),
                            new CellSelection(RowIdentifier.R7, ColumnIdentifier.C4),
                            new CellSelection(RowIdentifier.R7, ColumnIdentifier.C5),
                            new CellSelection(RowIdentifier.R7, ColumnIdentifier.C6),
                            new CellSelection(RowIdentifier.R7, ColumnIdentifier.C7),
                            new CellSelection(RowIdentifier.R7, ColumnIdentifier.C8),
                            new CellSelection(RowIdentifier.R7, ColumnIdentifier.C9))
                            .AddChoiceGroup(new CellSelection(RowIdentifier.R8, ColumnIdentifier.None),
                            new CellSelection(RowIdentifier.R8, ColumnIdentifier.C1),
                            new CellSelection(RowIdentifier.R8, ColumnIdentifier.C2),
                            new CellSelection(RowIdentifier.R8, ColumnIdentifier.C3),
                            new CellSelection(RowIdentifier.R8, ColumnIdentifier.C4),
                            new CellSelection(RowIdentifier.R8, ColumnIdentifier.C5),
                            new CellSelection(RowIdentifier.R8, ColumnIdentifier.C6),
                            new CellSelection(RowIdentifier.R8, ColumnIdentifier.C7),
                            new CellSelection(RowIdentifier.R8, ColumnIdentifier.C8),
                            new CellSelection(RowIdentifier.R8, ColumnIdentifier.C9))
                            .AddChoiceGroup(new CellSelection(RowIdentifier.R9, ColumnIdentifier.None),
                            new CellSelection(RowIdentifier.R9, ColumnIdentifier.C1),
                            new CellSelection(RowIdentifier.R9, ColumnIdentifier.C2),
                            new CellSelection(RowIdentifier.R9, ColumnIdentifier.C3),
                            new CellSelection(RowIdentifier.R9, ColumnIdentifier.C4),
                            new CellSelection(RowIdentifier.R9, ColumnIdentifier.C5),
                            new CellSelection(RowIdentifier.R9, ColumnIdentifier.C6),
                            new CellSelection(RowIdentifier.R9, ColumnIdentifier.C7),
                            new CellSelection(RowIdentifier.R9, ColumnIdentifier.C8),
                            new CellSelection(RowIdentifier.R9, ColumnIdentifier.C9));
        }

        private static Table GenerateEmptyTable()
        {
            var table = new Table();
            table.Centered();
            table.HideHeaders();
            table.Border(TableBorder.Heavy);

            table.AddColumn(new TableColumn("C1").Centered().NoWrap());
            table.AddColumn(new TableColumn("C2").Centered().NoWrap());
            table.AddColumn(new TableColumn("C3").Centered().NoWrap());
            table.AddColumn(new TableColumn("C4").Centered().NoWrap());
            table.AddColumn(new TableColumn("C5").Centered().NoWrap());
            table.AddColumn(new TableColumn("C6").Centered().NoWrap());
            table.AddColumn(new TableColumn("C7").Centered().NoWrap());
            table.AddColumn(new TableColumn("C8").Centered().NoWrap());
            table.AddColumn(new TableColumn("C9").Centered().NoWrap());

            table.AddRow(EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel());
            table.AddRow(EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel());
            table.AddRow(EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel());
            table.AddRow(EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel());
            table.AddRow(EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel());
            table.AddRow(EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel());
            table.AddRow(EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel());
            table.AddRow(EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel());
            table.AddRow(EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel(), EmptyPanel());

            return table;
        }

        private static Panel EmptyPanel()
        {
            var panel = new Panel(new Markup("[bold] [/]"));
            panel.Border = BoxBorder.Rounded;

            return panel;
        }
    }
}