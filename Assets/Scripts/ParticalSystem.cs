using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoSingleton<ParticalSystem>
{
    [SerializeField] private int _OPTreeHitParticalCount, _OPMergeParticalCount, _OPChangeAxeParticalCount;
    [SerializeField] private float treeHitTime, mergeTime, changeAxeTime;
    [SerializeField] private GameObject treeHitPos, changeAxePos;

    public IEnumerator CallTreeHitPartical()
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPTreeHitParticalCount);
        obj.transform.position = treeHitPos.transform.position;
        yield return new WaitForSeconds(treeHitTime);
        ObjectPool.Instance.AddObject(_OPTreeHitParticalCount, obj);
    }
    public IEnumerator CallMergePartical(GameObject mergePos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPMergeParticalCount);
        obj.transform.position = mergePos.transform.position;
        yield return new WaitForSeconds(mergeTime);
        ObjectPool.Instance.AddObject(_OPMergeParticalCount, obj);
    }
    public IEnumerator ChangeAxePartical()
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPChangeAxeParticalCount);
        obj.transform.position = changeAxePos.transform.position;
        yield return new WaitForSeconds(changeAxeTime);
        ObjectPool.Instance.AddObject(_OPChangeAxeParticalCount, obj);
    }
}
