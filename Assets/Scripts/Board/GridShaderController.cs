using UnityEngine;

public class GridShaderController
{
    private Material targetMaterial;
    private Texture2D maskTexture;

    // Color when cell should be visible
    private readonly Color maskVisibleColor = new Color(1, 1, 1, 1);
    // Color when cell should be hidden
    private readonly Color maskHiddenColor = new Color(0, 0, 0, 1);

    private readonly string texturePropertiesName = "_Mask";
    private readonly string offsetPropertiesName = "_GridOffset";



    /// <summary>
    /// Assigns the target material that the mask texture will be applied to.
    /// Does NOT automatically apply the mask; call ApplyMask() after changing pixels or creating a texture.
    /// </summary>
    public void SetMaterial(Material newMaterial)
    {
        targetMaterial = newMaterial;
    }


    /// <summary>
    /// Assigns a mask texture to the controller.
    /// Any pixel changes to this texture must be followed by ApplyMask() to update the material.
    /// </summary>
    public void SetMaskTexture(Texture2D newTexture)
    {
        maskTexture = newTexture;
    }


    /// <summary>
    /// Sets the grid offset in the shader (in grid coordinates vs UV coordinates).
    /// UVs are rotated 90°, so x and y are swapped.
    /// </summary>
    public void SetGridOffset(Vector2Int offset)
    {
        var pixelOffset = new Vector2(offset.y + 1, -offset.x); // I do not know why I need to add 1 to x
        targetMaterial.SetVector(offsetPropertiesName, pixelOffset);
    }


    /// <summary>
    /// Creates a new mask texture with the given size and assigns it to the controller.
    /// The new texture must be populated with pixels and ApplyMask() called to be visible in the shader.
    /// </summary>
    public void CreateAndSetMaskTexture(Vector2Int textureSize)
    {
        // Initialize maskTexture
        var newTexture = new Texture2D(textureSize.x, textureSize.y);
        newTexture.filterMode = FilterMode.Point;
        newTexture.wrapMode = TextureWrapMode.Clamp;

        SetMaskTexture(newTexture);
    }


    /// <summary>
    /// Sets a single pixel in the mask texture. 
    /// Changes are not visible in the shader until <see cref="ApplyMask"/> is called.
    /// </summary>
    public void SetMaskPixel(Vector2Int coord, bool isVisible) 
    { 
        if(maskTexture != null)
        {
            var textCoord = ToTextureCoord(coord);
            maskTexture.SetPixel(textCoord.x, textCoord.y, isVisible ? maskVisibleColor : maskHiddenColor);
        }
    }



    /// <summary>
    /// Uploads all changes from the mask texture to the GPU and applies it to the material.
    /// Must be called after pixel changes for them to appear in the shader.
    /// </summary>
    public void ApplyMask()
    {
        if (maskTexture == null || targetMaterial == null) return;

        maskTexture.Apply();
        targetMaterial.SetTexture(texturePropertiesName, maskTexture);
    }


    /// <summary>
    /// Fills the entire mask with hidden color.
    /// Changes are not applied to the material until <see cref="ApplyMask"/> is called.
    /// </summary>
    public void ClearMask()
    {
        if (maskTexture == null) return;

        for (int x = 0; x < maskTexture.width; x++)
        {
            for (int y = 0; y < maskTexture.height; y++)
            {
                maskTexture.SetPixel(x, y, maskHiddenColor);
            }
        }
    }


    /// <summary>
    /// Fills the entire mask with visible color.
    /// Changes are not applied to the material until <see cref="ApplyMask"/> is called.
    /// </summary>
    public void FillMask()
    {
        if (maskTexture == null) return;

        for (int x = 0; x < maskTexture.width; x++)
        {
            for (int y = 0; y < maskTexture.height; y++)
            {
                maskTexture.SetPixel(x, y, maskVisibleColor);
            }
        }
    }


    // Convert grid coordinates to maskTexture coordinates.
    // ShaderGraph UVs are rotated 90°, so we swap x and y.
    private Vector2Int ToTextureCoord(Vector2Int coord)
    {
        return new Vector2Int(coord.y, coord.x);
    }
}
