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
    [SerializeField]
    private GameObject OutlineSquare;
    [SerializeField]
    private GameObject Home;
    private GameObject Symbols;
    private ArrayList storage = new ArrayList();
    private int count = 0;
    private int internalcount = 0;
    private Vector2 spawner = new Vector2(0,0);
    private float x;
    private float y;
    private int c1;
    private int c2;
    private int temp;

    private float halfheight;
    //private float halfwidth;
    private float bottomlimitpos;
    private float toplimitpos;
    private float topoffset;
    private float bottomoffset;
    // Start is called before the first frame update
    void Start()
    {
        BoardSetup();
        createOutline();
        addHomeBase();
        addSymbols();
        finishLineHighlight();
        safeAreaCalc();
        BoardCenter();
        BoardResize();
    }
    
    private void BoardSetup()
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
            if (internalcount == 1 || internalcount == 7 || internalcount == 13)
            {
                temp = c1;
                c1 = c2;
                c2 = temp;
            }
            if (count <= 27)
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
            else if (count > 27 && count <= 41)
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
        while (count < 6)
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

        GameObject BoardBaserealized = Instantiate(BoardBase, new Vector2(-1, 7), Quaternion.identity);
        BoardBaserealized.GetComponent<SpriteRenderer>().sortingOrder = -1;
        BoardBaserealized.transform.parent = this.transform;
        for (int i = 0; i < storage.Count; i++)
        {
            ((GameObject)storage[i]).transform.parent = this.transform;
            ((GameObject)storage[i]).GetComponent<SpriteRenderer>().sortingOrder = 1;
            ((GameObject)storage[i]).transform.localScale = new Vector2(0.9f, 0.9f);
            AddStartGridColor(i);
        }
        this.transform.parent = GameObject.Find("Main Camera").transform;
    }
    private float Ratio()
    {
        return ((toplimitpos - bottomlimitpos) / 15);
    }

    private void AddStartGridColor(int i)
    {
        if(i==(3-1))
        {
            //Red Color
            ((GameObject)storage[i]).GetComponent<SpriteRenderer>().color = new Color(152 / 255f, 4 / 255f, 50 / 255f, 255 / 255f);
        }
        else if (i == (16 - 1))
        {
            //Yellow color
            ((GameObject)storage[i]).GetComponent<SpriteRenderer>().color = new Color(219 / 255f, 212 / 255f, 66 / 255f, 255 / 255f);
        }
        else if(i==(29-1))
        {
            //Blue color
            ((GameObject)storage[i]).GetComponent<SpriteRenderer>().color = new Color(60 / 255f, 17 / 255f, 203 / 255f, 255 / 255f);
        }
        else if (i == (42 - 1))
        {
            //Green color
            ((GameObject)storage[i]).GetComponent<SpriteRenderer>().color = new Color(0 / 255f, 92 / 255f, 67 / 255f, 255 / 255f);
        }
    }
    private void BoardCenter()
    {
        this.transform.position = new Vector2(1*Ratio(), (-7+bottomoffset-topoffset)*Ratio());
    }

    private void BoardResize()
    {
        this.transform.localScale = new Vector2(Ratio(),Ratio());
    }

    private void safeAreaCalc()
    {
        halfheight = Camera.main.orthographicSize;
        //halfwidth = Camera.main.aspect * halfheight;

        bottomlimitpos = Screen.safeArea.y * halfheight * 2 / Screen.height;
        //Debug.Log(halfheight * 2);
        //Debug.Log(bottomlimitpos);
        toplimitpos = Screen.safeArea.yMax * halfheight * 2 / Screen.height;
        //Debug.Log(toplimitpos);
        topoffset = halfheight * 2 - toplimitpos;
        bottomoffset = bottomlimitpos;
    }

    private void createOutline()
    {
        for(int i=0;i<storage.Count;i++)
        {
            Instantiate(OutlineSquare, ((GameObject)storage[i]).transform.position, Quaternion.identity).transform.parent = this.transform;
        }
    }

    private void addHomeBase()
    {
        GameObject temp = Instantiate(FinalSprintGreen, new Vector2(3.5f, 2.5f), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.transform.localScale = new Vector2(5, 5);
        temp.GetComponent<SpriteRenderer>().color = new Color(0 / 255f, 92 / 255f, 67 / 255f, 255 / 255f);
        addHomes(temp.transform);

        temp = Instantiate(FinalSprintBlue, new Vector2(3.5f, 11.5f), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.transform.localScale = new Vector2(5, 5);
        temp.GetComponent<SpriteRenderer>().color = new Color(60 / 255f, 17 / 255f, 203 / 255f, 255 / 255f);
        addHomes(temp.transform);

        temp = Instantiate(FinalSprintRed, new Vector2(-5.5f, 2.5f), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.transform.localScale = new Vector2(5, 5);
        temp.GetComponent<SpriteRenderer>().color = new Color(152 / 255f, 4 / 255f, 50 / 255f, 255 / 255f);
        addHomes(temp.transform);

        temp = Instantiate(FinalSprintYellow, new Vector2(-5.5f, 11.5f), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.transform.localScale = new Vector2(5, 5);
        temp.GetComponent<SpriteRenderer>().color = new Color(219 / 255f, 212 / 255f, 66 / 255f, 255 / 255f);
        addHomes(temp.transform);
    }

    private void addHomes(Transform transform)
    {
        //Instantiate exisitng prefabs and then relocate them.
        GameObject temp;
        temp = Instantiate(Home, new Vector2(transform.position.x - 1,transform.position.y - 1), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 1;

        temp = Instantiate(Home, new Vector2(transform.position.x + 1, transform.position.y - 1), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 1;

        temp = Instantiate(Home, new Vector2(transform.position.x - 1, transform.position.y + 1), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 1;

        temp = Instantiate(Home, new Vector2(transform.position.x + 1, transform.position.y + 1), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    private void addSymbols()
    {
        //HomeSymbols
        CreateSymbolObject(0, 6, "Symbol1");
        CreateSymbolObject(-2, 6, "Symbol2");
        CreateSymbolObject(0, 8, "Symbol3");
        CreateSymbolObject(-2, 8, "Symbol4");

        //SafeSpots
        CreateSymbolObject(-2, 12, "Symbol1");
        CreateSymbolObject(4, 8, "Symbol2");
        CreateSymbolObject(-6, 6, "Symbol3");
        CreateSymbolObject(0, 2, "Symbol4"); 
    }

    private void CreateSymbolObject(int x, int y, string z)
    {
        Symbols = new GameObject();
        Symbols.transform.position = new Vector2(x, y);
        Symbols.AddComponent<SpriteRenderer>();
        Symbols.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(z);
        Symbols.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Symbols.transform.parent = this.transform;
    }

    private void finishLineHighlight()
    {
        GameObject temp;
        temp = Instantiate(Home, new Vector2(-1,6), Quaternion.identity);
        temp.transform.localScale = new Vector2(0.8f, 0.8f);
        temp.transform.parent = this.transform;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 2;
        temp.GetComponent<SpriteRenderer>().color = new Color(152 / 255f, 4 / 255f, 50 / 255f, 255 / 255f);

        temp = Instantiate(Home, new Vector2(-1, 8), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 2;
        temp.transform.localScale = new Vector2(0.8f, 0.8f);
        temp.GetComponent<SpriteRenderer>().color = new Color(60 / 255f, 17 / 255f, 203 / 255f, 255 / 255f);

        temp = Instantiate(Home, new Vector2(0, 7), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 2;
        temp.transform.localScale = new Vector2(0.8f, 0.8f);
        temp.GetComponent<SpriteRenderer>().color = new Color(0 / 255f, 92 / 255f, 67 / 255f, 255 / 255f);

        temp = Instantiate(Home, new Vector2(-2, 7), Quaternion.identity);
        temp.transform.parent = this.transform;
        temp.GetComponent<SpriteRenderer>().sortingOrder = 2;
        temp.transform.localScale = new Vector2(0.8f, 0.8f);
        temp.GetComponent<SpriteRenderer>().color = new Color(219 / 255f, 212 / 255f, 66 / 255f, 255 / 255f);
    }
    /*private void getPosition()
    {
        for (int i = 0; i < storage.Count; i++)
        {
            Debug.Log(((GameObject)storage[i]).transform.position);
        }
    }*/
}