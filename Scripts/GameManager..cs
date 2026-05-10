using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI errorsText;

    [Header("End Game Settings")]
    public GameObject gameplayPanel;
    public GameObject successImage;
    public int totalImages = 6;

    [Header("Sound Effects (الدلع)")]
    public AudioSource audioSource;
    public AudioClip successSound;  // صوت لما يركب الصورة صح
    public AudioClip errorSound;    // صوت لما يغلط
    public AudioClip winGameSound;  // صوت لما يخلص التمرين كله

    private float timeElapsed = 0f;
    private int errorCount = 0;
    private int correctMatches = 0;
    private bool isGameActive = false;

    void Start()
    {
        if (successImage != null) successImage.SetActive(false);

        // لو نسيت تضيف AudioSource الكود بيضيفه لنفسه تلقائياً
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        StartGame();
    }

    void Update()
    {
        if (isGameActive)
        {
            timeElapsed += Time.deltaTime;
            UpdateUI();
        }
    }

    public void StartGame()
    {
        timeElapsed = 0f;
        errorCount = 0;
        correctMatches = 0;
        isGameActive = true;
        UpdateUI();
    }

    public void AddError()
    {
        if (isGameActive)
        {
            errorCount++;
            // تشغيل صوت الغلط
            if (audioSource != null && errorSound != null)
                audioSource.PlayOneShot(errorSound);

            UpdateUI();
        }
    }

    public void AddCorrectMatch()
    {
        if (isGameActive)
        {
            correctMatches++;

            // تشغيل صوت الصح
            if (audioSource != null && successSound != null)
                audioSource.PlayOneShot(successSound);

            if (correctMatches >= totalImages)
            {
                WinGame();
            }
        }
    }

    void WinGame()
    {
        isGameActive = false;
        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (successImage != null) successImage.SetActive(true);

        // تشغيل صوت الفوز الكبير
        if (audioSource != null && winGameSound != null)
            audioSource.PlayOneShot(winGameSound);
    }

    void UpdateUI()
    {
        if (timerText != null)
            timerText.text = "Time: " + timeElapsed.ToString("F1") + "s";

        if (errorsText != null)
            errorsText.text = "Errors: " + errorCount.ToString();
    }

    public void LoadNextExercise(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}