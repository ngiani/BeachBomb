using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {


	public float thrust;

	Rigidbody _rigidBody;

    // Use this for initialization
    void Start ()
    {
		_rigidBody = GetComponent<Rigidbody>();

		_rigidBody.AddForce (new Vector3(0.5f,0,0.5f) * thrust, ForceMode.Impulse);
	}
	
    private void FixedUpdate()
    {
		_rigidBody.velocity = _rigidBody.velocity.normalized * thrust;

		//Check if user has touched the ball, then exploed it
		RaycastHit hitInfo = new RaycastHit();

		bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity);

		if (Input.GetMouseButton(0) && hit && hitInfo.collider == GetComponent<SphereCollider>() && TimeManager.timeLeft >= 0)
		{
			//decrement balls counter
			GameManager.Instance.ballCount -= 1;

			//play explosion animation
			GameObject effect = GameObject.Find("Explosion");

			Instantiate(effect, this.gameObject.transform.position, Quaternion.identity);

			effect.GetComponentInChildren<Animator>().Play(0);

			//Play sound
			effect.GetComponent<AudioSource>().Play();

			//destroy object
			Destroy(gameObject);
		}
	}

}
