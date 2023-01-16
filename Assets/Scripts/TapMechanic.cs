using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapMechanic : MonoSingleton<TapMechanic>
{
    [SerializeField] private Button tapButton;

    public void StartButton()
    {
        tapButton.onClick.AddListener(TapMechanicButton);
    }

    public void TapMechanicButton()
    {
        GameSystem.Instance.CutterUpper();
    }
}
