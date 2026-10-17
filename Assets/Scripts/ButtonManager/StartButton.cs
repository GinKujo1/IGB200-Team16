using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Equation;

public class StartButton : MonoBehaviour
{
    //===================================================//
    [Header("Operation Buttons")]
    public Button multiplyButton;
    public Button divisionButton;
    public Button addButton;
    public Button subtractButton;
  
    [Header("Random Button")]
    public Button randomButton;


    private Operations selectedOperation = Operations.Multiply;
    private bool selectedRandom = false;


    //===================================================//
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        addButton.onClick.AddListener(() => SelectOperation(Operations.Add));
        subtractButton.onClick.AddListener(() => SelectOperation(Operations.Subtract));
        multiplyButton.onClick.AddListener(() => SelectOperation(Operations.Multiply));
        divisionButton.onClick.AddListener(() => SelectOperation(Operations.Division));
        randomButton.onClick.AddListener(SelectRandom);

        // Initialize with default (Add selected)
        SelectOperation(Operations.Add);
    }

    private void SelectOperation(Operations op)
    {
        selectedOperation = op;
        selectedRandom = false;
        UpdateButtonVisuals();
    }

    private void SelectRandom()
    {
        selectedRandom = true;
        UpdateButtonVisuals();
    }

    private void UpdateButtonVisuals()
    {
        // A button is selected if it's the chosen operation and we're not in random mode
        SetSelected(addButton, !selectedRandom && selectedOperation == Operations.Add);
        SetSelected(subtractButton, !selectedRandom && selectedOperation == Operations.Subtract);
        SetSelected(multiplyButton, !selectedRandom && selectedOperation == Operations.Multiply);
        SetSelected(divisionButton, !selectedRandom && selectedOperation == Operations.Division);
        SetSelected(randomButton, selectedRandom);
    }

    private static void SetSelected(Button b, bool selected)
    {
        ColorBlock colours = b.colors;
        colours.normalColor = selected ? Color.green : Color.white;
        colours.selectedColor = colours.normalColor;
        b.colors = colours;
    }
    //===========================================================================================================//
    public CanvasGroup fadeOutImage;
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Build");
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void StartGame() {
        GameModeStatic.Operation = selectedOperation;   // game settings
        GameModeStatic.RandomOperations = selectedRandom; // game settings

        SoundManager.Instance.PlaySoundEffect(SFXType.SFX_StartButtonClick);

        StartCoroutine(LoadingToMainGame(1f));
        
    }
    public IEnumerator FadeOutScreen(float fadeTime) {
        float startAlphaValue = fadeOutImage.alpha;
        float t = 0f;
        while (fadeOutImage.alpha < 1)
        {
            Debug.Log("Fading");
            fadeOutImage.alpha += Time.deltaTime;
            //fadeOutImage.alpha += startAlphaValue * Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public IEnumerator LoadingToMainGame(float transitionTime) {
        StartCoroutine(SoundManager.Instance.FadeOutMusic(transitionTime));
        StartCoroutine(FadeOutScreen(transitionTime));
        yield return new WaitForSeconds(transitionTime);
        LoadSceneByIndex(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    


}
