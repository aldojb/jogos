/*
 * Adaptacao 2014 Aldo JB
 * 
 */

#region File Description
//-----------------------------------------------------------------------------
// ParticleSettings.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Particle3DSample
{
    /// <summary>
    /// Configuracoes que descrevem todas as opcoes tweakable (puxadas) usadas
    /// para controlar o aparecimento de um sistema de particulas.
    /// </summary>
    public class ParticleSettings
    {
        
        // Nome da textura usada por este sistema de particulas.
        public string TextureName = null;
        
        // O numero maximo de particulas que podem ser apresentados de uma vez.
        public int MaxParticles = 100;
        
        // Quanto tempo vai durar essas particulas.
        public TimeSpan Duration = TimeSpan.FromSeconds(1);
        
        // Se maior que zero, algumas particulas vai durar um tempo mais curto do que outros.
        public float DurationRandomness = 0;

        // Controla a quantidade de particulas são influenciadas pela velocidade do objecto
        // Que os criou. Você pode ver isso em ação com o efeito de explosao,
        // Onde as chamas continuam a se mover na mesma direcao da fonte
        // Projetil. As particulas de trilha projetil, por outro lado, definir esta
        // Valor muito baixo para que eles são menos afetadas pela velocidade do projétil.
        public float EmitterVelocitySensitivity = 1;
        
        // Intervalo de valores que controlam a quantidade de X e velocidade do eixo Z para dar a cada
        // particula. Os valores para as particulas individuais sao escolhidos aleatoriamente a partir de algum lugar
        // entre esses limites.
        public float MinHorizontalVelocity = 0;
        public float MaxHorizontalVelocity = 0;
        
        // Intervalo de valores que controlam a quantidade de velocidade do eixo Y para dar a cada particula.
        // Os valores para particulas individuais sao escolhidos aleatoriamente a partir de algum lugar entre
        // esses limites.
        public float MinVerticalVelocity = 0;
        public float MaxVerticalVelocity = 0;
        
        // Direcao e intensidade do efeito da gravidade. Note-se que isto pode apontar em qualquer
        // Direcao, nao apenas para baixo! O efeito de fogo aponta-o para cima para fazer as chamas
        // Ascensao, e os pontos de fumaça pluma-o de lado para simular vento.
        public Vector3 Gravity = Vector3.Zero;
        
        // Controla como a velocidade da particula vai mudar ao longo da sua vida. Se definido
        // 1, as particulas vao continuar na mesma velocidade como quando eles foram criados.
        // Se definido como 0, as particulas vao chegar a uma paragem completa direito antes de morrer.
        // Valores maiores que 1 fazer as particulas acelerar ao longo do tempo.
        public float EndVelocity = 1;


        // Intervalo de valores que controlam a cor da particula e alfa. Valores para
        // particulas individuais sao escolhidos aleatoriamente a partir de algum lugar entre esses limites.
        public Color MinColor = Color.White;
        public Color MaxColor = Color.White;
        
        // Intervalo de valores que controlam a rapidez com que as particulas girar. valores para
        // Particulas individuais são escolhidos aleatoriamente a partir de algum lugar entre estes
        // Limites. Se ambos os valores sao definidos para 0, o sistema de particulas vai
        // Mudar automaticamente para uma tecnica de sombreamento alternativa que não rotacao 
        // support, e, portanto, requer muito menos energia GPU. Isto significa que se você 
        // nao precisar do efeito de rotacao, voce pode obter um desempenho
        // impulso de deixar esses valores em 0.
        public float MinRotateSpeed = 0;
        public float MaxRotateSpeed = 0;
        
        // Intervalo de valores que controlam como grande as particulas são quando o primeiro criado.
        // Os valores para particulas individuais sao escolhidos aleatoriamente a partir de algum lugar entre
        // esses limites.
        public float MinStartSize = 100;
        public float MaxStartSize = 100;


        // Intervalo de valores que controlam como as particulas se tornam grandes no final do seu
        // vida. Os valores para as particulas individuais são escolhidos aleatoriamente a partir de algum lugar
        // entre esses limites.
        public float MinEndSize = 100;
        public float MaxEndSize = 100;

        // Configuracoes de mistura alpha.
        public BlendState BlendState = BlendState.NonPremultiplied;
    }
}
