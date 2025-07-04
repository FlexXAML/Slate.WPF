﻿using System.Windows.Media.Animation;

namespace Slate.WPF.Markup
{
    public static partial class MotionExtensions
    {
        public static T MotionFade<T>(this T control, State<bool> state, double durationValue = 300) where T : FrameworkElement
        {
            state.OnBeforeChangeAsync (async (oldVal, newVal) =>
            {
                if (oldVal == true && newVal == false)
                {
                    await AnimateFadeOutAsync (control,durationValue);
                }
                return true;
            });
            state.OnAfterChangeAsync (async (oldVal, newVal) =>
            {
                if (oldVal == false && newVal == true)
                {
                    await AnimateFadeInAsync (control,durationValue);
                }
            });
            return control;
        }

        private static Task AnimateFadeOutAsync(FrameworkElement control, double durationValue = 300)
        {
            // 애니메이션 구현
            // 예시: Opacity 1 -> 0
            var tcs = new TaskCompletionSource<object?> ();

            var animation = new DoubleAnimation (1, 0, TimeSpan.FromMilliseconds (durationValue));
            animation.Completed += (s, e) => tcs.SetResult (null);

            control.BeginAnimation (UIElement.OpacityProperty, animation);
            return tcs.Task;
        }

        private static Task AnimateFadeInAsync(FrameworkElement control, double durationValue = 300)
        {
            var tcs = new TaskCompletionSource<object?> ();

            var animation = new DoubleAnimation (0, 1, TimeSpan.FromMilliseconds (durationValue));
            animation.Completed += (s, e) => tcs.SetResult (null);

            control.BeginAnimation (UIElement.OpacityProperty, animation);
            return tcs.Task;
        }
    }
    public static partial class MotionExtensions
    {
        public static T MotionSlide<T>(this T control, State<bool> state, double durationValue = 300, double? width = null) where T : FrameworkElement
        {
            state.OnBeforeChangeAsync (async (oldVal, newVal) =>
            {
                if (oldVal == true && newVal == false)
                    await AnimateSlideOutAsync (control, durationValue, width);
                return true;
            });
            state.OnAfterChangeAsync (async (oldVal, newVal) =>
            {
                if (oldVal == false && newVal == true)
                    await AnimateSlideInAsync (control, durationValue, width);

            });
            return control;
        }

        private static Task AnimateSlideOutAsync(FrameworkElement control, double durationValue = 300, double? width = null)
        {
            var tcs = new TaskCompletionSource<object?> ();

            double fromValue = -control.ActualWidth;
            double toValue = 0.0;
            if (control.Parent is Panel pnl)
            {
                fromValue = width.Value;
            }

            var animation = new DoubleAnimation (fromValue, toValue, TimeSpan.FromMilliseconds (durationValue))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            animation.Completed += (s, e) => tcs.SetResult (null);

            if (control.Parent is Panel pnl1)
                control.BeginAnimation (FrameworkElement.WidthProperty, animation);
            else
            {
                control.RenderTransform = new TranslateTransform ();
                control.RenderTransform.BeginAnimation (TranslateTransform.XProperty, animation);
            }
            return tcs.Task;
        }

        private static Task AnimateSlideInAsync(FrameworkElement control, double durationValue = 300, double? width = null)
        {
            var tcs = new TaskCompletionSource<object?> ();

            double fromValue = -control.ActualWidth;
            double toValue = 0.0;
            if (control.Parent is Panel pnl)
            {
                fromValue = 0.0;
                toValue = width.Value;
            }

            var animation = new DoubleAnimation (fromValue, toValue, TimeSpan.FromMilliseconds (durationValue))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            animation.Completed += (s, e) => tcs.SetResult (null);

            if (control.Parent is Panel pnl1)
                control.BeginAnimation (FrameworkElement.WidthProperty, animation);
            else
            {
                control.RenderTransform = new TranslateTransform ();
                control.RenderTransform.BeginAnimation (TranslateTransform.XProperty, animation);
            }

            return tcs.Task;
        }
    }

    public static partial class MotionExtensions
    {
        public static T WithAnimation<T>(this T brush, Color value, double durationValue = 300) where T : Brush
        {
            var mutableBrush = brush.IsFrozen ? brush.Clone () : brush;

            ColorAnimation animation = new ColorAnimation ();
            animation.To = value;
            animation.Duration = new Duration (TimeSpan.FromMilliseconds (durationValue));

            mutableBrush.BeginAnimation (SolidColorBrush.ColorProperty, animation);

            return brush;
        }

        public static T WithAnimation<T>(this T brush, string value, double durationValue = 300) where T : Brush
        {
            var mutableBrush = brush.IsFrozen ? brush.Clone () : brush;

            ColorAnimation animation = new ColorAnimation ();
            animation.To = ColorTool.Get (value);
            animation.Duration = new Duration (TimeSpan.FromMilliseconds (durationValue));

            mutableBrush.BeginAnimation (SolidColorBrush.ColorProperty, animation);

            return brush;
        }

        public static T WidthAnimation<T>(this T element, double value, double durationValue = 300) where T : FrameworkElement
        {
            DoubleAnimation animation = new DoubleAnimation ();
            animation.To = value;
            animation.Duration = new Duration (TimeSpan.FromMilliseconds (durationValue));

            element.BeginAnimation (FrameworkElement.WidthProperty, animation);

            return element;
        }

        public static T HeightAnimation<T>(this T element, double value, double durationValue = 300) where T : FrameworkElement
        {
            DoubleAnimation animation = new DoubleAnimation ();
            animation.To = value;
            animation.Duration = new Duration (TimeSpan.FromMilliseconds (durationValue));

            element.BeginAnimation (FrameworkElement.HeightProperty, animation);

            return element;
        }
        public static T OpacityAnimation<T>(this T element, double value, double durationValue = 300) where T : FrameworkElement
        {
            DoubleAnimation animation = new DoubleAnimation ();
            animation.To = value;
            animation.Duration = new Duration (TimeSpan.FromMilliseconds (durationValue));

            element.BeginAnimation (FrameworkElement.OpacityProperty, animation);

            return element;
        }

        public static T MarginAnimation<T>(this T element, Thickness thickness, double durationValue = 300) where T : FrameworkElement
        {
            ThicknessAnimation animation = new ThicknessAnimation ();
            animation.To = thickness;
            animation.Duration = new Duration (TimeSpan.FromMilliseconds (durationValue));

            element.BeginAnimation (FrameworkElement.MarginProperty, animation);

            return element;
        }

        public static T MarginAnimation<T>(this T element, double leftright = 0.0, double topbottom = 0.0, double durationValue = 300) where T : FrameworkElement
            => element.MarginAnimation (new Thickness (leftright, topbottom, leftright, topbottom), durationValue);

        public static T MarginAnimation<T>(this T element, double left = 0.0, double top = 0.0, double right = 0.0, double bottom = 0.0, double durationValue = 300) where T : FrameworkElement
            => element.MarginAnimation (new Thickness (left, top, right, bottom), durationValue);

        public static T MarginAnimation<T>(this T element, double all, double durationValue = 300) where T : FrameworkElement
            => element.MarginAnimation (new Thickness (all, all, all, all), durationValue);


        public static T TransitionXAnimation<T>(this T element, double value = 2.0, double durationValue = 300) where T : FrameworkElement
        {
            // 기존 RenderTransform이 TranslateTransform인지 확인
            if (element.RenderTransform is not TranslateTransform translate)
            {
                translate = new TranslateTransform ();
                element.RenderTransform = translate;
            }

            var animation = new DoubleAnimation (translate.X, value, TimeSpan.FromMilliseconds (durationValue));
            translate.BeginAnimation (TranslateTransform.XProperty, animation);

            return element;
        }

        public static T TransitionYAnimation<T>(this T element, double value = 2.0, EasingMode easingMode = EasingMode.EaseInOut, double durationValue = 300) where T : FrameworkElement
        {
            // 기존 RenderTransform이 TranslateTransform인지 확인
            if (element.RenderTransform is not TranslateTransform translate)
            {
                translate = new TranslateTransform ();
                element.RenderTransform = translate;
            }

            var animation = new DoubleAnimation (translate.Y, value, TimeSpan.FromMilliseconds (durationValue));
            animation.EasingFunction = new CubicEase { EasingMode = easingMode };
            translate.BeginAnimation (TranslateTransform.YProperty, animation);

            return element;
        }
    }
}
