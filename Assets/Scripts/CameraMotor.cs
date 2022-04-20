using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    //ÂÀÐÈÀÍÒ 1

    private Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;


    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }


        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
// ------------------------------------------------------------------------------------------------------------------------------
// //ÂÀÐÈÀÍÒ 2
//public Transform player;
//public float smoothing;
//public Vector3 offset;

//private void FixedUpdate()
//{
//    if (player != null)
//    {
//        Vector3 newPosition = Vector3.Lerp(transform.position, player.transform.position + offset, smoothing);
//        transform.position = newPosition;
//    }
//}
//-----------------------------------------------------------------------------------------------------------------------------


// [Header("Parameters")]
// [SerializeField] private Transform playerTransform;
//[SerializeField] private string playerTag;
//[SerializeField] private float movingSpeed;

//private void Awake()
//{
// if(this.playerTransform == null)
// {
//    if(this.playerTag == "")
//    {
//     this.playerTag = "Player";
//   }


//  this.playerTransform = GameObject.FindGameObjectWithTag(this.playerTag).transform;
// }

// this.transform.position = new Vector3()
// {
//   x = this.playerTransform.position.x,
//    y = this.playerTransform.position.y,
//    z = this.playerTransform.position.z - 10,
//};
// }

//  private void Start()
// {
//
//}

// private void Update()
//{
//  if (this.playerTransform)
// {
//   Vector3 target = new Vector3()
//   {
// x = this.playerTransform.position.x,
// y = this.playerTransform.position.y,
// z = this.playerTransform.position.z - 10,
//  };

// Vector3 pos = Vector3.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);

//  this.transform.position = pos;
// }
//}

