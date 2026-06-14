using TMPro;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color validColor = Color.green;
    [SerializeField] private SpriteRenderer highlightRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private SpriteRenderer baseHexRenderer;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private Sprite occupiedSprite;

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
        if (valueText == null) return;

        if (Value > 0)
        {
            valueText.text = Value.ToString();
            valueText.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            valueText.text = "";
            valueText.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public bool IsOccupied { get; private set; }

    public void SetOccupied(NumberPiece piece, int value, Color color)
    {
        OccupyingPiece = piece;
        IsOccupied = piece != null;
        Value = value;

        UpdateVisual();
        SetBaseColor(color);

        SetOccupiedVisual();
    }

    public void ShowValidPlacement()
    {
        highlightRenderer.enabled = true;
    }

    public void ResetColor()
    {
        highlightRenderer.enabled = false;
    }

    public void SetBaseColor(Color color)
    {
        if (baseHexRenderer != null)
            baseHexRenderer.color = color;
    }

    public void SetOccupiedVisual()
    {
        baseHexRenderer.sprite = occupiedSprite;
    }

    public void SetEmptyVisual()
    {
        baseHexRenderer.sprite = emptySprite;
    }
    

}