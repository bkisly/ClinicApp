# NTR laboratorium

W czasie laboratorium nalezy z zaprojektować i zaimplementować aplikację wykonyjac 4 zadania (etapy tworzenia programu): 

* Zadanie 1 MVC - 15 pkt
* Zadanie 2 (baza danych + migracje) - 10 pkt
* Zadanie 3 (baza danych + równoległość) - 5 pkt
* Zadanie 4 React (+ klient w React) - 30 pkt

Dla każdego zadania należy stworzyć oddzielne odgałęzenie i w nim umieszczać kod dla tego etapu/zadania. Każdy etap bazuje na wcześniejszym rozwiązaniu. Terminy oddania zadań będą podane w regulaminie laboratorium.

Proszę użyć następujących nazw gałęzi **(kod w innych odgałęzieniach nie bedzie sprawdzany)**:
* 1-mvc
* 2-migrations
* 3-concurrency
* 4-react

Samouczek: [Get started with ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-6.0&tabs=visual-studio)

## Treść zadania

Napisać program do obsługi przychodni.

### Zadanie 1 - MVC

Lista lekarzy - każdy lekarz ma jedną specjalność ze stałej listy specjalnosci (domowy, laryngolog, dermatolog, okulista, neurolog, ortopeda, pediatra).
Konto lekarza zakłada dyrektor.
Lekaz loguje sie za pomocą hasła.

Lista pacjentów (logowanie z hasłem)
Pacjent rejestruje się w systemie (zakłada konto), ale zapisy do lekarzy i przegladanie wizyt po uaktywnieniu konta przez dyrektora.


Grafik na każdy kolejny tydzien: lekaz, dzien, start, koniec  np: Kowalski, 2023-11-08 8.00 12.00. W ramach jednego wpisu w grafiku moze odbyć się wiele wizyt.
Grafik wpisuje/modyfikuje dyrektor (dodatkowa opcja: kopiuj porzedni tydzień).

### Zadanie 2 - Migracje

Pacjenci moga sie zapisywać/wypisywać z wizyt - każda wizyta trwa 15 minut. Pacjent szuka lekarza po specjalności. Wyświetlane maja być wszystkie wolne terminy wizyty wszystkich lekarzy danej specjalności w kolejnosci chronologicznej.
Lekaz na wizycie wpisuje do wizyty opis (dowolny tekst).

Pacjent może przeglądać odbyte wizyty i czytać ich opisy.

Dyrektor może dla podanego okresu (tylko pełne dni) wygenerować raport gdzie dla kazdego lekaża bedzie podany czas z grafiku oraz liczba przeprowadzonych wizyt.