using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{

    public float panSpeed = 30f;
    public float padding = 10f;

    public float scrollSpeed = 15f;

    public float minHeight = 20f;
    public float maxHeight = 50f;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Scene_Manager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - padding)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        else if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= padding)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        else if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - padding)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        else if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= padding)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 100 *  scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minHeight, maxHeight);

        transform.position = pos;
    }
}
