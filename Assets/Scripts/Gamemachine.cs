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
    [SerializeField]
    private AnimationCurve curve;

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
    
    IEnumerator         LoadSceneTransition(int scene_to_load)
    {
        float           time = 0;
        float           speed = 2.0f;
        Color           transitionColor = new Color(transition.color.r, transition.color.g, transition.color.b);

        while (time < 1)
        {
            time += Time.deltaTime * speed;

            transitionColor.a = Mathf.Lerp(0.0f, 1.0f, curve.Evaluate(time));
            transition.color = transitionColor;
            yield return null;
        }
        SceneManager.LoadScene(scene_to_load, LoadSceneMode.Single);
        while (time > 0)
        {
            time -= Time.deltaTime * speed;

            transitionColor.a = Mathf.Lerp(0.0f, 1.0f, curve.Evaluate(time));
            transition.color = transitionColor;
            yield return null;
        }
    }

    public void         LoadNarration(StepData data)
    {
        current_mode = Gamemodes.Narration;
        StopAllCoroutines();
        StartCoroutine(LoadSceneTransition(1));
    }

    public void         LoadKitchen(StepData data)
    {
        ++current_day;
        current_mode = Gamemodes.Kitchen;
        StartCoroutine(LoadSceneTransition(2));
    }

    public void         EndGame()
    {
        current_mode = Gamemodes.Endgame;
        StartCoroutine(LoadSceneTransition(3));
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
        {
            intro.gameObject.SetActive(false);
            NextScene();
        }
    }
}
