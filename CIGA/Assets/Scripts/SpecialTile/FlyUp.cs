using UnityEngine;

    public class FlyUp : BaseTile
    {
        public float force=1500.0f;
        protected override void OnTouchPlayer(GameObject other)
        {
            base.OnTouchPlayer(other);
            print(other.name);
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
    }