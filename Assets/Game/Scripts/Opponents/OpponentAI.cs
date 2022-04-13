using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] private int numberOfRays = 17;
    [SerializeField] private float angle = 60;
    [SerializeField] private LayerMask obstacleMask;
    private Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        var deltaPosition = Vector3.zero;
        var hit = false;
        for (int i = 0; i <= numberOfRays; i++)
        {
            
            var rotation = this.transform.rotation;
            //When i is 0 new angle will be -angle 
            //When i is numberOfRays-1 new angle will be angle
            var rotationMod = Quaternion.AngleAxis((i / (float)numberOfRays) * angle * 2 - angle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;

            var ray = new Ray(this.transform.position+ transform.up, direction);
            if(Physics.Raycast(ray, out RaycastHit raycastHit, 1f, obstacleMask))
            {
                hit = true;
                if(raycastHit.normal.x < 0)
                {

                }
                else
                {

                }
                deltaPosition -= (1f / numberOfRays) * 5f * direction;
            }
            else
            {
                deltaPosition += (1f / numberOfRays) * 5f * direction;
            }

        }
        //TODO
        Debug.Log($"{deltaPosition}, {gameObject.name}");
        if (deltaPosition.z >= 0.5) this.transform.position += deltaPosition * Time.deltaTime;
        else HandleObstacleHit();

    }
    public void HandleObstacleHit()
    {
        //Returns player to spawn point which is 0,0,0
        transform.position = oldPos;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i <= numberOfRays; i++)
        {
            var rotation = this.transform.rotation;
            //When i is 0 new angle will be -angle 
            //When i is numberOfRays-1 new angle will be angle
            var rotationMod = Quaternion.AngleAxis((i / (float)numberOfRays) * angle*2 - angle, this.transform.up);
            var direction = rotation * rotationMod * Vector3.forward;

            //Debug.Log($"Angle: {(i / (float)numberOfRays) * 60 * 2 - 60} Direction: {direction}");

            Gizmos.DrawRay(this.transform.position + transform.up, direction);

        }
    }
}
