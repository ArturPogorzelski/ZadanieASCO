# ZadanieASCO
Przygotowałem dwie klasy TextEncoder.cs i TextEncoder2.cs teoretycznie  druga z nich powinna być bardziej wydajna niestety testy tego nie potwierdziły więc mimo iż w teori klasa TextEncoder2.cs powinna być szybsza to rekomenduję klasę TextEncoder.cs w związku z tym że mamy większą kontrole mad nią 
# Opis optymalizacji TextEncoder2.cs
Optymalizacja klasy TextEncoder, którą przedstawiłem, koncentruje się na efektywniejszym użyciu słowników przez wykorzystanie metody TryGetValue zamiast kombinacji ContainsKey i indeksowania. Chociaż ta optymalizacja zwiększa wydajność, towarzyszą jej konkretne zagrożenia i kompromisy dla tej klasy:

1. Zakładanie poprawności danych wejściowych: Używając TryGetValue, zakładamy, że każdy znak po znaku ucieczki ma swoją parę w słowniku characterPairs lub reverseCharacterPairs. Jeśli dane wejściowe są niespójne lub zawierają błędy, optymalizowany kod może nie być w stanie poprawnie ich obsłużyć. Oryginalna implementacja, która nie zakładała poprawności danych na tym etapie, mogła być bardziej odporna na pewne typy błędów wejściowych.

2. Obsługa błędów: Zoptymalizowana implementacja może być mniej transparentna w zakresie obsługi błędów. W oryginalnym kodzie, brak dopasowania znaku po znaku ucieczki mógłby być traktowany inaczej niż w zoptymalizowanej wersji. Optymalizacja wymusza rzucanie wyjątku, gdy nie znajdziemy dopasowania w reverseCharacterPairs, co może nie być zachowaniem oczekiwanym w niektórych kontekstach użycia.

3. Kompleksowość debugowania: W przypadku wystąpienia problemów, zoptymalizowany kod może być trudniejszy do zdebugowania. Zmniejszenie liczby wywołań funkcji i uproszczenie logiki poprzez TryGetValue może utrudnić identyfikację, czy błąd wynika z nieprawidłowego mapowania znaków, błędnego stanu danych wejściowych, czy też z innej, mniej oczywistej przyczyny.

4. Potencjalna redukcja elastyczności: Optymalizując klasę pod kątem konkretnego przypadku użycia (np. zakładając, że każdy znak po znaku ucieczki jest poprawnie mapowany), możemy niechcący ograniczyć jej możliwości adaptacji do nowych wymagań lub rozszerzeń funkcjonalności. Na przykład, jeśli zechcemy dodać obsługę znaków, które mogą występować po znaku ucieczki, ale nie są zakodowane, konieczne może być przemyślenie podejścia do optymalizacji.

Podsumowując, optymalizacja ta poprawia wydajność przez zmniejszenie liczby operacji na słownikach, ale wymaga świadomości potencjalnych kompromisów w zakresie obsługi błędów, elastyczności i klarowności debugowania. W projektach oprogramowania ważne jest, aby ważyć korzyści płynące z optymalizacji wydajności z potencjalnymi zagrożeniami i utrzymaniem kodu.
