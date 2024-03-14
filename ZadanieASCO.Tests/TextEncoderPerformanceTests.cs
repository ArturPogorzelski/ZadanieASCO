using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieASCO.Core;

namespace ZadanieASCO.Tests
{

    public class TextEncoderPerformanceTests
    {
        private readonly Dictionary<char, char> characterPairs = Enumerable.Range(0, 100)
     .Take(40) 
     .ToDictionary(i => (char)('a' + i), i => (char)('1' + i % 40));
       

        [Fact]
        public void Encode_LongText_PerformanceTest()
        {
            // tworzy długi tekst wejściowy, powtarzając wzór
            string input = new StringBuilder().Insert(0, "abcdefghijklmnopqrstuvwxyz", 1000).ToString();
            var encoder = new TextEncoder(',', characterPairs);

            Stopwatch stopwatch = Stopwatch.StartNew();
            string encoded = encoder.Encode(input);
            stopwatch.Stop();

            
            Debug.WriteLine($"Encoding 1000x alphabet took: {stopwatch.ElapsedMilliseconds} ms");
        }

        [Fact]
        public void Decode_LongText_PerformanceTest()
        {
           
            string input = new StringBuilder().Insert(0, "abcdefghijklmnopqrstuvwxyz", 1000).ToString();
            var encoder = new TextEncoder(',', characterPairs);
            string encodedText = encoder.Encode(input);

            Stopwatch stopwatch = Stopwatch.StartNew();
            string decoded = encoder.Decode(encodedText);
            stopwatch.Stop();

           
            Debug.WriteLine($"Decoding took: {stopwatch.ElapsedMilliseconds} ms");
        }

        [Fact]
        public void Encode_LongText2_PerformanceTest()
        {
            
            string input = new StringBuilder().Insert(0, "abcdefghijklmnopqrstuvwxyz", 1000).ToString();
            var encoder = new TextEncoder2(',', characterPairs);

            Stopwatch stopwatch = Stopwatch.StartNew();
            string encoded = encoder.Encode(input);
            stopwatch.Stop();
            
            Debug.WriteLine($"Encoding 1000x alphabet took: {stopwatch.ElapsedMilliseconds} ms");
        }

        [Fact]
        public void Decode_LongText2_PerformanceTest()
        {
            
            string input = new StringBuilder().Insert(0, "abcdefghijklmnopqrstuvwxyz", 1000).ToString();
            var encoder = new TextEncoder2(',', characterPairs);
            string encodedText = encoder.Encode(input);

            Stopwatch stopwatch = Stopwatch.StartNew();
            string decoded = encoder.Decode(encodedText);
            stopwatch.Stop();

            Debug.WriteLine($"Decoding took: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
