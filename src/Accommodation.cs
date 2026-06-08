namespace Dbwebb.src;

public abstract class Accommodation
{
    private readonly int _maxNumberOfGuests;
    private int _accommodationNumber;
    private bool _hasBreakfast;
    private bool _isBooked;
    public Accommodation(int accommodationNumber, int maxNumberOfGuests, bool hasBreakfast)
    {
        this._accommodationNumber = accommodationNumber;
        this._maxNumberOfGuests = maxNumberOfGuests;
        this._hasBreakfast = hasBreakfast;
        this._isBooked = false;
    }
    public int GetAccommodationNumber()
    {
        return this._accommodationNumber;
    }
    public int GetMaxNumberOfGuests()
    {
        return this._maxNumberOfGuests;
    }
    public void Book()
    {
        this._isBooked = true;
    }
    public void Cancel()
    {
        this._isBooked = false;
    }
    public bool GetBooking()
    {
        return this._isBooked;
    }
    public abstract int CalculatePrice(int numberOfNights);
    public virtual string GetDescription()
    {
        return $"Boendenummer: {this._accommodationNumber}\n" +
                $"Max antal gäster: {this._maxNumberOfGuests}\n" +
                $"Har frukost: {(this._hasBreakfast ? "Ja" : "Nej")}\n" +
                $"Är bokad: {(this._isBooked ? "Ja" : "Nej")}\n" +
                $"Pris per natt: {this.CalculatePrice(1)} kr";
    }
}
