using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public void ExitPage(GameObject currentPage) {
        currentPage.SetActive(false);
    }

    public void OpenPage(GameObject pageToOpen) {
        pageToOpen.SetActive(true);
    }


}
