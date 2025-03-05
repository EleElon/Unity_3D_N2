using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SG {
    class UI_Match_Scroll_Wheel_To_Selected_Button : MonoBehaviour {

        [SerializeField] GameObject currentSelected;
        [SerializeField] GameObject previouslySelected;
        [SerializeField] RectTransform currentSelectedTransform;

        [SerializeField] RectTransform contentPanel;
        [SerializeField] ScrollRect scrollRect;

        void Update() {
            currentSelected = EventSystem.current.currentSelectedGameObject;

            if (currentSelected != null) {
                if (currentSelected != previouslySelected) {
                    previouslySelected = currentSelected;
                    currentSelectedTransform = currentSelected.GetComponent<RectTransform>();

                    SnapTo(currentSelectedTransform);
                }
            }
        }

        void SnapTo(RectTransform target) {
            Canvas.ForceUpdateCanvases();

            Vector2 newPosition = (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

            newPosition.x = 0;
            contentPanel.anchoredPosition = newPosition;
        }
    }
}
