using System.Collections.Generic;
using UnityEngine;


public class GridShaderController : IMaskShaderController
{
    protected Material targetMaterial;
    protected Texture2D maskTexture;

    protected static readonly int ControlMapID = Shader.PropertyToID("_Mask");
    protected static readonly int GridOffsetID = Shader.PropertyToID("_GridOffset");

    // Value used in the border channel (Red) when the cell border is visible
    public float BorderVisibleValue = 1f;

    // Value used in the border channel (Red) when the cell is invisible
    public float BorderHiddenValue = 0f;

    // Value used in the fill channel (Green) when the cell is filled
    public float FillFilledValue = 1f;

    // Value used in the fill channel (Green) when the cell is empty
    public float FillEmptyValue = 0f;

    /// <summary>
    /// Color when cell should be visible
    /// </summary>
    public Color MaskVisibleColor { get { return Color.white; } }

    /// <summary>
    /// Color when cell should be hidden
    /// </summary>
    public Color MaskHiddenColor { get { return Color.black; } }

    public bool HasMaterial { get { return targetMaterial != null; } }
    public bool HasTexture { get { return targetMaterial.GetTexture(ControlMapID) != null; } }



    /// <summary>
    /// Assigns the target gridMaterial that the mask texture will be applied to.
    /// Does NOT automatically apply the mask; call ApplyMask() after changing pixels or creating a texture.
    /// </summary>
    public void SetMaterial(Material newMaterial)
    {
        targetMaterial = newMaterial;
    }


    /// <summary>
    /// Assigns a mask texture to the controller.
    /// Any pixel changes to this texture must be followed by ApplyMask() to update the gridMaterial.
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
        targetMaterial.SetVector(GridOffsetID, pixelOffset);
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
    /// Uploads all changes from the mask texture to the GPU and applies it to the gridMaterial.
    /// Must be called after pixel changes for them to appear in the shader.
    /// </summary>
    public void ApplyMask()
    {
        if (maskTexture == null || targetMaterial == null) return;

        maskTexture.Apply();
        targetMaterial.SetTexture(ControlMapID, maskTexture);
    }


    /// <summary>
    /// Fills the entire mask with hidden newColor.
    /// Changes are not applied to the gridMaterial until <see cref="ApplyMask"/> is called.
    /// </summary>
    public void ClearMask()
    {
        if (maskTexture == null) return;

        for (int x = 0; x < maskTexture.width; x++)
        {
            for (int y = 0; y < maskTexture.height; y++)
            {
                maskTexture.SetPixel(x, y, MaskHiddenColor);
            }
        }
    }


    /// <summary>
    /// Fills the entire mask with visible newColor.
    /// Changes are not applied to the gridMaterial until <see cref="ApplyMask"/> is called.
    /// </summary>
    public void FillMask()
    {
        if (maskTexture == null) return;

        for (int x = 0; x < maskTexture.width; x++)
        {
            for (int y = 0; y < maskTexture.height; y++)
            {
                maskTexture.SetPixel(x, y, MaskVisibleColor);
            }
        }
    }


    /// <summary>
    /// Sets a single pixel in mask texture. 
    /// Changes are not visible in the shader until <see cref="ApplyMask"/> is called.
    /// </summary>
    public void SetPixelColor(Vector2Int coord, Color color)
    {
        var pixelCoord = ToTextureCoord(coord);
        maskTexture.SetPixel(pixelCoord.x, pixelCoord.y, color);
    }


    /// <summary>
    /// Sets a single pixel in Border channel of the mask texture. 
    /// Changes are not visible in the shader until <see cref="ApplyMask"/> is called.
    /// </summary>
    public void SetBorderPixel(Vector2Int coord, bool isVisible)
    {
        var currentColor = GetPixelColor(coord);
        currentColor.r = isVisible ? BorderVisibleValue : BorderHiddenValue;
        SetPixelColor(coord, currentColor);
    }


    /// <summary>
    /// Updates the fill channel (green) of the mask at the specified coordinate.  
    /// Changes will not be visible in the shader until <see cref="ApplyMask"/> is called.
    /// </summary>
    public void SetFillPixel(Vector2Int coord, bool isFilled)
    {
        var currentColor = GetPixelColor(coord);
        currentColor.g = isFilled ? FillFilledValue : FillEmptyValue;
        SetPixelColor(coord, currentColor);
    }


    /// <summary>
    /// Get color of pixel in mask texture
    /// </summary>
    public Color GetPixelColor(Vector2Int coord)
    {
        var pixelCoord = ToTextureCoord(coord);
        return maskTexture.GetPixel(pixelCoord.x, pixelCoord.y);
    }


    // Convert grid coordinates to maskTexture coordinates.
    // ShaderGraph UVs are rotated 90°, so we swap x and y.
    protected Vector2Int ToTextureCoord(Vector2Int coord)
    {
        return new Vector2Int(coord.y, coord.x);
    }
}

