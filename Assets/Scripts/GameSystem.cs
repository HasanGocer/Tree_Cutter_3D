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
            }
        }
    }

    private void CutterUpper()
    {
        RandomSystem.Arrays arrays = RandomSystem.Instance.arrays;

        for (int i1 = 0; i1 < 5; i1++)
            for (int i2 = 0; i2 < 5; i2++)
                if (arrays.ObjectGrid[i1, i2])
                    MoneySystem.Instance.MoneyTextRevork((int)Mathf.Pow(2, arrays.ObjectInt[i1, i2]));

        MoneySystem.Instance.MoneyTextRevork((int)Mathf.Pow(2, arrays.EquipInt));
    }
}
