using System.Collections.Generic;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;

    [Header("References")]
    [SerializeField] private HexCell cellPrefab;

    private Dictionary<Vector2Int, HexCell> cells = new Dictionary<Vector2Int, HexCell>();

    private float cellWidth;
    private float cellHeight;

    private void Awake()
    {
        SpriteRenderer sr = cellPrefab.GetComponentInChildren<SpriteRenderer>();
        cellWidth = sr.bounds.size.x;
        cellHeight = sr.bounds.size.y;

        HexSettings.Initialize(cellWidth, cellHeight);
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int q = 0; q < width; q++)
        {
            for (int r = 0; r < height; r++)
            {
                CreateCell(q, r);
            }
        }
    }

    private void CreateCell(int q, int r)
    {
        Vector3 pos = GetPosition(q, r);


        HexCell cell = Instantiate(
            cellPrefab,
            pos,
            Quaternion.identity,
            transform);

        cell.Initialize(q, r);

        cells.Add(new Vector2Int(q, r), cell);
    }

    private Vector3 GetPosition(int column, int row)
    {
        float horizontalSpacing = cellWidth * HexSettings.HorizontalSpacingMultiplier;

        float verticalSpacing = cellHeight * HexSettings.VerticalSpacingMultiplier;

        float x = column * horizontalSpacing;

        if (row % 2 != 0)
        {
            x += horizontalSpacing * 0.5f;
        }

        float y = -row * verticalSpacing;

        return transform.position + new Vector3(x, y, 0);
    }

    public Vector2Int GetCoordinates(HexCell cell)
    {
        return cell.Coordinates;
    }

    public HexCell GetCell(Vector2Int coord)
    {
        cells.TryGetValue(coord, out HexCell cell);
        return cell;
    }

    public HexCell GetClosestCell(Vector3 worldPos)
    {
        HexCell closest = null;

        float minDistance = float.MaxValue;

        foreach (var cell in cells.Values)
        {
            float distance =
                Vector3.Distance(
                    worldPos,
                    cell.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = cell;
            }
        }

        return closest;
    }
    public bool IsPositionInsideGrid(Vector3 worldPosition)
    {
        HexCell closest = GetClosestCell(worldPosition);
        float distance = Vector3.Distance(worldPosition, closest.transform.position);

        return distance < HexSettings.HorizontalSpacing * 0.75f;
    }

}