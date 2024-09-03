using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActiveSkillSO : ScriptableObject
{
    public int ID;
    public int BaseAttackDamage;
    public float BaseAttackInterval;
    public int NumberOfTargets;
    public Sprite SkillIcon;
    public int UnlockLevel;
    public string Description;
}
