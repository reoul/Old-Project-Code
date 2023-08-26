using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void PlayButtonSound()
    {
        SoundManager.Instance.PlaySound("Button");
    }
}
