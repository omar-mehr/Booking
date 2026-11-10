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
        return $"Guest ID: {this._id}"
            + $"\nName: {this._name}"
            + $"\nLoyal customer: {(this._loyalCustomer ? "Yes" : "No")}"
            + $"\nEmail: {this._email}"
            + $"\nAddress: {this._address}\n";
    }
}
