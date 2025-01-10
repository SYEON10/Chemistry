using System.Collections.Generic;

public class GameData
{
    /// <summary>
    /// 각 캐릭터 별 초기 스탯 지정
    /// </summary>
    public Dictionary<Character, CharacterStat> stats = new Dictionary<Character, CharacterStat>
    {
        { Character.Player, new CharacterStat()},
        { Character.Boy, new CharacterStat()},
        { Character.Girl, new CharacterStat()},
        { Character.Furry, new CharacterStat()}
    };
}
