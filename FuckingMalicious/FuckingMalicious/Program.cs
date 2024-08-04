using System.Drawing;
using System.IO;
using Colorful;

namespace FuckingMalicious
{
    internal class Program
    {
        static void Log(string message, int type = 0)
        {
            switch (type)
            {
                case 0:
                    Colorful.Console.WriteLine($"[!] {message}", Color.Green);
                    break;
                case 1:
                    Colorful.Console.WriteLine($"[!] {message}", Color.Yellow);
                    break;
                case 2:
                    Colorful.Console.WriteLine($"[!] {message}", Color.Red);
                    break;
                default:
                    break;
            }
        }
        static void CheckFile(string PathFile)
        {
            string[] lines = File.ReadAllLines(PathFile);
            string file = Path.GetFileName(PathFile);
            if (file.EndsWith("..sln"))
            {
                Log($"[FILE] {PathFile}");
                Log($"[+] Most Likely A Screen Saver", 3);
                return;
            }
            if (file.EndsWith("suo"))
            {
                Log($"[FILE] {PathFile}");
                Log($"[+] Suo File Detected Auto Deleting | May Contain Malicous Shit", 0);
                File.Delete(PathFile);
                return;
            }
            foreach (string line in lines)
            {
                if (file.Contains("csproj") || file.Contains("vcxproj"))
                {
                    if (line.Contains("Exec") || line.Contains("ScriptLocation") || line.Contains("PowerShellExe") || line.Contains("<Command>"))
                    {
                        Log($"[FILE] {PathFile}");
                        Log("[+] Maybe Executing A Command!");
                    }
                }
            }
        }
        static void CheckFile2(string PathFile)
        {
            string[] lines = File.ReadAllLines(PathFile);
            string file = Path.GetFileName(PathFile);
            foreach (string line in lines)
            {
                if (file.Contains("cs") || file.Contains("cc") || file.Contains("cpp") || !file.Contains("h") || !file.Contains("c"))
                {
                    if (line.Contains("curl --silent"))
                    {
                        Log($"[FILE] {PathFile}");
                        Log("[+] Someone Trynna Download Something From Curl Silently", 2);
                    }
                    if (line.Contains("webhooks"))
                    {
                        Log($"[FILE] {PathFile}");
                        Log("[+] Discord Webhook Detected", 2);
                    }
                    if (line.Contains("http"))
                    {
                        Log($"[FILE] {PathFile}");
                        Log("[+] Url Detected", 2);
                    }
                    if (line.Contains("Convert"))
                    {
                        Log($"[FILE] {PathFile}");
                        Log("[+] Convert Detected | Can Be Encoded Url - Payload", 0);
                    }
                }
            }
        }
        static void GetCsproj(string path, int type)
        {
            string[] Folders = Directory.GetDirectories(path);
            foreach (string Folder in Folders)
            {
                string[] files = Directory.GetFiles(Folder);
                foreach (string File in files)
                {
                    if (type == 1)
                    {
                        CheckFile(File);
                    }
                    else if (type == 2)
                    {
                        CheckFile(File);
                        CheckFile2(File);
                    }
                }
                GetCsproj(Folder, type);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string SlnPath = args[0];
                int Type = int.Parse(args[1]);
                Colorful.Console.Title = "FuckingMalicous | Made By AntiBitter";
                if (Directory.Exists(SlnPath))
                {
                    Log($"Found Directory: {SlnPath}");
                    GetCsproj(SlnPath, Type);
                }
            }
            else
            {
                Log("Cant Read Instructions? Huh? Dumbass Autistic Child");
                Log("Argument 1 is path of directory. Argument 2 is type of scan. Recommended is 1.");
                Log("Example: FuckingLithium.exe C:/Repos/MaybeMalicous/ 1");
            }

            Colorful.Console.ReadLine();
        }
    }
}
