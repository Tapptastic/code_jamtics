using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Serialization;

public class SimpleCharacterControl : MonoBehaviour
{

    [FormerlySerializedAs("m_moveSpeed")] [SerializeField]
    public float moveSpeed = 2;

    [FormerlySerializedAs("m_turnSpeed")] [SerializeField]
    public float turnSpeed = 200;

    [FormerlySerializedAs("m_jumpForce")] [SerializeField]
    public float jumpForce = 4;

    [FormerlySerializedAs("m_animator")] [SerializeField]
    public Animator animator;

    [FormerlySerializedAs("m_rigidBody")] [SerializeField]
    public Rigidbody rigidBody;


    public float m_currentV = 0;
    public float m_currentH = 0;

    public readonly float m_interpolation = 10;
    public readonly float m_walkScale = 0.33f;

    public bool m_wasGrounded;
    public Vector3 m_currentDirection = Vector3.zero;

    public float m_jumpTimeStamp = 0;
    public float m_minJumpInterval = 0.25f;

    public bool m_isGrounded;
    public List<Collider> m_collisions = new List<Collider>();


    public float shrinkTimestamp = 0;
    [SerializeField] public float shrinkInterval = 2f;

    [SerializeField] public bool hasShrunk;

    public StateMachine<SimpleCharacterControl> State;

    void Start()
    {
        State = new StateMachine<SimpleCharacterControl>(this);
        State.Set(new PlayerStateIdle());
    }

    void Update()
    {
        animator.SetBool("Grounded", m_isGrounded);
        m_wasGrounded = m_isGrounded;

        State.Update();
    }


    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        foreach (var t in contactPoints)
        {
            if (Vector3.Dot(t.normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }

                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true;
                break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }

            if (m_collisions.Count == 0)
            {
                m_isGrounded = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }

        if (m_collisions.Count == 0)
        {
            m_isGrounded = false;
        }
    }


    public void ChangeScale(float lerpedValue)
    {
        transform.localScale = new Vector3(lerpedValue, lerpedValue, lerpedValue);
    }

    public bool Shrinking()
    {
        //Check if cooldown has finished
        bool shrinkCooldownOver = (Time.time - shrinkTimestamp) >= shrinkInterval;

        if (shrinkCooldownOver && Input.GetKey(KeyCode.Q))
        {
            return true;
        }

        return false;
    }

    public bool Growing()
    {
        bool shrinkCooldownOver = (Time.time - shrinkTimestamp) >= shrinkInterval;

        if (shrinkCooldownOver && Input.GetKey(KeyCode.Q) && hasShrunk)
        {
            return true;
        }

        return false;
    }

    public void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }
}