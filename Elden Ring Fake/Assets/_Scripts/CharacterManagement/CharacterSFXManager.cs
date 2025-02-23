using UnityEngine;

namespace SG {
    class CharacterSFXManager : MonoBehaviour {

        AudioSource _audioSource;

        protected virtual void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }

        // internal void PlaySFX(AudioClip sfx) {
        //     _audioSource.PlayOneShot(sfx);
        // }

        public void PlayRollSFX() {
            _audioSource.PlayOneShot(WorldSFXManager.Instance.GetRollSFX());
        }
    }
}
