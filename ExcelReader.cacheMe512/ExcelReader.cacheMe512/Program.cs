using ExcelReader.cacheMe512;

Console.WriteLine("Initializing Sales Order Application...");
SalesOrderContext.ResetDatabase();

string currentDirectory = Directory.GetCurrentDirectory();
string projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;

if (projectDirectory == null)
{
    Console.WriteLine("Error: Could not determine project directory.");
    return;
}

string filePath = Path.Combine(projectDirectory, "sales_orders.xlsx");

Console.WriteLine($"Loading file from: {filePath}");

if (!File.Exists(filePath))
{
    Console.WriteLine("Error: File not found. Make sure the file is in the project directory.");
    return;
}

SalesOrderUploader.UploadExcel(filePath);