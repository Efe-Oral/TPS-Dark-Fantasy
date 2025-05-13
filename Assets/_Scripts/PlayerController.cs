using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Third Person Controller References
    [SerializeField]
    private Animator playerAnim;


    //Equip-Unequip parameters
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject swordOnShoulder;
    public bool isEquipping;
    public bool isEquipped; //Equipped as in its in the hand
    public bool isBlocking;
    public bool isKicking;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Equip();
        Block();
        Kick();
    }

    private void Equip()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerAnim.GetBool("Grounded"))
        {
            isEquipping = true;
            playerAnim.SetTrigger("Equip");
        }
    }

    public void ActiveWeapon() //This methods hides and shows (and vice verse) the sword as the "Equip" animations plays
    {
        if (!isEquipped)
        {
            sword.SetActive(true);
            swordOnShoulder.SetActive(false);
            isEquipped = !isEquipped;
        }
        else //isEquipped = true
        {
            sword.SetActive(false);
            swordOnShoulder.SetActive(true);
            isEquipped = !isEquipped;
        }
    }

    public void Equipped()
    {
        isEquipping = false;
    }

    private void Block()
    {
        if (Input.GetKey(KeyCode.Mouse1) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("Block", true);
            isBlocking = true;
        }

        else
        {
            playerAnim.SetBool("Block", false);
            isBlocking = false;
        }
    }

    private void Kick()
    {
        if (Input.GetKey(KeyCode.E))
        {
            playerAnim.SetBool("Kick", true);
            isKicking = true;
        }

        else
        {
            playerAnim.SetBool("Kick", false);
            isKicking = false;
        }
    }
}
