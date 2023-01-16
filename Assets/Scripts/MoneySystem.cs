using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoSingleton<MoneySystem>
{
    [SerializeField] private int _billion = 1000000000, _million = 1000000, _thousand = 1000;

    public void MoneyTextRevork(float plus)
    {
        GameManager.Instance.money += plus;

        if (GameManager.Instance.money >= _billion)
        {
            int money = (int)GameManager.Instance.money / 100000000;
            float Floatmoney = (float)money / 10;
            Buttons.Instance.moneyText.text = Floatmoney.ToString() + " B";
        }
        else if (GameManager.Instance.money >= _million)
        {
            int money = (int)GameManager.Instance.money / 100000;
            float Floatmoney = (float)money / 10;
            Buttons.Instance.moneyText.text = Floatmoney.ToString() + " M";
        }
        else if (GameManager.Instance.money >= _thousand)
        {
            int money = (int)GameManager.Instance.money / 100;
            float Floatmoney = (float)money / 10;
            Buttons.Instance.moneyText.text = Floatmoney.ToString() + " K";
        }
        else
        {
            Buttons.Instance.moneyText.text = ((int)GameManager.Instance.money).ToString();
        }
        GameManager.Instance.SetMoney();
    }

    public string NumberTextRevork(int number)
    {
        string numberString;

        if (number >= _billion)
        {
            int money = number / 100000000;
            float Floatmoney = (float)money / 10;
            numberString = Floatmoney.ToString() + " B";
        }
        else if (number >= _million)
        {
            int money = number / 100000;
            float Floatmoney = (float)money / 10;
            numberString = Floatmoney.ToString() + " M";
        }
        else if (number >= _thousand)
        {
            int money = number / 100;
            float Floatmoney = (float)money / 10;
            numberString = Floatmoney.ToString() + " K";
        }
        else
        {
            numberString = number.ToString();
        }
        return numberString;
    }
}
