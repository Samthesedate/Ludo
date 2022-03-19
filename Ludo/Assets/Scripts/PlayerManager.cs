using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tempo;
        transform.SetParent(GameObject.Find("Main Camera").transform, false);
        for(int i=0;i<LudoGrid.PawnArray.Length;i++)
        {
            tempo = Instantiate(Player, new Vector2(LudoGrid.PawnArray[i].transform.position.x, LudoGrid.PawnArray[i].transform.position.y), Quaternion.identity);
            if(i<=3) //Green
            {
                tempo.transform.SetParent(transform.GetChild(2), false);
                tempo.GetComponent<SpriteRenderer>().color = new Color(0 / 255f, 92 / 255f, 67 / 255f, 255 / 255f);
            }
            else if(i>3 && i<=7) //Blue
            {
                tempo.transform.SetParent(transform.GetChild(0), false);
                tempo.GetComponent<SpriteRenderer>().color = new Color(60 / 255f, 17 / 255f, 203 / 255f, 255 / 255f);
            }
            else if (i > 7 && i <= 11) //Red
            {
                tempo.transform.SetParent(transform.GetChild(1), false);
                tempo.GetComponent<SpriteRenderer>().color = new Color(152 / 255f, 4 / 255f, 50 / 255f, 255 / 255f);
            }
            else if (i > 11 && i <= 15) //Yellow
            {
                tempo.transform.SetParent(transform.GetChild(3), false);
                tempo.GetComponent<SpriteRenderer>().color = new Color(219 / 255f, 212 / 255f, 66 / 255f, 255 / 255f);
            }
            tempo.GetComponent<SpriteRenderer>().sortingOrder = 2;
            tempo.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
