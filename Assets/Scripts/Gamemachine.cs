using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Thematics
{
    None,
    Emotional,
    Job,
    Social,
    NB_THEMATICS
};

public enum Gamemodes
{
    PressStart = 0,
    Narration,
    Kitchen,
    Endgame
};

public class            Gamemachine : MonoBehaviour
{
    static int          max_days = (int)Thematics.NB_THEMATICS * 2;
    public int[]        daily_scores = new int[max_days];
    public int          current_day = -1;

    public List<StepData> steps = new List<StepData>();
    public int next_step = 0;
    public int current_step = 0;
    public Gamemodes current_mode = Gamemodes.PressStart;

    [SerializeField]
    private Image       intro;
    [SerializeField]
    private Image       transition;

    public static Gamemachine instance;

    void                Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else GameObject.Destroy(this.gameObject);
    }

    public void         LoadNarration(StepData data)
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        current_mode = Gamemodes.Narration;
    }

    public void         LoadKitchen(StepData data)
    {
        ++current_day;
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        current_mode = Gamemodes.Kitchen;
    }

    public void         EndGame()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
        current_mode = Gamemodes.Endgame;
    }

    public void         NextScene()
    {
        if (next_step >= steps.Count)
        {
            EndGame();
        }
        else if (steps[next_step].type == StepData.stepType.Narration)
        {
            LoadNarration(steps[next_step]);
        }
        else
        {
            LoadKitchen(steps[next_step]);
        }
        current_step = next_step;
        ++next_step;
    }

    public StepData     GetData()
    {
        return steps[current_step];
    }

    void                Update()
    {
        if (current_mode == Gamemodes.PressStart &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
            NextScene();
    }
}
