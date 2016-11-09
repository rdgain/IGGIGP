using UnityEngine;
using System.Collections;

public class RestaurantOrderScript : MonoBehaviour
{
    public GameObject text;
    public GameObject player;
    public GameObject evening_fish;

    InteractiveObject me;
    public static int DISTANCE = 5;

    // Use this for initialization
    void Start()
    {
        me = GetComponent<InteractiveObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (me.isActive && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueConversation();
        }
        if (Vector3.Distance(transform.position, player.transform.position) > DISTANCE)
        {
            ShowText("");
        }
    }

    void ContinueConversation()
    {
        if (GameManagerScript.moment != GameManagerScript.EVENING)
        {
            ShowText("Closed! Come back in the evening, please.");
        } 
        else
        {
            ShowText("Thank you for your order, it will be with you shortly.");
            evening_fish.SetActive(true);
        }
    }

    void ShowText(string s)
    {
        text.GetComponent<TextMesh>().text = s;
    }
}
