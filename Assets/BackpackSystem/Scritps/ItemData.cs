
public class ItemData 
{
    public int id;
    public string Name;
    public string Icon;
    public ItemType Type;
    public Equipment EType;
    public int HP;
    public int MP;
    public float speed;
    public int Attack;
    public int Defense;
    public string Description;

}

public enum ItemType
{
    Normal ,
    Equipment
}

public enum Equipment
{
    None ,
    头盔 ,
    甲胄 ,
    裤子 ,
    鞋子 ,
    腰带 ,
    项链 ,
    戒指 

}