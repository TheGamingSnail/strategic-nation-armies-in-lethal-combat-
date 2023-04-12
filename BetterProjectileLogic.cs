using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour {

	[SerializeField] private Rigidbody ball;
	[SerializeField] private Transform target;

    private Vector3 initialVelocity;

    private Vector3 acceleration;

	private float h = 25;
    [SerializeField] private float upDisplacement = 20f;
	[SerializeField] private float gravity = -18;


    [SerializeField] private float targetSpeed = 10f;
    [SerializeField] private float projectileSpeed = 10f;
    private Vector3 targetPosition;

    void Start() {
		ball.useGravity = false;
        h = target.position.y + upDisplacement;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Launch ();
		}
        targetPosition = target.position + target.forward * targetSpeed * Time.time;
	}

	void Launch() {
        gravity = Physics.gravity.y;
		ball.useGravity = true;
		ball.velocity = CalculateLaunchData ();
	}
    Vector3 CalculateLaunchData() {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

        float time = CalculateFlightTime(displacementY, displacementXZ.magnitude);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }

    float CalculateFlightTime(float displacementY, float displacementXZ) {
        float time = (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity));
        return time + (displacementXZ / projectileSpeed);
    }


    float CalculateTimeToTarget(Vector3 displacement)
    {
        float a = gravity / 2f;
        float b = targetSpeed;
        float c = displacement.magnitude;

        float discriminant = b * b - 4f * a * c;

        float timeToTarget = (-b + Mathf.Sqrt(discriminant)) / (2f * a);

        return timeToTarget;
    }

    Vector3 CalculateTargetVelocity(Vector3 displacement, float timeToTarget)
    {
        Vector3 targetVelocity = displacement / timeToTarget - 0.5f * gravity * timeToTarget * Vector3.up;
        return targetVelocity;
    }

    Vector3 CalculateInitialVelocity(Vector3 displacement, Vector3 targetVelocity, float timeToTarget)
    {
        Vector3 initialVelocity = (displacement - 0.5f * gravity * timeToTarget * timeToTarget * Vector3.up) / timeToTarget - targetVelocity;
        return initialVelocity;
    }
}
