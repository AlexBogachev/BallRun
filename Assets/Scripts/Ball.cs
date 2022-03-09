using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystick;
    bool isJoystickTouched;

    Vector3 initialPosition;

    Rigidbody rigidbody;

    private void Awake()
    {
        joystick.joystickTouched.AddListener(Touched);
        initialPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameManager.Instance.gameRestarted.AddListener(PlaceToInitialPosition);
    }

    private void FixedUpdate()
    {
        if (isJoystickTouched)
        {
            float sign = Mathf.Sign(joystick.Horizontal);
            transform.position += new Vector3(sign * Time.deltaTime * 4.5f, 0.0f, 0.0f);
        }
    }

    private void Touched(bool isTouched)
    {
        isJoystickTouched = isTouched;
    }

    private void PlaceToInitialPosition()
    {
        isJoystickTouched = false;
        transform.position = initialPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.CheckGameOver();
    }
}
