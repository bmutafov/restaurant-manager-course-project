using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    #region variables
    public Vector3 defaultPosition;
    public Vector3 defaultRotation;

    [Header("Zoom settings")] public float minCameraSize = 1;
    public float maxCameraSize = 12;

    [Header("Move Settings")]
    [Header("X")]
    public float maxAxisX = 25;
    public float minAxisX = 10;
    [Header("Y")] public float maxAxisY = 15;
    public float minAxisY = 10;
    [Space] public float speedConstant = 5;

    [Header("Rotation settings")]
    public bool xInverted = false;
    public bool yInverted = false;
    public float rotateSpeed = 3f;

    private Camera mainCamera;

    private bool isMoveAllowed = true;

    public bool IsMoveAllowed
    {
        get { return isMoveAllowed; }
        set { isMoveAllowed = value; }
    }
#endregion


    private void Start ()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update ()
    {
        WASDMovement();
    }

    void LateUpdate ()
    {
        ResetCamera();
        RotateCamera();

        if ( EventSystem.current.IsPointerOverGameObject() )
            return;

        ZoomCamera();

        if ( !IsForbiddenDirection() )
            MoveCamera();

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
            Camera.current.transform.Translate(new Vector3(xAxisValue * deltaTime * speedConstant, 0.0f, zAxisValue * speedConstant * deltaTime));
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
            transform.position += direction;
        }
    }

    private void MoveCamera ()
    {
        if ( Input.GetMouseButton(0) )
        {
            var deltaTime = Time.deltaTime;
            float xOffset = transform.position.x + Input.GetAxis("Mouse X") * speedConstant * deltaTime;
            float yOffset = transform.position.z + Input.GetAxis("Mouse Y") * speedConstant * deltaTime;
            Vector3 newOffest = new Vector3(xOffset, transform.position.y, yOffset);
            transform.position = newOffest;
        }
    }

    private void RotateCamera ()
    {
        if ( Input.GetMouseButton(1) )
        {
            var deltaTime = Time.deltaTime;
            var y = Input.GetAxis("Mouse X");
            var x = Input.GetAxis("Mouse Y");
            var rotateValue = new Vector3(x * (yInverted ? -1 : 1) * rotateSpeed * deltaTime, y * (xInverted ? -1 : 1) * rotateSpeed * deltaTime, 0);
            transform.eulerAngles = transform.eulerAngles - rotateValue;
        }
    }

    private bool IsForbiddenDirection ()
    {
        if ( !Input.GetMouseButton(0) )
            return true;

        if ( Input.GetAxis("Mouse X") > 0 && transform.position.x > maxAxisX )
            return true;
        if ( Input.GetAxis("Mouse X") < 0 && transform.position.x < minAxisX )
            return true;

        if ( Input.GetAxis("Mouse Y") > 0 && transform.position.z > maxAxisY )
            return true;
        if ( Input.GetAxis("Mouse Y") < 0 && transform.position.z < minAxisY )
            return true;

        return false;
    }

    private bool IsMouseMoving ()
    {
        return Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0;
    }
}