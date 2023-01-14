using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoSingleton<GameSystem>
{
    public ObjectID focusObjectID;


    public IEnumerator StartCutter()
    {
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
                AnimController.Instance.CallIdleAnim();
                yield return new WaitForSeconds(1 / focusObjectID.objectID);
                print(1);
            }
        }
    }

    private void CutterUpper()
    {
        RandomSystem.Arrays arrays = RandomSystem.Instance.arrays;

        for (int i1 = 0; i1 < 5; i1++)
            for (int i2 = 0; i2 < 5; i2++)
                if (arrays.randomFields[i1].ObjectGrid[i2])
                {
                    int count = (int)Mathf.Pow(2, arrays.randomFields[i1].ObjectInt[i2]);
                    MoneySystem.Instance.MoneyTextRevork(count);
                    PointText.Instance.CallPointMoneyText(RandomSystem.Instance.CallPosition(i1, i2), count);
                    CharacterBar.Instance.BarUpdate(ItemData.Instance.field.maxTreeHealth, CharacterBar.Instance.treeHealth, count);
                }

        int EquipCount = (int)Mathf.Pow(2, arrays.EquipInt);

        MoneySystem.Instance.MoneyTextRevork(EquipCount);
        PointText.Instance.CallPointMoneyText(RandomSystem.Instance.CallPosition(6, 6), EquipCount);
        CharacterBar.Instance.BarUpdate(ItemData.Instance.field.maxTreeHealth, CharacterBar.Instance.treeHealth, EquipCount);
    }
}
