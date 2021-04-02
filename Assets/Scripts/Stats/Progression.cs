using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]

    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionCharacterClasses;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        public float GetStatInProgression(Stat baseStat, CharacterClass baseStatClass, int baseStatLevel)
        {
            BuildLookup();

            if (!lookupTable.ContainsKey(baseStatClass))
            {
                Debug.Log("No matching classes found for " + baseStatClass);
                return 0;
            }

            var levels = lookupTable[baseStatClass][baseStat];

            if (levels.Length < baseStatLevel)
            {
                Debug.Log("No matching levels found for " + baseStatClass);
                return 0;
            }

            return levels[baseStatLevel - 1];
        }

        public int GetLevelsLength(Stat baseStat, CharacterClass baseStatClass)
        {
            BuildLookup();
            
            float[] levels = lookupTable[baseStatClass][baseStat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (var progressionCharacterClass in progressionCharacterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach (var progressionStat in progressionCharacterClass.progressionStats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progressionCharacterClass.characterClass] = statLookupTable;

            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public string className; //Labeling in Inspector

            public CharacterClass characterClass;
            public ProgressionStat[] progressionStats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public string statName; //Labeling in Inspector

            public Stat stat;
            public float[] levels;
        }
    }
}