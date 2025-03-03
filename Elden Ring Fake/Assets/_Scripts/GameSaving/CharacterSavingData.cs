using UnityEngine;

namespace SG {
    [System.Serializable]
    class CharacterSavingData {

        [Header("---------- Character Name ----------")]
        string characterName;

        [Header("---------- Time Played ----------")]
        float secondPlayed;

        [Header("---------- World Coordinates ----------")]
        float xPosition;
        float yPosition;
        float zPosition;

        internal string GetCharacterName() {
            return characterName;
        }

        internal void SetCharacterName(string name) {
            characterName = name;
        }

        internal float GetXPosition() {
            return xPosition;
        }

        internal void SetXPosition(Transform newPosition) {
            xPosition = newPosition.position.x;
        }

        internal float GetYPosition() {
            return yPosition;
        }

        internal void SetYPosition(Transform newPosition) {
            yPosition = newPosition.position.y;
        }

        internal float GetZPosition() {
            return zPosition;
        }

        internal void SetZPosition(Transform newPosition) {
            zPosition = newPosition.position.z;
        }
    }
}