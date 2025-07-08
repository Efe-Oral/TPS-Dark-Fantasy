using System.Collections;
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
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int numberOfClicks = 0;
    private float lastClickTime = 0f;
    private float maxComboDelay = 0.9f;

    public int currentAttack = 0;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        AttackCombo();
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
            {
                return;
            }
            currentAttack = currentAttack + 1;
            isAttacking = true;

            if (currentAttack > 3)
            {
                currentAttack = 1;
            }

            if (timeSinceAttack > 1.0f)
            {
                currentAttack = 1;
            }

            playerAnim.SetTrigger("Attack" + currentAttack);
            timeSinceAttack = 0;
        }
    }

    public void AttackCombo()
    {
        timeSinceAttack = timeSinceAttack + Time.deltaTime;
        if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && playerAnim.GetCurrentAnimatorStateInfo(0).IsName("combo2-1"))
        {
            playerAnim.SetBool("combo2-1", false);
            ResetAttack();
        }
        if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && playerAnim.GetCurrentAnimatorStateInfo(0).IsName("combo2-2"))
        {
            playerAnim.SetBool("combo2-2", false);
            numberOfClicks = 0;
            ResetAttack();
        }

        if (Time.time - lastClickTime > maxComboDelay)
        {
            numberOfClicks = 0;
        }

        if (Input.GetKey(KeyCode.L) && playerAnim.GetBool("Grounded") && isEquipped)
        {

            lastClickTime = Time.time;
            numberOfClicks = numberOfClicks + 1;
            isAttacking = true;

            if (numberOfClicks == 1)
            {
                playerAnim.SetBool("combo2-1", true);
            }
            numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 2);

            if (numberOfClicks >= 2 && playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && playerAnim.GetCurrentAnimatorStateInfo(0).IsName("combo2-1"))
            {
                playerAnim.SetBool("combo2-1", false);
                playerAnim.SetBool("combo2-2", true);
            }
        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
    }
}
