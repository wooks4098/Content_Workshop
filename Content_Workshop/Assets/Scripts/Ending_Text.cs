using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ending_Text : MonoBehaviour
{
    public float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
        gameObject.transform.position += Vector3.up * 120f * Time.deltaTime;
        if (time >= 25f)
            SceneManager.LoadScene("Title");
    }
}
