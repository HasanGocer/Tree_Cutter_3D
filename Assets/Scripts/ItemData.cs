using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    //managerde bulunacak

    [System.Serializable]
    public class Field
    {
        public int maxTreeHealth, objectCount;
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
        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (factor.maxTreeHealth > maxFactor.maxTreeHealth)
        {
            factor.maxTreeHealth = maxFactor.maxTreeHealth;
            field.maxTreeHealth = standart.maxTreeHealth + (factor.maxTreeHealth * constant.maxTreeHealth);
            fieldPrice.maxTreeHealth = fieldPrice.maxTreeHealth * factor.maxTreeHealth;
        }
        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }

        NewObjectPrize.Instance.StartNewObject();
        CharacterBar.Instance.startBar();
        StartCoroutine(GameSystem.Instance.StartCutter());
        TreeManager.Instance.StartTreeManager();
        TapMechanic.Instance.StartButton();
    }

    public void SetMaxTreeHealth()
    {
        factor.maxTreeHealth++;

        field.maxTreeHealth = standart.maxTreeHealth + (factor.maxTreeHealth * constant.maxTreeHealth);
        fieldPrice.maxTreeHealth = fieldPrice.maxTreeHealth + factor.maxTreeHealth;

        if (factor.maxTreeHealth > maxFactor.maxTreeHealth)
        {
            factor.maxTreeHealth = maxFactor.maxTreeHealth;
            field.maxTreeHealth = standart.maxTreeHealth + (factor.maxTreeHealth * constant.maxTreeHealth);
            fieldPrice.maxTreeHealth = fieldPrice.maxTreeHealth + factor.maxTreeHealth;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetObjectCount()
    {
        factor.objectCount++;

        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount + factor.objectCount;

        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount + factor.objectCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
}
