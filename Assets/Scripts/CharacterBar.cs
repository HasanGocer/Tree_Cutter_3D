using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBar : MonoSingleton<CharacterBar>
{
    [SerializeField] private Image bar;
    public int treeHealth;

    public void startBar()
    {
        treeHealth = ItemData.Instance.field.maxTreeHealth;
    }

    public void BarUpdate(int max, int count, int down)
    {
        float nowBar = count / max;
        float afterBar = (float)((float)count - (float)down) / (float)max;
        treeHealth -= down;
        StartCoroutine(BarUpdateIenumurator(nowBar, afterBar));
    }

    private IEnumerator BarUpdateIenumurator(float start, float finish)
    {
        yield return null;
        float temp = 0;
        while (true)
        {
            temp += Time.deltaTime * 100;
            bar.fillAmount = Mathf.Lerp(start, finish, temp);
            yield return new WaitForEndOfFrame();
            if (bar.fillAmount == 0)
            {
                ItemData.Instance.SetMaxTreeHealth();
                bar.fillAmount = 1;
                treeHealth = ItemData.Instance.field.maxTreeHealth;
                BarSystem.Instance.barPanel.SetActive(true);
                BarSystem.Instance.finishMoney.text = MoneySystem.Instance.NumberTextRevork(ItemData.Instance.field.maxTreeHealth);
                StartCoroutine(BarSystem.Instance.NoThanxButtonSeen());
                StartCoroutine(BarSystem.Instance.BarImageFillAmountIenum());
                StartCoroutine(ParticalSystem.Instance.AroundPartical());
                break;
            }
            else if (bar.fillAmount == finish)
            {
                break;
            }
        }
    }
}
