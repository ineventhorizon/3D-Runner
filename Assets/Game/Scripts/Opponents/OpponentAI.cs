using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] private int numberOfRays = 17;
    [SerializeField] private float angle = 60, rayLength = 1f;
    [SerializeField] private LayerMask obstacleMask;
    private Vector3 oldPos;
    private Vector3 moveDirection = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
        StartCoroutine(RandomDirectionRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }
    //Need to add randomness to it's movement
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
            var value = i%2 == 0 ? 2f : 1f;
            
            if (Physics.Raycast(ray, out RaycastHit raycastHit, rayLength*value, obstacleMask))
            {
                hit = true;
                Debug.DrawRay(transform.position + transform.up, direction*rayLength*value, Color.red, 0f);
                if (raycastHit.normal.x < 0)
                {
                    //Lean left
                }
                else
                {
                    //Lean right
                }
                deltaPosition -= (1f / numberOfRays) * 5f * direction;
            }
            else
            {
                Debug.DrawRay(transform.position + transform.up, direction* rayLength*value, Color.green, 0f);
                deltaPosition += (1f / numberOfRays) * 5f * direction;
            }

        }
        //TODO
        Debug.Log($"{deltaPosition}, {gameObject.name}");
        if (deltaPosition.z >= 0.2)
        {

            this.transform.position += deltaPosition*Time.deltaTime;
           //var pos = this.transform.position;
           //pos.x += deltaPosition.x;
           //pos.x = Mathf.Clamp(pos.x, -8, 8);
           //this.transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 20f); ;
        }
        else HandleObstacleHit();

    }
    public void HandleObstacleHit()
    {
        //Returns player to spawn point which is 0,0,0
        transform.position = oldPos;
    }

    //TODO
    private IEnumerator RandomDirectionRoutine()
    {
        while (true)
        {
            transform.position += new Vector3(Random.Range(-1f, 1f), transform.position.y, Random.Range(0f, 1f)).normalized * Time.deltaTime*5f;
            yield return new WaitForSeconds(3f);

        }
    }

    //private void OnDrawGizmos()
    //{
    //    
    //    for (int i = 0; i <= numberOfRays; i++)
    //    {
    //        var rotation = this.transform.rotation;
    //        //When i is 0 new angle will be -angle 
    //        //When i is numberOfRays-1 new angle will be angle
    //        var rotationMod = Quaternion.AngleAxis((i / (float)numberOfRays) * angle*2 - angle, this.transform.up);
    //        var direction = rotation * rotationMod * Vector3.forward;
    //
    //        //Debug.Log($"Angle: {(i / (float)numberOfRays) * 60 * 2 - 60} Direction: {direction}");
    //       //if ((bool)hitArray[i])
    //       //{
    //       //    Gizmos.color = Color.red;
    //       //}
    //       //else
    //       //{
    //       //    Gizmos.color = Color.white;
    //       //}
    //        Gizmos.DrawRay(this.transform.position + transform.up, direction);
    //
    //    }
    //}
}
