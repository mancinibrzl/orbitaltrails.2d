using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mancini.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public TextMeshProUGUI uiCoinsText;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        UpdateCoinsText();
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        //uiCoinsText.text = coins.ToString();
        //UIInGameManager.UpdateTextCoins(coins.value.ToString());
    }
}
