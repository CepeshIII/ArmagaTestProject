using UnityEngine;


[RequireComponent(typeof(GameBoard))]
public class BoardDisplayer : MonoBehaviour
{
    [SerializeField] private Material boardMaterial;
    [SerializeField] private Vector2Int textureSize = new Vector2Int(16, 16);

    private GameBoard gameBoard;
    private Texture2D texture;

    private readonly Color notAvailableColor = new Color(1, 1, 1, 1);
    private readonly Color availableColor = new Color(0, 0, 0, 1);


    private void Awake()
    {
        gameBoard = GetComponent<GameBoard>();
        CreateTexture();
    }


    private void Start()
    {
        // My ShaderGraph 's UV is rotated 90 degrees, so I need to swap x and y
        var pixelOffset = new Vector2(gameBoard.Grid.GridOffset.y + 1, -gameBoard.Grid.GridOffset.x); // I do not know why I need to add 1 to x
        boardMaterial.SetVector("_GridOffset", pixelOffset);
    }

    private void Update()
    {
        ResetTexture();
        DrawBoard();
        ApplyMaskToMaterial();
    }


    private void CreateTexture()
    {
        // Initialize texture
        texture = new Texture2D(textureSize.x, textureSize.y);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
    }


    private void DrawBoard()
    {
        foreach (var cell in gameBoard.Board)
        {
            var indexCoord = cell.indexCoord;
            var pixelCoord = ToTextureCoord(indexCoord);

            Color color = cell.isAvailable ? availableColor : notAvailableColor;
            texture.SetPixel(pixelCoord.x, pixelCoord.y, color);
        }
    }


    private void ResetTexture()
    {
        for(int x = 0; x < texture.width; x++)
        {
            for(int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, notAvailableColor);
            }
        }
    }


    private void ApplyMaskToMaterial()
    {
        texture.Apply();
        boardMaterial.SetTexture("_Mask", texture);
    }


    private Vector2Int ToTextureCoord(Vector2Int coord)
    {
        // My ShaderGraph 's UV is rotated 90 degrees, so I need to swap x and y
        return new Vector2Int(coord.y, coord.x);
    }

    private void FillMask()
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, availableColor);
            }
        }
    }

}
