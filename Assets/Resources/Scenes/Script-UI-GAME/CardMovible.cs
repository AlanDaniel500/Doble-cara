using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMovible : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public CardDisplay cardData;
    public RectTransform zonaManoJugador;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform originalParent;
    private Vector2 originalPosition;
    private bool estaEnZonaCombo = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        if (zonaManoJugador == null)
        {
            Debug.LogError("ZonaManoJugador no asignada en " + gameObject.name);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (estaEnZonaCombo) return; // No se puede arrastrar si está en combo

        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;

        // Para que se vea por encima al arrastrar
        transform.SetParent(canvas.transform, true);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (estaEnZonaCombo) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (estaEnZonaCombo) return;

        canvasGroup.blocksRaycasts = true;

        ComboZonaManager zona = Object.FindFirstObjectByType<ComboZonaManager>();
        if (zona == null)
        {
            Debug.LogWarning("No se encontró el ComboZonaManager.");
            VolverAMano();
            return;
        }

        RectTransform zonaRect = zona.GetComponent<RectTransform>();

        if (RectTransformUtility.RectangleContainsScreenPoint(zonaRect, Input.mousePosition, eventData.enterEventCamera))
        {
            bool pudoAgregar = zona.TryAgregarCarta(this.gameObject);
            if (pudoAgregar)
            {
                estaEnZonaCombo = true;
            }
            else
            {
                VolverAMano();
            }
        }
        else
        {
            VolverAMano();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && estaEnZonaCombo)
        {
            ComboZonaManager zona = Object.FindFirstObjectByType<ComboZonaManager>();
            if (zona != null && zona.GetCartas().Contains(this.gameObject))
            {
                zona.SacarCarta(this.gameObject);
                estaEnZonaCombo = false;
                VolverAMano();
            }
        }
    }

    private void VolverAMano()
    {
        if (zonaManoJugador == null)
        {
            Debug.LogError("ZonaManoJugador no asignada para la carta " + gameObject.name);
            return;
        }

        transform.SetParent(zonaManoJugador, false);

        // Si tiene LayoutGroup en mano, que lo acomode automáticamente
        LayoutGroup layout = zonaManoJugador.GetComponent<LayoutGroup>();
        if (layout == null)
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }

        // Ajustar tamaño para cartas en mano (normal)
        rectTransform.sizeDelta = new Vector2(100, 150); // Ajustá al tamaño que uses normalmente
    }
}
