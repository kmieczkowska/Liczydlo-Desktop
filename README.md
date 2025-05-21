# AgaXcel

 Aplikacja służy do łączenia dwóch plików aplikacji Excel i aktualizacji połączonych ze sobą wartości.
 W przypadku, gdy nie w jednym pliku nie istnieje dana kolumna, jest ona dopisywana do drugiego pliku, przy odpowiedniej osobie.

 Publikowanie kodu:

Plik excel stworzono w następujący sposób:

Zakładka Build -> Publish Selection
Należy wybrać ścieżkę do publikacji (Folder) oraz zaznaczyć typ RELEASE (nie debug).
Opcjonalnie wybrać opcję "Show all settings" i tam zaznaczyć "Deployment mode - Self-contained". Dzięki temu plik będzie ważył więcej, ale będzie możliwy do użycia na innym PC.
Jeśli wykonano opcję wyżej to KONIECZNIE dodać: File publish options - Produce Single File [V]
Ostatecznie opcja PUBLISH wyprodukuje plik exe, a przycisk navigate przekieruje do niego.

Opcjolanie można użyć komendy `dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true`, aby zbudować plik EXE.
Jego wynik znajduje się wtedy w `bin\Release\netX\win-x64\publish`

