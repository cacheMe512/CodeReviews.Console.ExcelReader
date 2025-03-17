using ConsoleTableExt;

using System.Diagnostics.CodeAnalysis;

namespace ExcelReader.cacheMe512;

internal class TableVisualizationEngine
{
    public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T : class
    {
        Console.Clear();

        if (tableName == null)
            tableName = "Sales Orders";

        Console.WriteLine("\n\n");

        ConsoleTableBuilder
            .From(tableData)
            .WithTitle(tableName)
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .ExportAndWriteLine(TableAligntment.Center);

        Console.WriteLine("\n\n");
    }
}
