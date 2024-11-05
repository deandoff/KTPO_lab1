using KTPO4317.Akhmetgaleev.Lib.LogAn;

namespace KTPO4317.Akhmetgaleev.Service
{
    public class Program
    {
        static void Main()
        {
            LogAnalyzer logAnalyzer = new LogAnalyzer();

            string[] fileNames = { "file1.log", "file2.txt", "file3.log", "file4.doc" };

            foreach (var fileName in fileNames)
            {
                bool isValid = logAnalyzer.IsValidLogFileName(fileName);
                Console.WriteLine($"File: {fileName}, Valid: {isValid}");
            }
        }
    }
}
