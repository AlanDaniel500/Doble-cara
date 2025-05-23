using UnityEngine;

public class ComboSelectable : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // clic derecho
        {
            var combo = FindFirstObjectByType<ComboArea>();
            if (combo != null)
            {
                combo.RemoveCard(gameObject);
            }
        }
    }
}
