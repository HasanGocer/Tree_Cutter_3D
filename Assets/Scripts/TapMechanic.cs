using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapMechanic : MonoSingleton<TapMechanic>
{
    private void Update()
    {
        if (Input.touchCount > 0 && !BarSystem.Instance.isFinish)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                StartCoroutine(TapMechanicButton());
        }
    }

    public IEnumerator TapMechanicButton()
    {
        Time.timeScale = 3f;
        GameSystem.Instance.CutterUpper();
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1f;
    }
}
