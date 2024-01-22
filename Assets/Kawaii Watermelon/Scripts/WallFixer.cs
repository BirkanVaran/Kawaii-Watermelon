using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFixer : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        FixWalls();
    }

    private void FixWalls()
    {
        mainCamera = Camera.main;

        float aspectRatio = (float)Screen.height / Screen.width;
        //Debug.Log("Aspect Ratio: " + aspectRatio);

        float screenWidth = mainCamera.orthographicSize / aspectRatio;
        //Debug.Log("Screen Width: " + screenWidth);

        rightWall.transform.position = new Vector3(screenWidth + .5f, 0, 0);
        leftWall.transform.position = -rightWall.transform.position;
    }
}
