using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    //managerde bulunacak

    [System.Serializable]
    public class Field
    {
        public int maxTreeHealth, axeCount;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    public void IDAwake()
    {
        field.maxTreeHealth = standart.maxTreeHealth + (factor.maxTreeHealth * constant.maxTreeHealth);
        fieldPrice.maxTreeHealth = fieldPrice.maxTreeHealth * factor.maxTreeHealth;
        field.axeCount = standart.axeCount + (factor.axeCount * constant.axeCount);
        fieldPrice.axeCount = fieldPrice.axeCount * factor.axeCount;

        if (field.maxTreeHealth > maxFactor.maxTreeHealth)
            field.maxTreeHealth = maxFactor.maxTreeHealth;
        if (field.axeCount > maxFactor.axeCount)
            field.axeCount = maxFactor.axeCount;

        CharacterBar.Instance.startBar();
        StartCoroutine(GameSystem.Instance.StartCutter());
        TreeManager.Instance.StartTreeManager();
    }

    public void SetMaxTreeHealth()
    {
        field.maxTreeHealth++;

        field.maxTreeHealth = standart.maxTreeHealth + (factor.maxTreeHealth * constant.maxTreeHealth);
        fieldPrice.maxTreeHealth = fieldPrice.maxTreeHealth * factor.maxTreeHealth;

        if (field.maxTreeHealth > maxFactor.maxTreeHealth)
            field.maxTreeHealth = maxFactor.maxTreeHealth;

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetAxeCount()
    {
        field.axeCount++;

        field.axeCount = standart.axeCount + (factor.axeCount * constant.axeCount);
        fieldPrice.axeCount = fieldPrice.axeCount * factor.axeCount;

        if (field.axeCount > maxFactor.axeCount)
            field.axeCount = maxFactor.axeCount;

        TreeManager.Instance.AxeChange();

        GameManager.Instance.FactorPlacementWrite(factor);
    }
}
