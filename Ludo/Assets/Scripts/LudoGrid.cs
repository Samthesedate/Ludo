using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LudoGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject GridLoc;
    [SerializeField]
    private GameObject FinalSprintRed;
    [SerializeField]
    private GameObject FinalSprintBlue;
    [SerializeField]
    private GameObject FinalSprintGreen;
    [SerializeField]
    private GameObject FinalSprintYellow;
    [SerializeField]
    private GameObject BoardBase;
    private ArrayList storage = new ArrayList();
    private int count = 0;
    private int internalcount = 0;
    private Vector2 spawner = new Vector2(0,0);
    private float x;
    private float y;
    private int c1;
    private int c2;
    private int temp;
    // Start is called before the first frame update
    void Start()
    {

        //Logic for creating the overall travel ring
        c1 = 1; c2 = 0;
        x = spawner.x - c1;
        y = spawner.y;
        while (count < 56)
        {
            if (count == 7 || count == 21 || count == 35 || count == 49)
            { }
            else
            {
                storage.Add(Instantiate(GridLoc, new Vector2(x, y), Quaternion.identity));
            }
            internalcount = count % 14;
            if(internalcount == 1 || internalcount == 7 || internalcount == 13)
            {
                temp = c1;
                c1 = c2;
                c2 = temp;
            }
            if(count <= 27)
            {
                y += c2;
            }
            else
            {
                y -= c2;
            }
            if (count <= 13)
            {
                x -= c1;
            }
            else if (count > 13 && count <= 27)
            {
                x += c1;
            }
            else if(count > 27 && count <= 41)
            {
                x += c1;
            }
            else
            {
                x -= c1;
            }
            count += 1;
        }

        //Logic for creating the blue, green, red and yellow final sprint
        count = 0; y = 0;
        while(count< 6)
        {
            storage.Add(Instantiate(FinalSprintRed, new Vector2(-1, ++y), Quaternion.identity));
            count += 1;
        }
        
        //Logic for creating the blue, green, red and yellow final sprint
        count = 0; y = 14;
        while (count < 6)
        {
            storage.Add(Instantiate(FinalSprintBlue, new Vector2(-1, --y), Quaternion.identity));
            count += 1;
        }

        //Logic for creating the blue, green, red and yellow final sprint
        count = 0; x = -1;
        while (count < 6)
        {
            storage.Add(Instantiate(FinalSprintGreen, new Vector2(++x, 7), Quaternion.identity));
            count += 1;
        }

        //Logic for creating the blue, green, red and yellow final sprint
        count = 0; x = -1;
        while (count < 6)
        {
            storage.Add(Instantiate(FinalSprintYellow, new Vector2(--x, 7), Quaternion.identity));
            count += 1;
        }

        GameObject BoardBaserealized = Instantiate(BoardBase, new Vector2(-1,7), Quaternion.identity);
        storage.Add(BoardBaserealized);
        BoardBaserealized.GetComponent<SpriteRenderer>().sortingOrder = -1;
        for(int i = 0; i< storage.Count;i++)
        {
            Debug.Log(storage[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
