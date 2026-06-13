using UnityEngine;

public class PieceDragHandler : MonoBehaviour
{
    private Vector3 dragOffset;
    private bool isDragging;
    private Vector3 mouseDownPosition;
    private bool hasDragged;
    private bool mouseHeld;
    private Vector3 spawnPosition;
    private HexCell previewCellA;
    private HexCell previewCellB;

    private void Awake()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {
        if (!mouseHeld)
            return;

        Vector3 currentMouse =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        currentMouse.z = 0;

        if (!hasDragged)
        {
            float distance = Vector3.Distance(currentMouse, mouseDownPosition);

            if (distance > 0.2f)
            {
                hasDragged = true;
                isDragging = true;
            }
        }

        if (!isDragging || !hasDragged)
            return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorld.z = 0;

        transform.position = mouseWorld + dragOffset;

        UpdatePlacementPreview();
    }

    private void OnMouseDown()
    {
        NumberPiece piece = GetComponent<NumberPiece>();

        if (piece.IsPlaced)
            return;

        mouseHeld = true;

        mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseDownPosition.z = 0;

        hasDragged = false;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorld.z = 0;

        dragOffset = transform.position - mouseWorld;
    }

    private void OnMouseUp()
    {
        previewCellA?.ResetColor();

        previewCellB?.ResetColor();

        mouseHeld = false;
        isDragging = false;

        if (!hasDragged)
        {
            GetComponent<NumberPiece>().RotateClockwise();

            return;
        }

        NumberPiece piece = GetComponent<NumberPiece>();

        if (piece.IsPlaced) return;

        isDragging = false;

        HexGridManager grid = FindFirstObjectByType<HexGridManager>();

        HexCell cellA = grid.GetClosestCell(piece.HexA.position);

        HexCell cellB = grid.GetClosestCell(piece.HexB.position);

        bool hexAValid = grid.IsPositionInsideGrid(piece.HexA.position);

        bool hexBValid = grid.IsPositionInsideGrid(piece.HexB.position);

        if (!hexAValid || !hexBValid)
        {

            transform.position = spawnPosition;

            return;
        }

        piece.transform.position = cellA.transform.position;

        if (cellA.IsOccupied || cellB.IsOccupied)
        {
            transform.position = spawnPosition;

            return;
        }

        cellA.SetOccupied(piece);
        cellB.SetOccupied(piece);
        piece.MarkAsPlaced();
        PieceSpawner.Instance.ReleaseCurrent();
        PieceSpawner.Instance.SpawnNewPiece();
    }

    private void UpdatePlacementPreview()
    {

        HexGridManager grid = FindFirstObjectByType<HexGridManager>();

        previewCellA?.ResetColor();

        previewCellB?.ResetColor();

        NumberPiece piece =
            GetComponent<NumberPiece>();

        HexCell cellA = grid.GetClosestCell(piece.HexA.position);

        HexCell cellB = grid.GetClosestCell(piece.HexB.position);

        bool validA = grid.IsPositionInsideGrid(piece.HexA.position);

        bool validB = grid.IsPositionInsideGrid(piece.HexB.position);

        Debug.Log($"A={validA} B={validB}");

        if (!validA || !validB) return;

        if (cellA.IsOccupied || cellB.IsOccupied) return;

        cellA.ShowValidPlacement();
        cellB.ShowValidPlacement();

        previewCellA = cellA;
        previewCellB = cellB;
    }
}