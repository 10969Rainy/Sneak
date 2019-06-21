using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour {

    public GameObject model;
    public IUserInput pi;
    public float walkSpeed = 2.0f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 2.0f;
    public float rollVelocity = 1.0f;

    [Header("===== Friction Settings =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private bool lockPlanar = false;
    private CapsuleCollider col;
    //private Vector3 wallPoint;
    public bool isBraced = false;

    void Awake () {

        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled == true)
            {
                pi = input;
                break;
            }
        }
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
	}
	
    //60帧1秒
	void Update () {

        //float targetRunMulti = ((pi.run) ? 2.0f : 1.0f);  //跑步动画的float
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), ((pi.run) ? 2.0f : 1.0f), 0.5f));   //跑步动画的缓动效果

        if (rigid.velocity.magnitude > 5.1f)
        {
            //anim.SetTrigger("roll");
        }

        if (pi.jump)
        {
            anim.SetTrigger("jump");
        }
        
        anim.SetBool("crouch", pi.crouch);

        if (pi.Dmag > 0.1f && !isBraced) //按键才转向
        {
            //Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);   //转向的缓动效果
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);  //转向
        }

        if (lockPlanar == false)
        {
            //计算角色的移动量 
            //TODO 蹲伏，隐蔽，攀爬  缓慢
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? (pi.crouch ? 1.0f : runMultiplier) : 1.0f);
        }

        if (isBraced)
        {
            planarVec = pi.Dmag * model.transform.right * walkSpeed * 0.5f * pi.Dright;
            anim.SetFloat("forward",pi.Dright);
        }
    }

    //50帧1秒
    void FixedUpdate() {

        //移动角色
        //rigid.position += movingVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;

        if (isBraced)
        {
            //model.transform.forward = wallPoint;
            if (pi.braced)
            {
                anim.SetTrigger("braced");
            }
        }
    }

    ///
    ///FSMOnEnter 或者 FSMOnExit 里 SendMessageUpwards 的方法
    ///以及其他脚本里 SendMessageUpwards 的方法
    //FSMOnEnter 或者 FSMOnExit 里
    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0.0f, jumpVelocity, 0.0f);
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnGroundEnter()
    {
        pi.inputEnable = true;
        lockPlanar = false;
        col.material = frictionOne;
    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0.0f, rollVelocity, 0.0f);
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnBracedExit()
    {
        isBraced = false;
    }

    //其他脚本里
    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void IsBraced(float angle)
    {
        if (pi.braced)
        {
            anim.SetTrigger("braced");
            model.transform.rotation = Quaternion.Euler(0, angle, 0);
            isBraced = true;
            //wallPoint = point;
        }
    }

    public void IsNotBraced()
    {
        if (isBraced)
        {
            anim.SetTrigger("braced");
            isBraced = false;
            pi.inputEnable = false;
        }
    }
}
