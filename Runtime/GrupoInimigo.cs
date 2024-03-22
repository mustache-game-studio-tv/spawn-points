using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MustacheGameStudioTV.SpawnPoints {

    [Serializable]
    public class GrupoInimigo {

        [SerializeField, HideInInspector]
        private string descricaoGrupoInimigo;

        [SerializeField]
        private FabricaInimigo fabricaInimigo;

        [SerializeField]
        private ModoConclusaoGrupoInimigo modoConclusao;

        [SerializeField]
        private int quantidadeTotalInimigos;

        [SerializeField]
        private float tempoEsperaAntesCriarInimigos;

        [SerializeField]
        private float intervaloEntreCriacaoInimigos;

        [SerializeField]
        private ConectorSpawnPoint conectorSpawnPoint;



        private bool criacaoInimigosIniciada;
        private float contagemTempoEsperaAntesCriarInimigo;
        private float contagemTempoIntervaloInimigos;
        private int quantidadeInimigosCriados;
        private bool criacaoInimigosFinalizada;
        private bool concluido;
        private List<InimigoBase> inimigos;


        public void Iniciar() {
            this.criacaoInimigosIniciada = false;
            this.concluido = false;
            this.criacaoInimigosFinalizada = false;
            this.contagemTempoIntervaloInimigos = this.intervaloEntreCriacaoInimigos;
            this.contagemTempoEsperaAntesCriarInimigo = 0;
            this.quantidadeInimigosCriados = 0;
            this.inimigos = new List<InimigoBase>();
        }

        public void Atualizar() {
            if (this.criacaoInimigosFinalizada) {
                return;
            }

            if (this.criacaoInimigosIniciada) {
                AtualizarCriacaoInimigos();
            } else {
                AtualizarEsperaAntesCriarInimigos();
            }
        }

        public bool Concluido {
            get {
                return this.concluido;
            }
        }

        public void RemoverInimigo(InimigoBase inimigo) {
            if (this.inimigos.Contains(inimigo)) {
                this.inimigos.Remove(inimigo);

                if ((this.inimigos.Count == 0) && this.criacaoInimigosFinalizada) {
                    if (this.modoConclusao == ModoConclusaoGrupoInimigo.TodosInimigosDerrotados) {
                        this.concluido = true;
                    }
                }
            }
        }

        public void AtualizarInspector(int indiceGrupoInimigo) {
            int numeroGrupoInimigo = (indiceGrupoInimigo + 1);
            this.descricaoGrupoInimigo = $"{(numeroGrupoInimigo < 10 ? "0" : "")}{numeroGrupoInimigo} - {this.quantidadeTotalInimigos} inimigos - concluído quando {this.modoConclusao}";
        }

        private void AtualizarEsperaAntesCriarInimigos() {
            this.contagemTempoEsperaAntesCriarInimigo += Time.deltaTime;
            if (this.contagemTempoEsperaAntesCriarInimigo >= this.tempoEsperaAntesCriarInimigos) {
                this.criacaoInimigosIniciada = true;
            }
        }

        private void AtualizarCriacaoInimigos() {
            this.contagemTempoIntervaloInimigos += Time.deltaTime;
            if (this.contagemTempoIntervaloInimigos >= this.intervaloEntreCriacaoInimigos) {
                this.contagemTempoIntervaloInimigos = 0;
                // Criar novos inimigo
                CriarInimigo();

                if (this.quantidadeInimigosCriados == this.quantidadeTotalInimigos) {
                    this.criacaoInimigosFinalizada = true;

                    if (this.modoConclusao == ModoConclusaoGrupoInimigo.CriacaoInimigosConcluida) {
                        this.concluido = true;
                    }
                }
            }
        }

        private void CriarInimigo() {
            this.quantidadeInimigosCriados++;
            SpawnPoint spawnPoint = this.conectorSpawnPoint.SpawnPoint;
            InimigoBase novoInimigo = this.fabricaInimigo.CriarInimigo(spawnPoint.Posicao);
            novoInimigo.GrupoInimigo = this;

            this.inimigos.Add(novoInimigo);
        }
    }
}
