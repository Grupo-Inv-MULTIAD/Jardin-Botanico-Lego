using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class movimientoPersonaje : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Camera cam;
    private MyTPCharacter tpc;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        tpc = FindObjectOfType<MyTPCharacter>();
        if(tpc == null)
            Debug.LogError("MyTPCharacter no encontrado en el GameObject");
        else
            Debug.Log("MyTPCharacter encontrado correctamente");

        anim = tpc?.GetFullBodyAnimator();
        if(anim == null)
            Debug.LogError("Animator no asignado desde MyTPCharacter");
        else
            Debug.Log("Animator asignado correctamente");
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(h, 0, v).normalized;

        if (input.magnitude > 0.1f)
        {
            // Movimiento relativo a cámara
            Vector3 camForward = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 camRight = Vector3.Scale(cam.transform.right, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDir = (camForward * v + camRight * h).normalized;

            // Rotación hacia la dirección de movimiento
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

            // Movimiento
            Vector3 move = moveDir * moveSpeed;
            move.y = rb.velocity.y; // mantener gravedad
            rb.velocity = move;

            ApplyAnimation(true);
        }
        else
        {
            // Detener movimiento
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            ApplyAnimation(false);
        }
    }

    void ApplyAnimation(bool walking)
    {

        //if (anim != null)
        //{
            anim.SetBool("walking", walking);
            Debug.Log($"Animator walking set to: {walking}");
        //}else{
            //Debug.LogWarning("Animator no asignado");
        //}
    }
}
