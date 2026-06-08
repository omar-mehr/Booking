namespace Dbwebb.src;

public class Filehandler
{

    public static List<string[]> ReadFromFile(string fileName)
    {
        try
        {
            string path = $"files/{fileName}";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            List<string[]> result = new List<string[]>();
            string[] rows = File.ReadAllLines(path);

            foreach (string row in rows)
            {
                string[] parts = row.Split(';');
                result.Add(parts);
            }

            return result;
        }
        catch (FileNotFoundException)
        {
            throw new FilehandlerException("Filen finns inte.");
        }
        catch (UnauthorizedAccessException)
        {
            throw new FilehandlerException("Du har inte behörighet att läsa filen.");
        }
        catch (Exception)
        {
            throw new FilehandlerException("Ett okänt fel inträffade vid filinläsning.");
        }
    }



    public List<Guest> LoadGuest()
    {
        List<Guest> guests = new List<Guest>();

        List<string[]> data = Filehandler.ReadFromFile("guests.txt");

        foreach (var rows in data)
        {
            int id = int.Parse(rows[0]);
            string name = rows[1];
            bool loyalCustomer = bool.Parse(rows[2]);
            string email = rows[3];
            string address = rows[4];

            Guest guest = new Guest(id, name, loyalCustomer, email, address);
            guests.Add(guest);
        }

        return guests;
    }

    public void SaveGuests(string filname, List<Guest> guests)
    {
        try
        {
            List<string> rows = new List<string>();

            foreach (Guest guest in guests)
            {
                string row = $"{guest.GetId()};{guest.GetName()};{guest.GetLoyalCustomer()};{guest.GetEmail()};{guest.GetAddress()}";
                rows.Add(row);
            }

            File.WriteAllLines($"files/{filname}", rows);
        }
        catch (UnauthorizedAccessException)
        {
            throw new FilehandlerException("Du har inte behörighet att skriva till filen.");
        }
        catch (IOException)
        {
            throw new FilehandlerException("Ett I/O-fel inträffade vid filsparning.");
        }
        catch (Exception)
        {
            throw new FilehandlerException("Ett okänt fel inträffade vid filsparning.");
        }
    }


    public void SaveBookings(string fileName, List<Booking> bookings)
    {
        try
        {
            List<string> rows = new List<string>();

            foreach (Booking booking in bookings)
            {
                Accommodation acc = booking.GetAccommodation();
                bool breakfastOrdered = false;
                if (acc is Cabin cabin)
                {
                    breakfastOrdered = cabin.GetBreakfastOrdered();
                }
                if (acc is Tent tent)
                {
                    breakfastOrdered = tent.GetBreakfastOrdered();
                }
                string row = $"{booking.GetAccommodation().GetAccommodationNumber()};{booking.GetGuest().GetId()};{booking.GetNumberOfNights()};{breakfastOrdered}";
                rows.Add(row);
            }

            File.WriteAllLines($"files/{fileName}", rows);
        }
        catch (UnauthorizedAccessException)
        {
            throw new FilehandlerException("Du har inte behörighet att skriva till filen.");
        }
        catch (IOException)
        {
            throw new FilehandlerException("Ett I/O-fel inträffade vid filsparning.");
        }
        catch (Exception)
        {
            throw new FilehandlerException("Ett okänt fel inträffade vid filsparning.");
        }
    }
}
