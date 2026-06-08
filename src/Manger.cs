namespace Dbwebb.src;
public class Manger
{
    private readonly DbwebbBB _dbWebbBB;

    public Manger()
    {
        this._dbWebbBB = new DbwebbBB();
    }

    private static void PrintMenu()
    {
        Console.WriteLine("===============================================");
        Console.WriteLine("    Välkommen till Dbwebb B&B Bokningssystem    ");
        Console.WriteLine("===============================================");
        Console.WriteLine($"Datum: {DateTime.Today:yyyy-MM-dd}\n");
        Console.WriteLine("Välj mellan följande menyalternativ:");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("1  -  Visa alla rum");
        Console.WriteLine("2  -  Lägg till en gäst (Visa gästlistan)");
        Console.WriteLine("3  -  Ta bort en gäst");
        Console.WriteLine("4  -  Visa lediga rum");
        Console.WriteLine("5  -  Boka ett rum");
        Console.WriteLine("6  -  Avboka ett rum");
        Console.WriteLine("7  -  Hitta en bokning");
        Console.WriteLine("q  -  Avsluta");
        Console.WriteLine("-----------------------------------------------");
    }

    public void Run()
    {
        Loadfiles();
        Console.Clear();
        Console.Title = "DBWEBB B&B";

        string choice = "";


        while (choice != "q")
        {
            PrintMenu();
            choice = ReadStringFromTerminal("\nDitt val: ");

            switch (choice)
            {
                case "1":
                    HandleShowAllAccommodation();
                    break;
                case "2":
                    HandleAddGuest();
                    break;
                case "3":
                    HandleRemoveGuest();
                    break;
                case "4":
                    HandleShowAvailableAccommodation();
                    break;
                case "5":
                    HandleBookAccommodation();
                    break;
                case "6":
                    HandleCancelBooking();
                    break;
                case "7":
                    HandleFindBookning();
                    break;
                case "q":
                    Console.WriteLine("Avslutar programmet...");
                    SaveFiles();
                    break;
                default:
                    Console.WriteLine("\nOgiltigt menyval");
                    break;
            }
        }

    }
    public void HandleShowAllAccommodation()
    {
        Console.WriteLine(this._dbWebbBB.ShowAllAccommodation());
    }

    public void HandleShowAvailableAccommodation()
    {
        Console.WriteLine(this._dbWebbBB.ShowAvailableAccommodation());
    }

    public void HandleFindBookning()
    {
        int guestId = ReadIntFromTerminal("Ange gäst-ID. Tryck Enter för att visa alla bokningar: ");
        if (guestId == 0)
        {
            Console.WriteLine(this._dbWebbBB.ShowBookings());
        }
        else
        {
            Console.WriteLine(this._dbWebbBB.ShowBookings(guestId));
        }
    }
    public void HandleBookAccommodation()
    {
        int guestId = ReadIntFromTerminal("Ange id-numret för gästen (värdet blir 0 vid tom input): ");
        int accommodationId = ReadIntFromTerminal("Ange boendenummret för boendet (värdet blir 0 vid tom input): ");
        int numberOfNights = ReadIntFromTerminal("Hur många nätter ska det vara: (värdet blir 0 vid tom input): ");

        bool breakfastOrdered = false;

        Accommodation? accommodation = this._dbWebbBB.FindAccommodation(accommodationId);

        if (accommodation is Cabin || accommodation is Tent)
        {
            breakfastOrdered = ReadStringFromTerminal("Vill du ha frukost för 110 kr / personer, vid ja gäller alla i bokningen  (Ja/Nej): ") == "Ja";
        }

        string result;

        if (breakfastOrdered)
        {

            result = this._dbWebbBB.BookAccommodation(accommodationId, guestId, numberOfNights, true);
        }
        else
        {
            result = this._dbWebbBB.BookAccommodation(accommodationId, guestId, numberOfNights);
        }

        Console.WriteLine("\n" + result);
    }

    public void HandleCancelBooking()
    {
        int guestId = ReadIntFromTerminal("Ange id-numret för gästen (värdet blir 0 vid tom input): ");
        int accommodationId = ReadIntFromTerminal("Ange boendenummret för boendet (värdet blir 0 vid tom input): ");
        Console.WriteLine(this._dbWebbBB.CancelBooking(accommodationId, guestId));
    }
    public void HandleRemoveGuest()
    {
        Console.WriteLine(this._dbWebbBB.ShowAllGuests());
        int guestId = ReadIntFromTerminal("Ange id-numret för gästen (värdet blir 0 vid tom input): ");
        Console.WriteLine(this._dbWebbBB.RemoveGuest(guestId));
    }

    public void HandleAddGuest()
    {
        Console.WriteLine(this._dbWebbBB.ShowAllGuests());
        int guestId = ReadIntFromTerminal("Ange gästens ID-nummer (de 4 sista siffrorna i personnumret, värdet blir 0 vid tom input): ");

        string name = ReadStringFromTerminal("Ange namnet för gästen: ");
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine(this._dbWebbBB.ShowAllGuests());
            return;
        }

        string email = ReadStringFromTerminal("Ange e-post adressen för gästen: ");
        string address = ReadStringFromTerminal("Ange adressen för gästen: ");
        bool loyalCustomer = ReadStringFromTerminal("Är gästen stamkund (Ja/Nej): ") == "Ja";
        this._dbWebbBB.AddGuest(guestId, name, loyalCustomer, email, address);
    }
    private static string ReadStringFromTerminal(string info, string defaultValue = "")
    // Om tom input, så används defaultValue istället 
    {
        Console.Write(info);
        string input = Console.ReadLine() ?? defaultValue;
        if (input == "")
        {
            input = defaultValue;
        }
        return input;
    }
    private static int ReadIntFromTerminal(string info, string defaultValue = "0")
    // Om tom input, så används defaultValue istället 
    {
        Console.Write(info);
        string input = Console.ReadLine() ?? defaultValue;
        if (!int.TryParse(input, out int number))
        {
            number = int.Parse(defaultValue);
        }

        return number;
    }

    private void Loadfiles()
    {
        this._dbWebbBB.LoadRooms();
        this._dbWebbBB.LoadCabins();
        this._dbWebbBB.LoadTents();
        this._dbWebbBB.LoadGuestsFromFile();
        this._dbWebbBB.LoadBookings();
    }

    private void SaveFiles()
    {
        this._dbWebbBB.Save();
    }

}
