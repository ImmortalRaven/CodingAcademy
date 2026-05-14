using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    public Image playerHPBar;
    public GameObject playerDamageScreen;
    public TMP_Text gameGoalCountText;

    public bool isPaused;
    public GameObject player;
    public playerControl playercontrol;
    public GameObject[] bossDoors;

    int gameGoalCount;
    int keyGoalCount;

    float timeScaleOrig;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
        player = GameObject.FindWithTag("Player");
        playercontrol = player.GetComponent<playerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                menuActive.SetActive(true);
            }
            else if (menuActive == menuPause)
            {
                stateUnpaused();
            }
        }
    }

    public void statePause()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void stateUnpaused()
    {
        isPaused = false;
        Time.timeScale = timeScaleOrig;
        menuActive.SetActive(false);
        menuActive = null;
    }

    public void updateGameGoal(int amount)
    {
        gameGoalCount += amount;
        gameGoalCountText.text = gameGoalCount.ToString("F0");
        if (gameGoalCount <= 0)
        {
            // you win!
            statePause();
            menuActive = menuWin;
            menuActive.SetActive(true);
        }

    }

    public void updateKeyGoal(int amount)
    {
        keyGoalCount += amount;

        if (keyGoalCount <= 0)
        {
            bossDoors = GameObject.FindGameObjectsWithTag("BossDoor");
            foreach (GameObject door in bossDoors)
            {
                Destroy(door);
            }
        }
    }

    public void YouLose()
    {
        statePause();
        menuActive = menuLose;
        menuActive.SetActive(true);
    }
}
