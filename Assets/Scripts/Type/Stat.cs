using System;

public class Stat
{
    public const int MaxValue = 95;
    public const int MinValue = 5;
    public int value { get; private set; }
    
    public Stat(int startValue)
    {
        value = startValue;
    }

    /// <summary>
    /// 현재 값을 기준으로 스탯을 변경시키는 함수
    /// </summary>
    /// <param name="delta">증가시키거나 감소시킬 양</param>
    public void ChangeStat(int delta)
    {
        value += delta;
        if (value < MinValue) value = MinValue;
        if (value > MaxValue) value = MaxValue;
    }
}
