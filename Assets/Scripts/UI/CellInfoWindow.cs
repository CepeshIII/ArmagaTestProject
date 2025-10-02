using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using Zenject;



public class CellInfoWindow : MonoBehaviour, ICellInfoWindow
{
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    private EffectFactory effectFactory;



    [Inject]
    public void Construct(EffectFactory effectFactory)
    {
        this.effectFactory = effectFactory;
    }


    public void Display(Cell cell)
    {
        var sb = new StringBuilder();
        sb.AppendLine(BuildCardsDescription(cell.cards));
        sb.AppendLine(BuildEffectsDescription(cell.effects));
        m_TextMeshProUGUI.text = sb.ToString();
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }


    private string BuildCardsDescription(List<CardInstance> cards)
    {
        var sb = new StringBuilder("Cards:\n");

        if (cards == null || cards.Count == 0)
        {
            return sb.Append("none".Red()).ToString();
        }

        foreach (var card in cards)
        {
            sb.AppendLine(card.Data.name.Bold().Color("red"));
        }
        return sb.ToString();
    }


    private string BuildEffectsDescription(List<EffectData> effectsData)
    {
        var sb = new StringBuilder("Effects:\n");

        if (effectsData == null || effectsData.Count == 0)
        {
            return sb.Append("none".Red()).ToString();
        }

        foreach (var effectData in effectsData)
        {
            var effect = effectFactory.GetEffect(effectData);
            var description = effect.GetDescription();
            var value = effectData.effectValue.ToString();
            var target = effectData.effectTarget.ToString();

            sb.AppendLine($"-{description.Blue()}: {value.Yellow()}");
            sb.AppendLine($"\t+Target: {target.Green()}");
        }
        return sb.ToString();
    }

}

