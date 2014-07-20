/*
 * Adaptacao 2014 Aldo JB
 * 
 */

#region File Description
//-----------------------------------------------------------------------------
// ParticleEmitter.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion

namespace Particle3DSample
{
    /// <summary>
    /// Helper para objetos que querem deixar particulas por tras deles como eles
    /// Movimento ao redor do mundo. Esta implementacao emissor resolve dois relacionados
    /// Problemas:
    ///
    /// Se um objeto quer criar particulas muito lentamente, menos de uma vez por
    /// Frame, pode ser uma dor para manter o controle de quais atualizações devem criar
    /// Uma nova particula contra o que nao deve.
    ///
    /// Se um objeto esta se movendo rapidamente e esta criando muitas particulas por quadro,
    /// Ele vai olhar feio se essas particulas estão todos amontoados juntos. muito
    /// Melhor se eles podem ser distribuidos ao longo de uma linha entre o local onde o objeto
    /// E agora e onde ele estava no quadro anterior. Isto e particularmente
    /// Importante para deixar para tras trilhas rapido objetos como foguetes em movimento.
    ///
    /// Esta classe emissor mantem o controle de um objeto em movimento, lembrando sua
    /// Posicao anterior para que ele possa calcular a velocidade do objecto. ele
    /// Funciona os locais perfeitos para a criacao de particulas em qualquer frequencia
    /// Especificado, independentemente se este e mais rapido ou mais lento do que o
    /// Taxa de atualização do jogo.
    /// </summary>

    public class ParticleEmitter
    {
        #region Fields

        ParticleSystem particleSystem;
        float timeBetweenParticles;
        Vector3 previousPosition;
        float timeLeftOver;

        #endregion


        /// <summary>
        /// Cria um novo objeto de particulas emissor.
        /// </summary>
        public ParticleEmitter(ParticleSystem particleSystem,
                               float particlesPerSecond, Vector3 initialPosition)
        {
            this.particleSystem = particleSystem;

            timeBetweenParticles = 1.0f / particlesPerSecond;
            
            previousPosition = initialPosition;
        }


        /// <summary>
        /// Atualiza o emissor, a criacao de um numero adequado de particulas
        /// nas posicoes apropriadas.
        /// </summary>
        public void Update(GameTime gameTime, Vector3 newPosition)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");

            // Calcular quanto tempo se passou desde a atualizacao anterior.
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime > 0)
            {
                // Previsao do quao rapido estamos nos movendo.
                Vector3 velocity = (newPosition - previousPosition) / elapsedTime;

                // Se tivessemos qualquer tempo de sobra que nao usar durante a
                // update anterior, acrescentar que para o tempo decorrido atual.
                float timeToSpend = timeLeftOver + elapsedTime;
                
                // Contador para fazer loop para o intervalo de tempo.
                float currentTime = -timeLeftOver;
                                
                // Cria particulas, enquanto temos um intervalo de tempo bastante grande.
                while (timeToSpend > timeBetweenParticles)
                {
                    currentTime += timeBetweenParticles;
                    timeToSpend -= timeBetweenParticles;

                    // Exercite-se na posicao ideal para essa particula. Isto produzira
                    // particulas espacadas uniformemente, independentemente da velocidade de objeto, frequencia
                    // da cricao de particulas, ou taxa de atualização do jogo.
                    float mu = currentTime / elapsedTime;

                    Vector3 position = Vector3.Lerp(previousPosition, newPosition, mu);

                    // Cria a particula.
                    particleSystem.AddParticle(position, velocity);
                }

                // Armazena qualquer momento nos nao usamos, por isso pode ser parte da proxima atualizacao.
                timeLeftOver = timeToSpend;
            }

            previousPosition = newPosition;
        }
    }
}
