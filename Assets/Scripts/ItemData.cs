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
        public float addedExtraMoney;
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
        field.addedExtraMoney = standart.addedExtraMoney + (factor.addedExtraMoney * constant.addedExtraMoney);
        fieldPrice.addedExtraMoney = fieldPrice.addedExtraMoney * factor.addedExtraMoney;

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
        if (factor.addedExtraMoney > maxFactor.addedExtraMoney)
        {
            factor.addedExtraMoney = maxFactor.addedExtraMoney;
            field.addedExtraMoney = standart.addedExtraMoney + (factor.addedExtraMoney * constant.addedExtraMoney);
            fieldPrice.addedExtraMoney = fieldPrice.addedExtraMoney * factor.addedExtraMoney;
        }

        BarSystem.Instance.barSystemStart();
        //NewObjectPrize.Instance.StartNewObject();
        CharacterBar.Instance.startBar();
        TreeManager.Instance.StartTreeManager();
        StartCoroutine(GameSystem.Instance.StartCutter());
    }

    public void SetAddedExtraMoney()
    {
        factor.addedExtraMoney++;

        field.addedExtraMoney = standart.addedExtraMoney + (factor.addedExtraMoney * constant.addedExtraMoney);
        fieldPrice.addedExtraMoney = fieldPrice.addedExtraMoney / (factor.addedExtraMoney - 1);
        fieldPrice.addedExtraMoney = fieldPrice.addedExtraMoney * factor.addedExtraMoney;

        if (factor.addedExtraMoney > maxFactor.addedExtraMoney)
        {
            factor.addedExtraMoney = maxFactor.addedExtraMoney;
            field.addedExtraMoney = standart.addedExtraMoney + (factor.addedExtraMoney * constant.addedExtraMoney);
            fieldPrice.addedExtraMoney = fieldPrice.addedExtraMoney / (factor.addedExtraMoney - 1);
            fieldPrice.addedExtraMoney = fieldPrice.addedExtraMoney * factor.addedExtraMoney;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetMaxTreeHealth()
    {
        TreeManager.Instance.Trees[factor.maxTreeHealth % TreeManager.Instance.Trees.Count].SetActive(false);
        factor.maxTreeHealth++;

        field.maxTreeHealth = standart.maxTreeHealth + (factor.maxTreeHealth * constant.maxTreeHealth);
        fieldPrice.maxTreeHealth = fieldPrice.maxTreeHealth / (factor.maxTreeHealth - 1);
        fieldPrice.maxTreeHealth = fieldPrice.maxTreeHealth * factor.maxTreeHealth;

        if (factor.maxTreeHealth > maxFactor.maxTreeHealth)
        {
            factor.maxTreeHealth = maxFactor.maxTreeHealth;
            field.maxTreeHealth = standart.maxTreeHealth + (factor.maxTreeHealth * constant.maxTreeHealth);
            fieldPrice.maxTreeHealth = fieldPrice.maxTreeHealth / (factor.maxTreeHealth - 1);
            fieldPrice.maxTreeHealth = fieldPrice.maxTreeHealth * factor.maxTreeHealth;
        }

        AnimController.Instance.focusTree = TreeManager.Instance.Trees[factor.maxTreeHealth % TreeManager.Instance.Trees.Count];
        TreeManager.Instance.Trees[factor.maxTreeHealth % TreeManager.Instance.Trees.Count].SetActive(true);

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetObjectCount()
    {
        factor.objectCount++;

        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
}
