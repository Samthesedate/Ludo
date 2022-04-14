using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private GameObject[] greenPlayers;
    private GameObject[] bluePlayers;
    private GameObject[] yellowPlayers;
    private GameObject[] redPlayers;

    private int[] greencounter;
    private int[] bluecounter;
    private int[] yellowcounter;
    private int[] redcounter;
    private int[][] allcounter;

    public GameObject[][] allPlayers;
    private int playerturn;
    //private PawnPositionManager pawnposition;
    
    public enum FourPlayers
    {
        red = 0,
        yellow = 1,
        blue = 2,
        green = 3
    }

    public enum Phase
    {
        home = 0,
        maingrid = 1,
        finalsprint = 2,
        goal = 3
    }
    private void Awake()
    {
        greenPlayers = new GameObject[4];
        yellowPlayers = new GameObject[4];
        redPlayers = new GameObject[4];
        bluePlayers = new GameObject[4];
        allPlayers = new GameObject[4][];
        //pawnposition = new PawnPositionManager();


        greencounter = new int[4];
        yellowcounter = new int[4];
        redcounter = new int[4];
        bluecounter = new int[4];
        allcounter = new int[4][];
        //finishcount = 0;
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

        allcounter[(int)FourPlayers.red] = redcounter;
        allcounter[(int)FourPlayers.green] = greencounter;
        allcounter[(int)FourPlayers.blue] = bluecounter;
        allcounter[(int)FourPlayers.yellow] = yellowcounter;
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
            if (i <= 3) //Red
            {
                tempo.tag = (i+1).ToString();
                tempo.transform.SetParent(transform.GetChild(1), false);
                redPlayers[i] = tempo;
                tempo.GetComponent<SpriteRenderer>().color = new Color(161 / 255f, 4 / 255f, 50 / 255f, 255 / 255f);
            }
            else if (i > 3 && i <= 7) //Yellow
            {
                tempo.tag = (i + 1 - 4).ToString();
                tempo.transform.SetParent(transform.GetChild(3), false);
                yellowPlayers[i - 4] = tempo;
                tempo.GetComponent<SpriteRenderer>().color = new Color(219 / 255f, 212 / 255f, 82 / 255f, 255 / 255f);
            }
            else if (i > 7 && i <= 11) //Blue
            {
                tempo.tag = (i + 1 - 8).ToString();
                tempo.transform.SetParent(transform.GetChild(0), false);
                bluePlayers[i - 8] = tempo;
                tempo.GetComponent<SpriteRenderer>().color = new Color(40 / 255f, 17 / 255f, 203 / 255f, 255 / 255f);
            }
            else if (i > 11 && i <= 15) //Green
            {
                tempo.tag = (i + 1 - 12).ToString();
                tempo.transform.SetParent(transform.GetChild(2), false); //here 2 shows the position of the color in the AllPawns PreFab
                greenPlayers[i - 12] = tempo;
                tempo.GetComponent<SpriteRenderer>().color = new Color(0 / 255f, 112 / 255f, 67 / 255f, 255 / 255f);
            }
            tempo.GetComponent<SpriteRenderer>().sortingOrder = 2;
            tempo.AddComponent<BoxCollider2D>();
            tempo.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    public void OpenPoint(int? choice, Vector2 pawnchoice)
    {
        int pawnchoice_pawnnumber = (int)pawnchoice.x - 1; //the tags are taken from1 to 4 which we want to re-calibrate to 0 to 3 for arrays
        int pawnchoice_pawncolor = (int)pawnchoice.y; // the colors are saved in enum from 0 to 3
        int whatyougot = (int)choice;
        //Debug.Log(choice + " " + pawnchoice);
        if (playerturn == pawnchoice.y)
        {
            GameObject chosenpawn = allPlayers[pawnchoice_pawncolor][pawnchoice_pawnnumber];
            Debug.Log(chosenpawn.transform.position);
            Debug.Log(LudoGrid.PawnArray[pawnchoice_pawncolor * 4 + pawnchoice_pawnnumber].transform.position);

            float posX = LudoGrid.PawnArray[pawnchoice_pawncolor * 4 + pawnchoice_pawnnumber].transform.position.x;
            float posY = LudoGrid.PawnArray[pawnchoice_pawncolor * 4 + pawnchoice_pawnnumber].transform.position.y;

            if (choice == 6)
            {
                if (chosenpawn.transform.position.x == posX && chosenpawn.transform.position.y == posY)
                {
                    chosenpawn.transform.position = ((GameObject)LudoGrid.storage[2 + 13 * pawnchoice_pawncolor]).transform.position;
                    allcounter[pawnchoice_pawncolor][pawnchoice_pawnnumber] = 0;
                }
                else
                {
                    allcounter[pawnchoice_pawncolor][pawnchoice_pawnnumber] += whatyougot;
                    chosenpawn.transform.position = ((GameObject)LudoGrid.storage[(2 + 13 * pawnchoice_pawncolor + allcounter[pawnchoice_pawncolor][pawnchoice_pawnnumber]) % 52]).transform.position;
                }
                MouseLogic.pawnchoice = new Vector2(-1, -1);
                MouseLogic.choice = null;
            }
            else
            {
                //code coul be improved by adding event listeners in the future to keep a check on the position of all pawns of one color
                if (allPlayers[pawnchoice_pawncolor][0].transform.position.x == LudoGrid.PawnArray[pawnchoice_pawncolor * 4].transform.position.x
                    && allPlayers[pawnchoice_pawncolor][0].transform.position.y == LudoGrid.PawnArray[pawnchoice_pawncolor * 4].transform.position.y
                    && allPlayers[pawnchoice_pawncolor][1].transform.position.x == LudoGrid.PawnArray[pawnchoice_pawncolor * 4 + 1].transform.position.x
                    && allPlayers[pawnchoice_pawncolor][1].transform.position.y == LudoGrid.PawnArray[pawnchoice_pawncolor * 4 + 1].transform.position.y
                    && allPlayers[pawnchoice_pawncolor][2].transform.position.x == LudoGrid.PawnArray[pawnchoice_pawncolor * 4 + 2].transform.position.x
                    && allPlayers[pawnchoice_pawncolor][2].transform.position.y == LudoGrid.PawnArray[pawnchoice_pawncolor * 4 + 2].transform.position.y
                    && allPlayers[pawnchoice_pawncolor][3].transform.position.x == LudoGrid.PawnArray[pawnchoice_pawncolor * 4 + 3].transform.position.x
                    && allPlayers[pawnchoice_pawncolor][3].transform.position.y == LudoGrid.PawnArray[pawnchoice_pawncolor * 4 + 3].transform.position.y)
                {
                    MouseLogic.pawnchoice = new Vector2(-1, -1);
                    MouseLogic.choice = null;
                    playerturn = (playerturn + 1) % 4;
                }
                else
                {
                    if (chosenpawn.transform.position.x != posX || chosenpawn.transform.position.y != posY)
                    {
                        allcounter[pawnchoice_pawncolor][pawnchoice_pawnnumber] += whatyougot;
                        chosenpawn.transform.position = ((GameObject)LudoGrid.storage[(2 + 13 * pawnchoice_pawncolor + allcounter[pawnchoice_pawncolor][pawnchoice_pawnnumber]) % 52]).transform.position;
                        MouseLogic.pawnchoice = new Vector2(-1, -1);
                        MouseLogic.choice = null;
                        playerturn = (playerturn + 1) % 4;
                    }
                }
            }
        }
    }
}