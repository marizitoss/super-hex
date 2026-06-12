using UnityEngine;

public class PieceDragHandler : MonoBehaviour
{
    private Vector3 dragOffset;
    private bool isDragging;

    private void OnMouseDown()
    {
        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorld.z = 0;

        dragOffset =
            transform.position - mouseWorld;

        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        HexGridManager grid =
            FindFirstObjectByType<HexGridManager>();

        HexCell cell =
            grid.GetClosestCell(transform.position);

        transform.position =
            cell.transform.position;
    }

    private void Update()
    {
        if (!isDragging)
            return;

        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorld.z = 0;

        transform.position =
            mouseWorld + dragOffset;
    }
}