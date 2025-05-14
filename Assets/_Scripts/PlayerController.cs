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
    public bool isKicking;
    public bool isAttacking;
    public float timeSinceAttack;
    public int currentAttack = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack = timeSinceAttack + Time.deltaTime;

        Attack();
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

    public void Kick()
    {
        if (Input.GetKey(KeyCode.E) && playerAnim.GetBool("Grounded") && isKicking == false)
        {
            StartCoroutine(WaitForKick());
        }
    }

    private IEnumerator WaitForKick()
    {
        playerAnim.SetBool("Kick", true);
        isKicking = true;

        yield return new WaitForSeconds(0.7f);

        playerAnim.SetBool("Kick", false);
        isKicking = false;
    }

    private void Attack()
    {

        if (Input.GetMouseButtonDown(0) && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f)
        {
            if (!isEquipped)
                return;

            currentAttack = currentAttack + 1;
            isAttacking = true;

            if (currentAttack > 3)
                currentAttack = 1;

            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            playerAnim.SetTrigger("Attack" + currentAttack);

            timeSinceAttack = 0;
        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
    }
}
