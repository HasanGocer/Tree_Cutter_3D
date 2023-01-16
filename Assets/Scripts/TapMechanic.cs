using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapMechanic : MonoSingleton<TapMechanic>
{
    [SerializeField] private Button tapButton;

    public void StartButton()
    {
        tapButton.onClick.AddListener(() => StartCoroutine(TapMechanicButton()));
    }

    public IEnumerator TapMechanicButton()
    {
        Time.timeScale = 2f;
        GameSystem.Instance.CutterUpper();
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1f;
    }
}
