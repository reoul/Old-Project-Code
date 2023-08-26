using UnityEngine;
using UnityEngine.UI;

public class IconMover : MonoBehaviour
{
    public Vector3 targetPos;

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 0.3f);
    }
}
