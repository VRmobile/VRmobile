using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler {
    public RectTransform m_rectTransform = null;

    private void Reset() {
        m_rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData e) {
        m_rectTransform.position += new Vector3(0f , e.delta.y , 0f);
    }
}