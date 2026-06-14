using UnityEngine;

public class PieceDragHandler : MonoBehaviour
{
    private Vector3 dragOffset;
    private bool isDragging;
    private bool hasDragged;
    private bool mouseHeld;
    private Vector3 mouseDownPosition;
    private Vector3 spawnPosition;
    private HexCell previewCellA;
    private HexCell previewCellB;
    private HexGridManager Grid => FindFirstObjectByType<HexGridManager>();
    private NumberPiece Piece => GetComponent<NumberPiece>();

    private void Awake()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {
        if (!mouseHeld) return;

        Vector3 mouse = MouseWorldPos();

        if (!hasDragged && Vector3.Distance(mouse, mouseDownPosition) > 0.2f)
        {
            hasDragged = true;
            isDragging = true;
        }

        if (!isDragging) return;

        transform.position = mouse + dragOffset;
        UpdatePlacementPreview();
    }

    private void OnMouseDown()
    {
        if (Piece.IsPlaced) return;

        mouseHeld = true;
        hasDragged = false;
        mouseDownPosition = MouseWorldPos();
        dragOffset = transform.position - mouseDownPosition;
    }

    private void OnMouseUp()
    {
        ClearPreview();

        mouseHeld = false;
        isDragging = false;

        if (!hasDragged)
        {
            Piece.RotateClockwise();
            return;
        }

        if (Piece.IsPlaced) return;

        TryPlacePiece();
    }

    private void TryPlacePiece()
    {
        HexGridManager grid = Grid;
        NumberPiece piece = Piece;

        if (!grid.IsPositionInsideGrid(piece.HexA.position) ||
            !grid.IsPositionInsideGrid(piece.HexB.position))
        {
            ReturnToSpawn();
            return;
        }

        HexCell targetA = grid.GetClosestCell(piece.HexA.position);
        HexCell targetB = grid.GetClosestCell(piece.HexB.position);

        if (targetA == targetB)
        {
            ReturnToSpawn();
            return;
        }

        bool canPlaceA = !targetA.IsOccupied || targetA.Value == piece.NumberA;
        bool canPlaceB = !targetB.IsOccupied || targetB.Value == piece.NumberB;

        if (!canPlaceA || !canPlaceB)
        {
            ReturnToSpawn();
            return;
        }

        PlacePiece(piece, targetA, targetB);
    }

    private void PlacePiece(NumberPiece piece, HexCell targetA, HexCell targetB)
    {
        int finalA = targetA.IsOccupied ? targetA.Value + piece.NumberA : piece.NumberA;
        int finalB = targetB.IsOccupied ? targetB.Value + piece.NumberB : piece.NumberB;

        Color colorA = PieceSpawner.Instance.GetColorForNumber(finalA);
        Color colorB = PieceSpawner.Instance.GetColorForNumber(finalB);

        NumberPiece oldA = targetA.OccupyingPiece;
        NumberPiece oldB = targetB.OccupyingPiece;

        targetA.SetOccupied(piece, finalA, colorA);
        targetB.SetOccupied(piece, finalB, colorB);

        if (oldA != null) Destroy(oldA.gameObject);
        if (oldB != null) Destroy(oldB.gameObject);

        Destroy(piece.gameObject);

        PieceSpawner.Instance.AddToPool(finalA);
        PieceSpawner.Instance.AddToPool(finalB);
        PieceSpawner.Instance.ReleaseCurrent();
        PieceSpawner.Instance.RegisterPlacement();
    }

    private void UpdatePlacementPreview()
    {
        ClearPreview();

        HexGridManager grid = Grid;
        NumberPiece piece = Piece;

        if (!grid.IsPositionInsideGrid(piece.HexA.position) ||
            !grid.IsPositionInsideGrid(piece.HexB.position)) return;

        HexCell targetA = grid.GetClosestCell(piece.HexA.position);
        HexCell targetB = grid.GetClosestCell(piece.HexB.position);

        if (targetA == targetB) return;

        bool canPlaceA = !targetA.IsOccupied || targetA.Value == piece.NumberA;
        bool canPlaceB = !targetB.IsOccupied || targetB.Value == piece.NumberB;

        if (!canPlaceA || !canPlaceB) return;

        targetA.ShowValidPlacement();
        targetB.ShowValidPlacement();

        previewCellA = targetA;
        previewCellB = targetB;
    }

    private void ClearPreview()
    {
        previewCellA?.ResetColor();
        previewCellB?.ResetColor();
        previewCellA = null;
        previewCellB = null;
    }

    private void ReturnToSpawn()
    {
        transform.position = spawnPosition;
    }

    private Vector3 MouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}