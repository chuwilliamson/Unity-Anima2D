using UnityEngine;
using System.Collections;

public class CharacterMotion : MonoBehaviour
{
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        var move = new Vector3(h, v);

        var dir = (forcemove) ? velocity.normalized : move.normalized;

        velocity = dir * speed;

        transform.position += velocity * Time.deltaTime;

        var dot = Vector3.Dot(dir, Vector3.right);

        if(dot > 0) transform.localRotation = new Quaternion(0, 0, 0, 1);
        else if(dot < 0) transform.localRotation = new Quaternion(0, 180, 0, 1);

        animator.SetFloat("speed", velocity.magnitude / speed);
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(50, 25, Screen.width / 4, Screen.height / 4), "move their"))
        {
            StartCoroutine(MoveTo(target));
        }
    }

    IEnumerator MoveTo(Transform to)
    {
        forcemove = true;
        var startPosition = transform.position;
        var endPosition = to.position;
        var journeyLength = Vector3.Distance(startPosition, endPosition);
        var startTime = Time.time;
        
        Debug.DrawLine(startPosition, endPosition, Color.blue);
        while(Vector3.Distance(startPosition, transform.position) < journeyLength - 5)
        {            
            velocity = (endPosition - startPosition).normalized * speed;
            yield return null;
        }
        forcemove = false;
        yield return null;

    }
    public bool forcemove = false;
    public Vector3 velocity;
    public Transform target;

    [SerializeField]
    float speed = 5f;


    Animator animator;
}
