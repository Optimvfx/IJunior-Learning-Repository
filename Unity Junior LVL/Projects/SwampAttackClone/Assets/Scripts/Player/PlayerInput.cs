using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerShoter))]
[RequireComponent(typeof(PlayerInventory))]
public class PlayerInput : MonoBehaviour
{
    private readonly int _RMBIndex = 0;

    [SerializeField] private Button _nextWeaponButton;
    [SerializeField] private Button _previusWeaponButton;
    
    private PlayerShoter _shoter;
    private PlayerInventory _inventory;

    private void Awake()
    {
        _shoter = GetComponent<PlayerShoter>();
        _inventory = GetComponent<PlayerInventory>();
    }

    private void OnEnable()
    {
        _nextWeaponButton.onClick.AddListener(SellectNextWeapon);
        _previusWeaponButton.onClick.AddListener(SellectPrewiusWeapon);
    }

    private void OnDisable()
    {
        _nextWeaponButton.onClick.RemoveListener(SellectNextWeapon);
        _previusWeaponButton.onClick.RemoveListener(SellectPrewiusWeapon);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_RMBIndex))
        {
            _shoter.Shot();
        }
    }

    private void SellectNextWeapon()
    {
        _inventory.SellectNextWeapon();
    }

    private void SellectPrewiusWeapon()
    {
        _inventory.SellectPreviousWeapon();
    }
}
