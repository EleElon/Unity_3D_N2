using UnityEngine;

namespace SG {
    [System.Serializable]
    class CharacterSavingData {

        [Header("---------- Scene Index ----------")]
        int sceneIndex = 1;

        [Header("---------- Character Name ----------")]
        string characterName = "Character";

        [Header("---------- Time Played ----------")]
        float secondPlayed;

        [Header("---------- World Coordinates ----------")]
        [SerializeField] float xPosition;
        [SerializeField] float yPosition;
        [SerializeField] float zPosition;

        [Header("---------- Character Stat ----------")]
        [SerializeField] int vitality;
        [SerializeField] int currentHealth;
        [SerializeField] int endurance;
        [SerializeField] float currentStamina;

        internal string GetCharacterName() { return characterName; }

        internal void SetCharacterName(string name) { characterName = name; }

        internal float GetXPosition() { return xPosition; }

        internal void SetXPosition(Transform newPosition) { xPosition = newPosition.position.x; }

        internal float GetYPosition() { return yPosition; }

        internal void SetYPosition(Transform newPosition) { yPosition = newPosition.position.y; }

        internal float GetZPosition() { return zPosition; }

        internal void SetZPosition(Transform newPosition) { zPosition = newPosition.position.z; }

        internal int GetSceneIndex() { return sceneIndex; }

        internal void SetSceneIndex(int index) { sceneIndex = index; }

        internal int GetVitality() { return vitality; }

        internal void SetVitality(int value) { vitality = value; }

        internal int GetCurrentHealth() { return currentHealth; }

        internal void SetCurrentHealth(int value) { currentHealth = value; }

        internal int GetEndurance() { return endurance; }

        internal void SetEndurance(int value) { endurance = value; }

        internal float GetCurrentStamina() { return currentStamina; }

        internal void SetCurrentStamina(float value) { currentStamina = value; }
    }
}