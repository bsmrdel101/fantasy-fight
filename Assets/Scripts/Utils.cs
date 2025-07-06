using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Utils Instance { get; private set; }
    
    [Header("References")]
    [SerializeField] private BoardManager boardManager;
    private float gridGap;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        gameObject.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        gridGap = boardManager.GetGridGap();
    }
    
    public Vector2 ParseGridCoords(int x, int y)
    {
        float worldX = x * gridGap;
        float worldY = y * gridGap;
        return new Vector2(worldX, worldY);
    }
}
