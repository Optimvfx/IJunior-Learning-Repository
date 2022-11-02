public struct PositiveInt
{
    private uint _value;

    public uint Value => _value;

    public PositiveInt(uint value)
    {
        _value = value;
    }

    public void Add(uint value)
    {
        _value += value;
    }
}
