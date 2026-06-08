namespace Dbwebb.src;

public class Booking
{
    private Accommodation _accommodation;
    private Guest _guest;
    private int _numberOfNights;
    private DateTime _date;
    public Booking(Accommodation accommodation, Guest guest, int numberOfNights)
    {
        this._accommodation = accommodation;
        this._guest = guest;
        this._date = DateTime.Today;
        this._numberOfNights = numberOfNights;
        this._accommodation.Book();
    }

    public void Cancel()
    {
        // Nollställ frukost om boendet har det
        if (this._accommodation is Cabin cabin)
            cabin.SetBreakfastOrdered(false);
        else if (this._accommodation is Tent tent)
            tent.SetBreakfastOrdered(false);
        // Avboka boendet på bassklassnivå
        this._accommodation.Cancel();
    }
    public Accommodation GetAccommodation()
    {
        return this._accommodation;
    }
    public Guest GetGuest()
    {
        return this._guest;
    }
    public int GetNumberOfNights()
    {
        return this._numberOfNights;
    }
    public string GetDescription()
    {
        return $"Bokningsdatum: {this._date.ToString("yyyy-MM-dd")}"
            + $"\nAntal nätter: {this._numberOfNights}"
            + $"\n\nGäst:"
            + $"\n{this._guest.GetDescription()}"
            + $"\n\nBoende:"
            + $"\n{this._accommodation.GetDescription()}"
            + $"\n\nTotalpris (inkl. antal nätter): {this._accommodation.CalculatePrice(this._numberOfNights)} kr";
    }
}
