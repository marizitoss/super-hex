using TMPro;
using UnityEngine;

public class NumberPiece : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Transform hexA;
    [SerializeField] private Transform hexB;
    [SerializeField] private TMP_Text textA;
    [SerializeField] private TMP_Text textB;
    [SerializeField] private SpriteRenderer hexSprite;
    [SerializeField] private float hexDistance = 1.9f;
    [SerializeField] private float diagonalX = 1.27f;
    [SerializeField] private float diagonalY = 1.90f;
    private int numberA;
    private int numberB;
    private int rotationIndex;
    private readonly Vector2[] directions =
       {
        new( 1f, 0f),
        new( 0.5f, 0.866f),
        new(-0.5f, 0.866f),
        new(-1f, 0f),
        new(-0.5f,-0.866f),
        new( 0.5f,-0.866f)
    };

    public void Setup(int a, int b)
    {
        numberA = a;
        numberB = b;

        textA.text = a.ToString();
        textB.text = b.ToString();
    }

    public void SetRotation(int index)
    {
        rotationIndex = index;
        UpdateLayout();
    }

    public void RotateClockwise()
    {
        rotationIndex = (rotationIndex + 1) % 6;

        UpdateLayout();
    }

    private void OnMouseDown()
    {
        RotateClockwise();
    }

    private void UpdateLayout()
    {
        hexA.localPosition = Vector3.zero;

        switch (rotationIndex)
        {
            case 0:
                hexB.localPosition = new Vector3(hexDistance, 0, 0);
                break;

            case 1:
                hexB.localPosition = new Vector3(diagonalX, diagonalY, 0);
                break;

            case 2:
                hexB.localPosition = new Vector3(-diagonalX, diagonalY, 0);
                break;

            case 3:
                hexB.localPosition = new Vector3(-hexDistance, 0, 0);
                break;

            case 4:
                hexB.localPosition = new Vector3(-diagonalX, -diagonalY, 0);
                break;

            case 5:
                hexB.localPosition = new Vector3(diagonalX, -diagonalY, 0);
                break;
        }
    }
}