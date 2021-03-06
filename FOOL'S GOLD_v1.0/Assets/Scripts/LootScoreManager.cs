using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootScoreManager : MonoBehaviour
{
    public static LootScoreManager instance;

    [SerializeField] GameObject realGoldBox;
    [SerializeField] GameObject foolsGoldBox;
    [SerializeField] GameObject miningInfoBox;
    [SerializeField] GameObject endScreen;

    [SerializeField] Texture2D miningCursorTexture;
    [SerializeField] Slider miningSlider;
    [SerializeField] TextMeshProUGUI scoreText;

    public static int highScore = 0;
    int score = 0;
    float timeShown;

    void Awake()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    void Start()
    {

        InvokeRepeating("timeBoxShown", 0.5f, 0.5f);       
        scoreText.text = score.ToString();
    }

    void Update()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    public void LootDrop()
    {
        int dropRate = Random.Range(1, 4); 

        if (dropRate != 1)
        {
            timeShown = 1.5f;

            realGoldBox.SetActive(true);
            foolsGoldBox.SetActive(false);

            score += 100;
            

            if (highScore <= score)
            {
                PlayerPrefs.SetInt("highScore", score);
            }          
        }
        else
        {
            timeShown = 1.5f;
            
            foolsGoldBox.SetActive(true);
            realGoldBox.SetActive(false);
        }        
    }

    void timeBoxShown()
    {  
        if (miningInfoBox.activeInHierarchy && !realGoldBox.activeInHierarchy && !foolsGoldBox.activeInHierarchy)
        {
            FindObjectOfType<AudioManager>().PlaySound("MiningAction");
        } else if (realGoldBox.activeInHierarchy || foolsGoldBox.activeInHierarchy || endScreen.activeInHierarchy)
        {
            FindObjectOfType<AudioManager>().StopSound("MiningAction");
        }

        if (realGoldBox.activeInHierarchy && !foolsGoldBox.activeInHierarchy)
        {
            timeShown -= 0.5f;
            scoreText.text = score.ToString();
            

            if (timeShown <= 0.01f)
            {
                realGoldBox.SetActive(false);
                Cursor.SetCursor(miningCursorTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (timeShown <= 0.65f)
            {
                scoreText.text = score.ToString();
                FindObjectOfType<AudioManager>().PlaySound("RealGoldDrop");
            }
        } else if (foolsGoldBox.activeInHierarchy && !realGoldBox.activeInHierarchy)
        {
            timeShown -= 0.5f;
            

            if (timeShown <= 0.01f)
            {
                foolsGoldBox.SetActive(false);
                Cursor.SetCursor(miningCursorTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (timeShown <= 0.65f)
            {
                FindObjectOfType<AudioManager>().PlaySound("FoolGoldDrop");
            }
        }
        
    }
}
