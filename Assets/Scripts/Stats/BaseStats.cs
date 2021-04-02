using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 10)] [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpFX = null;

        public event Action onLevelUp;

        int currentLevel;

        private void Start()
        {
            currentLevel = CalculateLevel();
            var experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            var newFX = Instantiate(levelUpFX, transform);
            onLevelUp();
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStatInProgression(stat, characterClass, GetLevelInExp()) + GetAdditiveMods(stat);
        }

        private float GetAdditiveMods(Stat stat)
        {
            float modSum = 0f;

            foreach (var provider in GetComponents<IModifierProvider>())
            {
                foreach (var mod in provider.GetAdditiveModifier(stat))
                {
                    modSum += mod;
                }
            }
            return modSum;
        }

        public int GetLevelInExp()
        {
            if (currentLevel < 1) currentLevel = CalculateLevel();
            return currentLevel;
        }

        private int CalculateLevel()
        {
            if (!GetComponent<Experience>()) return 1;

            float currentXP = GetComponent<Experience>().GetExpPoints();

            int levelLength = progression.GetLevelsLength(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= levelLength; level++)
            {
                float XPToLevelUp = progression.GetStatInProgression(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return levelLength + 1;
        }
    }
}