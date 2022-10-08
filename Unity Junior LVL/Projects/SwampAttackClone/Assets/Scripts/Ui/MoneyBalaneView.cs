using TMPro;
using UnityEngine;

public class MoneyBalaneView : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private PlayerInventory _inventory;

    private void OnEnable()
    {
        _money.text = _inventory.Money.ToString();
        _inventory.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _inventory.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _money.text = money.ToString();
    }
}