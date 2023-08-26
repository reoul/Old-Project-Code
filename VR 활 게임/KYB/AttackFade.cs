using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackFade : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    public void ShowAttackFade()
    {
        StopAllCoroutines();
        StartCoroutine(ShowFadeCoroutine());
    }

    private IEnumerator ShowFadeCoroutine()
    {
        float percent = 0;
        while (percent < 0.2f)
        {
            percent += Time.deltaTime * 1.5f;
            _fadeImage.color = new Color(0.6f, 0, 0, percent);
            yield return new WaitForEndOfFrame();
        }

        while (percent > 0)
        {
            percent -= Time.deltaTime * 1.5f;
            _fadeImage.color = new Color(0.6f, 0, 0, percent);
            yield return new WaitForEndOfFrame();
        }
    }
}
