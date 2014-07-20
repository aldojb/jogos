/*
 * Adaptacao 2014 Aldo JB
 * 
 */

#region File Description
//-----------------------------------------------------------------------------
// Projectile.cs
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
    /// Esta classe demonstra como combinar varios sistemas de particulas diferentes
    /// para construir um efeito composto mais sofisticado. Ele implementa um foguete
    /// projetil, que arcos para o ceu atraves de um ParticleEmitter para deixar um
    /// fluxo constante de particulas de trilha por tras dele. Depois de um tempo ela explode,
    /// criando uma subita explosao de explosao e fumaca particulas.
    /// </summary>
    class Projectile
    {
        #region Constants

        const float trailParticlesPerSecond = 200;
        const int numExplosionParticles = 30;
        const int numExplosionSmokeParticles = 50;
        const float projectileLifespan = 0;//1.5f;
        const float sidewaysVelocityRange = 60;
        const float verticalVelocityRange = 40;
        const float gravity = 15;

        #endregion

        #region Fields

        ParticleSystem explosionParticles;
        ParticleSystem explosionSmokeParticles;
        ParticleEmitter trailEmitter;

        Vector3 position;
        Vector3 velocity;
        float age;

        static Random random = new Random();

        #endregion


        /// <summary>
        /// Cria um novo projetil.
        /// </summary>
        public Projectile(ParticleSystem explosionParticles,
                          ParticleSystem explosionSmokeParticles,
                          ParticleSystem projectileTrailParticles,
                            Vector3 explosionPosition)
        {
            this.explosionParticles = explosionParticles;
            this.explosionSmokeParticles = explosionSmokeParticles;

            // Iniciar na origem, disparando de forma aleatoria (mas aproximadamente para cima) direcao.
            // posicao = Vector3.Zero;
            position = explosionPosition;
            velocity.X = 0;// (float)(random.NextDouble() - 0.5) * sidewaysVelocityRange;
            velocity.Y = 0;// (float)(random.NextDouble() + 0.5) * verticalVelocityRange;
            velocity.Z = 0;// (float)(random.NextDouble() - 0.5) * sidewaysVelocityRange;

            // Use o ajudante de particulas emissor para a saida nossas particulas trilha.
            trailEmitter = new ParticleEmitter(projectileTrailParticles,
                                               trailParticlesPerSecond, position);
        }


        /// <summary>
        /// Atualiza o projetil.
        /// </summary>
        public bool Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Simples fisica do projetil.
            position += velocity * elapsedTime;
            velocity.Y -= elapsedTime * gravity;
            age += elapsedTime;

            // Atualiza o emissor de particulas, o que ira criar o nosso rastro de particulas.
            trailEmitter.Update(gameTime, position);

            // Se passou tempo suficiente, explodir! Note como passamos nossa velocidade
            // em que o metodo AddParticle: isto permite que a explosao ser influenciado
            // com a velocidade e a direcao do projetil que o criou.
            if (age > projectileLifespan)
            {
                for (int i = 0; i < numExplosionParticles; i++)
                    explosionParticles.AddParticle(position, velocity);

                for (int i = 0; i < numExplosionSmokeParticles; i++)
                    explosionSmokeParticles.AddParticle(position, velocity);

                return false;
            }
                
            return true;
        }
    }
}
