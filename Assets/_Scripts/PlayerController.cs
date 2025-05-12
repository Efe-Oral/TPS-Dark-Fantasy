using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Equip();
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
}
