using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int slotID; // رقم الخانة اللي بنحطه من الـ Inspector

    public void OnDrop(PointerEventData eventData)
    {
        // هل فيه حاجة (صورة) سيبناها فوق الخانة دي؟
        if (eventData.pointerDrag != null)
        {
            // بنجيب سكريبت الـ DragDrop من الصورة
            DragDrop draggedItem = eventData.pointerDrag.GetComponent<DragDrop>();
            GameManager gm = Object.FindFirstObjectByType<GameManager>();

            // لو الصورة مطابقة لرقم الخانة
            if (draggedItem != null && draggedItem.itemID == slotID)
            {
                // 1. خلي الصورة ابن للخانة
                eventData.pointerDrag.transform.SetParent(transform);

                // 2. بنخلي الصورة تتجاهل الـ Grid Layout عشان متتبهدش
                LayoutElement layout = eventData.pointerDrag.GetComponent<LayoutElement>();
                if (layout == null) layout = eventData.pointerDrag.gameObject.AddComponent<LayoutElement>();
                layout.ignoreLayout = true;

                // 3. بنخلي الصورة تملأ الخانة بالظبط وتيجي في النص
                RectTransform rt = eventData.pointerDrag.GetComponent<RectTransform>();
                rt.anchorMin = new Vector2(0, 0);
                rt.anchorMax = new Vector2(1, 1);
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;

                // --- حتة الدلع (Pop Effect) ---
                // بنخلي الصورة تكبر فجأة 20% وبعدين ترجع لحجمها الطبيعي في ثانية
                eventData.pointerDrag.transform.localScale = Vector3.one * 1.2f;
                // ------------------------------

                // 4. بنثبت مكانها الجديد
                draggedItem.LockPosition(Vector2.zero);

                // 5. بنبلغ الـ GameManager إن فيه صورة ركبت صح عشان يحسب الفوز
                if (gm != null) gm.AddCorrectMatch();

                Debug.Log("صح! الصورة ركبت فوق الخانة بالظبط");
            }
            else
            {
                // لو الاختيار غلط
                if (gm != null) gm.AddError();

                Debug.Log("غلط! الأرقام مش متطابقة");
            }
        }
    }
}