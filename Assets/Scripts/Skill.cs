public class Skill
{
    public int ID;
    public int damageLevel;
    public int speedLevel;
    public bool unlocked;
    public bool isFirstPickup;

    public Skill() { }

    public Skill(int ID, int damageLevel, int speedLevel, bool unlocked, bool isFirstPickup)
    {
        this.ID = ID;
        this.damageLevel = damageLevel;
        this.speedLevel = speedLevel;
        this.unlocked = unlocked;
        this.isFirstPickup = isFirstPickup;

    }
}
