using System.Globalization;

namespace Gamerscore.Core
{
    public class MessageLogger
    {
        //A simple message logger used for debugging
        public static void Log(string message)
        {
            try
            {
                const string filePath = "output.log";
                using StreamWriter writer = new(filePath, true);
                var date = DateTime.Now.ToString(CultureInfo.CurrentCulture);
                writer.WriteLine(date + " - " + message);
            }
            catch
            {

            }
        }
    }
}
