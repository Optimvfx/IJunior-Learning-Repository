using UnityEngine;

[System.Serializable]
public class Commodity
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private uint _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;

    public Weapon Weapon => _weapon;

    public string Label => _weapon.Label;

    public int Price => (int)_price;

    public Sprite Icon => _icon;

    public bool IsBuyed => _isBuyed;

    public void Buy()
    {
        _isBuyed = true;
    }
}
