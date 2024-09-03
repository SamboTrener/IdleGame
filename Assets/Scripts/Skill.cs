public class Skill
{
    public int ID;
    public int damageLevel;
    public int speedLevel;
    public bool unlocked;

    public Skill() { }

    public Skill(int ID, int damageLevel, int speedLevel, bool unlocked)
    {
        this.ID = ID;
        this.damageLevel = damageLevel;
        this.speedLevel = speedLevel;
        this.unlocked = unlocked;
    }
}
