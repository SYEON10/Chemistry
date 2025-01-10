using System.Collections.Generic;

public class GameData
{
    /// <summary>
    /// 각 캐릭터 별 초기 스탯 지정
    /// </summary>
    public Dictionary<Character, Stat> stats = new Dictionary<Character, Stat>
    {
        { Character.Boy, new Stat()},
        { Character.Girl, new Stat()},
        { Character.Furry, new Stat()}
    };
}
