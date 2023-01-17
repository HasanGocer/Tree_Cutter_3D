using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarSystem : MonoSingleton<BarSystem>
{
    public GameObject barPanel;
    [SerializeField] private Button winButton, winPrizeButton;
    public TMP_Text finishMoney;
    [SerializeField] private Image _bar;
    public bool isFinish;
    [SerializeField] private bool _goRight = true;
    public int barMoneyFactor;
    [SerializeField] private GameObject startPos, finishPos;
    float amount = 0;

    public void barSystemStart()
    {
        winButton.onClick.AddListener(() =>
        {
            MoneySystem.Instance.MoneyTextRevork(ItemData.Instance.field.maxTreeHealth / 4);
            BarStopButton(0);
        });
        winPrizeButton.onClick.AddListener(() => BarStopButton(ItemData.Instance.field.maxTreeHealth / 4));
    }

    public IEnumerator NoThanxButtonSeen()
    {
        yield return new WaitForSeconds(3);
        winButton.gameObject.SetActive(true);
    }

    public void BarStopButton(int count)
    {
        print(1);
        isFinish = false;
        print(2);
        BarFactorPlacement(amount);
        print(3);
        MoneySystem.Instance.MoneyTextRevork(count * barMoneyFactor);
        print(4);
        barPanel.SetActive(false);
        print(5);
        winButton.gameObject.SetActive(false);
        print(6);
        TapMechanic.Instance.tapButton.enabled = true;
        print(7);
        CharacterBar.Instance.bar.fillAmount = 1;
        print(8);
    }

    public IEnumerator BarImageFillAmountIenum()
    {
        isFinish = true;
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
