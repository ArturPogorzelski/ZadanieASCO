using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieASCO.Core
{
    public class TextEncoder2
    {
        private readonly char _escapingCharacter;
        private readonly Dictionary<char, char> _characterPairs;
        private readonly Dictionary<char, char> _reverseCharacterPairs;

        public TextEncoder2(char escapingCharacter, IEnumerable<KeyValuePair<char, char>> characterPairs)
        {
            _escapingCharacter = escapingCharacter; // Ustaw znak ucieczki.
            _characterPairs = characterPairs.ToDictionary(pair => pair.Key, pair => pair.Value);

            // Dodajemy mapowanie dla samego znaku ucieczki, aby obsłużyć jego kodowanie.
            _characterPairs[_escapingCharacter] = _escapingCharacter;

            _reverseCharacterPairs = _characterPairs.ToDictionary(pair => pair.Value, pair => pair.Key);
        }

        public string Encode(string text)
        {
            var encodedText = new StringBuilder();

            foreach (var character in text)
            {
                char encodedChar;
                if (_characterPairs.TryGetValue(character, out encodedChar))
                {
                    encodedText.Append(_escapingCharacter);

                    // Jeśli kodujemy znak ucieczki, dodajemy go dwa razy, aby oznaczyć, że jest to zakodowany znak ucieczki.
                    if (character == _escapingCharacter)
                    {
                        encodedText.Append(_escapingCharacter);
                    }

                    encodedText.Append(encodedChar);
                }
                else
                {
                    encodedText.Append(character);
                }
            }

            return encodedText.ToString();
        }

        public string Decode(string encodedText)
        {
            var decodedText = new StringBuilder();
            var isEscaping = false;

            for (int i = 0; i < encodedText.Length; i++)
            {
                char character = encodedText[i];

                if (isEscaping)
                {
                    char decodedChar;
                    if (_reverseCharacterPairs.TryGetValue(character, out decodedChar))
                    {
                        // Jeśli napotykamy podwójny znak ucieczki, dodajemy go tylko raz.
                        if (character == _escapingCharacter && i < encodedText.Length - 1 && encodedText[i + 1] == _escapingCharacter)
                        {
                            i++; // Pomijamy następny znak ucieczki w sekwencji
                        }

                        decodedText.Append(decodedChar);
                    }
                    else
                    {
                        throw new ArgumentException("Nieprawidłowy znak po znaku ucieczki: " + character, nameof(encodedText));
                    }
                    isEscaping = false;
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

            // Jeśli po zakończeniu pętli flaga isEscaping jest nadal ustawiona, oznacza to, że tekst kończy się znakiem ucieczki.
            if (isEscaping)
            {
                // Można dodać logikę obsługującą tę sytuację, np. dodając znak ucieczki do wyniku.
                throw new ArgumentException("Tekst nie może kończyć się pojedynczym znakiem ucieczki.", nameof(encodedText));
            }

            return decodedText.ToString();
        }
    }
}
/*
Optymalizacja klasy TextEncoder, którą przedstawiłem, koncentruje się na efektywniejszym użyciu słowników przez wykorzystanie metody TryGetValue zamiast kombinacji ContainsKey i indeksowania. Chociaż ta optymalizacja zwiększa wydajność, towarzyszą jej konkretne zagrożenia i kompromisy dla tej klasy:

1. Zakładanie poprawności danych wejściowych: Używając TryGetValue, zakładamy, że każdy znak po znaku ucieczki ma swoją parę w słowniku characterPairs lub reverseCharacterPairs. Jeśli dane wejściowe są niespójne lub zawierają błędy, optymalizowany kod może nie być w stanie poprawnie ich obsłużyć. Oryginalna implementacja, która nie zakładała poprawności danych na tym etapie, mogła być bardziej odporna na pewne typy błędów wejściowych.

2. Obsługa błędów: Zoptymalizowana implementacja może być mniej transparentna w zakresie obsługi błędów. W oryginalnym kodzie, brak dopasowania znaku po znaku ucieczki mógłby być traktowany inaczej niż w zoptymalizowanej wersji. Optymalizacja wymusza rzucanie wyjątku, gdy nie znajdziemy dopasowania w reverseCharacterPairs, co może nie być zachowaniem oczekiwanym w niektórych kontekstach użycia.

3. Kompleksowość debugowania: W przypadku wystąpienia problemów, zoptymalizowany kod może być trudniejszy do zdebugowania. Zmniejszenie liczby wywołań funkcji i uproszczenie logiki poprzez TryGetValue może utrudnić identyfikację, czy błąd wynika z nieprawidłowego mapowania znaków, błędnego stanu danych wejściowych, czy też z innej, mniej oczywistej przyczyny.

4. Potencjalna redukcja elastyczności: Optymalizując klasę pod kątem konkretnego przypadku użycia (np. zakładając, że każdy znak po znaku ucieczki jest poprawnie mapowany), możemy niechcący ograniczyć jej możliwości adaptacji do nowych wymagań lub rozszerzeń funkcjonalności. Na przykład, jeśli zechcemy dodać obsługę znaków, które mogą występować po znaku ucieczki, ale nie są zakodowane, konieczne może być przemyślenie podejścia do optymalizacji.

Podsumowując, optymalizacja ta poprawia wydajność przez zmniejszenie liczby operacji na słownikach, ale wymaga świadomości potencjalnych kompromisów w zakresie obsługi błędów, elastyczności i klarowności debugowania. W projektach oprogramowania ważne jest, aby ważyć korzyści płynące z optymalizacji wydajności z potencjalnymi zagrożeniami i utrzymaniem kodu.
*/