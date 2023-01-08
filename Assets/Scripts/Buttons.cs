using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoSingleton<Buttons>
{
    //managerde bulunacak

    [SerializeField] private GameObject _money;

    public GameObject _startPanel;
    [SerializeField] private Button _startButton;

    [SerializeField] private Button _settingButton;
    [SerializeField] private GameObject _settingGame;

    [SerializeField] private Sprite _red, _green;
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _soundButton, _vibrationButton;

    public GameObject winPanel;
    [SerializeField] private Button _winPrizeButton;
    public Button winButton;

    public Text finishGameMoneyText;
    [SerializeField] private GameObject _startObject1;

    public Text moneyText, levelText;

    private void Start()
    {
        ButtonPlacement();
        SettingPlacement();
    }
    public IEnumerator NoThanxOnActive()
    {
        yield return new WaitForSeconds(3);
        winButton.gameObject.SetActive(true);
    }

    private void SettingPlacement()
    {
        if (GameManager.Instance.sound == 1)
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            SoundSystem.Instance.MainMusicPlay();
        }
        else
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
            SoundSystem.Instance.MainMusicStop();
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
        _startButton.onClick.AddListener(StartButton);
        _settingButton.onClick.AddListener(SettingButton);
        _settingBackButton.onClick.AddListener(SettingBackButton);
        _soundButton.onClick.AddListener(SoundButton);
        _vibrationButton.onClick.AddListener(VibrationButton);
        _winPrizeButton.onClick.AddListener(() => StartCoroutine(WinPrizeButton()));
        winButton.onClick.AddListener(() => StartCoroutine(WinButton()));
    }


    private void StartButton()
    {
        MarketSystem.Instance.GameStart();
        _startObject1.SetActive(true);
        _startPanel.SetActive(false);
        GameManager.Instance.isStart = true;
    }
    private IEnumerator WinPrizeButton()
    {
        BarSystem.Instance.BarStopButton();
        winButton.enabled = false;
        _winPrizeButton.enabled = false;
        Vibration.Vibrate(30);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
    private IEnumerator WinButton()
    {
        MoneySystem.Instance.MoneyTextRevork(GameManager.Instance.addedMoney);
        winButton.enabled = false;
        _winPrizeButton.enabled = false;
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(0);
    }
    private void SettingButton()
    {
        _startPanel.SetActive(false);
        _settingGame.SetActive(true);
        _settingButton.gameObject.SetActive(false);
        _money.SetActive(false);
    }
    private void SettingBackButton()
    {
        _startPanel.SetActive(true);
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
