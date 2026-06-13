using TMPro;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color validColor = Color.green;
    [SerializeField] private SpriteRenderer highlightRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text valueText;

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

    public void SetOccupied(NumberPiece piece, int value)
    {
        OccupyingPiece = piece;
        IsOccupied = piece != null;
        Value = value;
        UpdateVisual();
    }

    public void ShowValidPlacement()
    {
        highlightRenderer.enabled = true;
    }

    public void ResetColor()
    {
        highlightRenderer.enabled = false;
    }
}