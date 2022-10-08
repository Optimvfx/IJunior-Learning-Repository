using UnityEngine.UI;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    [SerializeField] private MenuManager _menuManger;

    [SerializeField] private Button _openMainMenuButton;
    [SerializeField] private Button _openShopMenuButton;

    [Header("Menus")]
    [SerializeField] private ShopMenu _shopMenu;
    [SerializeField] private MainMenu _mainMenu;

    private void OnEnable()
    {
        _openMainMenuButton.onClick.AddListener(OpenMainMenu);
        _openShopMenuButton.onClick.AddListener(OpenShopMenu);
    }

    private void OnDisable()
    {
        _openMainMenuButton.onClick.RemoveListener(OpenMainMenu);
        _openShopMenuButton.onClick.RemoveListener(OpenShopMenu);
    }


    private void OpenMainMenu()
    {
       _menuManger.OpenMenu(_mainMenu);
    }

    private void OpenShopMenu()
    {
        _menuManger.OpenMenu(_shopMenu);
    }
}
