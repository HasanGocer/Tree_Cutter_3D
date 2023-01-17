using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBar : MonoSingleton<CharacterBar>
{
    public Image bar;
    public int treeHealth;

    public void startBar()
    {
        if (PlayerPrefs.HasKey("treeHealth"))
            treeHealth = PlayerPrefs.GetInt("treeHealth");
        else
        {
            PlayerPrefs.SetInt("treeHealth", ItemData.Instance.field.maxTreeHealth);
            treeHealth = ItemData.Instance.field.maxTreeHealth;
        }
    }

    public void BarUpdate(int max, int count, int down)
    {
        float nowBar = count / max;
        float afterBar = (float)((float)count - (float)down) / (float)max;
        treeHealth -= down;
        SetTreeHealth();
        if (afterBar < 0)
            afterBar = 0;
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
                if (BarSystem.Instance.isFinish)
                    break;
                print(1);
                ItemData.Instance.SetMaxTreeHealth();
                treeHealth = ItemData.Instance.field.maxTreeHealth;
                SetTreeHealth();
                BarSystem.Instance.barPanel.SetActive(true);
                BarSystem.Instance.finishMoney.text = MoneySystem.Instance.NumberTextRevork(ItemData.Instance.field.maxTreeHealth / 4);
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

    private void SetTreeHealth()
    {
        PlayerPrefs.SetInt("treeHealth", treeHealth);

    }
}
