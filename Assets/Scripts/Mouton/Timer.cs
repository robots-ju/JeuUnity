using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    double time = 0;

    bool runTimer = true;

    [SerializeField]
    private GameObject timerTextObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer) {
            time += Time.deltaTime;
            timerTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Temps : " + time.ToString("0.00");
        }
    }

    public void stopTimer() {
        runTimer = false;
    }
}
