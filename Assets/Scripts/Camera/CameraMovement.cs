using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
	#region variables
	public Vector3 defaultPosition;
	public Vector3 defaultRotation;
	public float speedConstant = 5;
	public float smoothTime = 0.2f;

	[Header("Zoom settings")]
	public float minCameraSize = 1;
	public float maxCameraSize = 12;

	[Header("Rotation settings")]
	public bool xInverted = false;
	public bool yInverted = false;
	public float rotateSpeed = 3f;

	private Camera mainCamera;
	private Vector3 velocity;
	#endregion


	private void Start ()
	{
		mainCamera = GetComponent<Camera>();
	}

	private void Update ()
	{
		WASDMovement();
		ResetCamera();

		if ( EventSystem.current.IsPointerOverGameObject() )
		{
			return;
		}

		RotateCamera();
		ZoomCamera();

		if ( IsSceneInView() )
		{
			MoveCamera();
		}
	}

	private bool IsSceneInView ()
	{
		Ray centerRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		Ray leftRay = mainCamera.ViewportPointToRay(new Vector3(0, 0.5F, 0));
		Ray rightRay = mainCamera.ViewportPointToRay(new Vector3(1, 0.5F, 0));
		Ray bottomRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0F, 0));
		Ray topRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 1F, 0));

		RaycastHit hit;
		if ( Physics.Raycast(centerRay, out hit) )
		{
			Debug.DrawRay(centerRay.origin, hit.point);
			return true;
		}
		else
		{
			var mouseX = Input.GetAxis("Mouse X");
			var mouseY = Input.GetAxis("Mouse Y");
			if ( Physics.Raycast(leftRay, out hit) && mouseX > 0 ) return true;
			if ( Physics.Raycast(rightRay, out hit) && mouseX < 0 ) return true;
			if ( Physics.Raycast(bottomRay, out hit) && mouseY > 0 ) return true;
			if ( Physics.Raycast(topRay, out hit) && mouseY < 0 ) return true;
			return false;
		}
	}

	private void ResetCamera ()
	{
		if ( Input.GetKeyDown("r") )
		{
			transform.position = defaultPosition;
			transform.localEulerAngles = defaultRotation;
		}
	}

	private void WASDMovement ()
	{
		var deltaTime = Time.deltaTime;
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		if ( Camera.current != null )
		{
			Vector3 newPos = new Vector3(GetValueFromAxisValue(xAxisValue), 0.0f, GetValueFromAxisValue(zAxisValue));
			Camera.current.transform.Translate(newPos);
		}
	}

	private void ZoomCamera ()
	{
		if ( Input.GetAxis("Mouse ScrollWheel") != 0 )
		{
			RaycastHit hit;
			Ray ray = this.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			Vector3 desiredPosition;

			if ( Physics.Raycast(ray, out hit) )
			{
				desiredPosition = hit.point;
			}
			else
			{
				desiredPosition = transform.position;
			}
			float distance = Vector3.Distance(desiredPosition, transform.position);
			Vector3 direction = Vector3.Normalize(desiredPosition - transform.position) * (distance * Input.GetAxis("Mouse ScrollWheel"));
			Vector3 newPosition = transform.position + direction;
			newPosition.y = Mathf.Clamp(newPosition.y, minCameraSize, maxCameraSize);
			if ( newPosition.y == minCameraSize || newPosition.y == maxCameraSize ) return;
			transform.position = newPosition;
		}
	}

	private void MoveCamera ()
	{
		if ( Input.GetMouseButton(0) )
		{
			var deltaTime = Time.deltaTime;
			float xOffset = transform.position.x + Input.GetAxis("Mouse X") * speedConstant / Time.timeScale;
			float yOffset = transform.position.z + Input.GetAxis("Mouse Y") * speedConstant / Time.timeScale;
			Vector3 newOffest = new Vector3(xOffset, transform.position.y, yOffset);
			transform.position = Vector3.SmoothDamp(transform.position, newOffest, ref velocity, smoothTime);
			//transform.position = newOffest;
		}
	}

	private void RotateCamera ()
	{
		if ( Input.GetMouseButton(1) )
		{
			var deltaTime = Time.deltaTime;
			var y = Input.GetAxis("Mouse X");
			var x = Input.GetAxis("Mouse Y");
			var rotateValue = new Vector3(x * (yInverted ? -1 : 1) * rotateSpeed * deltaTime / Time.timeScale, y * (xInverted ? -1 : 1) * rotateSpeed * deltaTime / Time.timeScale, 0);
			transform.eulerAngles = transform.eulerAngles - rotateValue;
		}
	}

	private float GetValueFromAxisValue ( float value )
	{
		return value * Time.deltaTime * speedConstant / Time.timeScale;
	}
}