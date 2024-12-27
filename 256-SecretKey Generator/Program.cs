using System.Security.Cryptography;
using System.Text;
using TextCopy;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a phrase to generate a 256-bit key:");
        string input = Console.ReadLine();

        // Add a timestamp to the phrase
        string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
        string inputWithTimestamp = input + timestamp;

        // Generate the 256-bit key using the phrase with timestamp
        string key = Generate256BitKey(inputWithTimestamp);

        Console.WriteLine("\nYour 256-bit key (Base64):");
        Console.WriteLine(key);

        try
        {
            Clipboard cb = new Clipboard();
            // Copy the key to the clipboard
            cb.SetText(key);
            Console.WriteLine("\nThe key has been copied to the clipboard.");
            Console.WriteLine("Press any key to close the program.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nError copying to clipboard: " + ex.Message);
            Console.WriteLine("Press any key to close the program. DONT FORGET TO COPY YOUR KEY!");
            Console.ReadLine();
        }
    }

    static string Generate256BitKey(string phrase)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Converte a frase em bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(phrase);

            // Calcula o hash SHA256
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Converte o hash para Base64
            return Convert.ToBase64String(hashBytes);
        }
    }
}
