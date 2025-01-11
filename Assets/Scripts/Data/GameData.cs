using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameData
{
    public CharacterStat stats = new CharacterStat();

    public Stat GetStat(string stat)
    {
        return GetStat((StatEnum)Enum.Parse(typeof(StatEnum), stat));
    }
    
    public Stat GetStat(StatEnum statEnum)
    {
        switch (statEnum)
        {
            case StatEnum.mov:
                return stats.mov;
            case StatEnum.charm:
                return stats.charm;
            case StatEnum.mental:
                return stats.mental;
            case StatEnum.lvChris:
                return stats.lvChris;
            case StatEnum.lvEun:
                return stats.lvEun;
            case StatEnum.lvMint:
                return stats.lvMint;
            case StatEnum.Chris_Eun:
                return stats.Chris_Eun;
            case StatEnum.Eun_Mint:
                return stats.Eun_Mint;
            case StatEnum.Mint_Chris:
                return stats.Mint_Chris;
            default :
                return null;
        }
    }
}
