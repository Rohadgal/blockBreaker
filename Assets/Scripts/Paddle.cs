using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Configuration parameter
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    [SerializeField] float screenWidthInUnits = 16f;

    // cahced reference

    GameSession TheGameSession;
    Ball TheBall;

    // Start is called before the first frame update
    void Start()
    {
        TheGameSession = FindObjectOfType<GameSession>();
        TheBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if(TheGameSession.IsAutoPlayEnabled())
        {
            return TheBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }

}
