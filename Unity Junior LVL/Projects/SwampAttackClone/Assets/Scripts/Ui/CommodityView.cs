using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommodityView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private Commodity _commoditiy;

    public event UnityAction<Commodity, CommodityView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    private void TryLockItem()
    {
        if (_commoditiy.IsBuyed)
            _sellButton.interactable = false;
    }

    public void Render(Commodity weapon)
    {
        _commoditiy = weapon;

        _label.text = weapon.Label;
        _price.text = weapon.Price.ToString();
        _icon.sprite = weapon.Icon;
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_commoditiy, this);
    }
}