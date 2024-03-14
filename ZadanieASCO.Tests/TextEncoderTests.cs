using ZadanieASCO.Core;

namespace ZadanieASCO.Tests
{

    public class TextEncoderTests
    {
        // Globalny znak ucieczki, kt�ry mo�e by� zmieniony w jednym miejscu, wp�ywaj�c na wszystkie testy
        private readonly char _globalEscapingCharacter = ';';

        [Fact]
        // Testuje, czy metoda Encode poprawnie koduje specjalne znaki w tek�cie, w tym podwajaj�c znak ucieczki.
        public void Encode_WhenTextContainsSpecialCharacters_EncodesCorrectly()
        {
            // Arrange
            var characterPairs = new Dictionary<char, char> { { '>', 'g' }, { '<', 'l' }, { '&', 'a' } };
            var encoder = new TextEncoder(_globalEscapingCharacter, characterPairs);
            var input = $"Here is some text with special characters: <, >, &.";

            // Act
            var encoded = encoder.Encode(input);

            // Assert
            var expected = $"Here is some text with special characters: {_globalEscapingCharacter}l, {_globalEscapingCharacter}g, {_globalEscapingCharacter}a.";
            Assert.Equal(expected, encoded);
        }

        [Fact]
        // Testuje, czy metoda Decode poprawnie dekoduje zakodowany tekst z powrotem na oryginalny tekst.
        public void Decode_WhenTextContainsEncodedSpecialCharacters_DecodesCorrectly()
        {
            // Arrange
            var characterPairs = new Dictionary<char, char> { { '>', 'g' }, { '<', 'l' }, { '&', 'a' } };
            var encoder = new TextEncoder(_globalEscapingCharacter, characterPairs);
            var encodedText = $"Here is some text with special characters: {_globalEscapingCharacter}l, {_globalEscapingCharacter}g, {_globalEscapingCharacter}a.";

            // Act
            var decoded = encoder.Decode(encodedText);

            // Assert
            var expected = $"Here is some text with special characters: <, >, &.";
            Assert.Equal(expected, decoded);
        }

        [Fact]
        // Testuje, czy metoda Decode rzuca wyj�tek, gdy napotka nieprawid�ow� sekwencj� po znaku ucieczki.
        public void Decode_WhenEncodedTextContainsUnknownEscapeSequence_ThrowsArgumentException()
        {
            // Arrange
            var characterPairs = new Dictionary<char, char> { { '>', 'g' }, { '<', 'l' }, { '&', 'a' } };
            var encoder = new TextEncoder(_globalEscapingCharacter, characterPairs);
            // Dodanie nieprawid�owej sekwencji po znaku ucieczki
            var invalidEncodedText = $"Invalid{_globalEscapingCharacter}z sequence";

            // Act & Assert
            Assert.Throws<System.ArgumentException>(() => encoder.Decode(invalidEncodedText));
        }

        [Fact]
        // Testuje pe�ny cykl kodowania i dekodowania, w tym obs�ug� podwajania znaku ucieczki.
        public void EncodeAndDecode_WithEscapingCharacter_EncodesAndDecodesCorrectly()
        {
            // Arrange
            var characterPairs = new Dictionary<char, char> { { '>', 'g' }, { '<', 'l' }, { '&', 'a' } };
            var encoder = new TextEncoder(_globalEscapingCharacter, characterPairs);
            var input = $"Here is a comma, and another {_globalEscapingCharacter}{_globalEscapingCharacter}";

            // Act
            var encoded = encoder.Encode(input);
            var decoded = encoder.Decode(encoded);

            // Assert
            Assert.Equal(input, decoded);
        }
    }
}