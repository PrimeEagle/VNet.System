namespace VNet.System;

public class NormalizedPercentageBalancer<T> where T : Enum
{
    private readonly List<NormalizedPercentagePair<T>> _list;


    public NormalizedPercentageBalancer(params NormalizedPercentagePair<T>[] pairs)
    {
        _list = new List<NormalizedPercentagePair<T>>(pairs);
        Balance();
    }

    public NormalizedPercentagePair<T> this[int index]
    {
        get => _list[index];
        set
        {
            _list[index] = value;
            Balance();
        }
    }

    public void Add(NormalizedPercentagePair<T> pair)
    {
        _list.Add(pair);
        Balance();
    }

    public void Remove(NormalizedPercentagePair<T> pair)
    {
        _list.Remove(pair);
        Balance();
    }

    public void RemoveAt(int index)
    {
        _list.RemoveAt(index);
        Balance();
    }

    public void Clear()
    {
        _list.Clear();
        Balance();
    }

    public void Equalize()
    {
        var amount = 1.0d / _list.Count;
        foreach (var pair in _list)
        {
            pair.Percentage = amount;
        }
    }

    private void Balance()
    {
        var sum = 0d;
        foreach (var pair in _list)
        {
            var sumWouldBe = sum + pair.Percentage;
            if (sumWouldBe > 1.0d)
            {
                pair.Percentage = 1.0d - sum;
            }
            sum += pair.Percentage;
        }

        if (sum >= 1.0d) return;
        var remainder = 1.0d - sum;
            
        for (var i = _list.Count - 1; i >= 0; i--)
        {
            if (i >= _list.Count - 1 || _list[i + 1].Percentage != 0d || _list[i].Percentage == 0d) continue;
            _list[i].Percentage = remainder;
            break;
        }
    }
}