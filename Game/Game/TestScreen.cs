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
using Humble.Messages;
using Humble.Animations;

class TestScreen : Screen, IMessageObject
{
    ParticleEmitter particleEmitter;
    RandomParticleGenerator particleGen;
    //FmodSongInfo sng = null;
    bool clicked = false;
    bool emit = false;
    bool inputChanged = false;
    bool toResume = false;
    Effect effect;
    StaticSprite sprite;
    AnimatedSprite nyan;

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
        

        sprite = new StaticSprite(Content.Load<Texture2D>("blob"), new Vector2(300, 300));

        sprite.ZIndex = 2;
        particleEmitter.ZIndex = 1;

        SpriteSheet ss = Content.Load<SpriteSheet>("testanimation");
        nyan = new AnimatedSprite(Content.Load<Texture2D>("nyantest"), ss, new Vector2(400, 200));
        nyan.ZIndex = 100;
        AddComponent(nyan);

        Button button = new Button(Content.Load<Texture2D>("diamond"), new Vector2(400, 100),
        delegate()
        {

            this.nyan.SetAnimationState("boost");

        }, Color.Red, Color.Yellow);
        eventManager.RegisterClickable(button);
        button.ZIndex = 100;

        AddComponent(button);
        AddComponent(sprite);
        AddComponent(particleEmitter);

        MessageHandler.Singleton.CreateMessage("MSG_TEST_HUMBLE", true);
        MessageHandler.Singleton.RegisterListener(this, "MSG_TEST_HUMBLE");
        MessageHandler.Singleton.PostDelayedMessage("MSG_TEST_HUMBLE", null, null, 3000);
            
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
        nyan.Position = particleEmitter.Position;

        base.Update(gameTime);
        //emit = false;
        //particleEmitter.Stop();
    }

    public override void Draw()
    {
            
        //effect.
       // effect = Content.Load<Effect>("Effect1");
       // effect.CurrentTechnique = effect.Techniques[0];

        base.Draw();
        /*SpriteBatch.Begin();
        for (int i = 0; i < Components.Count; ++i)
            Components[i].Draw(SpriteBatch);
        SpriteBatch.End();*/
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
        base.UnloadContent();
    }


    public void HandleCallback(string msg, object param1, object param2)
    {
        if (msg == "MSG_TEST_HUMBLE")
        {
            sprite.Visible = !sprite.Visible;
        }
    }
}