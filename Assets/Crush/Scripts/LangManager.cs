using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangManager : MonoBehaviour
{
    private static LangManager instance;
    public static LangManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject("LangManager");
                instance = gameObject.AddComponent<LangManager>();
                DontDestroyOnLoad(gameObject);
            }
            return instance;
        }
        private set { }
    }

    public static string Language = "en";
    public const string English = "en";
    public const string VietNamese = "vn";

    private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

    void Awake()
    {
        if (instance != null && instance.GetInstanceID() != this.GetInstanceID())
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as LangManager;
            DontDestroyOnLoad(gameObject);
        }

        switch (LangManager.Language)
        {
            case LangManager.English:
                keyValuePairs["LostMonster"] = "Lost {0} monster";
                keyValuePairs["Retreat"] = "Retreat";
                keyValuePairs["ContentRetreat"] = "Do you want to leave the battle?";
                keyValuePairs["Damage"] = "Damage";
                keyValuePairs["DamageTaken"] = "Damage taken";
                keyValuePairs["DamagePerMonster"] = "Damage per monster";
                keyValuePairs["Reward"] = "Reward";
                keyValuePairs["Okay"] = "Okay";
                keyValuePairs["Stats"] = "Stats";

                keyValuePairs["Atk"] = "Atk";
                keyValuePairs["Hp"] = "Hp";
                keyValuePairs["Def"] = "Def";
                keyValuePairs["CritRate"] = "Crit Rate";
                keyValuePairs["CriteDamage"] = "Crite Damage";
                keyValuePairs["AtkPerSec"] = "Atk Per Sec";
                keyValuePairs["AtkRange"] = "Atk Range";
                keyValuePairs["MoveSpeed"] = "Move Speed";
                keyValuePairs["EffectResistance"] = "Effect Resistance";
                keyValuePairs["FrenzyChance"] = "Frenzy Chance";
                keyValuePairs["DodgeRate"] = "Dodge Rate";
                keyValuePairs["StunChance"] = "Stun Chance";
                keyValuePairs["StunTime"] = "Stun Time";
                keyValuePairs["AoERadius"] = "AoE Radius";
                keyValuePairs["AoEDmg"] = "AoE Dmg";
                keyValuePairs["UltimateAtk"] = "Ultimate Atk";
                keyValuePairs["KnightShieldHP"] = "Knight ShieldHP";
                keyValuePairs["BonusLoot"] = "Bonus Loot";
                keyValuePairs["FreezeChance"] = "Freeze Chance";
                keyValuePairs["FreezeTime"] = "Freeze Time";
                keyValuePairs["FreezeExplotionDmg"] = "Freeze Explotion Dmg";
                keyValuePairs["BurnChance"] = "Burn Chance";
                keyValuePairs["BurnTime"] = "Burn Time";
                keyValuePairs["BurnDmg"] = "Burn Dmg";
                keyValuePairs["PoisonChance"] = "Poison Chance";
                keyValuePairs["PoisonDmg"] = "Poison Dmg";

                keyValuePairs["Passive"] = "Passive";
                keyValuePairs["Active"] = "Active";
                keyValuePairs["NoteSkillOn"] = "Next <sprite=0> Evolve: <color=#FC7374>{0}</color>";
                keyValuePairs["NoteSkillOff"] = "This skill will unlock when this monster reach {0} <sprite=0>";

                keyValuePairs["time"] = "time";
                keyValuePairs["times"] = "times";
                keyValuePairs["OutOfPlayAmount"] = "Out of <sprite=10>";
                keyValuePairs["OutOfPlayAmountDes"] = "Get {0} <sprite=10> price {1} <sprite=6> ?\nYou've purchased {2} {3} today";
                break;
            case LangManager.VietNamese:
                break;
        }

    }

    public string Get(string key)
    {
        if (keyValuePairs.ContainsKey(key)) return keyValuePairs[key];
        return key;
    }
}
