namespace Dbwebb.src;

public class Tent : Accommodation
{
    private bool _electricity;
    private bool _coolerBox;
    private bool _breakfastOrdered;

    public Tent(int accommodationNumber, int maxNumberOfGuests, bool breakfastOrdered, bool electricity, bool coolerBox) : base(accommodationNumber, maxNumberOfGuests, false) // frukost ingår inte
    {
        this._breakfastOrdered = breakfastOrdered;
        this._electricity = electricity;
        this._coolerBox = coolerBox;
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
        total += 400 * numberOfNights;
        if (this._electricity)
        {
            total += 20 * numberOfNights;
        }
        if (this._coolerBox)
        {
            total += 30 * numberOfNights;
        }
        if (this._breakfastOrdered)
        {
            total += 110 * base.GetMaxNumberOfGuests() * numberOfNights;
        }
        return total;
    }
    public override string GetDescription()
    {
        return base.GetDescription()
            + $"\nEl: {(this._electricity ? "Ja" : "Nej")}"
            + $"\nKylbox: {(this._coolerBox ? "Ja" : "Nej")}"
            + $"\nFrukost beställd: {(this._breakfastOrdered ? "Ja" : "Nej")}"
            + $"\nIngår: 2 sovsäckar och 2 liggunderlag\n";
    }
}
