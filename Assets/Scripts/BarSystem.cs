using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSystem : MonoSingleton<BarSystem>
{
    [SerializeField] private Image _bar;
    public bool isFinish = true;
    [SerializeField] private bool _goRight = true;
    public int barMoneyFactor;
    [SerializeField] private GameObject startPos, finishPos;
    float amount = 0;

    public void BarStopButton()
    {
        isFinish = false;
        BarFactorPlacement(amount);
        MoneySystem.Instance.MoneyTextRevork(ItemData.Instance.field.maxTreeHealth * barMoneyFactor);
    }

    public IEnumerator BarImageFillAmountIenum()
    {
        while (isFinish)
        {
            if (_goRight)
            {
                amount += Time.deltaTime;
                _bar.transform.position = Vector2.Lerp(startPos.transform.position, finishPos.transform.position, amount);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            else
            {
                amount += Time.deltaTime;
                _bar.transform.position = Vector2.Lerp(finishPos.transform.position, startPos.transform.position, amount);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            if (amount >= 1)
            {
                amount = 0;
                if (_goRight)
                    _goRight = false;
                else
                    _goRight = true;
            }
        }
    }

    private void BarFactorPlacement(float barAmount)
    {
        if (barAmount < 0.2f)
            barMoneyFactor = 1;
        else if (barAmount < 0.4f)
            barMoneyFactor = 2;
        else if (barAmount < 0.6f)
            barMoneyFactor = 3;
        else if (barAmount < 0.8f)
            barMoneyFactor = 2;
        else
            barMoneyFactor = 1;
    }
}
