using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public float WorldColorIntensity;
    public Color WorldColor;
    public GameObject Background;
    public GameObject GlobalLight;
    public bool Day;
    public bool _inChange = false;
    public float TransitionTime = 5f;

    private GameObject _DayNight;
    Light2D _Light;
    Color colorNight;
    Color colorDayBG;
    Color colorDayLight;
    private bool _transitioning;

    private GameObject[] objectslight;

    EnnemyController ennemyController;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DayUpdating());

        _transitioning = false;
        Day = true;
        _Light = GetComponent<Light2D>();

        //Color
        colorNight = Color.black;
        colorDayBG = Color.white;
        ColorUtility.TryParseHtmlString("#CEFBFF", out colorDayLight);

        //Tab of GameObject Light
        objectslight = GameObject.FindGameObjectsWithTag("Light");
    }

    void Update()
    {
        if (_inChange) {
            if (Day)
            {
                StopAllCoroutines();
                StartCoroutine(NightUpdating());

                Day = false;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(DayUpdating());
                Day = true;

            }
            _inChange = false;
        }
    }

    private IEnumerator DayUpdating()
    {
        yield return null;
        StartCoroutine(UpdateDayNight());
        StartCoroutine(GetComponent<EnnemyController>().DayEnnemy());
    }
    private IEnumerator NightUpdating()
    {
        yield return null;
        StartCoroutine(UpdateDayNight());
        StartCoroutine(GetComponent<EnnemyController>().NightEnnemy());
    }

    private IEnumerator UpdateDayNight()
    {
        _transitioning = true;

        float timeElapsed = 0f;
        float totalTime = TransitionTime;

        Color DayColorBG = colorDayBG;
        Color NightColor = colorNight;
        Color DayColorLight = colorDayLight;


        while (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;

            if (!Day)
            {           //Night
                Background.GetComponent<SpriteRenderer>().color = Color.Lerp(DayColorBG, NightColor, timeElapsed / totalTime);
                GlobalLight.GetComponent<Light2D>().color = Color.Lerp(DayColorLight, NightColor, timeElapsed / totalTime);
            }
            else
            {           //Day
                Background.GetComponent<SpriteRenderer>().color = Color.Lerp(NightColor, DayColorBG, timeElapsed / totalTime);
                GlobalLight.GetComponent<Light2D>().color = Color.Lerp(NightColor, DayColorLight, timeElapsed / totalTime);
            }

            yield return null;
        }
        StartCoroutine(TurnOnOffLight());

        _transitioning = false;
    }

    private IEnumerator TurnOnOffLight()
    {
        yield return null;
        foreach (GameObject light in objectslight)
        {
            if (Day) { light.SetActive(false); }
            else { light.SetActive(true); }
        }
    }
}
