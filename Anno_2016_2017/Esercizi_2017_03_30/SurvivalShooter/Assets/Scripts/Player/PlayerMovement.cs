using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6.0f;
	public LayerMask floorMask;

	private Animator _anim;
	private Rigidbody _rg;
	//private float _camRayLenght = 100.0f;

	private float rotationSpeed = 10f;
	private float xPos = 0.0f;
	private float yPos = 0.0f;

	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_anim = GetComponent<Animator> ();
		_rg = GetComponent<Rigidbody> ();

	}

	void Update()
	{
		xPos += rotationSpeed * Input.GetAxis("Mouse X");
		yPos -= rotationSpeed * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(yPos, xPos, 0.0f);
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Rotate ();
		Animating (h, v);

	}

	private void Move(float h, float v)
	{
		Vector3 movement = new Vector3 (h, 0.0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		Vector3 direction = transform.TransformDirection (movement);
		_rg.MovePosition (transform.position + direction);

	}

	private void Rotate()
	{
		
		/*Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorhit;

		if(Physics.Raycast (camRay, out floorhit, _camRayLenght, floorMask))
		{
			Vector3 playerToMouse = floorhit.point - transform.position;
			//playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			_rg.MoveRotation (newRotation);
		}*/
	}

	private void Animating(float h, float v)
	{
		bool walking = (h != 0 || v != 0);
		_anim.SetBool ("isWalking", walking);
	}
}