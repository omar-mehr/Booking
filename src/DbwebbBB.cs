namespace Dbwebb.src;

public class DbwebbBB
{
    private List<Accommodation> _accommodations;
    private List<Guest> _guests;
    private List<Booking> _bookings;
    public DbwebbBB()
    {
        this._accommodations = [];
        this._guests = [];
        this._bookings = [];
    }

    public void AddGuest(int id, string name, bool loyalCustomer, string email, string address)
    {
        Guest newGuest = new Guest(id, name, loyalCustomer, email, address);
        this._guests.Add(newGuest);
    }

    public void LoadGuestsFromFile()
    {
        Filehandler file = new Filehandler();
        List<Guest> guestFromFile = file.LoadGuest();
        foreach (Guest guest in guestFromFile)
        {
            this._guests.Add(guest);
        }
    }

    public void SaveGuestsToFile()
    {
        Filehandler file = new Filehandler();
        file.SaveGuests("guests.txt", this._guests);
    }

    public string RemoveGuest(int guestId)
    {
        Guest? guest = this.FindGuest(guestId);

        if (guest != null)
        {
            this._guests.Remove(guest);
            return $"Gästen med ID {guestId} togs bort.";
        }
        else
        {
            return $"Ingen gäst med ID {guestId} hittades.";
        }
    }
    public Accommodation? FindAccommodation(int accommodationNumber)
    {
        foreach (Accommodation accommodation in this._accommodations)
        {
            if (accommodation.GetAccommodationNumber() == accommodationNumber)
            {
                return accommodation;
            }
        }

        return null;
    }

    private Guest? FindGuest(int guestId)
    {
        foreach (Guest guest in this._guests)
        {
            if (guest.GetId() == guestId)
            {
                return guest;
            }
        }
        return null;
    }
    private Booking? FindBooking(int accommodationNumber, int guestId)
    {
        foreach (Booking booking in this._bookings)
        {
            if (booking.GetAccommodation().GetAccommodationNumber() == accommodationNumber &&
                booking.GetGuest().GetId() == guestId)
            {
                return booking;
            }
        }
        return null;
    }

    private string? FindAccommodationAndGuest(int accommodationNumber, int guestId, bool isCancel)
    {
        Accommodation? accommodation = FindAccommodation(accommodationNumber);
        if (accommodation == null)
            return $"Inget boende med nummer {accommodationNumber} hittades.";

        if (!isCancel && accommodation.GetBooking())
            return $"Boende {accommodationNumber} är redan bokat.";

        if (isCancel && !accommodation.GetBooking())
            return $"Boende {accommodationNumber} är inte bokat.";

        Guest? guest = FindGuest(guestId);
        if (guest == null)
            return $"Ingen gäst med ID {guestId} hittades.";

        return null;
    }

    public string ShowBookings(int guestId)
    {
        foreach (Booking booking in this._bookings)
        {
            if (booking.GetGuest().GetId() == guestId)
            {
                return booking.GetDescription();
            }
        }
        return "Ingen bokning hittades för denna gäst.";
    }

    public string ShowBookings()
    {
        if (_bookings.Count == 0)
            return "Det finns inga bokningar i systemet.";

        string result = "";

        foreach (Booking booking in this._bookings)
        {
            result += booking.GetDescription() + "\n";
        }

        return result;
    }

    public void LoadTents()
    {
        List<string[]> data = Filehandler.ReadFromFile("tent.txt");
        foreach (var row in data)
        {
            int nr = int.Parse(row[0]);
            int maxGuests = int.Parse(row[1]);
            bool breakfastOrdered = bool.Parse(row[2]);
            bool hasElectricity = bool.Parse(row[3]);
            bool hasCooler = bool.Parse(row[4]);
            Tent tent = new Tent(nr, maxGuests, breakfastOrdered, hasElectricity, hasCooler);
            this._accommodations.Add(tent);
        }
    }

    public void LoadCabins()
    {
        List<string[]> data = Filehandler.ReadFromFile("cabin.txt");
        foreach (var row in data)
        {
            int nr = int.Parse(row[0]);
            int maxGuests = int.Parse(row[1]);
            bool breakfastOrdered = bool.Parse(row[2]);
            bool cleaning = bool.Parse(row[3]);
            Cabin cabin = new Cabin(nr, maxGuests, breakfastOrdered, cleaning);
            this._accommodations.Add(cabin);
        }
    }

    public void LoadRooms()
    {
        List<string[]> data = Filehandler.ReadFromFile("room.txt");
        foreach (var row in data)
        {
            int nr = int.Parse(row[0]);
            int maxGuests = int.Parse(row[1]);
            bool hasBreakfast = bool.Parse(row[2]);
            bool extraBed = bool.Parse(row[3]);
            bool privateBathroom = bool.Parse(row[4]);
            Room room = new Room(nr, maxGuests, hasBreakfast, extraBed, privateBathroom);
            this._accommodations.Add(room);
        }
    }
    public void LoadBookings()
    {
        List<string[]> data = Filehandler.ReadFromFile("bookings.txt");

        foreach (var row in data)
        {
            int accommodationNumber = int.Parse(row[0]);
            int guestId = int.Parse(row[1]);
            int nights = int.Parse(row[2]);
            bool breakfastOrdered = bool.Parse(row[3]);

            Accommodation? accommodation = FindAccommodation(accommodationNumber);
            Guest? guest = FindGuest(guestId);

            if (accommodation != null && guest != null)
            {
                if (accommodation is Cabin cabin)
                {
                    cabin.SetBreakfastOrdered(breakfastOrdered);
                }

                if (accommodation is Tent tent)
                {
                    tent.SetBreakfastOrdered(breakfastOrdered);
                }
                accommodation.Book();

                Booking booking = new Booking(accommodation, guest, nights);

                this._bookings.Add(booking);
            }
        }
    }

    public string BookAccommodation(int accommodationNumber, int guestId, int numberOfNights)
    {
        string? message = FindAccommodationAndGuest(accommodationNumber, guestId, false);
        if (message != null)
            return message;

        Accommodation foundAccommodation = FindAccommodation(accommodationNumber)!;
        Guest foundGuest = FindGuest(guestId)!;

        Booking newBooking = new Booking(foundAccommodation, foundGuest, numberOfNights);
        this._bookings.Add(newBooking);

        return $"Boende {accommodationNumber} är nu bokat av gäst {guestId} i {numberOfNights} nätter.";
    }

    public string BookAccommodation(int accommodationNumber, int guestId, int numberOfNights, bool breakfastOrdered)
    {
        string? message = FindAccommodationAndGuest(accommodationNumber, guestId, false);
        if (message != null)
            return message;

        Accommodation foundAccommodation = FindAccommodation(accommodationNumber)!;
        Guest foundGuest = FindGuest(guestId)!;
        if (foundAccommodation is Cabin cabin)
        {
            cabin.SetBreakfastOrdered(breakfastOrdered);
        }
        if (foundAccommodation is Tent tent)
        {
            tent.SetBreakfastOrdered(breakfastOrdered);
        }
        Booking newBooking = new Booking(foundAccommodation, foundGuest, numberOfNights);
        this._bookings.Add(newBooking);

        return $"Boende {accommodationNumber} är nu bokat av gäst {guestId} i {numberOfNights} nätter.";
    }

    public string CancelBooking(int accommodationNumber, int guestId)
    {
        string? message = FindAccommodationAndGuest(accommodationNumber, guestId, true);
        if (message != null)
            return message;

        Booking? foundBooking = FindBooking(accommodationNumber, guestId);
        if (foundBooking == null)
            return "Ingen matchande bokning hittades.";

        foundBooking.Cancel();
        this._bookings.Remove(foundBooking);

        return $"Bokningen för boende {accommodationNumber} och gäst {guestId} är nu avbokad.";
    }

    private string BuildAccommodationList(bool onlyAvailable)
    {
        string result = "";
        bool hasTent = false;
        bool hasCabin = false;
        bool hasRoom = false;

        foreach (Accommodation accommodation in this._accommodations)
        {
            if (onlyAvailable && accommodation.GetBooking())
            {
                continue;
            }
            if (accommodation is Tent)
            {
                if (!hasTent)
                {
                    result += "=== Tält ===\n";
                    hasTent = true;
                }
            }
            else if (accommodation is Cabin)
            {
                if (!hasCabin)
                {
                    result += "\n=== Stugor ===\n";
                    hasCabin = true;
                }
            }
            else if (accommodation is Room)
            {
                if (!hasRoom)
                {
                    result += "\n=== Rum ===\n";
                    hasRoom = true;
                }
            }

            result += accommodation.GetDescription() + "\n";
        }

        return result;
    }

    public string ShowAllAccommodation()
    {
        string result = BuildAccommodationList(false);

        if (result == "")
        {
            return "Det finns inga boenden i systemet.";
        }

        return result;
    }

    public string ShowAvailableAccommodation()
    {
        string result = BuildAccommodationList(true);

        if (result == "")
        {
            return "Inga lediga rum just nu.";
        }

        return result;
    }

    public string ShowAllGuests()
    {
        string result = "";

        foreach (Guest guest in this._guests)
        {
            result += guest.GetDescription() + "\n";
        }

        if (result == "")
        {
            return "Det finns inga rum i systemet.";
        }

        return result;
    }

    public void Save()
    {
        Filehandler file = new Filehandler();
        file.SaveBookings("bookings.txt", this._bookings);
        this.SaveGuestsToFile();
    }
}
