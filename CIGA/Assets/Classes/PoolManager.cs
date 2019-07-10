
    using System.Collections.Generic;
    using UnityEngine;

    public static class PoolManager
    {
        private static Queue<GameObject> hearts = new Queue<GameObject>();
        public static GameObject GetHeart(Vector3 position)
        {
            if (hearts.Count > 0)
            {
                var heart = hearts.Dequeue();
                if (heart == null)
                    return Object.Instantiate(Resources.Load<GameObject>("heart"), position, Quaternion.identity);
                heart.transform.position = position;
                heart.SetActive(true);
                return heart;
            }
            else
            {
                var heart = Object.Instantiate(Resources.Load<GameObject>("heart"), position, Quaternion.identity);
                heart.transform.position = position;
                heart.SetActive(true);
                return heart;
            }
        }

        public static void RecycleHeart(GameObject heart)
        {
            heart.SetActive(false);
            hearts.Enqueue(heart);
        }
    }