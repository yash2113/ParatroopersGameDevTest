using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public Text scoreText;
    public Image fadeImage;
    [SerializeField]
    private MachineController machine;
    public GameObject pauseMenuCanvas;

    private int score;

    public AudioSource blast;

    private void Awake()
    {
        machine = FindObjectOfType<MachineController>();
    }

    private void Start()
    {
        NewGame();
        pauseMenuCanvas.SetActive(false);
    }

    public void NewGame()
    {
     //   SceneManager.LoadScene(1);

        Time.timeScale = 1f;
        machine.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        ClearScene();
    }

    public void MainMenu()
    {
        pauseMenuCanvas.SetActive(true);
    }

   

    private void ClearScene()
    {
        HelicopterMovement[] helicopters = FindObjectsOfType<HelicopterMovement>();

        foreach (HelicopterMovement helicopter in helicopters)
        {
            Destroy(helicopter.gameObject);
        }

        Soldier[] soldiers = FindObjectsOfType<Soldier>();
        foreach (Soldier soldier in soldiers)
        {
            Destroy(soldier.gameObject);
        }

    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void Explode()
    {
        machine.enabled = false;
        blast.Play();
        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);


            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);


            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        SceneManager.LoadScene(0);

    }
}
