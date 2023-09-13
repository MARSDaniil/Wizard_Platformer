using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Component {
    public class Move :MonoBehaviour {
        protected Rigidbody2D rb2d {  get; set; }

        public void SetRbVelocity(Vector2 value) => rb2d.velocity = value;
    }
}