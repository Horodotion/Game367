using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    standard,
    earth,
    fire,
    water,
    poison
}

public enum ResistanceType
{
    weak,
    normal,
    resisted,
    immune
}

public enum StatType
{
    health,
    damage,
    speed,
    ammo,
    score
}

[System.Serializable] public class StatList
{
    public List<StatValue> statValues;
}

// This allows us to have a single variable with both a StatType and a float for a value
[System.Serializable] public class StatValue
{
    public StatType stat;
    public float value;
}

[System.Serializable] public class Resistance
{
    public DamageType damageType;
    public ResistanceType resistance;
}

[System.Serializable]
[CreateAssetMenu(menuName = "Stat set")]
public class Stats : ScriptableObject
{
    // These Arrays are unused, and are purely to allowed us to utilize them in the hierarchy
    public StatList baseStatList; // The starting stats of the player
    public StatList maxStatList; // The maximum a stat can be
    public List<Resistance> resistanceList;

    // The dictionaries to actually be used in game
    // Stat is what will be referenced to the player, and allows buffs and debuffs to occur numerically.
    public Dictionary<StatType, float> stat = new Dictionary<StatType, float> {};

    // BaseStat is used to try resetting the stats back to a set number
    // IE, if a speedbuff only lasts 5 seconds and then goes back to the normal speed.
    public Dictionary<StatType, float> baseStat = new Dictionary<StatType, float> {};

    //MaxStat is to put a hard cap on a certain stat. IE, getting 150 points of healing but we can only have 100 health.
    public Dictionary<StatType, float> maxStat = new Dictionary<StatType, float> {};

    
    public Dictionary<DamageType, ResistanceType> resistances = new Dictionary<DamageType, ResistanceType> {};



    // This function that tells each dictionary to piece together
    public void SetStats()
    {
        stat = NewDictionary(baseStatList.statValues);
        baseStat = NewDictionary(baseStatList.statValues);
        maxStat = NewDictionary(maxStatList.statValues);
    
        if (resistanceList.Count != 0)
        {
            foreach(Resistance damageType in resistanceList)
            {
                resistances.Add(damageType.damageType, damageType.resistance);
            }
        }

    }

    public void ResetStat(StatType statType)
    {
        stat[statType] = baseStat[statType];
    }

    // This function changes the stat to be called, up to the maximum set by the Maximum Stat
    public void AddToStat(StatType statType, float amountToAdd)
    {
        stat[statType] = Mathf.Clamp(stat[statType] + amountToAdd, 0f, maxStat[statType]);
    }

    // This is the function that pieces a dictionary from a list
    public Dictionary<StatType, float> NewDictionary(List<StatValue> statValues)
    {
        // This creates a new temporary dictionary to hold values
        Dictionary<StatType, float> dict = new Dictionary<StatType, float> {};

        // Looping through each value in the List, it takes the stattype as the Key, and the value as the value to add new values to the dictionary.
        foreach (StatValue stat in statValues)  // (int i = 0; i < statValues.Count; i++)
        {
            dict.Add(stat.stat, stat.value);
        }

        return dict;
    }

    public float ResistCalculation(DamageType damageType)
    {
        float damageModifier = 1f;
        if (resistances.ContainsKey(damageType))
        {
            switch (resistances[damageType])
            {
                case ResistanceType.immune:
                    damageModifier = 0f;
                    break;
                
                case ResistanceType.resisted:
                    damageModifier = 0.5f;
                    break;
                
                case ResistanceType.normal:
                    damageModifier = 1f;
                    break;
                
                case ResistanceType.weak:
                    damageModifier = 2f;
                    break;
            }
        }

        return damageModifier;
    }

    public float DamageCalculation(float damageAmont, DamageType damageType)
    {
        damageAmont *= ResistCalculation(damageType);
        // damageAmont -= stat[StatType.defense];
        return damageAmont;
    }
}