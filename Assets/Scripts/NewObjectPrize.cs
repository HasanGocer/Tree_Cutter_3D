using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewObjectPrize : MonoSingleton<NewObjectPrize>
{
    [SerializeField] private GameObject _newObjectPanel;
    [SerializeField] private Button backButton, prizeButton;
    [SerializeField] private bool isClose;

    public void StartNewObject()
    {
        backButton.onClick.AddListener(BackButton);
        prizeButton.onClick.AddListener(PrizeButton);
        StartCoroutine(StartNewObjectIenum());
    }

    private void PrizeButton()
    {
        isClose = false;
        Buttons.Instance.globalPanel.SetActive(true);
        _newObjectPanel.SetActive(false);

        int tempX = Random.Range(0, Random.Range(0, 5));
        int tempZ = Random.Range(0, Random.Range(0, 5));
        bool isFull = false;
        if (RandomSystem.Instance.arrays.randomFields[tempX].ObjectGrid[tempZ] == true)
            isFull = true;
        if (!isFull)
        {
            RandomSystem.Instance.NewObjectUgrade(1, tempX, tempZ);
        }
        else
            PrizeButton();

    }
    private void BackButton()
    {
        isClose = false;
        Buttons.Instance.globalPanel.SetActive(true);
        _newObjectPanel.SetActive(false);
    }
    private IEnumerator StartNewObjectIenum()
    {
        while (true)
        {
            yield return null;
            if (!isClose)
            {
                yield return new WaitForSeconds(40);
                _newObjectPanel.SetActive(true);
                isClose = true;
                Buttons.Instance.globalPanel.SetActive(false);
            }
        }
    }
}
