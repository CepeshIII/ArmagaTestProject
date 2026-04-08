using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;



public class CellInfoWindow : MonoBehaviour, ICellInfoWindow
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 pointerOffset;

    private EffectFactory effectFactory;
    private new Camera camera;



    [Inject]
    public void Construct(EffectFactory effectFactory, Camera camera)
    {
        this.effectFactory = effectFactory;
        this.camera = camera;
    }


    private void Update()
    {
        MoveWindow((Vector2)Mouse.current.position.value);
    }


    public void Display(Cell cell)
    {
        var sb = new StringBuilder();
        sb.AppendLine(BuildCardsDescription(cell.cards));
        sb.AppendLine(BuildEffectsDescription(cell.effects));
        textMeshProUGUI.text = sb.ToString();
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }


    private void MoveWindow(Vector2 screenPoint)
    {
        RectTransform parent = rectTransform.parent as RectTransform;
        Rect parentRect = parent.rect;

        Vector2 size = rectTransform.rect.size;

        // Start with pivot at top-left (mouse on that corner)
        Vector2 pivot = new Vector2(0f, 1f);

        // Convert screen point to local space of parent
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, screenPoint, camera, out Vector2 localPoint);

        // Default offset (applied away from cursor)
        Vector2 offset = pointerOffset;

        // If window goes off the right side → flip horizontally
        if (localPoint.x + size.x + offset.x > parentRect.xMax)
        {
            pivot.x = 1f;              // place mouse on right corner
            offset.x = -Mathf.Abs(offset.x); // push window left instead of right
        }
        else
        {
            pivot.x = 0f;
            offset.x = Mathf.Abs(offset.x);
        }

        // If window goes off the top side → flip vertically
        if (localPoint.y - size.y - offset.y < parentRect.yMin)
        {
            pivot.y = 0f;              // place mouse on bottom corner
            offset.y = Mathf.Abs(offset.y); // push window upward
        }
        else
        {
            pivot.y = 1f;
            offset.y = -Mathf.Abs(offset.y); // push window downward
        }

        // Apply pivot and position with offset
        rectTransform.pivot = pivot;
        rectTransform.localPosition = localPoint + offset;
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

            foreach(var description in card.GetDescription())
            {
                sb.AppendLine("\t" + description);
            }
        }

        return sb.ToString();
    }


    private string BuildEffectsDescription(List<EffectInstance> effectInstances)
    {
        var sb = new StringBuilder("Effects:\n");

        if (effectInstances == null || effectInstances.Count == 0)
        {
            return sb.Append("none".Red()).ToString();
        }

        foreach (var effectInstance in effectInstances)
        {
            var effect = effectFactory.GetEffect(effectInstance.Data);
            var description = effect.GetDescription();
            var value = effectInstance.Data.effectValue.ToString();
            var target = effectInstance.Data.filter.targetType.ToString();

            sb.AppendLine($"-({(effectInstance.Source as CardData).name.ToString().Green()}){description.Blue()}: {value.Yellow()}");
            sb.AppendLine($"\t+Target: {target.Green()}");
        }
        return sb.ToString();
    }

}

