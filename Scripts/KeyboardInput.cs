using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput {

    //变量
    [Header("===== Keys setting =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA; //left shift 跑步
    public string keyB; //space 跳跃
    public string keyC; //left ctrl 蹲伏
    public string keyD; //e 交互

    public string keyJUp;
    public string keyJDown;
    public string keyJRight;
    public string keyJLeft;

    [Header("===== Mouse settings =====")]
    public bool mouseEnable = true;
    public float mouseSensitivityX = 2.0f;
    public float mouseSensitivityY = 2.0f;

    void Update () {
        if (mouseEnable)
        {
            Jup = Input.GetAxis("Mouse Y") * mouseSensitivityY;
            Jright = Input.GetAxis("Mouse X") * mouseSensitivityX;
        }
        else
        {
            Jup = (Input.GetKey(keyJUp) ? 1.0f : 0.0f) - (Input.GetKey(keyJDown) ? 1.0f : 0.0f);
            Jright = (Input.GetKey(keyJRight) ? 1.0f : 0.0f) - (Input.GetKey(keyJLeft) ? 1.0f : 0.0f);
        }

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0.0f) - (Input.GetKey(keyDown) ? 1.0f : 0.0f);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0.0f) - (Input.GetKey(keyLeft) ? 1.0f : 0.0f);

        if (inputEnable == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag= Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = Input.GetKey(keyA);

        //jump信号触发一次
        bool newJump = Input.GetKey(keyB);
        if (newJump != lastjump && newJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastjump = newJump;

        if (Input.GetKeyDown(keyC))
        {
            if (crouch == false)
            {
                crouch = true;
            }
            else
            {
                crouch = false;
            }
        }

        braced = Input.GetKeyDown(keyD);
    }
}
