using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    public GameObject[] greenPlayers;
    public GameObject[] bluePlayers;
    public GameObject[] yellowPlayers;
    public GameObject[] redPlayers;
    public GameObject[][] allPlayers;
    private int playerturn;
    //private PawnPositionManager pawnposition;
    private int finishcount;
    
    public enum FourPlayers
    {
        red = 0,
        yellow = 1,
        blue = 2,
        green = 3
    }

    private void Awake()
    {
        greenPlayers = new GameObject[4];
        yellowPlayers = new GameObject[4];
        redPlayers = new GameObject[4];
        bluePlayers = new GameObject[4];
        allPlayers = new GameObject[4][];
        //pawnposition = new PawnPositionManager();
        finishcount = 0;
        playerturn = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        SettlePlayers();
        allPlayers[(int)FourPlayers.red] = redPlayers;
        allPlayers[(int)FourPlayers.blue] = bluePlayers;
        allPlayers[(int)FourPlayers.green] = greenPlayers;
        allPlayers[(int)FourPlayers.yellow] = yellowPlayers;
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseLogic.choice != null)
        {
            if (MouseLogic.pawnchoice != new Vector2(-1,-1))
            {
                OpenPoint(MouseLogic.choice, MouseLogic.pawnchoice);
            }
        }
    }

    private void SettlePlayers()
    {
        GameObject tempo;
        transform.SetParent(GameObject.Find("Main Camera").transform, false);
        for (int i = 0; i < LudoGrid.PawnArray.Length; i++)
        {
            tempo = Instantiate(Player, new Vector2(LudoGrid.PawnArray[i].transform.position.x, LudoGrid.PawnArray[i].transform.position.y), Quaternion.identity);
            if (i <= 3) //Green
            {
                tempo.tag = (i+1).ToString();
                tempo.transform.SetParent(transform.GetChild(2), false);
                greenPlayers[i] = tempo;
                tempo.GetComponent<SpriteRenderer>().color = new Color(0 / 255f, 92 / 255f, 67 / 255f, 255 / 255f);
            }
            else if (i > 3 && i <= 7) //Blue
            {
                tempo.tag = (i + 1 - 4).ToString();
                tempo.transform.SetParent(transform.GetChild(0), false);
                bluePlayers[i-4] = tempo;
                tempo.GetComponent<SpriteRenderer>().color = new Color(60 / 255f, 17 / 255f, 203 / 255f, 255 / 255f);
            }
            else if (i > 7 && i <= 11) //Red
            {
                tempo.tag = (i + 1 - 8).ToString();
                tempo.transform.SetParent(transform.GetChild(1), false);
                redPlayers[i-8] = tempo;
                tempo.GetComponent<SpriteRenderer>().color = new Color(152 / 255f, 4 / 255f, 50 / 255f, 255 / 255f);
            }
            else if (i > 11 && i <= 15) //Yellow
            {
                tempo.tag = (i + 1 - 12).ToString();
                tempo.transform.SetParent(transform.GetChild(3), false);
                yellowPlayers[i-12] = tempo;
                tempo.GetComponent<SpriteRenderer>().color = new Color(219 / 255f, 212 / 255f, 66 / 255f, 255 / 255f);
            }
            tempo.GetComponent<SpriteRenderer>().sortingOrder = 2;
            tempo.AddComponent<BoxCollider2D>();
            tempo.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    public void OpenPoint(int? choice, Vector2 pawnchoice)
    {
        //Debug.Log(choice + " " + pawnchoice);
        if (playerturn == pawnchoice.y)
        {
            if (choice == 6)
            {
                Debug.Log(playerturn + " " + pawnchoice);
                allPlayers[(int)pawnchoice.y][(int)pawnchoice.x - 1].transform.position = ((GameObject)LudoGrid.storage[40]).transform.position;
                MouseLogic.pawnchoice = new Vector2(-1, -1);
                MouseLogic.choice = null;
                playerturn = (playerturn + 1) % 4;
            }
        }
    }
}
