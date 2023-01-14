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

        if (field.maxTreeHealth > maxFactor.maxTreeHealth)
            field.maxTreeHealth = maxFactor.maxTreeHealth;
        if (field.objectCount > maxFactor.objectCount)
            field.objectCount = maxFactor.objectCount;

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

    public void SetObjectCount()
    {
        field.objectCount++;

        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (field.objectCount > maxFactor.objectCount)
            field.objectCount = maxFactor.objectCount;

        GameManager.Instance.FactorPlacementWrite(factor);
    }
}
