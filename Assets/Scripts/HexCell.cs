using UnityEngine;

public class HexCell : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color validColor = Color.green;
    [SerializeField] private SpriteRenderer highlightRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Vector2Int Coordinates { get; private set; }
    public Vector3 WorldPosition => transform.position;
    public int Value { get; private set; }
    public NumberPiece OccupyingPiece { get; private set; }



    public void Initialize(int q, int r)
    {
        Coordinates = new Vector2Int(q, r);
    }

    public void SetValue(int value)
    {
        Value = value;
    }

    private void UpdateVisual()
    {
        // implementar depois
    }

    public bool IsOccupied { get; private set; }

    public void SetOccupied(NumberPiece piece)
    {
        OccupyingPiece = piece;
        IsOccupied = piece != null;
    }

    public void ShowValidPlacement()
    {
        Debug.Log($"Highlight {Coordinates}");

        highlightRenderer.enabled = true;
    }

    public void ResetColor()
    {
        highlightRenderer.enabled = false;
    }
}