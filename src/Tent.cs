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
        + $"\nElectricity: {(this._electricity ? "Yes" : "No")}"
        + $"\nCooler box: {(this._coolerBox ? "Yes" : "No")}"
        + $"\nBreakfast ordered: {(this._breakfastOrdered ? "Yes" : "No")}"
        + $"\nIncluded: 2 sleeping bags and 2 sleeping pads\n";
    }
}
