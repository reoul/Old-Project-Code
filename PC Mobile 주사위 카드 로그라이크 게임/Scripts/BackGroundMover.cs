
using UnityEngine;
using UnityEngine.UI;

public class BackGroundMover : MonoBehaviour
{
    [SerializeField]
    Image[] bg;
    [SerializeField, Tooltip("bg와 크기가 같아야 함")]
    float[] speed;

    struct BG
    {
        Image backGound;
        GameObject copiedBG;
        float width;
        float speed;

        public BG(Image bg, float tmpspeed){
            speed = tmpspeed;
            backGound = bg;
            copiedBG = new GameObject();
            copiedBG = GameObject.Instantiate(backGound.gameObject, backGound.transform.parent);
            copiedBG.transform.SetSiblingIndex(bg.transform.GetSiblingIndex() + 1);
            width = backGound.rectTransform.rect.width * backGound.rectTransform.localScale.x;
            copiedBG.transform.localPosition = backGound.transform.localPosition + new Vector3(width, 0, 0);
        }

        public void Move()
        {
            backGound.transform.localPosition -= new Vector3(100 * speed * Time.deltaTime, 0, 0);
            copiedBG.transform.localPosition -= new Vector3(100 * speed * Time.deltaTime, 0, 0);
            if(backGound.transform.localPosition.x <= -width)
            {
                backGound.transform.localPosition += new Vector3(width * 2, 0, 0);
            }
            if(copiedBG.transform.localPosition.x <= -width)
            {
                copiedBG.transform.localPosition += new Vector3(width * 2, 0, 0);
            }
        }
    }

    BG[] backGround;

    private void Start()
    {
        backGround = new BG[bg.Length];

        for(int i = 0; i < bg.Length; i++)
        {
            backGround[i] = new BG(bg[i], speed[i]);
        }
    }

    void Update()
    {
        foreach(BG bg in backGround)
        {
            bg.Move();
        }
    }
}
