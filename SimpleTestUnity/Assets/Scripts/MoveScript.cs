using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class MoveScript : MonoBehaviour
    {
        public float Speed = 0.02f;
        public Queue<IEnumerator> Queue = new Queue<IEnumerator>();

        private bool isMove = false;
        private Vector3 targetPosition = Vector3.zero;

        void Start()
        {
            targetPosition = transform.position;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {

                var coordsMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var coordsVector = new Vector3(coordsMouse.x, coordsMouse.y, 0);
                Queue.Enqueue(Move(coordsVector));
                if (!isMove)
                {
                    StartCoroutine(CommandsManager());
                }
                
            }

        }

        private IEnumerator CommandsManager()
        {
            while (Queue.Count > 0)
            {
                isMove = true;
                yield return StartCoroutine(Queue.Dequeue());
            }

            isMove = false;
        }


        private IEnumerator Move(Vector3 coordsVector)
        {
            float t = 0;
            while (t < 1f)
            {
                targetPosition = Vector3.MoveTowards(targetPosition, coordsVector, t * Time.deltaTime * Speed);
                transform.position = targetPosition;
                t += 0.015f;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}

