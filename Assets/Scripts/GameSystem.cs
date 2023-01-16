using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoSingleton<GameSystem>
{
    public ObjectID focusObjectID;


    public IEnumerator StartCutter()
    {
        AnimController.Instance.CallIdleAnim();
        while (true)
        {
            yield return null;
            if (RandomSystem.Instance.arrays.EquipBool)
            {
                if (focusObjectID.lineCount != 6)
                {
                    RandomSystem.Instance.arrays.EquipBool = false;
                    focusObjectID = null;
                }
                CutterUpper();
                yield return new WaitForSeconds((float)(4 / (float)focusObjectID.objectID));
            }
        }
    }

    public void CutterUpper()
    {
        RandomSystem.Arrays arrays = RandomSystem.Instance.arrays;

        int cutterDamage = 0;

        for (int i1 = 0; i1 < 5; i1++)
            for (int i2 = 0; i2 < 5; i2++)
                if (arrays.randomFields[i1].ObjectGrid[i2])
                {
                    int count = (int)Mathf.Pow(2, arrays.randomFields[i1].ObjectInt[i2]);
                    cutterDamage += count;
                    MoneySystem.Instance.MoneyTextRevork(count);
                    StartCoroutine(PointText.Instance.CallPointMoneyText(RandomSystem.Instance.CallPosition(i1, i2), count));
                }

        if (RandomSystem.Instance.arrays.EquipBool)
        {
            int EquipCount = (int)Mathf.Pow(2, arrays.EquipInt);
            cutterDamage += EquipCount;
            MoneySystem.Instance.MoneyTextRevork(EquipCount);
            StartCoroutine(PointText.Instance.CallPointMoneyText(RandomSystem.Instance.CallPosition(6, 6), EquipCount));
        }

        CharacterBar.Instance.BarUpdate(ItemData.Instance.field.maxTreeHealth, CharacterBar.Instance.treeHealth, cutterDamage);
    }
}
