using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public bool allowForward = true, allowBack = true, allowRight = true, allowLeft = true;
	public float speed = 1f;
	public bool playAnimation = true;
	public int jumpForce = 150;
	public Camera playerCamera = null;
	public bool inputEnabled = true;
	private Transform playerTransform;
	private bool _onGround = false, _jump = false;
	private Vector3  _offset = Vector3.zero;
	private Collider collider;

	// Use this for initialization
	void Awake () {
		//Physics.gravity = Vector3.down * 0.1f;
		if (playerCamera != null) _offset = playerCamera.transform.position - transform.position;
		collider = gameObject.GetComponentInChildren<Collider>();
	}

	void Start () {

	}

	void Update () {
		if (transform.position.y <= -20) {
			GameLogic.Instance().Message(Constants.STATE["LOSE"]);
		}

		if (Input.GetKey(KeyCode.Space)) {
			_jump = true;
		}

		_onGround = Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y);

		if (playerCamera != null) {
			playerCamera.transform.position = transform.position + _offset;
			playerCamera.transform.LookAt(transform.position);
			//Debug.Log(cam.transform.position.ToString());
		}
	}

	IEnumerator WaitForAnimation(Animation _anim)
	{
		do {
			yield return null;
		} while (_anim.isPlaying);
	}


	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Finish") {
			if ( playAnimation == true ) {
				Animator _anim = gameObject.GetComponentInChildren<Animator>();
				inputEnabled = false;
				transform.rotation = Quaternion.FromToRotation(Vector3.zero, Vector3.up);
				_anim.Play("cube_Rotation");
				GameLogic.Instance().Message(Constants.STATE["WIN"]);
			} else {
				GameLogic.Instance().Message(Constants.STATE["WIN"]);
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (inputEnabled == false) return;

		float sideInput = Input.GetAxis("Horizontal");
		float frontInput = Input.GetAxis("Vertical");
		//Vector3 vectorTurn = Vector3.zero;

		if (frontInput != 0) {
			sideInput = 0;
			//vectorTurn = frontInput * Vector3.left;
		} else if (sideInput != 0) {
			frontInput = 0;
			//vectorTurn = sideInput * Vector3.back;
		}

		if ((!allowForward && frontInput > 0) ||
			(!allowBack && frontInput < 0)) {
			frontInput = 0;
		}
		if ((!allowRight && sideInput > 0) || 
			(!allowLeft && sideInput < 0)) {
			sideInput = 0;
		}
		
		Vector3 moveForce = sideInput * Vector3.right * speed + frontInput * Vector3.forward * speed + Vector3.up * GetComponent<Rigidbody>().velocity.y;
		
		
		GetComponent<Rigidbody>().velocity = moveForce;

		//Debug.Log(_onGround && _jump);
		if (_jump && _onGround) {
			GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
			_jump = false;
			//Debug.Log(Vector3.up * jumpForce);
		}
		//Debug.Log(vectorTurn);
	}
}
