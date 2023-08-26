using System.Collections.Generic;
using UnityEngine;

/// <summary> 화살표가 생성되는 방향 </summary>
public enum ArrowCreateDirection
{
    Left,
    Right,
    Up,
    Down
}

public class ArrowManager : Singleton<ArrowManager>
{
    public GameObject arrowPrefab;

    [SerializeField] private List<Arrow> arrows;

    private void Awake()
    {
        arrows = new List<Arrow>();
        CheckExistInstanceAndDestroy(this);
    }

    /// <summary> 화살표 생성 </summary>
    /// <param name="pos">생성 위치</param>
    /// <param name="direction">화살표 생성 방향</param>
    /// <param name="parent">부모 지정</param>
    public void CreateArrowObj(Vector3 pos, ArrowCreateDirection direction, Transform parent = null)
    {
        var arrow = Instantiate(arrowPrefab, pos, GetRotate(direction)).GetComponent<Arrow>();
        arrows.Add(arrow);
        if (parent != null)
        {
            arrow.transform.parent = parent;
        }
    }

    /// <summary> 화살표 방향에 따른 회전값 계산 </summary>
    /// <param name="direction">화살표 방향</param>
    /// <returns>회전값</returns>
    private Quaternion GetRotate(ArrowCreateDirection direction)
    {
        Quaternion quaternion = Quaternion.identity;
        switch (direction)
        {
            case ArrowCreateDirection.Left:
                quaternion.eulerAngles = new Vector3(0, 0, 270);
                break;
            case ArrowCreateDirection.Right:
                quaternion.eulerAngles = new Vector3(0, 0, 90);
                break;
            case ArrowCreateDirection.Up:
                quaternion.eulerAngles = new Vector3(0, 0, 180);
                break;
            case ArrowCreateDirection.Down:
                quaternion.eulerAngles = new Vector3(0, 0, 0);
                break;
            default:
                quaternion.eulerAngles = new Vector3(0, 0, 0);
                break;
        }

        return quaternion;
    }

    /// <summary> 활성화된 모든 화살표 오브젝트 제거 </summary>
    public void DestroyAllArrow()
    {
        int arrowCnt = arrows.Count;
        for (int i = 0; i < arrowCnt; i++)
        {
            if (arrows[0] == null || arrows[0].isActiveAndEnabled == false)
            {
                arrows.RemoveAt(0);
                i++;
            }

            Arrow arrow = arrows[0];
            arrows.RemoveAt(0);
            arrow.ArrowDestroy();
        }
    }
}
