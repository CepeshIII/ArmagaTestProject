using UnityEngine;

public interface IMaskShaderController
{
    bool HasMaterial { get; }
    bool HasTexture { get; }

    void SetMaterial(Material newMaterial);
    void SetMaskTexture(Texture2D newTexture);
    void SetGridOffset(Vector2Int offset);
    void CreateAndSetMaskTexture(Vector2Int textureSize);
    void SetMaskPixel(Vector2Int coord, bool isVisible);
    void ApplyMask();
    void ClearMask();
    void FillMask();
}
