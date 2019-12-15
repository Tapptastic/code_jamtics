using System;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerStateWalking : State<SimpleCharacterControl>
{
    public override void Update(SimpleCharacterControl source)
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= source.m_walkScale;
            h *= source.m_walkScale;
        }

        source.m_currentV = Mathf.Lerp(source.m_currentV, v, Time.deltaTime * source.m_interpolation);
        source.m_currentH = Mathf.Lerp(source.m_currentH, h, Time.deltaTime * source.m_interpolation);

        Vector3 direction = camera.forward * source.m_currentV + camera.right * source.m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            source.m_currentDirection =
                Vector3.Slerp(source.m_currentDirection, direction, Time.deltaTime * source.m_interpolation);

            source.transform.rotation = Quaternion.LookRotation(source.m_currentDirection);
            source.transform.position += Time.deltaTime * source.moveSpeed * source.m_currentDirection;

            source.animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        source.JumpingAndLanding();

        if (source.Shrinking()) source.State.Set(new PlayerStateShrinking());
        if (source.Growing()) source.State.Set(new PlayerStateGrowing());

        if (!(Math.Abs(h) < 0) || !(Math.Abs(v) < 0)) return;
        source.State.Pop();
        source.State.Push(new PlayerStateIdle());
    }
}

public class PlayerStateGrowing : State<SimpleCharacterControl>
{
    public override void Update(SimpleCharacterControl source)
    {
        source.ChangeScale(Mathf.Lerp(source.transform.localScale.x, 4, .05f));

        if (Math.Abs(source.transform.localScale.x) >= 3.8f)
        {
            source.State.Set(new PlayerStateIdle());
        }
    }

    public override void Exit(SimpleCharacterControl source)
    {
        source.hasShrunk = false;
    }
}

public class PlayerStateShrinking : State<SimpleCharacterControl>
{
    public override void Update(SimpleCharacterControl source)
    {
        source.ChangeScale(Mathf.Lerp(source.transform.localScale.x, .2f, .05f));

        if (Math.Abs(source.transform.localScale.x) <= .22f)
        {
            source.State.Set(new PlayerStateIdle());
        }
    }

    public override void Exit(SimpleCharacterControl source)
    {
        source.hasShrunk = true;
    }
}

public class PlayerStateIdle : State<SimpleCharacterControl>
{
    public override void Enter(SimpleCharacterControl source)
    {
        base.Enter(source);
        source.animator.SetFloat("MoveSpeed", 0);
    }

    public override void Update(SimpleCharacterControl source)
    {
        if (Input.GetKey(KeyCode.Q) && !source.hasShrunk)
        {
            source.State.Set(new PlayerStateShrinking());
            return;
        }

        if (Input.GetKey(KeyCode.Q) && source.hasShrunk)
        {
            source.State.Set(new PlayerStateGrowing());
            return;
        }

        if (Input.anyKey)
        {
            source.State.Set(new PlayerStateWalking());
        }
    }
}