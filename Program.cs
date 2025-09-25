using System;
using System.Threading;

// See https://aka.ms/new-console-template for more information
class Program
{
    delegate void Wekker();

    static void Main()
    {
        Wekker acties = null;
        acties += Boodschap;

        Timer alarmtimer = null;
        Timer pulseTimer = null;

        TimeSpan sluimertijd = TimeSpan.FromSeconds(10);

        bool running = true;
        while (running)
        {
            Console.WriteLine("Wekker menu:");
            Console.WriteLine("1. Stel alarmtijd in ");
            Console.WriteLine("2. Stel sluimertijd in ");
            Console.WriteLine("3. Start wekker ");
            Console.WriteLine("4. Snooze ");
            Console.WriteLine("5. Stop wekker ");
            Console.WriteLine("6. Geluid aan ");
            Console.WriteLine("7. Geluid uit ");
            Console.WriteLine("8. Boodschap aan ");
            Console.WriteLine("9. Boodschap uit ");
            Console.WriteLine("10. Knipperlicht aan ");
            Console.WriteLine("11. Knipperlicht uit ");
            Console.WriteLine("12. Afsluiten ");
            Console.Write("Maak een keuze: ");
            string keuze = Console.ReadLine();

            /* gebruiker maak de keuze in en typte nadien leest de programma en uitvoerd wat de gebruiker vraagt*/
            switch (keuze)
            {
                case "1":
                    Console.Write("vul de tijd in , je hoeft de seconden ook niet te typen (hh:mm:ss): ");
                    var tijd = TimeSpan.Parse(Console.ReadLine());// gaat de time span lezen van de gebruiker
                    var target = DateTime.Today.Add(tijd); // gaat de tijd van vandaag nemen en de tijd van de gebruiker erbij optellen
                    var wachttijd = target - DateTime.Now; // gaat de tijd van nu aftrekken van de tijd van de gebruiker
                    if (wachttijd < TimeSpan.Zero) wachttijd = TimeSpan.Zero;// als de wachttijd negatief is gaat die 1 dag erbij optellen

                    alarmtimer?.Dispose(); // als de timer al bestaat gaat die die eerst verwijderen
                    alarmtimer = new Timer(_ => acties?.Invoke(), null, wachttijd, Timeout.InfiniteTimeSpan); // gaat de timer instellen met de wachttijd
                    Console.WriteLine($"Alarm is ingesteld op {target}");
                    break;

                case "2":
                    Console.Write("Sluimertijd (seconden): ");
                    sluimertijd = TimeSpan.FromSeconds(double.Parse(Console.ReadLine()));// gaat de sluimertijd lezen van de gebruiker
                    Console.WriteLine($"Sluimertijd is ingesteld op {sluimertijd.TotalSeconds} seconden");
                    break;

                case "3":
                    Console.WriteLine("Wekker gestart...");
                    pulseTimer?.Dispose(); // als de timer al bestaat gaat die die eerst verwijderen
                    pulseTimer = new Timer(_ => acties?.Invoke(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1)); // gaat de timer instellen met interval van 1 seconde
                    break;

                case "4":
                    Console.WriteLine("Snooze geactiveerd...");
                    pulseTimer?.Dispose(); // als de timer al bestaat gaat die die eerst verwijderen
                    pulseTimer = new Timer(_ => acties?.Invoke(), null, sluimertijd, Timeout.InfiniteTimeSpan); // gaat de timer instellen met de sluimertijd
                    break;

                case "5":
                    Console.WriteLine("Alarm gestopt.");
                    alarmtimer?.Dispose(); // als de timer al bestaat gaat die die eerst verwijderen
                    pulseTimer?.Dispose(); // als de timer al bestaat gaat die die eerst verwijderen
                    break;

                case "6":
                    acties += Geluid; // voegt Geluid toe aan acties
                    Console.WriteLine("Geluid aan.");
                    break;

                case "7":
                    acties -= Geluid; // verwijdert Geluid van acties
                    Console.WriteLine("Geluid uit.");
                    break;

                case "8":
                    acties += Boodschap; // voegt Boodschap toe aan acties
                    Console.WriteLine("Boodschap aan.");
                    break;

                case "9":
                    acties -= Boodschap; // verwijdert Boodschap van acties
                    Console.WriteLine("Boodschap uit.");
                    break;

                case "10":
                    acties += Knipper; // voegt Knipper toe aan acties
                    Console.WriteLine("Knipperlicht aan.");
                    break;

                case "11":
                    acties -= Knipper; // verwijdert Knipper van acties
                    Console.WriteLine("Knipperlicht uit.");
                    break;

                case "12":
                    running = false; // stopt de while-loop en sluit het programma af
                    break;

                default:
                    Console.WriteLine("Ongeldige keuze.");
                    break;
            }       
        }
    }
        
    //de methodes die de acties uitvoeren
    static void Geluid()
    {
        try { Console.Beep(800, 200); } catch { }
        Console.WriteLine(" GELUID!");
    }

    static void Boodschap()
    {
        Console.WriteLine(" BOODSCHAP: Tijd om op te staan!");
    }

    static void Knipper()
    {
        Console.WriteLine(" KNIPPER!");
    }
}