using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    int coinCount = 0;

    [SerializeField]
    private GameObject coinCounterTextObject;

    private TMPro.TextMeshProUGUI coinCounterText;

    // Start is called before the first frame update
    void Start()
    {
        coinCounterText = coinCounterTextObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCoin() {
        coinCount++;
        coinCounterText.text = "Score : " + coinCount.ToString();
    }
}
