using System;
using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : UnityEngine.Networking.NetworkBehaviour
{
    private PlatformerCharacter2D m_Character;
    BasePlayer basePlayer;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
        }
        basePlayer = GetComponent<BasePlayer>();
    }

    private void Update()
    {
        if (!m_Character.IsDead)
        {
            // Read the inputs.
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool ability1 = Input.GetButtonDown("Fire1");
            bool ability1Rel = Input.GetButtonUp("Fire1");
            bool ability2 = Input.GetButtonDown("Fire2");
            bool ability2Rel = Input.GetButtonUp("Fire2");
            bool unstun = Input.GetButtonDown("space");
            float a3 = Input.GetAxis("Fire3");

            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Pass all parameters to the character control script.
            m_Character.Move(h, v);
            m_Character.FaceMouse(mousePosition);

            if (unstun)
            {
                m_Character.Unstun();
                m_Character.isEnabled = true;
            }
            // use abilities
            if (ability1)
            {
                Debug.Log("one pressed");
                m_Character.AbilityOnePressed();
            }
            if (ability2)
            {
                m_Character.AbilityTwoPressed();
            }

            if (ability1Rel)
            {
                m_Character.AbilityOneReleased();
            }
        }  
    }
}
