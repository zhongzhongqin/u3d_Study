using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //get the player transform
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        //set the position follow the player's position;
        var playerV3 = player.position;
        transform.position = new Vector3(playerV3.x, playerV3.y,-10f);
    }
}
