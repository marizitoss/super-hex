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
    private int numberA;
    private int numberB;
    private int rotationIndex;
    public int RotationIndex => rotationIndex;
    public Transform HexA => hexA;
    public Transform HexB => hexB;
    public bool IsPlaced { get; private set; }
    public int NumberA => numberA;
    public int NumberB => numberB;

    private readonly Vector2[] directions =
       {
        new( 1f, 0f),
        new( 0.5f, 0.866f),
        new(-0.5f, 0.866f),
        new(-1f, 0f),
        new(-0.5f,-0.866f),
        new( 0.5f,-0.866f)
    };

    private void Awake()
    {
        UpdateLayout();
    }

    public void Setup(int a, int b)
    {
        numberA = a;
        numberB = b;

        textA.text = a.ToString();
        textB.text = b.ToString();

        UpdateLayout();
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


    public void MarkAsPlaced()
    {

        IsPlaced = true;
    }

    private void UpdateLayout()
    {
        hexA.localPosition = Vector3.zero;

        if (HexSettings.CellWidth == 0) return;

        float hexDistance = HexSettings.HorizontalSpacing;
        float diagonalX = HexSettings.DiagonalX;
        float diagonalY = HexSettings.DiagonalY;

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
    public void HideTexts()
    {
        textA.gameObject.SetActive(false);
        textB.gameObject.SetActive(false);
    }

    public void ApplyColors(Color colorA, Color colorB)
    {
        HexA.GetComponentInChildren<HexCell>().SetBaseColor(colorA);
        HexB.GetComponentInChildren<HexCell>().SetBaseColor(colorB);
    }
}