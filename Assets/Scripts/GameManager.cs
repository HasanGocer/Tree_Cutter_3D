using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //managerde bulunacak

    public bool isStart;

    public int money;
    public int addedMoney;
    public int vibration;
    public int sound;

    public void Awake()
    {
        PlayerPrefsPlacement();
    }

    private void PlayerPrefsPlacement()
    {
        if (PlayerPrefs.HasKey("money"))
            money = PlayerPrefs.GetInt("money");
        else
            PlayerPrefs.SetInt("money", 100);
        MoneySystem.Instance.MoneyTextRevork(0);

        if (PlayerPrefs.HasKey("vibration"))
            vibration = PlayerPrefs.GetInt("vibration");
        else
            PlayerPrefs.SetInt("vibration", 1);

        if (PlayerPrefs.HasKey("sound"))
            sound = PlayerPrefs.GetInt("sound");
        else
            PlayerPrefs.SetInt("sound", 1);

        if (!PlayerPrefs.HasKey("first"))
        {
            FactorPlacementWrite(ItemData.Instance.factor);
            MarketPlacementWrite(MarketSystem.Instance.fieldBool);
            RandomSystem.Instance.NewObjectSpawn();
            PlayerPrefs.SetInt("first", 1);
        }
        else
        {
            RandomSystem.Instance.arrays = RandomPlacementRead();
            RandomSystem.Instance.StartObject();
        }

        MarketSystem.Instance.fieldBool = MarketPlacementRead();
        ItemData.Instance.factor = FactorPlacementRead();
        ItemData.Instance.IDAwake();

    }

    public void RandomPlacementWrite(RandomSystem.Arrays Arrays)
    {
        string jsonData = JsonUtility.ToJson(Arrays);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/ArraysData.json", jsonData);
    }

    public void FactorPlacementWrite(ItemData.Field factor)
    {
        string jsonData = JsonUtility.ToJson(factor);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/FactorData.json", jsonData);
    }

    public void MarketPlacementWrite(MarketSystem.FieldBool fieldBool)
    {
        string jsonData = JsonUtility.ToJson(fieldBool);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/MarketData.json", jsonData);
    }

    public RandomSystem.Arrays RandomPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/ArraysData.json");
        RandomSystem.Arrays Arrays = new RandomSystem.Arrays();
        Arrays = JsonUtility.FromJson<RandomSystem.Arrays>(jsonRead);
        return Arrays;
    }

    public ItemData.Field FactorPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/FactorData.json");
        ItemData.Field factor = new ItemData.Field();
        factor = JsonUtility.FromJson<ItemData.Field>(jsonRead);
        //MarketSystem.Instance.MarketStart();
        return factor;
    }

    public MarketSystem.FieldBool MarketPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/MarketData.json");
        MarketSystem.FieldBool fieldBool = new MarketSystem.FieldBool();
        fieldBool = JsonUtility.FromJson<MarketSystem.FieldBool>(jsonRead);
        return fieldBool;
    }

    public void SetMoney()
    {
        PlayerPrefs.SetInt("money", money);
    }

    public void SetSound()
    {
        PlayerPrefs.SetInt("sound", sound);
    }

    public void SetVibration()
    {
        PlayerPrefs.SetInt("vibration", vibration);
    }
}
