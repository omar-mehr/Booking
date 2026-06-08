namespace Dbwebb.src;

public class Room : Accommodation
{
    private bool _extraBed;
    private bool _privateBathroom;
    public Room(int accommodationNumber, int maxNumberOfGuests, bool hasBreakfast, bool extraBed, bool privateBathroom) : base(accommodationNumber, maxNumberOfGuests, hasBreakfast)
    {
        this._extraBed = extraBed;
        this._privateBathroom = privateBathroom;
    }

    public override int CalculatePrice(int numberOfNights)
    {
        int pricePerNight = 0;
        if (base.GetMaxNumberOfGuests() == 1)
        {
            pricePerNight = 800;
        }
        else if (base.GetMaxNumberOfGuests() == 2)
        {
            pricePerNight = 1200;
        }
        if (this._extraBed)
        {
            pricePerNight += 300;
        }
        if (this._privateBathroom)
        {
            pricePerNight += 200;
        }

        return pricePerNight * numberOfNights;
    }
    public override string GetDescription()
    {
        return base.GetDescription()
        + $"\nExtrasäng: {(this._extraBed ? "Ja" : "Nej")}"
        + $"\nEget badrum: {(this._privateBathroom ? "Ja" : "Nej")}\n";

    }
}
