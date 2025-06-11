using UnityEngine;

public class CardSelector : MonoBehaviour
{
    private bool isSelected = false;
    private Vector3 originalPosition;
    public float liftAmount = 0.3f;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            transform.position = originalPosition + new Vector3(0, liftAmount, 0);
            isSelected = true;
        }
        else
        {
            transform.position = originalPosition;
            isSelected = false;
        }
    }
}
