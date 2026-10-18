using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    public void OnPrevPagePressed(GameObject prevPage) {
        if (prevPage != null) {
            //currentPage.SetActive(false);
            prevPage.SetActive(true);
        }
    }

    public void OnNextPagePressed(GameObject currentPage, GameObject nextPage) {
        if (nextPage != null) { 
            currentPage.SetActive(false);
            nextPage.SetActive(true);
        }
    }

    public void ExitPage(GameObject currentPage) {
        currentPage.SetActive(false);
    }

    public void OpenPage(GameObject pageToOpen) {
        pageToOpen.SetActive(true);
    }


}
