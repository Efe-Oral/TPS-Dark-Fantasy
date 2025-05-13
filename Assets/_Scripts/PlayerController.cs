using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;

    [SerializeField]
    private GameObject sword;

    [SerializeField]
    private GameObject swordOnShoulder;

    public bool isEquipping;
    public bool isEquipped; //Equipped as in its in the hand
    public bool isBlocking;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Equip();
        Block();
    }

    private void Equip()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerAnim.GetBool("Grounded"))
        {
            isEquipping = true;
            playerAnim.SetTrigger("Equip");
        }
    }

    public void ActiveWeapon()
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
        if (Input.GetKeyDown(KeyCode.Mouse1) && playerAnim.GetBool("Grounded"))
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
}
