using UnityEngine;
using System.Collections;

public class PieceSpawner : MonoBehaviour
{
    public static PieceSpawner Instance { get; private set; }
    [SerializeField] private NumberPiece piecePrefab;
    [SerializeField] private Transform spawnPoint;

    private int[] numberPool = { 1, 2, 3 };
    private NumberPiece currentPiece;

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
}