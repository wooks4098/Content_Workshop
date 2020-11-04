using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool Player1Check;
    public bool Player2Check;

    private void Start()
    {
        Player1Check = false;
        Player2Check = false;
    }
}
