using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	[HideInInspector] public Transform leftHandObject;
	[HideInInspector] public Transform rightHandObject;
	[HideInInspector] public bool isLeftHandBusy;
	[HideInInspector] public bool isRightHandBusy;

	[SerializeField] private float _speed;
	[SerializeField] private float _jumpForce;
	[SerializeField] private float _jumpSpeedTolerance;
	[SerializeField] private float _mouseSensivity;
	[SerializeField] private float _groundCheckRadius;
	[SerializeField] private Transform _groundCheck = null;
	[SerializeField] private LayerMask _layerMask;
	[SerializeField] private Text _text = null;
	[SerializeField] private Transform _leftHand = null;
	[SerializeField] private Transform _rightHand = null;
	private bool _isGrounded;
	private bool _isJumping;
	private bool _isActionClicked;
	private float _xInput;
	private float _yInput;
	private float _xMouse;
	private float _yMouse;
	private Rigidbody _rigidbody;

	public void Collect(Transform collectable)
	{
		collectable.rotation = Quaternion.identity;

		if (!isLeftHandBusy)
		{
			leftHandObject = collectable;
			isLeftHandBusy = true;
			return;
		}

		if (!isRightHandBusy)
		{
			rightHandObject = collectable;
			isRightHandBusy = true;
			return;
		}
	}

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		_xMouse = Input.GetAxis("Mouse X");
		_yMouse = Input.GetAxis("Mouse Y");

		transform.Rotate(Vector3.up, _xMouse * _mouseSensivity);
		Camera.main.transform.localRotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x - (_yMouse * _mouseSensivity), 0f, 0f);

		_xInput = Input.GetAxis("Horizontal");
		_yInput = Input.GetAxis("Vertical");

		_isJumping = _isJumping != false || Input.GetButtonDown("Jump");
		_isActionClicked = _isActionClicked != false || Input.GetButtonDown("Action");

		if (leftHandObject != null)
		{
			leftHandObject.position = _leftHand.position;
		}
		if (rightHandObject != null)
		{
			rightHandObject.position = _rightHand.position;
		}
	}

	private void FixedUpdate()
	{
		_isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _layerMask) && Mathf.Abs(_rigidbody.velocity.y) < _jumpSpeedTolerance;

		SetVelocity(new Vector3(_xInput * _speed, _rigidbody.velocity.y, _yInput * _speed));

		if (_isGrounded && _isJumping)
		{
			Jump(Vector3.up * _jumpForce);
			_isJumping = false;
		}

		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, 2.5f))
		{
			IInteractive interactiveObject = hitInfo.transform.GetComponentInParent<IInteractive>();
			if (interactiveObject != null)
			{
				_text.text = interactiveObject.GetMessage();
				_text.enabled = true;
				if (_isActionClicked)
				{
					interactiveObject.Action(this);
					_isActionClicked = false;
				}
			}
			else
			{
				_text.enabled = false;
				_text.text = string.Empty;
			}
		}
		else
		{
			_text.enabled = false;
			_text.text = string.Empty;
		}
	}

	private void SetVelocity(Vector3 direction)
	{
		_rigidbody.velocity = transform.TransformDirection(direction);
	}

	private void Jump(Vector3 force)
	{
		_rigidbody.AddForce(force);
	}
}
