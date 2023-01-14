using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PointText : MonoSingleton<PointText>
{
    [SerializeField] private int _OPMoneyIntCount;
    [SerializeField] private float _textMoveTime;
    [SerializeField] private float _moneyJumpDistance;
    [SerializeField] Ease _moveEaseType;

    public IEnumerator CallPointMoneyText(Vector3 Pos, int count)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPMoneyIntCount);
        obj.GetComponent<TMP_Text>().text = MoneySystem.Instance.NumberTextRevork(count);
        obj.transform.position = Pos;
        obj.transform.DOMove(new Vector3(Pos.x, Pos.y + _moneyJumpDistance, Pos.z), _textMoveTime).SetEase(_moveEaseType);
        yield return new WaitForSeconds(_textMoveTime);
        ObjectPool.Instance.AddObject(_OPMoneyIntCount, obj);
    }
}
