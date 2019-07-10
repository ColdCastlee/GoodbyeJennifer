

    using UnityEngine;
    using UnityEngine.Assertions;

    public class DragBulletSkill:AbstractDragSkill
    {
        private float maxforce;
        private GameObject bulletPrefab;
        private float gravityScale;
        private float delayTime;
        private float rate=10.0f;

        public DragBulletSkill( GameObject bulletPrefab, float gravityScale,float rate, float maxforce, float delayTime=0.0f)
        {
            this.maxforce = maxforce;
            this.rate = rate;
            this.bulletPrefab = bulletPrefab;
            this.gravityScale = gravityScale;
            this.delayTime = delayTime;
        }

        public override void OnDrag(Vector3 startPos, Vector3 end)
        {
            var bullet = Object.Instantiate(bulletPrefab, startPos+new Vector3(0,0,10), Quaternion.identity);
            bullet.SetActive(true);
            var rig = bullet.GetComponent<Rigidbody2D>();
            rig.gravityScale = gravityScale;
            Assert.IsTrue(rig);
            var force= new Vector2(end.x - startPos.x, end.y - startPos.y)*rate;

            if (force.magnitude >= maxforce)
                force = force.normalized * maxforce;
            
            
            rig.AddForce(force,ForceMode2D.Impulse);
        }
    }