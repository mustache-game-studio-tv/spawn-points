using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MustacheGameStudioTV.SpawnPoints {

    [CreateAssetMenu(fileName = "Novo Conector Spawn Point", menuName = "Spawn Points/Conectores/Novo conector")]
    public class ConectorSpawnPoint : ScriptableObject {

        private SpawnPoint spawnPoint;

        public SpawnPoint SpawnPoint {
            get {
                return this.spawnPoint;
            }
            set {
                this.spawnPoint = value;
            }
        }
    }

}