using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

namespace SkillProvider
{
    public abstract class Skill : NetworkBehaviour
    {
        //time In milliseconds:
        private readonly int r_Cooldown;
        public int CooldownTimer { get; private set; }
        private DateTime m_LastUse = new DateTime();

        protected bool m_Active = true;

        [SerializeField]
        protected GameObject m_Button;
        protected Text m_ButtonCooldown;
        public Button button { get; set; }

        public Skill(int i_Cooldown)
        {
            r_Cooldown = i_Cooldown;
        }

        public virtual void Start()
        {
            m_LastUse = DateTime.Now;
            CooldownTimer = r_Cooldown;
            m_ButtonCooldown = m_Button.transform.FindChild("Text Cooldown").GetComponent<Text>();
            button = m_Button.GetComponent<Button>();
        }

        public virtual void Update()
        {
            if (CooldownTimer > 0)
            {
                Debug.Log("time update");
                TimeSpan deltaTime = DateTime.Now - m_LastUse;
                CooldownTimer = r_Cooldown - (int)deltaTime.TotalMilliseconds;
                
                m_ButtonCooldown.text = string.Format("{0}.{1}", CooldownTimer / 1000, (CooldownTimer % 1000)/100);
                if (CooldownTimer <= 0)
                {
                    m_ButtonCooldown.text = "";
                }
            }
        }

        protected abstract void Activate();
        public void UseSkill()
        {
            if (m_Active && CooldownTimer <= 0)
            {
                Activate(); // injection
                StackableSkill stackable = this as StackableSkill;
                if (stackable != null)
                {
                    stackable.DeStack();
                }

                ResetCooldwon();
            }
        }

        private void ResetCooldwon()
        {
            m_LastUse = DateTime.Now;
            CooldownTimer = r_Cooldown;
        }
    }

    public abstract class StackableSkill : Skill
    {
        private readonly int r_MaxStacks;
        public int CurrStacks { get; private set; }

        private Text m_ButtonStacks;

        public StackableSkill(int i_Cooldown, int i_MaxStacks) : base(i_Cooldown)
        {
            r_MaxStacks = i_MaxStacks;   
        }

        public override void Start()
        {
            base.Start();
            CurrStacks = r_MaxStacks;
            m_ButtonStacks = m_Button.transform.FindChild("Text Stacks").GetComponent<Text>();
            m_ButtonStacks.text = CurrStacks.ToString();
        }

        public override void Update()
        {
            base.Update();
        }

        public void DeStack()
        {
            if (CurrStacks != 0)
            {
                CurrStacks--;
                m_ButtonStacks.text = CurrStacks.ToString();
                if (CurrStacks == 0)
                {
                    m_Active = false;
                }
            }
        }
    }
}