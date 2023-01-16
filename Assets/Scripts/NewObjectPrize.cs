using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewObjectPrize : MonoSingleton<NewObjectPrize>
{
    [SerializeField] private GameObject _newObjectPanel;
    [SerializeField] private Button prizeButton;
    [SerializeField] private bool isClose;

    public void StartNewObject()
    {
        prizeButton.onClick.AddListener(PrizeButton);
        StartCoroutine(StartNewObjectIenum());
    }

    private void PrizeButton()
    {
        isClose = false;
        Buttons.Instance.globalPanel.SetActive(true);
        _newObjectPanel.SetActive(false);
        Time.timeScale = 1;

        int tempX = Random.Range(0, Random.Range(0, 5));
        int tempZ = Random.Range(0, Random.Range(0, 5));
        bool isFull = false;
        if (RandomSystem.Instance.arrays.randomFields[tempX].ObjectGrid[tempZ] == true)
            isFull = true;
        if (!isFull)
        {
            RandomSystem.Instance.NewObjectUgrade(3, tempX, tempZ);
        }
        else
            PrizeButton();

    }
    private IEnumerator StartNewObjectIenum()
    {
        while (true)
        {
            yield return null;
            if (!isClose)
            {
                yield return new WaitForSeconds(60);
                Time.timeScale = 0;
                StartCoroutine(ParticalSystem.Instance.AroundPartical());
                _newObjectPanel.SetActive(true);
                isClose = true;
                Buttons.Instance.globalPanel.SetActive(false);
            }
        }
    }
}
