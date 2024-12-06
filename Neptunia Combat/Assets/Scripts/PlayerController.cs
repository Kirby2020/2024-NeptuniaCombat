using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerBaseState currentState;
    private IdleState idleState;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputLightAttack = Input.GetButtonDown("Mouse0");
        var inputBlock = Input.GetButton("Mouse1");
        currentState.UpdateState(this);

        Debug.Log("InputLightAttack: " + inputLightAttack);
        Debug.Log("InputBlock: " + inputBlock);

        if (inputLightAttack)
        {
            animator.SetTrigger(AnimatorParameters.LIGHT_ATTACK);
        }

        animator.SetBool(AnimatorParameters.IS_BLOCKING, inputBlock);
    }
}
