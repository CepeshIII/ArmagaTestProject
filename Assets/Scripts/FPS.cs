using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private string displayString;
    private float nextUpdate = 0.0f;
    private const float updateInterval = 0.5f;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        if (Time.time > nextUpdate)
        {
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            displayString = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            nextUpdate = Time.time + updateInterval;
        }

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 10, 200, 30), displayString, style);
    }
}