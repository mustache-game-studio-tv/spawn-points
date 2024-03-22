using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MustacheGameStudioTV.SpawnPoints {

    [Serializable]
    public class OndaInimigo {

        [SerializeField, HideInInspector]
        private string descricaoOndaInimigo;

        [SerializeField]
        private TipoExecucaoGrupoInimigo tipoExecucaoGrupoInimigo;

        [SerializeField]
        private GrupoInimigo[] gruposInimigo;

        private int indiceGrupoInimigoAtual;
        private GrupoInimigo grupoInimigoAtual;
        private bool finalizada;


        public void Iniciar() {
            this.finalizada = false;
            if (this.tipoExecucaoGrupoInimigo == TipoExecucaoGrupoInimigo.Sequencial) {
                IniciarExecutacaoSequencial();
            } else {
                IniciarExecucaoParalela();
            }
        }

        public void Atualizar() {
            if (this.finalizada) {
                return;
            }

            if (this.tipoExecucaoGrupoInimigo == TipoExecucaoGrupoInimigo.Sequencial) {
                AtualizarExecucaoSequencial();
            } else {
                AtualizarExecucaoParalela();
            }
        }

        public bool Finalizada {
            get {
                return this.finalizada;
            }
        }

        public void AtualizarInspector(int indiceOndaInimigo) {
            int numeroOndaInimigo = (indiceOndaInimigo + 1);
            this.descricaoOndaInimigo = $"{(numeroOndaInimigo < 10 ? "0" : "")}{numeroOndaInimigo} - Onda {this.tipoExecucaoGrupoInimigo} com {this.gruposInimigo.Length} grupo(s)";

            for (int i = 0; i < this.gruposInimigo.Length; i++) {
                GrupoInimigo grupoInimigo = this.gruposInimigo[i];
                grupoInimigo.AtualizarInspector(i);
            }
        }

        private void AvancarParaProximoGrupoInimigo() {
            // Existe um próximo grupo?
            if (this.indiceGrupoInimigoAtual < (this.gruposInimigo.Length - 1)) {
                this.indiceGrupoInimigoAtual++;
                this.grupoInimigoAtual = this.gruposInimigo[this.indiceGrupoInimigoAtual];
                this.grupoInimigoAtual.Iniciar();
            } else {
                this.finalizada = true;
            }
        }

        private void IniciarExecutacaoSequencial() {
            this.indiceGrupoInimigoAtual = 0;
            this.grupoInimigoAtual = this.gruposInimigo[this.indiceGrupoInimigoAtual];
            this.grupoInimigoAtual.Iniciar();
        }

        private void AtualizarExecucaoSequencial() {
            this.grupoInimigoAtual.Atualizar();
            if (this.grupoInimigoAtual.Concluido) {
                AvancarParaProximoGrupoInimigo();
            }
        }

        private void IniciarExecucaoParalela() {
            foreach (GrupoInimigo grupoInimigo in this.gruposInimigo) {
                grupoInimigo.Iniciar();
            }
        }

        private void AtualizarExecucaoParalela() {
            bool todosGruposFinalizados = true;
            foreach (GrupoInimigo grupoInimigo in this.gruposInimigo) {
                grupoInimigo.Atualizar();
                if (!grupoInimigo.Concluido) {
                    todosGruposFinalizados = false;
                }
            }

            if (todosGruposFinalizados) {
                this.finalizada = true;
            }
        }
    }
}