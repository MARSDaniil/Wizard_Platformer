using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMainCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void Update() {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2f,-10f);
    }
}
