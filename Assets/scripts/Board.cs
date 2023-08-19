using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tilePrefab; // Assign the tile prefab in the Inspector
    public GameObject[] piecePrefabs; // Assign the chess piece prefabs in the Inspector
    public int boardSize = 8;
    public float tileSize = 1.0f; // Size of each tile
    public float xOffset = 0.0f; // Adjust this value to move the board to the right
    public float yOffset = 0.0f; // Adjust this value to move the board down
    public Color lightColor;
    public Color darkColor;

    private void Start()
    {
        GenerateChessboard();
        SpawnChessPieces();
    }

    private void GenerateChessboard()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                Vector3 position = new Vector3(col * tileSize + xOffset, row * tileSize + yOffset, 0);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                MeshRenderer tileRenderer = tile.GetComponent<MeshRenderer>();
                tileRenderer.material.color = (row + col) % 2 == 0 ? lightColor : darkColor;
            }
        }
    }

    private void SpawnChessPieces()
    {
        // Initial setup for a standard chess game
        string[] pieceOrder = { "rook", "knight", "bishop", "queen", "king", "bishop", "knight", "rook" };

        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                int pieceIndex = col % boardSize; // Cycle through the piece order
                string pieceType = pieceOrder[pieceIndex];

                Vector3 position = new Vector3(col * tileSize + xOffset, row * tileSize + yOffset, 0);
                GameObject piecePrefab = FindPiecePrefab(pieceType);
                Instantiate(piecePrefab, position, Quaternion.identity);
            }
        }
    }

    private GameObject FindPiecePrefab(string pieceType)
    {
        // Match the piece type to the appropriate prefab
        foreach (GameObject piecePrefab in piecePrefabs)
        {
            if (piecePrefab.name.StartsWith(pieceType))
            {
                return piecePrefab;
            }
        }
        return null; // Return null if no match is found
    }
}

