using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoSingleton<ParticalSystem>
{
    [SerializeField] private int _OPTreeHitParticalCount, _OPMergeParticalCount, _OPChangeAxeParticalCount, _OPAxeParticalCount, _OPAroundCount;
    [SerializeField] private float _treeHitTime, _mergeTime, _changeAxeTime, _axeTime, _aroundTime;
    [SerializeField] private GameObject _treeHitPos, _changeAxePos, _aroundLeftPos, _aroundRightPos;

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
    public IEnumerator AroundPartical()
    {
        GameObject objLeft = ObjectPool.Instance.GetPooledObject(_OPAroundCount);
        GameObject obtRight = ObjectPool.Instance.GetPooledObject(_OPAroundCount);
        objLeft.transform.position = _aroundLeftPos.transform.position;
        obtRight.transform.position = _aroundLeftPos.transform.position;
        yield return new WaitForSeconds(_aroundTime);
        ObjectPool.Instance.AddObject(_OPAroundCount, objLeft);
        ObjectPool.Instance.AddObject(_OPAroundCount, obtRight);

    }
}
