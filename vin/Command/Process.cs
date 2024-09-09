using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.IO;

using vin.Utils;

using System.Diagnostics;

namespace vin.Command
{

    internal class Prooocessesss
    {

        public static string CurrentDirDest = UserHomeDir;

        private static string UserHomeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private static string VinEnvDir = Path.Combine(UserHomeDir, "vin_env", "vars");
        private static string EnvVarsFile = Path.Combine(VinEnvDir, "env_vars.json");
        private static string AliasesFile = Path.Combine(VinEnvDir, "aliases.json");

        public static List<string> DecoraterCommands = new List<string>();
        public static List<string> AlertCommands = new List<string>();
        private static Dictionary<string, string> EnvironmentVariables = new Dictionary<string, string>();
        private static Dictionary<string, string> Aliases = new Dictionary<string, string>();
        private static List<string> CommandHistory = new List<string>();

        static Prooocessesss()
        {
            CurrentDirDest = Environment.CurrentDirectory;
            EnsureEnvironmentSetup();
            LoadEnvironmentVariables();
            LoadAliases();
        }

        // Ensure the directory and files exist, or create them after showing an error
        // Ensure the directory and files exist, or create them after showing an error
        private static void EnsureEnvironmentSetup()
        {
            try
            {
                if (!Directory.Exists(VinEnvDir))
                {
                    VinErrorList.New("Error: Environment directory does not exist. Creating directory at " + VinEnvDir);
                    Directory.CreateDirectory(VinEnvDir);
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                }

                if (!File.Exists(EnvVarsFile))
                {
                    VinErrorList.New("Error: env_vars.json file not found. Creating file.");
                    File.WriteAllText(EnvVarsFile, "{}");
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                }

                if (!File.Exists(AliasesFile))
                {
                    VinErrorList.New("Error: aliases.json file not found. Creating file.");
                    File.WriteAllText(AliasesFile, "{}");
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                }
            }
            catch (Exception ex)
            {
                VinErrorList.New($"Error creating environment setup: {ex.Message}");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
            }
        }

        private static void SaveEnvironmentVariables()
        {
            File.WriteAllText(EnvVarsFile, JsonSerializer.Serialize(EnvironmentVariables, new JsonSerializerOptions { WriteIndented = true }));
        }

        private static void LoadEnvironmentVariables()
        {
            try
            {
                if (File.Exists(EnvVarsFile))
                {
                    string json = File.ReadAllText(EnvVarsFile);
                    EnvironmentVariables = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                VinErrorList.New($"Error loading environment variables: {ex.Message}");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
            }
        }

        private static void SaveAliases()
        {
            File.WriteAllText(AliasesFile, JsonSerializer.Serialize(Aliases, new JsonSerializerOptions { WriteIndented = true }));
        }

        private static void LoadAliases()
        {
            try
            {
                if (File.Exists(AliasesFile))
                {
                    string json = File.ReadAllText(AliasesFile);
                    Aliases = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                VinErrorList.New($"Error loading aliases: {ex.Message}");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
            }
        }

        public static List<List<string>> DecoCommands(List<string> commands)
        {
            DecoraterCommands.Clear();

            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i].StartsWith("@"))
                {
                    if (!commands[i].Equals("@"))
                    {
                        DecoraterCommands.Add(commands[i]);
                        commands.RemoveAt(i);
                        i--;
                    }
                    else if (commands[i].Equals("@"))
                    {
                        commands.RemoveAt(i);
                        i--;
                    }
                }
            }

            return new List<List<string>> { DecoraterCommands, commands };
        }

        public static List<string> SeperateThemCommands(List<string> commands)
        {
            List<string> SepCommands = new List<string>();
            StringBuilder currentCommand = new StringBuilder();

            foreach (var command in commands)
            {
                if (command == ";")
                {
                    SepCommands.Add(currentCommand.ToString().Trim());
                    currentCommand.Clear();
                }
                else
                {
                    if (currentCommand.Length > 0)
                    {
                        currentCommand.Append(" ");
                    }
                    currentCommand.Append(command);
                }
            }

            if (currentCommand.Length > 0)
            {
                SepCommands.Add(currentCommand.ToString().Trim());
            }

            return SepCommands;
        }

        public static void ProcessBinCommand(string[] parts)
        {
            if (parts.Length == 1 && parts[0] == "@bin")
            {
                VinErrorList.New($"Usage: @bin <command> - Display file contents in binary format");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
                return;
            }

            string executable = parts.Length > 1 ? parts[1] : parts[0];
            string[] args = parts.Skip(2).ToArray();

            string filePath = $"C:\\Users\\{Environment.UserName}\\vin_env\\bin\\{executable}\\{executable}.exe";
            try
            {
                using (Process process = Process.Start(filePath, string.Join(" ", args)))
                {
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                VinErrorList.New($"Error starting process: {ex.Message}");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
            }
        }

        public static void ProcessIbinCommand(string[] parts)
        {

            //parts[0] = "";
            // Print the command parts for debugging purposes
            //Console.WriteLine($"PARTS: len: {parts.Length}");
            //foreach (var abc in parts)
            //{
            //    Console.WriteLine(abc);
            //}

            // Ensure the first part (parts[0]) is in the <executable>:<file> format
            if (parts.Length < 2 || !parts[1].Contains(":"))
            {
                VinErrorList.New($"Usage: @ibin <executable>:<file> <args> - Run binary files with arguments");
                //VinErrorList.New($"YourCommand:");
                //int x = 0;
                //foreach (var item in parts)
                //{
                //    VinErrorList.New($"{x}: Expected: `python:abc.py` Got: `{item}`");
                //    x = x + 1;
                //}
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
                return;
            }

            try
            {
                // Split the executable and file part (e.g., python:abc.py)
                string[] execFileParts = parts[1].Split(':');
                if (execFileParts.Length != 2)
                {
                    VinErrorList.New("Invalid command format. Expected format: <executable>:<file>");
                    //VinErrorList.New($"YourCommand:");
                    //int x = 0;
                    //foreach (var item in parts)
                    //{
                    //    VinErrorList.New($"{x}: Expected: `python:abc.py` Got: `{item}`");
                    //    x = x + 1;
                    //}
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                    return;
                }

                // Extract the executable (e.g., "python") and file (e.g., "abc.py")
                string executable = execFileParts[0];  // e.g., "python"
                string fileName = execFileParts[1];    // e.g., "abc.py"

                // Combine the rest of the arguments (if any) (e.g., arg1, arg2)
                string[] args = parts.Length > 2 ? parts.Skip(1).ToArray() : new string[1];  // Skip the first part

                // Construct the full path for the script in the ibin directory
                string filePath = Path.Combine($"C:\\Users\\{Environment.UserName}\\vin_env\\ibin\\{executable}", fileName);

                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    VinErrorList.New($"Error: File '{fileName}' not found in {filePath}");
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                    return;
                }

                // Prepare the process to execute the file
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = executable,  // e.g., "python"
                    Arguments = $"\"{filePath}\" " + string.Join(" ", args),  // Script path and arguments
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the process
                using (Process process = Process.Start(processStartInfo))
                {
                    // Capture the output and error streams
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    // Display the output or error
                    if (!string.IsNullOrEmpty(output))
                    {
                        Console.WriteLine(output);
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        VinErrorList.New($"Error: {error}");
                        VinErrorList.ListThem();
                        VinErrorList.CacheClean();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle general errors during process execution
                VinErrorList.New($"Error starting process: {ex.Message}");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
            }
        }

        private static string GetExtension(string executable)
        {
            switch (executable)
            {
                case "py":
                    return "py";
                case "roobi":
                    return "rb";
                default:
                    throw new ArgumentException("Unsupported executable");
            }
        }

        public static void ProoocessesssEach(List<string> commands)
        {
            if (!commands.Any()) { return; }

            foreach (var command in commands)
            {
                CommandHistory.Add(command);
                var parts = command.Split(' ');
                string mainCommand = parts[0].ToLower();

                // Check if the command is an alias
                if (Aliases.ContainsKey(mainCommand))
                {
                    var aliasedCommand = Aliases[mainCommand];
                    parts = (aliasedCommand + " " + string.Join(" ", parts.Skip(1))).Split(' ');
                    mainCommand = parts[0].ToLower();
                }

                switch (mainCommand)
                {
                    case "@help":
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(" @help     - Get this help message");
                        Console.WriteLine(" @cls      - Clear the console");
                        Console.WriteLine(" @exit     - Exit the application");
                        Console.WriteLine(" @evars    - Manage environment variables ('set', 'get', 'list', 'unset')");
                        Console.WriteLine(" @alias    - Manage command aliases ('add', 'remove', 'list')");
                        Console.WriteLine(" @history  - View or clear command history");
                        Console.WriteLine(" @encrypt  - Encrypt a file or directory");
                        Console.WriteLine(" @decrypt  - Decrypt a file or directory");
                        Console.WriteLine(" @bin      - Display file contents in binary format");
                        Console.WriteLine(" @ibin     - Convert binary string back to file");
                        Console.WriteLine(" @cd       - Change the current directory");
                        Console.ResetColor();

                        Console.WriteLine("\n!: You can add more commands and aliases! Just explore and have fun!");
                        Console.WriteLine("!: Need more help? Type '@help' anytime!");
                        Console.ResetColor();
                        break;



                    case "@cls":
                        Console.Clear();
                        break;

                    case "@cd":
                        ProoocessesssCdCommand(parts);
                        break;

                    case "@exit":
                        Console.WriteLine("Exiting the application...");
                        Environment.Exit(0);
                        break;

                    case "@evars":
                        ProoocessesssEnvCommand(parts);
                        break;

                    case "@alias":
                        ProoocessesssAliasCommand(parts);
                        break;

                    case "@history":
                        ProoocessesssHistoryCommand(parts);
                        break;

                    case "@encrypt":
                    case "@decrypt":
                        ProoocessesssEncryptionCommand(parts);
                        break;

                    case "@bin":
                        ProcessBinCommand(parts);
                        break;

                    case "@ibin":
                        ProcessIbinCommand(parts);
                        break;

                    default:
                        VinErrorList.New($"Command: `{command}` is not a valid internal command, type `@help` for help!");
                        VinErrorList.ListThem();
                        VinErrorList.CacheClean();
                        break;
                }
            }
        }

        private static void ProoocessesssCdCommand(string[] parts)
        {
            if (parts.Length < 2)
            {
                VinErrorList.New($"Usage: @cd <directory> - Change the current working directory.");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
                return;
            }

            string newDir = parts[1];

            try
            {
                if (newDir == "..")
                {
                    // Move up one directory level
                    newDir = Path.GetDirectoryName(CurrentDirDest);
                }
                else if (newDir == "~")
                {
                    newDir = UserHomeDir;
                }
                else if (!Path.IsPathRooted(newDir))
                {
                    newDir = Path.Combine(CurrentDirDest, newDir);
                }

                if (newDir == null)
                {
                    VinErrorList.New($"Error: Cannot navigate above the root directory.");
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                    return;
                }

                newDir = Path.GetFullPath(newDir);

                if (Directory.Exists(newDir))
                {
                    CurrentDirDest = newDir;
                    Environment.CurrentDirectory = newDir;  // Ensure the process working directory is also updated
                    Console.WriteLine($"Changed directory to: {CurrentDirDest}");
                }
                else
                {
                    VinErrorList.New($"Error: Directory '{newDir}' does not exist.");
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                }
            }
            catch (Exception ex)
            {
                VinErrorList.New($"Error: {ex.Message}");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
            }
        }


        private static void ProoocessesssEnvCommand(string[] parts)
        {
            if (parts.Length < 2) return;

            switch (parts[1].ToLower())
            {
                case "set":
                    if (parts.Length >= 4)
                    {
                        EnvironmentVariables[parts[2]] = string.Join(" ", parts.Skip(3));
                        SaveEnvironmentVariables();
                        //Console.WriteLine($"Environment variable '{parts[2]}' set.");
                        VinOutput.result($"Environment variable '{parts[2]}' set.");
                    }
                    break;
                case "get":
                    if (parts.Length >= 3 && EnvironmentVariables.ContainsKey(parts[2]))
                    {
                        VinOutput.result(EnvironmentVariables[parts[2]]);
                    }
                    else
                    {
                        VinErrorList.New($"Environment variable '{parts[2]}' not found.");
                        VinErrorList.ListThem();
                        VinErrorList.CacheClean();
                    }
                    break;
                case "list":
                    if (EnvironmentVariables.Count == 0)
                    {
                        VinErrorList.New("No environment variables set.");
                        VinErrorList.ListThem();
                        VinErrorList.CacheClean();
                    }
                    else
                    {
                        foreach (var kvp in EnvironmentVariables)
                        {
                            Console.WriteLine($" {kvp.Key} ==> {kvp.Value}");
                        }
                    }
                    break;
                case "unset":
                    if (parts.Length >= 3)
                    {
                        if (EnvironmentVariables.Remove(parts[2]))
                        {
                            SaveEnvironmentVariables();
                            Console.WriteLine($"Environment variable '{parts[2]}' removed.");
                        }
                        else
                        {
                            VinErrorList.New($"Environment variable '{parts[2]}' not found.");
                            VinErrorList.ListThem();
                            VinErrorList.CacheClean();
                        }
                    }
                    break;
                default:
                    VinErrorList.New("Invalid @evars command. Use 'set', 'get', 'list', or 'unset'.");
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                    break;
            }
        }

        private static void ProoocessesssAliasCommand(string[] parts)
        {
            if (parts.Length < 2) return;

            switch (parts[1].ToLower())
            {
                case "add":
                    if (parts.Length >= 4)
                    {
                        Aliases[parts[2]] = string.Join(" ", parts.Skip(3));
                        SaveAliases();
                        VinOutput.result($"Alias '{parts[2]}' added.");
                    }
                    else
                    {
                        VinErrorList.New("Invalid alias command. Use '@alias add [name] [command]'.");
                        VinErrorList.ListThem();
                        VinErrorList.CacheClean();
                    }
                    break;
                case "remove":
                    if (parts.Length >= 3)
                    {
                        if (Aliases.Remove(parts[2]))
                        {
                            SaveAliases();
                            VinOutput.result($"Alias '{parts[2]}' removed.");
                        }
                        else
                        {
                            VinErrorList.New($"Alias '{parts[2]}' not found.");
                            VinErrorList.ListThem();
                            VinErrorList.CacheClean();
                        }
                    }
                    else
                        Console.WriteLine("Invalid alias command. Use '@alias remove [name]'.");
                    break;
                case "list":
                    if (Aliases.Count == 0)
                    {
                        VinErrorList.New("No aliases defined.");
                        VinErrorList.ListThem();
                        VinErrorList.CacheClean();
                    }
                    else
                    {
                        foreach (var kvp in Aliases)
                        {
                            Console.WriteLine($" {kvp.Key} ==> {kvp.Value}");
                        }
                    }
                    break;
                default:
                    VinErrorList.New("Invalid @alias command. Use 'add', 'remove', or 'list'.");
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                    break;
            }
        }

        private static void ProoocessesssHistoryCommand(string[] parts)
        {
            if (parts.Length > 1 && parts[1].ToLower() == "clear")
            {
                CommandHistory.Clear();
                VinOutput.result("Command history cleared.");
            }
            else
            {
                if (CommandHistory.Count == 0)
                {
                    VinErrorList.New("Command history is empty.");
                    VinErrorList.ListThem();
                    VinErrorList.CacheClean();
                }
                else
                    for (int i = 0; i < CommandHistory.Count; i++)
                        Console.WriteLine($"{i + 1}: {CommandHistory[i]}");
            }
        }

        private static void ProoocessesssEncryptionCommand(string[] parts)
        {
            if (parts.Length < 2)
            {
                VinErrorList.New($"Usage: {parts[0]} [file_or_directory]");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
                return;
            }

            string path = parts[1];
            bool isEncrypt = parts[0].ToLower() == "@encrypt";

            if (File.Exists(path))
            {
                ProoocessesssFileEncryption(path, isEncrypt);
            }
            else if (Directory.Exists(path))
            {
                ProoocessesssDirectoryEncryption(path, isEncrypt);
            }
            else
            {
                VinErrorList.New($"File or directory not found: {path}");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
            }
        }

        private static void ProoocessesssFileEncryption(string filePath, bool isEncrypt)
        {
            try
            {
                string outputPath = isEncrypt ? filePath + ".enc" : filePath.Replace(".enc", "");
                byte[] key = new byte[32]; // 256-bit key
                byte[] iv = new byte[16];  // 128-bit IV

                // In a real-world scenario, you'd want to securely generate and manage these
                new Random().NextBytes(key);
                new Random().NextBytes(iv);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;

                    using (FileStream inputFile = new FileStream(filePath, FileMode.Open))
                    using (FileStream outputFile = new FileStream(outputPath, FileMode.Create))
                    {
                        ICryptoTransform cryptoTransform = isEncrypt
                            ? aes.CreateEncryptor()
                            : aes.CreateDecryptor();

                        using (CryptoStream cryptoStream = new CryptoStream(outputFile, cryptoTransform, CryptoStreamMode.Write))
                        {
                            inputFile.CopyTo(cryptoStream);
                        }
                    }
                }

                VinOutput.result($"{(isEncrypt ? "Encrypted" : "Decrypted")} file: {outputPath}");
            }
            catch (Exception ex)
            {
                VinErrorList.New($"Error during {(isEncrypt ? "encryption" : "decryption")}: {ex.Message}");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
            }
        }

        private static void ProoocessesssDirectoryEncryption(string dirPath, bool isEncrypt)
        {
            try
            {
                foreach (string filePath in Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories))
                {
                    ProoocessesssFileEncryption(filePath, isEncrypt);
                }
                VinOutput.result($"Finished {(isEncrypt ? "encrypting" : "decrypting")} directory: {dirPath}");
            }
            catch (Exception ex)
            {
                VinErrorList.New($"Error during directory {(isEncrypt ? "encryption" : "decryption")}: {ex.Message}");
                VinErrorList.ListThem();
                VinErrorList.CacheClean();
            }
        }
    }

    public class PleaseProoocessesss
    {
        public static void TheseCommands(List<string> commands)
        {
            List<string> separatedCommands = Prooocessesss.SeperateThemCommands(commands);
            Prooocessesss.ProoocessesssEach(separatedCommands);
        }
    }
}