namespace ZadanieASCO.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TextEncoder
    {
        // Znak używany do wskazywania początku zakodowanego znaku.
        private readonly char _escapingCharacter;

        // Słownik przechowujący pary znaków do kodowania.
        private readonly Dictionary<char, char> _characterPairs;

        // Słownik przechowujący pary znaków do dekodowania.
        private readonly Dictionary<char, char> _reverseCharacterPairs;

        public TextEncoder(char escapingCharacter, IEnumerable<KeyValuePair<char, char>> characterPairs)
        {
            _escapingCharacter = escapingCharacter; // Ustaw znak ucieczki.
                                                    // Utwórz mapę kodowania z podanych par znaków.
            _characterPairs = characterPairs.ToDictionary(pair => pair.Key, pair => pair.Value);
            // Utwórz mapę dekodowania, odwracając pary znaków.
            _reverseCharacterPairs = _characterPairs.ToDictionary(pair => pair.Value, pair => pair.Key);
        }

        // Metoda do kodowania tekstu.
        public string Encode(string text)
        {
            var encodedText = new StringBuilder();

            foreach (var character in text)
            {
                if (_characterPairs.ContainsKey(character))
                {
                    encodedText.Append(_escapingCharacter).Append(_characterPairs[character]);
                }
                else if (character == _escapingCharacter)
                {
                    // Podwajanie znaku ucieczki w tekście zakodowanym
                    encodedText.Append(_escapingCharacter).Append(_escapingCharacter);
                }
                else
                {
                    encodedText.Append(character);
                }
            }

            return encodedText.ToString();
        }

        // Metoda do dekodowania tekstu.
        public string Decode(string encodedText)
        {
            var decodedText = new StringBuilder();
            var isEscaping = false;

            for (int i = 0; i < encodedText.Length; i++)
            {
                char character = encodedText[i];
                if (isEscaping)
                {
                    if (character == _escapingCharacter)
                    {
                        // Obsługa podwójnego znaku ucieczki jako pojedynczego znaku ucieczki w tekście odkodowanym
                        decodedText.Append(_escapingCharacter);
                        isEscaping = false; // Zresetuj flagę, ale pomiń następny znak ucieczki
                        continue;
                    }
                    else if (_reverseCharacterPairs.ContainsKey(character))
                    {
                        decodedText.Append(_reverseCharacterPairs[character]);
                        isEscaping = false;
                    }
                    else
                    {
                        // Teoretycznie ten przypadek nie powinien wystąpić, ale można tu dodać obsługę błędu
                        throw new ArgumentException("Nieprawidłowy znak po znaku ucieczki: " + character);
                    }
                }
                else if (character == _escapingCharacter)
                {
                    isEscaping = true;
                }
                else
                {
                    decodedText.Append(character);
                }
            }

            return decodedText.ToString();
        }

    }
}
