using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LvlVisulizer : MonoBehaviour
{
    [SerializeField] private Image _lable;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _goToLvl;
    [SerializeField] private Color _complitedColor;
    [SerializeField] private Color _notComplitedColor;

    private LvlMenu.ReadOnlyLvlInfo _lvlInfo;

    public event UnityAction<LvlMenu.ReadOnlyLvlInfo> OnSellected;

    private void OnEnable()
    {
        _goToLvl.onClick.AddListener(GoToLvl);
    }
    private void OnDisable()
    {
        _goToLvl.onClick.RemoveListener(GoToLvl);
    }

    public void Visualize(LvlMenu.ReadOnlyLvlInfo info)
    {
        _lvlInfo = info;

        _lable.sprite = info.Lable;
        _name.text = info.Name;

        if (info.IsComplited)
            _lable.color = _complitedColor;
        else
            _lable.color = _notComplitedColor;
    }

    private void GoToLvl()
    {
        OnSellected?.Invoke(_lvlInfo);
    }
}
