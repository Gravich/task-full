using UnityEngine;

public class Movement : MonoBehaviour
{

    CharacterController CC;
    public float Speed;

    void Start()
    {
        CC = GetComponent<CharacterController>();
    }
    void Update()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");

        Vector3 f = CC.transform.forward;
        Vector3 r = CC.transform.right;
        f *= V;
        r *= H;
        Vector3 moveData = f+r;
        if(CC.isGrounded)
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

        moveData.y = GetComponent<Rigidbody>().velocity.y;

       


        CC.Move(moveData * Time.deltaTime*Speed);

    }
}