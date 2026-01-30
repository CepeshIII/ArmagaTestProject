using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DamageDisplay : MonoBehaviour, IDamageDisplay
{
    [SerializeField] private float timeToLive = 1f;

    private readonly List<TextMesh> activeTexts = new();


    private void Update()
    {
        foreach (var textObj in activeTexts)
        {
            if (textObj.gameObject.activeInHierarchy)
            {
                var pos = textObj.transform.position;
                pos += Vector3.up * Time.deltaTime;
                textObj.transform.position = pos;
                var color = textObj.color;
                color.a -= Time.deltaTime / timeToLive;
                textObj.color = color;
                if (color.a <= 0f)
                {
                    textObj.gameObject.SetActive(false);
                }
            }
        }
    }


    public void ShowDamage(float damage, Vector2 position)
    {
        var textObj = GetInactiveTextObject();
        textObj.transform.position = position + Vector2.up * 2f;
        textObj.gameObject.SetActive(true);
        textObj.color = Color.red;
        textObj.text = (damage.ToString());
    }


    public void ShowHeal(float amount, Vector2 position)
    {
        var textObj = GetInactiveTextObject();
        textObj.transform.position = position + Vector2.up * 2f;
        textObj.gameObject.SetActive(true);
        textObj.color = Color.red;
        textObj.text = (amount.ToString());
    }


    private TextMesh GetInactiveTextObject()
    {
        foreach (var textObj in activeTexts)
        {
            if (!textObj.gameObject.activeInHierarchy)
            {
                return textObj;
            }
        }

        var newTextObj = new GameObject("DamageText").AddComponent<TextMesh>();
        newTextObj.characterSize = 0.25f;
        activeTexts.Add(newTextObj);
        return newTextObj;
    }
}
