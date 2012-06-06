using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humble.Screens;
using Humble.Components;
using Humble.Components.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Humble;

class TestScreen : Screen
{
    ParticleEmitter particleEmitter;
    RandomParticleGenerator particleGen;
    //FmodSongInfo sng = null;
    bool clicked = false;
    bool emit = false;
    bool inputChanged = false;
    bool toResume = false;
    Effect effect;

    public bool randomColor
    {
        get;
        set;
    }

    public TestScreen(HumbleGame game) : base(game)
    {
    }

    public override void Initialize()
    {
        particleGen = new RandomParticleGenerator();
        particleGen.Textures.Add(Content.Load<Texture2D>("circle"));
        particleGen.Textures.Add(Content.Load<Texture2D>("star"));
        particleGen.Textures.Add(Content.Load<Texture2D>("diamond"));
        particleGen.RandomTint = true;
        particleGen.Lifespan = 20;
        particleGen.ScaleMin = 0f;
        particleGen.ScaleMax = 0.2f;
        particleGen.TintColor = Color.CornflowerBlue;

        particleEmitter = new ParticleEmitter(particleGen, new Vector2(300, 300));
        particleEmitter.GenerationCount = 10;

        StaticSprite tmp = new StaticSprite(Content.Load<Texture2D>("blob"), new Vector2(300, 300));

        tmp.ZIndex = 2;
        particleEmitter.ZIndex = 1;

        AddComponent(tmp);
        AddComponent(particleEmitter);

            
        // FmodSoundManager sm = FmodSoundManager.getInstance();
        // sng = sm.CreateSound("C:\\Users\\krik\\Music\\Jamestown OST\\01 Prologue.mp3");
 

        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        /* if (emit && inputChanged)
        {
            particleEmitter.Start();
            if (toResume)
                sng.Resume();
            else
                sng.Play();
            inputChanged = false;
        }
        else if (inputChanged)
        {
            particleEmitter.Stop();
            if (sng.Chan != null)
            {
                sng.Pause();
                toResume = true;
            }
            inputChanged = false;
        }*/
        particleEmitter.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

        base.Update(gameTime);
        //emit = false;
        //particleEmitter.Stop();
    }

    public override void Draw()
    {
            
        //effect.
       // effect = Content.Load<Effect>("Effect1");
       // effect.CurrentTechnique = effect.Techniques[0];
            
        SpriteBatch.Begin();
        for (int i = 0; i < Components.Count; ++i)
            Components[i].Draw(SpriteBatch);
        SpriteBatch.End();
    }

    public override void HandleInput()
    {
        if (Mouse.GetState().LeftButton == ButtonState.Pressed && clicked == false)
        {
            emit = true;
            clicked = true;
            inputChanged = true;
        }
        else if (Mouse.GetState().LeftButton == ButtonState.Released)
        {
            emit = false;
            clicked = false;
            inputChanged = true;
        }
        base.HandleInput();
    }

    public override void UnloadContent()
    {
        //sng.Close();
        base.UnloadContent();
    }

}