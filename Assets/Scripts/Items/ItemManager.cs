using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mancini.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public int coins;
    [SerializeField] private TMP_Text _coinsText;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        UpdateCoinsText();
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        _coinsText.text = "x " + coins.ToString();
    }
}
