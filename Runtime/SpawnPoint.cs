using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MustacheGameStudioTV.SpawnPoints {

    public class SpawnPoint : MonoBehaviour {

        [SerializeField]
        private Color corGizmos = Color.yellow;

        [SerializeField]
        private float raioGizmos = 0.25f;

        [SerializeField]
        private ConectorSpawnPoint conector;


        private void Awake() {
            if (this.conector != null) {
                this.conector.SpawnPoint = this;
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = this.corGizmos;
            Gizmos.DrawSphere(this.transform.position, this.raioGizmos);
        }

        public Vector2 Posicao {
            get {
                return this.transform.position;
            }
        }

    }

}