using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MustacheGameStudioTV.SpawnPoints {

    public class InimigoBase : MonoBehaviour {

        private GrupoInimigo grupoInimigo;


        public virtual void Destruir() {
            this.grupoInimigo.RemoverInimigo(this);
        }

        public GrupoInimigo GrupoInimigo {
            set {
                this.grupoInimigo = value;
            }
        }

    }

}