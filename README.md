# Pacman (Unity 2D)

Trabalho individual da disciplina de **Desenvolvimento de Jogos Digitais**.
Professor: Luis Filipe Grael Tinós

**Aluno:** Victor Augusto de Mattos Carbelotti

Recriação do clássico Pacman em 2D, feita no Unity.

## Como abrir o projeto

1. Instale o Unity **6000.3.25f1** (Unity 6.3) pelo Unity Hub.
2. No Unity Hub, clique em **Add** e selecione a pasta do projeto.
3. Abra a cena `Assets/Scenes/Pacman.unity`.
4. Clique em **Play**.

Na primeira abertura o Unity demora um pouco, pois recria a pasta `Library`.

## Como jogar

| Tecla                           | Ação                  |
| ------------------------------- | --------------------- |
| W / Seta para cima              | Mover para cima       |
| S / Seta para baixo             | Mover para baixo      |
| A / Seta para a esquerda        | Mover para a esquerda |
| D / Seta para a direita         | Mover para a direita  |
| Qualquer tecla (após Game Over) | Reiniciar o jogo      |

**Objetivo:** comer todas as pellets do labirinto sem ser pego pelos fantasmas.
Ao comer uma power pellet, os fantasmas ficam assustados e podem ser comidos
por mais pontos (o valor dobra a cada fantasma seguido).

## Recursos implementados

- **Entrada do jogador:** teclado, usando o Input System.
- **Movimentação:** física com `Rigidbody2D` e `MovePosition`, com direção
  "enfileirada" (o Pacman faz a curva assim que o caminho abre).
- **Colisões:** Circle Colliders para Pacman, fantasmas e pellets; `BoxCast`
  para detectar paredes.
- **IA dos fantasmas:** baseada em regras, com máquina de estados
  (Home, Scatter, Chase e Frightened). A decisão de direção acontece nos
  cruzamentos (Nodes):
  - _Chase:_ escolhe o caminho que aproxima do Pacman.
  - _Scatter:_ escolhe direções aleatórias.
  - _Frightened:_ escolhe o caminho que afasta do Pacman.
- **Animações com Animator:** Pacman, animação de morte, fantasmas, estado
  assustado e piscando (azul/branco) antes de voltar ao normal. Os olhos
  seguem a direção do movimento.
- **Pontuação e vidas:** score e vidas (ícones do Pacman) exibidos na tela
  com TextMeshPro e fonte pixel (Press Start 2P).
- **Condições de fim de jogo:** derrota ao perder as 3 vidas (tela de
  GAME OVER) e nova rodada ao comer todas as pellets.
- **Túneis laterais:** passagem entre os lados do mapa.

## Estrutura dos scripts

| Script                                                          | Função                                           |
| --------------------------------------------------------------- | ------------------------------------------------ |
| `GameManager`                                                   | Pontuação, vidas, rodadas, Game Over e interface |
| `Pacman`                                                        | Entrada do jogador, rotação e morte              |
| `Movement`                                                      | Movimentação por física e checagem de paredes    |
| `Ghosts`                                                        | Estado geral do fantasma e colisão com o Pacman  |
| `GhostBehaviour`                                                | Classe base dos estados dos fantasmas            |
| `GhostHome` / `GhostScatter` / `GhostChase` / `GhostFrightened` | Comportamentos de cada estado                    |
| `GhostEyes`                                                     | Troca os olhos conforme a direção                |
| `Node`                                                          | Cruzamentos onde os fantasmas decidem a direção  |
| `Passage`                                                       | Túnel lateral                                    |
| `Pellet` / `PowerPellet`                                        | Itens coletáveis                                 |
