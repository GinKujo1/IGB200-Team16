using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] private GameObject tutScreenFirstPage;
    public void OpenTutorialScreen() {
        tutScreenFirstPage.SetActive(true);
        this.gameObject.SetActive(true);
    }
    public void ExitPage(GameObject currentPage) {
        currentPage.SetActive(false);
    }

    public void OpenPage(GameObject pageToOpen) {
        pageToOpen.SetActive(true);
    }


}
