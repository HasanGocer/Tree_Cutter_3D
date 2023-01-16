using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoSingleton<ParticalSystem>
{
    [SerializeField] private int _OPTreeHitParticalCount, _OPMergeParticalCount, _OPChangeAxeParticalCount, _OPAxeParticalCount;
    [SerializeField] private float _treeHitTime, _mergeTime, _changeAxeTime, _axeTime;
    [SerializeField] private GameObject _treeHitPos, _changeAxePos;

    public IEnumerator CallTreeHitPartical()
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPTreeHitParticalCount);
        obj.transform.position = _treeHitPos.transform.position;
        yield return new WaitForSeconds(_treeHitTime);
        ObjectPool.Instance.AddObject(_OPTreeHitParticalCount, obj);
    }
    public IEnumerator CallMergePartical(GameObject mergePos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPMergeParticalCount);
        obj.transform.position = mergePos.transform.position;
        yield return new WaitForSeconds(_mergeTime);
        ObjectPool.Instance.AddObject(_OPMergeParticalCount, obj);
    }
    public IEnumerator ChangeAxePartical()
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPChangeAxeParticalCount);
        obj.transform.position = _changeAxePos.transform.position;
        yield return new WaitForSeconds(_changeAxeTime);
        ObjectPool.Instance.AddObject(_OPChangeAxeParticalCount, obj);
    }
    public IEnumerator AxeMoneyPartical(GameObject axePos)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPAxeParticalCount);
        obj.transform.position = axePos.transform.position;
        yield return new WaitForSeconds(_axeTime);
        ObjectPool.Instance.AddObject(_OPAxeParticalCount, obj);
    }
}
