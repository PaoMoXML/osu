﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Game.Graphics.Sprites;
using osu.Game.Overlays;
using OpenTK.Graphics;

namespace osu.Game.Graphics.UserInterface
{
    public class OsuTextBox : TextBox
    {
        protected override Color4 BackgroundUnfocused => Color4.Black.Opacity(0.5f);
        protected override Color4 BackgroundFocused => OsuColour.Gray(0.3f).Opacity(0.8f);
        protected override Color4 BackgroundCommit => BorderColour;

        protected override float LeftRightPadding => 10;

        protected override SpriteText CreatePlaceholder() => new OsuSpriteText
        {
            Font = @"Exo2.0-MediumItalic",
            Colour = new Color4(180, 180, 180, 255),
            Margin = new MarginPadding { Left = 2 },
        };

        public OsuTextBox()
        {
            Height = 40;
            TextContainer.Height = 0.5f;
            CornerRadius = 5;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colour)
        {
            BorderColour = colour.Yellow;
        }

        protected override bool OnFocus(InputState state)
        {
            BorderThickness = 3;

            return base.OnFocus(state);
        }

        protected override void OnFocusLost(InputState state)
        {
            BorderThickness = 0;

            base.OnFocusLost(state);
        }

        protected override Drawable GetDrawableCharacter(char c) => new OsuSpriteText { Text = c.ToString(), TextSize = CalculatedTextSize };
    }
}
