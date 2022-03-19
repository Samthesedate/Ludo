using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLogic : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 worldPos;
    private GameObject objectholder;
    private bool roll;
    [SerializeField]
    private GameObject Dice;
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
    }

    // Update is called once per frame
    void Update()
    {
        DiceRollAnimation();
    }

    private void DiceRollAnimation()
    {
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (Physics2D.Raycast(new Vector2(worldPos.x, worldPos.y), Vector2.zero, 0) && Input.GetMouseButtonDown(0) && roll)
        {
            roll = false;
            objectholder = Instantiate(Dice, new Vector2(8, 7), Quaternion.identity);
            objectholder.transform.SetParent(transform, false);
            objectholder.GetComponent<Animator>().Play("DiceAnimation");
        }
        if (!roll && objectholder.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("New State 0"))
        {
            Destroy(objectholder);
            roll = true; //logic to change to provide chance only when its players turn
            int choice = Random.Range(1, 7);
            //Debug.Log(choice);
            Choice chosen = (Choice)choice;
            GameObject abc = new GameObject();
            abc.transform.position = new Vector2(8, 7);
            abc.AddComponent<SpriteRenderer>();
            abc.transform.SetParent(transform, false);
            abc.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(chosen.ToString());
        }
    }
}