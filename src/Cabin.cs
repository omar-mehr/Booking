namespace Dbwebb.src;

public class Cabin : Accommodation
{
    private bool _cleaning;
    private bool _parkingSpace;
    private bool _breakfastOrdered;

    public Cabin(int accommodationNumber, int maxNumberOfGuests, bool breakfastOrdered, bool cleaning, bool parkingSpace = true) : base(accommodationNumber, maxNumberOfGuests, false)
    {
        this._breakfastOrdered = breakfastOrdered;
        this._cleaning = cleaning;
        this._parkingSpace = parkingSpace;
    }
    public void SetBreakfastOrdered(bool newValue)
    {
        this._breakfastOrdered = newValue;
    }
    public bool GetBreakfastOrdered()
    {
        return this._breakfastOrdered;
    }
    public override int CalculatePrice(int numberOfNights)
    {
        int total = 0;
        total += 800 * numberOfNights;

        if (this._breakfastOrdered)
        {
            total += 110 * base.GetMaxNumberOfGuests() * numberOfNights;
        }
        if (this._cleaning)
        {
            total += 400;
        }
        return total;
    }
    public override string GetDescription()
    {
        return base.GetDescription()
            + $"\nFrukost beställd: {(this._breakfastOrdered ? "Ja" : "Nej")}"
            + $"\nStädning(engångskostnad 400 kr): {(this._cleaning ? "Ja" : "Nej")}"
            + $"\nParkeringsplats: {(this._parkingSpace ? "Ja" : "Nej")}\n";
    }
}
