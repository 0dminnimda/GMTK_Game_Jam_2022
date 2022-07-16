using System;
using Random = UnityEngine.Random;

public class ChanceTable
{
    private uint[] table;
    private int lastValue;

    public ChanceTable()
    {
        table = default;
        lastValue = default;
    }

    public ChanceTable(uint[] table)
    {
        SetTable(table);
    }

    public void SetTable(uint[] table)
    {
        this.table = new uint[table.Length];
        uint curVal = 0;

        for (var i = 0; i < table.Length; i++)
        {
            curVal += table[i];
            this.table[i] = curVal;
        }

        lastValue = (int)curVal;
    }

    public int GetRandomIndex()
    {
        int ind = Array.BinarySearch(table, (uint)Random.Range(1, lastValue + 1));
        if (ind < 0)
            ind = ~ind;

        return ind;
    }

    public T GetRandomItem<T>(ref T[] items)
    {
        return items[GetRandomIndex()];
    }
}
