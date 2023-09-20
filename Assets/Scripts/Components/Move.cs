using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Component {
    public class Move :MonoBehaviour {
        protected Rigidbody2D rb2d {  get; set; }

        public Vector2 RbVelocity {

            set {
                rb2d.velocity = value;
            }
            get {
                return rb2d.velocity;
            }
        } 
    }
}