using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScubaCat : MonoBehaviour
{

    private Rigidbody rigidbody;
    public float Speed = 50;
    public Material CatMaterial;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        string cSpeedNanem = "_SpeedZ";
        float mSpeed = CatMaterial.GetFloat(cSpeedNanem);
        float ms = (rigidbody.velocity.magnitude * 2);
        if (ms < 0.25f)
            ms = 0.25f;
        float MatSpeed = Mathf.Lerp(mSpeed, Mathf.Min(5, ms), Time.deltaTime * 0.1f);
        CatMaterial.SetFloat(cSpeedNanem, MatSpeed);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(transform.forward * Speed, ForceMode.Acceleration);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -5, 0) * Time.deltaTime * 5f, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 5, 0) * Time.deltaTime * 5f, Space.Self);
        }
        if (Input.GetKey(KeyCode.W) && transform.rotation.x > -30)
        {
            transform.Rotate(new Vector3(-2, 0, 0) * Time.deltaTime * 5f, Space.Self);
        }
        if (Input.GetKey(KeyCode.S) && transform.rotation.x < 30)
        {
            transform.Rotate(new Vector3(2, 0, 0) * Time.deltaTime * 5f, Space.Self);
        }

        transform.rotation.SetAxisAngle(Vector3.right, 0);

        /*if(transform.rotation.z > 1)
        {
            transform.Rotate(new Vector3(0, 0, -0.5f) * Time.deltaTime * 5f, Space.Self);
        } else if(transform.rotation.z < -1)
        {
            transform.Rotate(new Vector3(0, 0, 0.5f) * Time.deltaTime * 5f, Space.Self);
        }*/
    }
}
