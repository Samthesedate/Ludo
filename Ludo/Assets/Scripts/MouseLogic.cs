using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLogic : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 worldPos;
    private GameObject objectholder;
    private bool roll;
    //private bool selectpawn;
    [SerializeField]
    private GameObject Dice;
    public static int? choice;
    public static Vector2 pawnchoice;
    private GameObject abc;
    // Start is called before the first frame update

    enum Choice
    {
        Dice_1 = 1,
        Dice_2 = 2,
        Dice_3 = 3,
        Dice_4 = 4,
        Dice_5 = 5,
        Dice_6 = 6
    }
    void Start()
    {
        roll = true;
        //selectpawn = false;
        choice = null;
        pawnchoice = new Vector2(-1, -1);
    }

    // Update is called once per frame
    void Update()
    {
        if(choice == null)
        {
            Destroy(abc);
            DiceRoll();
        }
        else
        {
            ChoosePiecetoMove();
        }
    }

    public void ChoosePiecetoMove()
    {
        //Debug.Log("Hi");
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Ray ray;
        //ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y), Vector2.zero, 0);
        if(hit && Input.GetMouseButtonDown(0) /*&& !selectpawn*/)
        {
            if(hit.transform.parent.tag == "Blue")
            {
                Debug.Log("I visited Blue as it was clicked");
                //selectpawn = true;
                pawnchoice = new Vector2(int.Parse(hit.transform.tag), (int)PlayerManager.FourPlayers.blue);
            }
            else if(hit.transform.parent.tag == "Green")
            {
                Debug.Log("I visited Green as it was clicked");
                //selectpawn = true;
                pawnchoice = new Vector2(int.Parse(hit.transform.tag), (int)PlayerManager.FourPlayers.green);
            }
            else if(hit.transform.parent.tag == "Yellow")
            {
                Debug.Log("I visited Yellow as it was clicked");
                //selectpawn = true;
                pawnchoice = new Vector2(int.Parse(hit.transform.tag), (int)PlayerManager.FourPlayers.yellow);
            }
            else if(hit.transform.parent.tag == "Red")
            {
                Debug.Log("I visited Red as it was clicked");
                //selectpawn = true;
                pawnchoice = new Vector2(int.Parse(hit.transform.tag), (int)PlayerManager.FourPlayers.red);
            }
        }
    }
    public void DiceRoll()
    {
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y), Vector2.zero, 0);
        if (hit && Input.GetMouseButtonDown(0) && roll)
        {
            if (hit.transform.tag == "DiceThrow")
            {
                roll = false;
                objectholder = Instantiate(Dice, new Vector2(8, 7), Quaternion.identity);
                objectholder.transform.SetParent(transform, false);
                objectholder.GetComponent<Animator>().Play("DiceAnimation");
            }
        }

        if(!roll && objectholder.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("New State 0"))
        {
            Destroy(objectholder);
            roll = true; //logic to change to provide chance only when its players turn
            choice = Random.Range(5, 7);
            //Debug.Log(choice);
            Choice chosen = (Choice)choice;
            abc = new GameObject();
            abc.transform.position = new Vector2(8, 7);
            abc.AddComponent<SpriteRenderer>();
            abc.transform.SetParent(transform, false);
            abc.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(chosen.ToString());
        }
    }
}