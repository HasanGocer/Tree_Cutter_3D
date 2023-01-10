using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    //managerde bulunacak

    [System.Serializable]
    public class Field
    {
        public int maxTreeHealth;
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
        /*field.runnerSpeed = standart.runnerSpeed - (factor.runnerSpeed * constant.runnerSpeed);
        fieldPrice.runnerSpeed = fieldPrice.runnerSpeed * factor.runnerSpeed;

        field.runnerCount = standart.runnerCount + (factor.runnerCount * constant.runnerCount);
        fieldPrice.runnerCount = fieldPrice.runnerCount * factor.runnerCount;

        if (field.runnerCount > max.runnerCount)
        {
            field.runnerCount = max.runnerCount;
        }


        if (field.runnerSpeed < max.runnerSpeed)
        {
            field.runnerSpeed = max.runnerSpeed;
        }*/

        GameSystem.Instance.StartCutter();
    }

    /*public void RunnerCount()
    {
        field.runnerCount = standart.runnerCount + (factor.runnerCount * constant.runnerCount);

        if (field.runnerCount > max.runnerCount)
        {
            field.runnerCount = max.runnerCount;
        }
    }


    public void RunnerSpeed()
    {
        field.runnerSpeed = standart.runnerSpeed - (factor.runnerSpeed * constant.runnerSpeed);

        if (field.runnerSpeed < max.runnerSpeed)
        {
            field.runnerSpeed = max.runnerSpeed;
        }
    }*/
}
