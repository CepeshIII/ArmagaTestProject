using UnityEngine;

public interface IMaskShaderController
{
    bool HasMaterial { get; }
    bool HasTexture { get; }

    void SetMaterial(Material newMaterial);
    void SetMaskTexture(Texture2D newTexture);
    void SetGridOffset(Vector2Int offset);
    void CreateAndSetMaskTexture(Vector2Int textureSize);

    public void SetPixelColor(Vector2Int coord, Color color);
    public Color GetPixelColor(Vector2Int coord);

    public void SetBorderPixel(Vector2Int coord, bool isVisible);
    public void SetFillPixel(Vector2Int coord, bool isFilled);

    void ApplyMask();
    void ClearMask();
    void FillMask();
}

