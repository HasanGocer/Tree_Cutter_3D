using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoSingleton<GameSystem>
{
    public IEnumerator StartCutter()
    {
        while (true)
        {
            AnimController.Instance.CallIdleAnim();
            CutterUpper();
            yield return new WaitForSeconds((float)(2));
        }
    }

    public void CutterUpper()
    {
        RandomSystem.Arrays arrays = RandomSystem.Instance.arrays;

        float cutterDamage = 0;

        for (int i1 = 0; i1 < 5; i1++)
            for (int i2 = 0; i2 < 5; i2++)
                if (arrays.randomFields[i1].ObjectGrid[i2])
                {
                    int count = (int)Mathf.Pow(2, arrays.randomFields[i1].ObjectInt[i2]);
                    cutterDamage += count;
                    cutterDamage += ItemData.Instance.field.addedExtraMoney;
                    MoneySystem.Instance.MoneyTextRevork(ItemData.Instance.field.addedExtraMoney + count);
                    StartCoroutine(PointText.Instance.CallPointMoneyText(RandomSystem.Instance.CallPosition(i1, i2), ItemData.Instance.field.addedExtraMoney + count));
                    StartCoroutine(ParticalSystem.Instance.AxeMoneyPartical(arrays.randomFields[i1].objectGO[i2]));
                }

        CharacterBar.Instance.BarUpdate(ItemData.Instance.field.maxTreeHealth, CharacterBar.Instance.treeHealth, (int)cutterDamage);
    }
}
