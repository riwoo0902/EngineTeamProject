public class MoreInfoUIData
{
    public string NameText;
    public string PriceText;
    public string DescriptionText;
    public float Mass;
    public float Friction;
    public float Bounciless;

    public MoreInfoUIData(string name ,string price, string description)
    {
        NameText = name;
        PriceText = price;
        this.DescriptionText = description;
    }
    public MoreInfoUIData(string name, string price, string description, float mass, float friction, float bounciless)
    {
        NameText = name; 
        PriceText = price; 
        DescriptionText = description;
        Mass = mass;
        Friction = friction;
        Bounciless = bounciless;
    }

}
