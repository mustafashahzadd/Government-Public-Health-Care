namespace Governement_Public_Health_Care.LogErrors
{
    public class ErrorsFile
    {
        private readonly string Path = "Log.txt";

        public void ErrorsDetail(string message, Exception ex = null)
        {
            try
            {
                string errorMessage = $"{DateTime.Now}: {message} - Exception: {ex}";
                if (ex != null)
                {
                    errorMessage = $"{DateTime.Now}: {message} - Exception: {ex}";
                }
                else if (!string.IsNullOrEmpty(message))
                {
                    errorMessage = $"{DateTime.Now}: {message}";
                }

                using (StreamWriter sw = File.AppendText(Path))
                {
                    sw.WriteLine(errorMessage);
                }
            }
            catch (Exception fileEx)
            {
                // Optionally, log this to a secondary logging mechanism
                Console.WriteLine($"Error writing to log file: {fileEx.Message}");
            }
        }
    }

}
