namespace Dbwebb.src;
public class Guest
{
    private int _id;
    private string _name;
    private bool _loyalCustomer;
    private string _email;
    private string _address;
    public Guest(int id, string name, bool loyalCustomer, string email, string address)
    {
        this._id = id;
        this._name = name;
        this._loyalCustomer = loyalCustomer;
        this._email = email;
        this._address = address;
    }
    public int GetId()
    {
        return this._id;
    }
    public string GetName()
    {
        return this._name;
    }
    public bool GetLoyalCustomer()
    {
        return this._loyalCustomer;
    }
    public string GetEmail()
    {
        return this._email;
    }
    public string GetAddress()
    {
        return this._address;
    }
    public string GetDescription()
    {
        return $"Gäst-ID: {this._id}"
            + $"\nNamn: {this._name}"
            + $"\nStamkund: {(this._loyalCustomer ? "Ja" : "Nej")}"
            + $"\nEmail: {this._email}"
            + $"\nAdress: {this._address}\n";
    }
}
