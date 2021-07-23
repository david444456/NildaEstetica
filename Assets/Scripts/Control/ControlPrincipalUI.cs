using Est.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPrincipalUI : SingletonInInspector<ControlPrincipalUI>, IControlUI
{


    [Header("UI")]
    [SerializeField] TextMeshProUGUI textCoin = null;
    [SerializeField] TextMeshProUGUI textCoinPremium = null;
    [SerializeField] TextMeshProUGUI textGenerationCoin = null;
    [SerializeField] GameObject GOTextInformationSlot = null;
    [SerializeField] Text textInformationSlot = null;
    [SerializeField] Image imageBossInformationSlot = null;
    [SerializeField] Text textTypeSlot = null;
    [SerializeField] Image imageTypeSlot = null;

    [Header("Data and var")]
    [SerializeField] float timeLastShowInfSlot = 10f;
    [SerializeField] Configuration configuration = null;


    private float m_lastShowInformationSlot = 0;

    void Start()
    {
        textGenerationCoin.text = MathFunction.ChangeUnitNumberWithString(ControlCoins.Instance.CoinGenerationSecond, " ");
    }

    void Update()
    {
        //slot inf
        if (GOTextInformationSlot.activeInHierarchy)
        {
            m_lastShowInformationSlot += Time.deltaTime;
            if (m_lastShowInformationSlot > timeLastShowInfSlot)
            {
                GOTextInformationSlot.SetActive(false);
                m_lastShowInformationSlot = 0;
            }
        }
    }

    public void changeTextCoins(float newTextCoin, string keyStringUnit) {
        textCoin.text = MathFunction.ChangeUnitNumberWithString(newTextCoin, keyStringUnit);
    }

    public void changeTextCoinPremium(int newTextCoin)
    {
        textCoinPremium.text = newTextCoin.ToString();
    }

    public void changeTextGenerationCoins(float coinGenerationPerSecond, string keyStringUnit)
    {
        textGenerationCoin.text = MathFunction.ChangeUnitNumberWithString(coinGenerationPerSecond, keyStringUnit);
    }

    //ui
    public void ShowInformationSlot(string textSlot, Sprite spriteSlot, TypeSlot typeSlot)
    {
        GOTextInformationSlot.SetActive(true);
        textInformationSlot.text = textSlot;
        imageBossInformationSlot.sprite = spriteSlot;
        changeValueUITypeSlot(typeSlot);
    }

    private void changeValueUITypeSlot(TypeSlot typeSlot)
    {
        switch (typeSlot)
        {
            case TypeSlot.SlotMainCoin:
                ChangeUITypeSlot(configuration.typeSpriteCoin, configuration.stringTypeCoin);
                break;
            case TypeSlot.SlotAdvertising:
                ChangeUITypeSlot(configuration.typeSpriteGenerationCoin, configuration.stringTypeGenerationCoin);
                break;
        }
    }

    private void ChangeUITypeSlot(Sprite spriteType, string stringType)
    {
        imageTypeSlot.sprite = spriteType;
        textTypeSlot.text = stringType;
    }
}

public interface IControlUI {
    void changeTextCoins(float newTextCoin, string keyStringUnit);
    void changeTextGenerationCoins(float coinGenerationPerSecond, string keyStringUnit);
    void ShowInformationSlot(string textSlot, Sprite spriteSlot, TypeSlot typeSlot);
    void changeTextCoinPremium(int newTextCoin);
}
