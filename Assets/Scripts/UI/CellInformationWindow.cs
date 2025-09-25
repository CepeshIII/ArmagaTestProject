using TMPro;
using UnityEngine;


public class CellInformationWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;
    [SerializeField] private CardData cardData;
    [SerializeField] private EffectData effectData;


    public string Bold(string text)
    {
        return $"<b>{text}</b>";
    }


    public string Italian(string text)
    {
        return $"<i>{text}</i>";
    }

    public string Red(string text)
    {
        return $"<color=\"red\">{text}</color>";
    }

    public string Green(string text)
    {
        return $"<color=\"green\">{text}</color>";
    }

    public string Blue(string text)
    {
        return $"<color=\"blue\">{text}</color>";
    }


    public void Start()
    {
        var text = "";
        text = ParseCardsPart(text, cardData, cardData);
        text += "\n";
        text = ParseEffectsPart(text, cardData, effectData);

        m_TextMeshProUGUI.text = text;
    }


    private string ParseCardsPart(string str, CardData card1, CardData card2)
    {
        str += $"Main Card: {Red(Bold(ParseCardName(card1)))}\n";
        str += $"Second Card: {Red(Bold(ParseCardName(card2)))}\n";

        return str;
    }


    private string ParseCardName(CardData card)
    {
        var cardName = card != null ? card.cardName : "none";
        return cardName;
    }


    private string ParseEffectsPart(string str, CardData card, EffectData effectData)
    {
        str += "Effects:\n";

        if (effectData != null) 
        {
            str += ParseEffectHeader(card, effectData);
            str += ParseEffectParameters(effectData);
        }

        return str;
    }


    private string ParseEffectHeader(CardData card, EffectData effectData)
    {
        return $"-{Italian(Green(card.name))} {effectData.unitEffectType}:\n";
    }


    private string ParseEffectParameters(EffectData effectData) 
    {
        return ParseEffectParameter("effect Area", effectData.effectArea.ToString()) +
        ParseEffectParameter("target1", effectData.effectTarget.ToString());
    }

    private string ParseEffectParameter(string name, string value)
    {
        return $"\t+{name}: {Blue(value)}\n";
    }
}


public class CardParser
{
    public string Parse(CardData cardData)
    {



        return null;
    }
}
