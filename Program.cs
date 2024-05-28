// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace _Net_Microsoft_Learning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region dotnet_files
            dotnet_files dotnet_Files = new dotnet_files();
            char dpc = dotnet_Files.Directory_Separator_Char;

            Console.WriteLine("Current Directory : " + dpc);
            Console.WriteLine("Special Folder Directory : " + dotnet_Files.Special_Folder_Directory);
            Console.WriteLine("Directory Separator OS Wise: " + dotnet_Files.Directory_Separator_Char);
            Console.WriteLine("Path Comvine: " + dotnet_Files.path_comnine);
            Console.WriteLine("Get Extension: " + dotnet_Files.get_extension);
            Console.WriteLine("Directories : ");
            dotnet_Files.ListOfDirectories();
            Console.WriteLine("Files : ");
            dotnet_Files.ListOfFiles();
            Console.WriteLine("All Files in including sub folders: ");
            dotnet_Files.AllFilesInAllFolders();
            Console.WriteLine("Find Files: ");
            dotnet_Files.FindFiles(dotnet_Files.path, "greetings.txt");
            dotnet_Files.GetFileInfo(Path.Combine(dotnet_Files.path, $"sales.json"));
            dotnet_Files.CreateDirectory(Path.Combine(dotnet_Files.path, "New_Dir_1"));
            dotnet_Files.CreateFilesToDirectory(Path.Combine(dotnet_Files.path, "New_Dir_1","greetings.txt"),"Congratulations More !!");
            dotnet_Files.ReadFiles(Path.Combine(dotnet_Files.path, "New_Dir_1","greetings.txt"));
            dotnet_Files.ReadFiles($"{dotnet_Files.path}{dpc}201{dpc}sales.json");
            dotnet_Files.ReadSalesTotalFromJsonFile($"{dotnet_Files.path}{dpc}201{dpc}sales.json");
            dotnet_Files.WriteDataFileToFile($"{dotnet_Files.path}{dpc}201{dpc}sales.json", $"{dotnet_Files.path}{dpc}totals.txt");
            dotnet_Files.AppendDataFileToFile($"{dotnet_Files.path}{dpc}201{dpc}sales.json", $"{dotnet_Files.path}{dpc}totals.txt");
            dotnet_Files.Exercise_1(Path.Combine(dotnet_Files.path, "totals.txt"));
            decimal x = 7m / 5m;
            Console.WriteLine(x);
            #endregion
        }
    }
}

public class dotnet_files
{
    //vscode
    //git clone https://github.com/MicrosoftDocs/mslearn-dotnet-files && cd mslearn-dotnet-files
    //dotnet new console -f net6.0 -n mslearn-dotnet-files -o .
    //code -a .
    public static char dsc = Path.DirectorySeparatorChar;//Directory_Separator_Char
    public string path = $"H:{dsc}PL{dsc}_Net_Microsoft_Learning{dsc}Files{dsc}stores";

    public string Currect_Directory = Directory.GetCurrentDirectory();
    public string Special_Folder_Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public char Directory_Separator_Char = dsc;
    public string path_comnine = Path.Combine($"H:{dsc}PL{dsc}_Net_Microsoft_Learning{dsc}Files{dsc}stores", "201");
    public string get_extension = Path.GetExtension("sales.json");

    public void ListOfDirectories()
    {
        IEnumerable<string> lod = Directory.EnumerateDirectories(path);
        foreach (string dir in lod)
        {
            Console.WriteLine(dir);
        }
    }
    public void ListOfFiles()
    {        
        IEnumerable<string> lof = Directory.EnumerateFiles(path);
        foreach (string file in lof)
        {
            Console.WriteLine(file);
        }
    }
    public void AllFilesInAllFolders()
    {
        IEnumerable<string> allFilesInAllFolders = Directory.EnumerateFiles(path,"*.txt",SearchOption.AllDirectories);
        foreach (string file in allFilesInAllFolders)
        {
            Console.WriteLine(file);
        }
    }
    public void FindFiles(string folder,string file_name)
    {
        IEnumerable<string> foundFiles = Directory.EnumerateFiles(folder,"*",SearchOption.AllDirectories);
        foreach (string file in foundFiles)
        {
            if (file.EndsWith(file_name))
            {
                Console.WriteLine(file);
            }
        }
    }
    public IEnumerable<string> FindFileListOfDirectory(string folder)
    {
        List<string> files = new List<string>();
        IEnumerable<string> foundFiles = Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories);
        foreach (string file in foundFiles)
        {            
            if (Path.GetExtension(file)==".json")
            {
                files.Add(file);
            }            
        }
        return files;
    }
    public void GetFileInfo(string? fileName)
    {
        fileName = fileName ?? Path.Combine(path,$"sales.json");
        FileInfo info = new FileInfo(fileName);
        Console.WriteLine($"File Info :{Environment.NewLine}Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}"); // And many more

    }
    public void CreateDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            Console.WriteLine(path + " already exist");
        }
        else 
        {
            Directory.CreateDirectory(path);
            Console.WriteLine(path + " created susscessfully");
        }
            
    }
    public void CreateFilesToDirectory(string path,string txt)
    {
        if (File.Exists(path))
        {
            Console.WriteLine(path + " already exist and file overwriten");
        }
        else
        {
            Console.WriteLine(path + " created susscessfully");
        }

        File.WriteAllText(path, txt);
    }
    public void ReadFiles(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine(path + " Not exist");
        }
        else
        {
            Console.WriteLine(File.ReadAllText(path));
        }        
    }
    public void ReadSalesTotalFromJsonFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine(path + " Not exist");
        }
        else
        {
            var ext = Path.GetExtension(path);
            if (ext != ".json")
            {
                Console.WriteLine($"Its a {ext} file, Please provide a .json file");
            }
            else
            {
                string json_str = File.ReadAllText(path);
                var data = JsonConvert.DeserializeObject<SalesTotal>(json_str);
                Console.WriteLine("Total : " + data.Total);
            }            
        }
    }
    public void WriteDataFileToFile(string fromPath,string toPath)
    {
        var data = new SalesTotal();
        if (!File.Exists(fromPath))
        {
            Console.WriteLine(fromPath + " Not exist");
        }
        else
        {
            var ext = Path.GetExtension(fromPath);
            if (ext != ".json")
            {
                Console.WriteLine($"Its a {ext} file, Please provide a .json file");
            }
            else
            {
                string json_str = File.ReadAllText(fromPath);
                data = JsonConvert.DeserializeObject<SalesTotal>(json_str);
            }
        }
        if (File.Exists(toPath))
        {
            Console.WriteLine(toPath + " already exist and file overwriten");
        }
        else
        {
            Console.WriteLine(toPath + " created susscessfully");
        }

        File.WriteAllText(toPath, data.Total.ToString());
    }
    public void AppendDataFileToFile(string fromPath, string toPath)
    {
        var data = new SalesTotal();
        if (!File.Exists(fromPath))
        {
            Console.WriteLine(fromPath + " Not exist");
        }
        else
        {
            var ext = Path.GetExtension(fromPath);
            if (ext != ".json")
            {
                Console.WriteLine($"Its a {ext} file, Please provide a .json file");
            }
            else
            {
                string json_str = File.ReadAllText(fromPath);
                data = JsonConvert.DeserializeObject<SalesTotal>(json_str);
            }
        }
        if (File.Exists(toPath))
        {
            Console.WriteLine(toPath + " already exist and file overwriten");
        }
        else
        {
            Console.WriteLine(toPath + " created susscessfully");
        }

        File.AppendAllText(toPath, $"{Environment.NewLine}{data.Total}");
    }
    public void Exercise_1(string to_path)
    {
        var store_directory = path;
        var sales_files = FindFileListOfDirectory(store_directory);
        var sales_total = Calculate_Sales_Total(sales_files);

        File.AppendAllText(to_path,$"{Environment.NewLine}{sales_total}");
    }
    public double Calculate_Sales_Total(IEnumerable<string> files)
    {
        double sales_total = 0;
        foreach (var file in files)
        {
            string sales_json = File.ReadAllText(file);
            SalesTotal? sales_data = JsonConvert.DeserializeObject<SalesTotal>(sales_json);
            sales_total += sales_data?.Total ?? 0;
        }
        return sales_total;
    }
}
class SalesTotal
{
    public double Total { get; set; }
}