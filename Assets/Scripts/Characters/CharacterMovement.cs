using System;
using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Unity Actions")]
    public static Action<RaycastHit2D> OnClickTileAction;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("References")]
    private Pathfinding pathfinding;
    private Utils utils;
    
    private Coroutine moveRoutine;


    private void Start()
    {
        pathfinding = GameObject.Find("BoardManager").GetComponent<Pathfinding>();
        utils = GameObject.Find("Utils").GetComponent<Utils>();
    }

    private void OnEnable()
    {
        OnClickTileAction += OnClickTile;
    }

    private void OnDisable()
    {
        OnClickTileAction -= OnClickTile;
    }

    private void OnClickTile(RaycastHit2D hit)
    {
        Vector2 pos = hit.transform.position;
        MoveTo(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }

    public void MoveTo(int x, int y)
    {
        Vector2 targetPos = utils.ParseGridCoords(x, y);
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        Node[] path = pathfinding.FindPath(transform.position, targetPos);
        moveRoutine = StartCoroutine(HandleMovement(path));
    }

    private IEnumerator HandleMovement(Node[] path)
    {
        foreach (Node node in path)
        {
            Vector2 targetPos = utils.ParseGridCoords(node.x, node.y);
            while ((Vector2)transform.position != targetPos)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
