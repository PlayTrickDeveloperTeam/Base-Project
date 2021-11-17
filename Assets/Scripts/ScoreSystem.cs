    using System;
    using System.Collections;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class ScoreSystem : MonoBehaviour
    {
        private int totalScore;
        public int TotalScore => totalScore;
        
        private int score;
        public int Score => score;
        
        public Action<int> ScoreAdd;

        public static ScoreSystem instance;

        private ComboSystem comboSystem;
        
        [OnValueChanged("Test")]
        [SerializeField] private bool combo;


    #if UNITY_EDITOR
        
        private void Test()
        {
            if (combo)
            {
                comboSystem = gameObject.AddComponent<ComboSystem>();
            }
            else
            {
                var get = GetComponent<ComboSystem>();
                if (get != null)
                {
                    Destroy(this);
                }
            }
        }
        
    #endif
        

        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }else Destroy(gameObject);
        }
        
        private void OnEnable()
        {
            ScoreAdd += AddScore;
        }
        private void OnDisable()
        {
            ScoreAdd -= AddScore;
        }
        private void AddScore(int value)
        {
            score += value;
            if(combo) comboSystem.ComboAdd.Invoke(1);
            //Add text change
        }
    }

    public class ComboSystem : MonoBehaviour
    {
        private int totalCombo;
        public int TotalCombo => totalCombo;

        private float comboTime;
        [SerializeField] private float addingComboTime;
        [SerializeField] private bool perfectCombo;
        [ShowIf("perfectCombo")]
        [SerializeField] private int perfectCount;
        [ShowIf("perfectCombo")]
        [SerializeField] private int perfectTime;

        public Action<int> ComboAdd;
        public Action OverrideStopCombo;
        public static ComboSystem instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }else Destroy(gameObject);
        }
        
        private void OnEnable()
        {
            OverrideStopCombo += () => StopCoroutine("ComboCounter");
        }

        private void OnDisable()
        {
            OverrideStopCombo -= () => StopCoroutine("ComboCounter");
        }

        private void AddCombo(int value)
        {
            if (totalCombo == 0)
            {
                StartCoroutine("ComboCounter");
            }else if (comboTime > 0)
            {
                comboTime += addingComboTime;
            }
            
            totalCombo += value;

            if (perfectCombo)
            {
                if (totalCombo % perfectCount == 0 & totalCombo != 0)
                {
                    comboTime += perfectTime;
                    totalCombo += 2;
                }
            }
        }

        IEnumerator ComboCounter()
        {
            while (comboTime > 0)
            {
                comboTime -= 0.1f;
                yield return new WaitForFixedUpdate();
            }

            comboTime = 0;
            totalCombo = 0;
        }

    }
