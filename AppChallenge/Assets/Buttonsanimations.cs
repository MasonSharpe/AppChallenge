using UnityEngine;
using UnityEngine.UI;

public class Buttonsanimations : MonoBehaviour
{
    public Button myButton;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = myButton.transform.localScale;
    }

    public void AnimateButton()
    {
        myButton.transform.localScale = originalScale * 1.2f;

    }
}
