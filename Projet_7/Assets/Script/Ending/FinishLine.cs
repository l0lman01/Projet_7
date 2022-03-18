using UnityEngine;

public class FinishLine : MonoBehaviour
{

    private GameObject BillBoard;
    public GameObject RealFinishLine;

    private void Start()
    {
        BillBoard = GameObject.FindWithTag("Finish");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<DayNightController>().Day == true)              //JSP pourquoi ça marche mais ça marche alors que la logique voudrait l'inverse
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<DayNightController>().Day = true;

        }
        else
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<DayNightController>().Day = false;
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<DayNightController>()._inChange = true;
        BillBoard.SetActive(false);
        RealFinishLine.SetActive(true);
    }
    
}