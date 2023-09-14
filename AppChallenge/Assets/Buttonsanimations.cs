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
        // Scale up the button
        myButton.transform.localScale = originalScale * 1.2f;

        // You can also change other properties like color, position, etc.
    }
}
