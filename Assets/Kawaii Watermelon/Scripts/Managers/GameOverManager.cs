using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header(" Elemenets ")]
    [SerializeField] private GameObject deadLine;
    [SerializeField] private Transform fruitsParent;

    [Header(" Timer ")]
    [SerializeField] private float duationTreshold;
    private float timer;
    private bool timerOn;
    private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
            ManageGameOver();
    }
    private void ManageGameOver()
    {
        if (timerOn)
            ManageTimerOn();
        else
        {
            if (IsFruitAboveLine())
                StartTimer();
        }
    }
    private void ManageTimerOn()
    {
        timer += Time.deltaTime;
        Debug.Log(" Timer: " + timer);

        if (!IsFruitAboveLine())
            StopTimer();

        if (timer >= duationTreshold)
            GameOver();
    }
    private bool IsFruitAboveLine()
    {
        for (int i = 0; i < fruitsParent.childCount; i++)
        {
            Fruit fruit = fruitsParent.GetChild(i).GetComponent<Fruit>();

            if (!fruit.HasCollided())
                continue;

            if (IsFruitAboveLine(fruitsParent.GetChild(i)))
                return true;
        }
        return false;
    }
    private bool IsFruitAboveLine(Transform fruit)
    {
        if (fruit.transform.position.y >= deadLine.transform.position.y)
            return true;

        return false;
    }
    private void StartTimer()
    {
        timer = 0;
        timerOn = true;
    }
    private void StopTimer()
    {
        timerOn = false;
    }
    private void GameOver()
    {
        Debug.LogError("Game Over!");
        isGameOver = true;

        GameManager.instance.SetGameOverState();
    }
}
