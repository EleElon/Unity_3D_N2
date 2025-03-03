using UnityEngine;
using Unity.Netcode;
using System.Collections;
using Unity.Collections;

namespace SG {
    class PlayerNetworkManager : CharacterNetworkManager {

        NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("Character", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        internal NetworkVariable<FixedString64Bytes> GetnSetCharacterName() {
            return characterName;
        }
    }
}
