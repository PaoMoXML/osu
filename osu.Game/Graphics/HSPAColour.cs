// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osuTK.Graphics;

namespace osu.Game.Graphics
{
    public struct HSPAColour
    {
        private const float p_r = 0.299f;
        private const float p_g = 0.587f;
        private const float p_b = 0.114f;

        /// <summary>
        /// The hue.
        /// </summary>
        public float H;

        /// <summary>
        /// The saturation.
        /// </summary>
        public float S;

        /// <summary>
        /// The perceived brightness of this colour.
        /// </summary>
        public float P;

        /// <summary>
        /// The alpha.
        /// </summary>
        public float A;

        public HSPAColour(float h, float s, float p, float a)
        {
            H = h;
            S = s;
            P = p;
            A = a;
        }

        public HSPAColour(Color4 colour)
        {
            H = 0;
            S = 0;
            P = MathF.Sqrt(colour.R * colour.R * p_r + colour.G * colour.G * p_g + colour.B + colour.B * p_b);
            A = colour.A;

            if (colour.R == colour.G && colour.R == colour.B)
                return;

            if (colour.R >= colour.G && colour.R >= colour.B)
            {
                if (colour.B >= colour.G)
                {
                    H = 6f / 6f - 1f / 6f * (colour.B - colour.G) / (colour.R - colour.G);
                    S = 1f - colour.G / colour.R;
                }
                else
                {
                    H = 0f / 6f + 1f / 6f * (colour.G - colour.B) / (colour.R - colour.B);
                    S = 1f - colour.B / colour.R;
                }
            }
            else if (colour.G >= colour.R && colour.G >= colour.B)
            {
                if (colour.R >= colour.B)
                {
                    H = 2f / 6f - 1f / 6f * (colour.R - colour.B) / (colour.G - colour.B);
                    S = 1f - colour.B / colour.G;
                }
                else
                {
                    H = 2f / 6f + 1f / 6f * (colour.B - colour.R) / (colour.G - colour.R);
                    S = 1f - colour.R / colour.G;
                }
            }
            else
            {
                if (colour.G >= colour.R)
                {
                    H = 4f / 6f - 1f / 6f * (colour.G - colour.R) / (colour.B - colour.R);
                    S = 1f - colour.R / colour.B;
                }
                else
                {
                    H = 4f / 6f + 1f / 6f * (colour.R - colour.G) / (colour.B - colour.G);
                    S = 1f - colour.G / colour.B;
                }
            }
        }

        public Color4 ToColor4()
        {
            float minOverMax = 1f - S;

            Color4 result = new Color4 { A = A };

            if (minOverMax > 0f)
            {
                if (H < 1f / 6f)
                {
                    H = 6f * (H - 0f / 6f);
                    float part = 1f + H * (1f / minOverMax - 1f);
                    result.B = P / MathF.Sqrt(p_r / minOverMax / minOverMax + p_g * part * part + p_b);
                    result.R = result.B / minOverMax;
                    result.G = result.B + H * (result.R - result.B);
                }
                else if (H < 2f / 6f)
                {
                    H = 6f * (-H + 2f / 6f);
                    float part = 1f + H * (1f / minOverMax - 1f);
                    result.B = P / MathF.Sqrt(p_g / minOverMax / minOverMax + p_r * part * part + p_b);
                    result.G = result.B / minOverMax;
                    result.R = result.B + H * (result.G - result.B);
                }
                else if (H < 3f / 6f)
                {
                    H = 6f * (H - 2f / 6f);
                    float part = 1f + H * (1f / minOverMax - 1f);
                    result.R = P / MathF.Sqrt(p_g / minOverMax / minOverMax + p_b * part * part + p_r);
                    result.G = result.R / minOverMax;
                    result.B = result.R + H * (result.G - result.R);
                }
                else if (H < 4f / 6f)
                {
                    H = 6f * (-H + 4f / 6f);
                    float part = 1f + H * (1f / minOverMax - 1f);
                    result.R = P / MathF.Sqrt(p_b / minOverMax / minOverMax + p_g * part * part + p_r);
                    result.B = result.R / minOverMax;
                    result.G = result.R + H * (result.B - result.R);
                }
                else if (H < 5f / 6f)
                {
                    H = 6f * (H - 4f / 6f);
                    float part = 1f + H * (1f / minOverMax - 1f);
                    result.G = P / MathF.Sqrt(p_b / minOverMax / minOverMax + p_r * part * part + p_g);
                    result.B = result.G / minOverMax;
                    result.R = result.G + H * (result.B - result.G);
                }
                else
                {
                    H = 6f * (-H + 6f / 6f);
                    float part = 1f + H * (1f / minOverMax - 1f);
                    result.G = P / MathF.Sqrt(p_r / minOverMax / minOverMax + p_b * part * part + p_g);
                    result.R = result.G / minOverMax;
                    result.B = result.G + H * (result.R - result.G);
                }
            }
            else
            {
                if (H < 1f / 6f)
                {
                    H = 6f * (H - 0f / 6f);
                    result.R = MathF.Sqrt(P * P / (p_r + p_g * H * H));
                    result.G = result.R * H;
                    result.B = 0f;
                }
                else if (H < 2f / 6f)
                {
                    H = 6f * (-H + 2f / 6f);
                    result.G = MathF.Sqrt(P * P / (p_g + p_r * H * H));
                    result.R = result.G * H;
                    result.B = 0f;
                }
                else if (H < 3f / 6f)
                {
                    H = 6f * (H - 2f / 6f);
                    result.G = MathF.Sqrt(P * P / (p_g + p_b * H * H));
                    result.B = result.G * H;
                    result.R = 0f;
                }
                else if (H < 4f / 6f)
                {
                    H = 6f * (-H + 4f / 6f);
                    result.B = MathF.Sqrt(P * P / (p_b + p_g * H * H));
                    result.G = result.B * H;
                    result.R = 0f;
                }
                else if (H < 5f / 6f)
                {
                    H = 6f * (H - 4f / 6f);
                    result.B = MathF.Sqrt(P * P / (p_b + p_r * H * H));
                    result.R = result.B * H;
                    result.G = 0f;
                }
                else
                {
                    H = 6f * (-H + 6f / 6f);
                    result.R = MathF.Sqrt(P * P / (p_r + p_b * H * H));
                    result.B = result.R * H;
                    result.G = 0f;
                }
            }

            return result;
        }
    }
}
