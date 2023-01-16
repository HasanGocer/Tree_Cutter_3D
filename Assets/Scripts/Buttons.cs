using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Buttons : MonoSingleton<Buttons>
{
    //managerde bulunacak

    [SerializeField] private GameObject _money;


    [SerializeField] private Button _settingButton;
    [SerializeField] private GameObject _settingGame;

    [SerializeField] private Sprite _red, _green;
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _soundButton, _vibrationButton;
    [SerializeField] private Button _newObjectButton, _addedExtraMoneyButton;
    [SerializeField] private TMP_Text _newObjectText, _addedExtraMoneyText;

    public GameObject globalPanel;
    public TMP_Text moneyText, levelText;

    private void Start()
    {
        ButtonPlacement();
        SettingPlacement();
        TextPalacement();
    }

    private void SettingPlacement()
    {
        if (GameManager.Instance.sound == 1)
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            // SoundSystem.Instance.MainMusicPlay();
        }
        else
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
            // SoundSystem.Instance.MainMusicStop();
        }

        if (GameManager.Instance.vibration == 1)
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _green;
        }
        else
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _red;
        }
    }
    private void ButtonPlacement()
    {
        _settingButton.onClick.AddListener(SettingButton);
        _settingBackButton.onClick.AddListener(SettingBackButton);
        _soundButton.onClick.AddListener(SoundButton);
        _vibrationButton.onClick.AddListener(VibrationButton);
        _newObjectButton.onClick.AddListener(NewObjectButton);
        _addedExtraMoneyButton.onClick.AddListener(AddedExtraMoneyButton);
    }
    private void TextPalacement()
    {
        _newObjectText.text = MoneySystem.Instance.NumberTextRevork(ItemData.Instance.fieldPrice.objectCount);
        _addedExtraMoneyText.text = MoneySystem.Instance.NumberTextRevork((int)ItemData.Instance.fieldPrice.addedExtraMoney);
    }

    private void NewObjectButton()
    {
        if (GameManager.Instance.money >= ItemData.Instance.fieldPrice.objectCount)
        {

            ItemData.Instance.SetObjectCount();
            MoneySystem.Instance.MoneyTextRevork(-1 * ItemData.Instance.fieldPrice.objectCount);
            _newObjectText.text = MoneySystem.Instance.NumberTextRevork(ItemData.Instance.fieldPrice.objectCount);
            RandomSystem.Instance.NewObjectSpawn();
        }
    }
    private void AddedExtraMoneyButton()
    {
        if (GameManager.Instance.money >= ItemData.Instance.fieldPrice.objectCount)
        {

            ItemData.Instance.SetAddedExtraMoney();
            MoneySystem.Instance.MoneyTextRevork(-1 * ItemData.Instance.fieldPrice.addedExtraMoney);
            _addedExtraMoneyText.text = MoneySystem.Instance.NumberTextRevork((int)ItemData.Instance.fieldPrice.addedExtraMoney);
        }
    }
    private void SettingButton()
    {
        _settingGame.SetActive(true);
        _settingButton.gameObject.SetActive(false);
        _money.SetActive(false);
    }
    private void SettingBackButton()
    {
        _settingGame.SetActive(false);
        _settingButton.gameObject.SetActive(true);
        _money.SetActive(true);
    }
    private void SoundButton()
    {
        if (GameManager.Instance.sound == 1)
        {
            GameManager.Instance.sound = 0;
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
            SoundSystem.Instance.MainMusicStop();
            GameManager.Instance.sound = 0;
            GameManager.Instance.SetSound();
        }
        else
        {
            GameManager.Instance.sound = 1;
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            SoundSystem.Instance.MainMusicPlay();
            GameManager.Instance.sound = 1;
            GameManager.Instance.SetSound();
        }
    }
    private void VibrationButton()
    {
        if (GameManager.Instance.vibration == 1)
        {
            GameManager.Instance.vibration = 0;
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _red;
            GameManager.Instance.vibration = 0;
            GameManager.Instance.SetVibration();
        }
        else
        {
            GameManager.Instance.vibration = 1;
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _green;
            GameManager.Instance.vibration = 1;
            GameManager.Instance.SetVibration();
        }
    }

}
