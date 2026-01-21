using UnityEngine;
using System.IO.Ports;

public class ik_1dof : MonoBehaviour
{
    private float phi;
    public float xf;
    public float yf;
    public Transform base_joint;
    public Transform Target;
    private int intAngle;
    private string strAngle;

    SerialPort serial = new SerialPort("COM5", 9600);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serial.NewLine = "\n";
        serial.WriteTimeout = 50;
        serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        xf = Target.position.x;
        yf = Target.position.y;
        phi = Mathf.Atan2(yf,xf) * Mathf.Rad2Deg;
        base_joint.rotation = Quaternion.Euler(0, 0, phi);
        intAngle = (int)phi;
        strAngle = intAngle.ToString();
        serial.WriteLine(strAngle);

    }

    void OnApplicationQuit()
    {
        serial.Close();
    }

    void OnDestroy()
    {
        if (serial != null && serial.IsOpen)
            serial.Close();
    }


}
