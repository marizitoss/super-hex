using UnityEngine;
using System.Collections;

public class PieceSpawner : MonoBehaviour
{
    private static readonly Color ColorOne = new Color32(255, 183, 197, 255);
    private static readonly Color ColorTwo = new Color32(181, 234, 215, 255);
    private static readonly Color ColorThree = new Color32(199, 206, 234, 255);
    private static readonly Color ColorFour = new Color32(255, 218, 185, 255);
    private static readonly Color ColorSix = new Color32(255, 240, 150, 255);
    private static readonly Color ColorEight = new Color32(178, 235, 242, 255);
    private static readonly Color ColorTen = new Color32(220, 180, 235, 255);
    private static readonly Color ColorTwelve = new Color32(255, 204, 153, 255);
    private static readonly Color ColorFourteen = new Color32(144, 238, 144, 255);
    private static readonly Color ColorSixteen = new Color32(255, 160, 180, 255);
    private static readonly Color ColorEighteen = new Color32(173, 216, 230, 255);
    private static readonly Color ColorTwenty = new Color32(221, 160, 221, 255);
    private static readonly Color ColorTwentyTwo = new Color32(152, 251, 152, 255);
    private static readonly Color ColorTwentyFour = new Color32(255, 228, 196, 255);

    public static PieceSpawner Instance { get; private set; }
    [SerializeField] private NumberPiece piecePrefab;
    [SerializeField] private Transform spawnPoint;
    private int[] numberPool = { 1, 2, 3 };
    private NumberPiece currentPiece;
    private int piecesPlaced = 0;
    private const int MaxPieces = 6;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Start()
    {
        yield return null;
        SpawnNewPiece();
    }

    public void SpawnNewPiece()
    {
        if (currentPiece != null)
            Destroy(currentPiece.gameObject);

        int a = numberPool[Random.Range(0, numberPool.Length)];
        int b = numberPool[Random.Range(0, numberPool.Length)];

        currentPiece = Instantiate(piecePrefab, spawnPoint.position, Quaternion.identity);

        currentPiece.SetRotation(Random.Range(0, 6));

        currentPiece.Setup(a, b);
        currentPiece.ApplyColors(GetColorForNumber(a), GetColorForNumber(b));
    }

    public void ReleaseCurrent()
    {
        currentPiece = null;
    }

    public void AddToPool(int value)
    {
        foreach (int n in numberPool)
            if (n == value) return;

        System.Array.Resize(ref numberPool, numberPool.Length + 1);
        numberPool[numberPool.Length - 1] = value;
    }

    public void RegisterPlacement()
    {
        piecesPlaced++;
        Debug.Log($"Peças colocadas: {piecesPlaced}/{MaxPieces}");

        if (piecesPlaced >= MaxPieces)
        {
            Debug.Log("Jogo encerrado!");
            if (currentPiece != null) Destroy(currentPiece.gameObject);
            gameObject.SetActive(false);

            return;
        }

        SpawnNewPiece();
    }

    public Color GetColorForNumber(int number)
    {
        return number switch
        {
            1 => ColorOne,
            2 => ColorTwo,
            3 => ColorThree,
            4 => ColorFour,
            6 => ColorSix,
            8 => ColorEight,
            10 => ColorTen,
            12 => ColorTwelve,
            14 => ColorFourteen,
            16 => ColorSixteen,
            18 => ColorEighteen,
            20 => ColorTwenty,
            22 => ColorTwentyTwo,
            24 => ColorTwentyFour,
            _ => new Color(Random.value, Random.value, Random.value)
        };
    }
}