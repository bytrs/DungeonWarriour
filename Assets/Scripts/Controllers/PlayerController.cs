using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] [Range(0f, 1000f)] float m_speed;
    [SerializeField] bool CombatMode;
    private Animator m_animator;
    private Rigidbody m_rigidbody;

    Vector3 m_move_world;

    private float m_animationSpeed;
    private float xRotation = 0f;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        #region Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        m_move_world = new Vector3(horizontal, 0, vertical);

        if (vertical > 0)
            m_animationSpeed = Mathf.Clamp(m_animationSpeed + Time.deltaTime, 0, vertical);
        else
            m_animationSpeed = Mathf.Clamp(m_animationSpeed - Time.deltaTime, 0, vertical);

        m_animator.SetFloat("Speed", m_animationSpeed);
        #endregion Movement

        #region Rotate
        // Mouse X ve Y eksenlerindeki hareketi al
        float mouseX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;

        // Dikey eksende hareketi uygulama, sadece oyuncu yukarý ve aþaðý bakar
        xRotation += mouseX;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Kamera hareketini sýnýrlamak için

        // Yatay eksende oyuncuyu döndür
        //transform.Rotate(Vector3.up * mouseX);

        // Kamerayý dikey eksende döndür (sadece X rotasyonu ile)
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0f, xRotation, 0f), Time.deltaTime);
        #endregion Rotate

        m_animator.SetBool("CombatMode", CombatMode);
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Vector3 playerMove = new Vector3(-transform.forward.x * m_move_world.x, 0, transform.forward.z * m_move_world.z);
        // m_rigidbody.velocity = playerMove * m_speed * Time.fixedDeltaTime;

        m_rigidbody.velocity = transform.forward * vertical * m_speed * Time.fixedDeltaTime;
        m_rigidbody.velocity += transform.right * horizontal * m_speed * Time.fixedDeltaTime;
    }

    public void Damage(float DamageAmount)
    {
        throw new System.NotImplementedException();
    }

    public void DestroySequence()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CombatArea"))
            CombatMode = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CombatArea"))
            CombatMode = false;
    }
}
