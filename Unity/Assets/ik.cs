using UnityEngine;
using System.IO.Ports;

public class ik : MonoBehaviour
{
    public Transform base_joint;
    public Transform elbow_joint;
    public Transform Target;
    public Transform elbow_joint_end;
    private float xf;
    private float yf;
    private float zf;
    private float l;
    private float r;
    private float phi;
    private float theta;
    private float l1;
    private float l2;
    public float t;
    private int intAngle1;
    private int intAngle2;
    private string strAngle1;
    private string strAngle2;
    private string relay;

    //SerialPort serial = new SerialPort("COM5", 9600);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //serial.NewLine = "\n";
        //serial.WriteTimeout = 50;
        //serial.Open();
        l1 = Vector3.Distance(base_joint.transform.position, elbow_joint.transform.position);
        l2 = Vector3.Distance(elbow_joint.transform.position, elbow_joint_end.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // 3D

        //xf = Target.position.x;
        //yf = Target.position.y;
        //zf = Target.position.z; 
        //base_joint.localRotation = Quaternion.Euler(0, 0, Mathf.Atan(yf / xf) * Mathf.Rad2Deg);
        //l = Mathf.Sqrt(Mathf.Pow(xf, 2) + Mathf.Pow(yf, 2));
        //h = Mathf.Sqrt(Mathf.Pow(l, 2) + Mathf.Pow(zf, 2));
        //phi = Mathf.Atan(zf / l) * Mathf.Rad2Deg;
        //theta = Mathf.Acos((h / 2) / dist) * Mathf.Rad2Deg;
        //base_joint.localRotation = Quaternion.Euler(0, phi + theta, 0);
        //elbow_joint.localRotation = Quaternion.Euler(0, phi-theta, 0);

        //2D 
        //xf = Target.position.x;
        /*yf = Target.position.y;
        zf = -Target.position.z;
        r = Mathf.Sqrt(Mathf.Pow(yf, 2) + Mathf.Pow(zf, 2));
        phi = Mathf.Atan2(yf , zf) * Mathf.Rad2Deg;
        theta = Mathf.Acos(((r * r) + (l1 * l1) - (l2 * l2)) / (2 * l1 * r)) * Mathf.Rad2Deg;
        base_joint.localRotation = Quaternion.Euler(phi + theta, 0, 0);
        elbow_joint.localRotation = Quaternion.Euler(-2*theta, 0, 0);
        intAngle1 = (int)(phi+theta)-11;
        intAngle2 = (int)(2*theta);
        strAngle1 = intAngle1.ToString();
        strAngle2 = intAngle2.ToString();
        relay = strAngle1 + " " + strAngle2;
        serial.WriteLine(relay);*/


        //3D test
        //3d test
        xf = Target.position.x;
        yf = Target.position.y;
        zf = Target.position.z;
        r = Mathf.Sqrt(Mathf.Pow(xf, 2) + Mathf.Pow(zf, 2));
        l = Mathf.Sqrt(Mathf.Pow(r, 2) + Mathf.Pow(yf, 2));
        phi =  Mathf.Acos(((l*l)+(l1*l1)-(l2*l2))/(2*l1*l)) * Mathf.Rad2Deg;
        theta = Mathf.Atan2(yf, r) * Mathf.Rad2Deg;
        Quaternion a = Quaternion.Euler(0, (180 + Mathf.Atan2(xf, zf) * Mathf.Rad2Deg), 0);
        Quaternion b = Quaternion.Euler(phi + theta, 0, 0);
        Quaternion c = Quaternion.Euler(-2 * phi, 0, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, a, t * Time.deltaTime);
        base_joint.localRotation = Quaternion.Slerp(base_joint.localRotation, b, t * Time.deltaTime);
        elbow_joint.localRotation = Quaternion.Slerp(elbow_joint.localRotation, c, t * Time.deltaTime);

        //base_joint.localRotation = Quaternion.Euler(0, test1, 0);
        //elbow_joint.localRotation = Quaternion.Euler(0, test2, 0);



    }

}
