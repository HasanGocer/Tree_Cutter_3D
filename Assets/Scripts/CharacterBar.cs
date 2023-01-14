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
        float afterBar = (count - down) / max;
        StartCoroutine(BarUpdateIenumurator(nowBar, afterBar));
    }

    private IEnumerator BarUpdateIenumurator(float start, float finish)
    {
        yield return null;
        float temp = 0;
        while (true)
        {
            temp += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(start, finish, temp);
            yield return new WaitForEndOfFrame();
            if (bar.fillAmount == finish)
            {
                MoneySystem.Instance.MoneyTextRevork(ItemData.Instance.field.maxTreeHealth);
                ItemData.Instance.SetMaxTreeHealth();
                bar.fillAmount = 0;
                treeHealth = ItemData.Instance.field.maxTreeHealth;
                break;
            }
        }
    }
}
