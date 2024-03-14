using System.Diagnostics;
using ZadanieASCO.Core;



public class Program
{
    static void Main(string[] args)
    {
        // Zdefiniuj znaki specjalne i ich zakodowane odpowiedniki.
        var characterPairs = new Dictionary<char, char>
        {
            { '>', 'g' }, { '<', 'l' }, { '&', 'a' }

        };


        var encoder = new TextEncoder(',', characterPairs);

        // Przykładowy tekst do kodowania i dekodowania
        string textToEncode = "Here is some text with special characters: <, >, &.";

        // Benchmarking metody Encode
        Stopwatch stopwatchEncode = Stopwatch.StartNew();
        string encodedText = encoder.Encode(textToEncode);
        stopwatchEncode.Stop();
        Console.WriteLine($"Encoding took: {stopwatchEncode.ElapsedMilliseconds} ms");

        // Benchmarking metody Decode
        Stopwatch stopwatchDecode = Stopwatch.StartNew();
        string decodedText = encoder.Decode(encodedText);
        stopwatchDecode.Stop();
        Console.WriteLine($"Decoding took: {stopwatchDecode.ElapsedMilliseconds} ms");

        // Wyświetlenie zakodowanego i odkodowanego tekstu dla weryfikacji
        Console.WriteLine($"Encoded text: {encodedText}");
        Console.WriteLine($"Decoded text: {decodedText}");
    }
}
