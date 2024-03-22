using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MustacheGameStudioTV.SpawnPoints {

    public abstract class FabricaInimigo : ScriptableObject {


        public abstract InimigoBase CriarInimigo(Vector3 posicao);

    }

}