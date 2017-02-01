using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    Direction currentDir;
    Vector2 input;
    bool isMoving = false;
    Vector3 startPos;
    Vector3 endPos;
    float t;

    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;

    public float walkSpeed = 1.0f;

	public Rigidbody2D rb;

    public bool isAllowedToMove = true;

    void Start()
    {
        isAllowedToMove = true;
		rb = GetComponent<Rigidbody2D> ();
    }

	void Update () { 
        if(!isMoving && isAllowedToMove)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;

            if(input != Vector2.zero)
            {

                if(input.x < 0)
                {
                    currentDir = Direction.West;
                }
                if(input.x > 0)
                {
                    currentDir = Direction.East;
                }
                if(input.y < 0)
                {
                    currentDir = Direction.South;
                }
                if (input.y > 0)
                {
                    currentDir = Direction.North;
                }

                switch(currentDir)
                {
                    case Direction.North:
                        gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
                        break;
                    case Direction.East:
                        gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                        break;
                    case Direction.South:
                        gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                        break;
                    case Direction.West:
                        gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                        break;
                }

                StartCoroutine(Move(transform));
            }
		}
	}
		
    public IEnumerator Move(Transform entity) {
        isMoving = true;
        startPos = entity.position;
        t = 0;

        endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);

            //t += Time.deltaTime * walkSpeed;
            //entity.position = Vector3.Lerp(startPos, endPos, t);
		rb.MovePosition(endPos + transform.forward * Time.deltaTime);
      
        isMoving = false;
        yield return 0;
    }

	void onCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Lake1") {
			
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Lake1") {
			Debug.Log ("Triggered");
		}

	}
}

enum Direction
{
    North,
    East,
    South,
    West
}
