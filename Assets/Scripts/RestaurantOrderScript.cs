using UnityEngine;
using System.Collections;

public class RestaurantOrderScript : MonoBehaviour
{
    GameObject text;
    public string orderText = "OrderText";
    GameObject player;
    public GameObject evening_fish;

    InteractiveObject me;
    public static int DISTANCE = 5;

    // Use this for initialization
    void Start()
    {
        me = GetComponent<InteractiveObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        text = GameObject.Find(orderText);
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
            ShowText("ORDERING MACHINE: Closed! \nORDERING MACHINE: Come back in the evening, please.");
        } 
        else
        {
            ShowText("ORDERING MACHINE: Thank you for your order, it will be with you shortly.");
            evening_fish.SetActive(true);
        }
    }

    void ShowText(string s)
    {
        text.GetComponent<TextMesh>().text = s;
    }
}
