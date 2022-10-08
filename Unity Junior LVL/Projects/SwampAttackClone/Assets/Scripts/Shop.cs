using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Commodity> _weapons;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private CommodityView _template;
    [SerializeField] private Transform _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            AddItem(_weapons[i]);
        }
    }

    private void AddItem(Commodity commoditiy)
    {
        var view = Instantiate(_template, _itemContainer);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(commoditiy);
    }

    private void OnSellButtonClick(Commodity commoditiy, CommodityView view)
    {
        TrySellWeapon(commoditiy, view);
    }

    private void TrySellWeapon(Commodity commoditiy, CommodityView view)
    {
        if (commoditiy.IsBuyed)
            return;

        if(_inventory.TryBuyWeapon(commoditiy));
        {
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}