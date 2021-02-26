using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HomeworkManager
{
    class Animation
    {
        public static void OpacityAndPositionAnimation(UIElement element, double Opacityfrom, double Opacityto, double Posfrom, double Posleto, TimeSpan time, EventHandler completed)
        {
            element.Visibility = Visibility.Visible;
            TranslateTransform move = new TranslateTransform();
            DoubleAnimationUsingKeyFrames opa = new DoubleAnimationUsingKeyFrames();   //透明度
            element.RenderTransform = move;
            //定义圆心位置
            //元素开始的位置是从new Point（0，0）到new Point（1，1），
            element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            //载入动画，淡入淡出等。
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
            };
            DoubleAnimation opaAnimation = new DoubleAnimation()
            {
                From = Opacityfrom,                                   //起始值
                To = Opacityto,                                     //结束值
                EasingFunction = easeFunction,                    //缓动函数
                Duration = time,  //动画播放时间
            };
            DoubleAnimation moveAnimation = new DoubleAnimation()
            {
                From = Posfrom,                                   //起始值
                To = Posleto,                                     //结束值
                EasingFunction = easeFunction,              //缓动函数
                FillBehavior = FillBehavior.HoldEnd,
                Duration = time,  //动画播放时间

            };

            if (completed != null)
            {
                moveAnimation.Completed += completed;
            }
            element.BeginAnimation(UIElement.OpacityProperty, opaAnimation);
            move.BeginAnimation(TranslateTransform.XProperty, moveAnimation);
        }
        public static void OpacityAnimation(UIElement element, double Opacityfrom, double Opacityto, TimeSpan time, EventHandler completed)
        {
            element.Visibility = Visibility.Visible;
            DoubleAnimationUsingKeyFrames opa = new DoubleAnimationUsingKeyFrames();   //透明度
            //载入动画，淡入淡出等。
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
            };
            DoubleAnimation opaAnimation = new DoubleAnimation()
            {
                From = Opacityfrom,                                   //起始值
                To = Opacityto,                                     //结束值
                EasingFunction = easeFunction,                    //缓动函数
                Duration = time,  //动画播放时间
            };
            if (completed != null)
            {
                opaAnimation.Completed += completed;
            }
            element.BeginAnimation(UIElement.OpacityProperty, opaAnimation);
        }
        public static void XPositionAnimation(UIElement element, double Posfrom, double Posleto, TimeSpan time, EventHandler completed)
        {
            element.Visibility = Visibility.Visible;
            TranslateTransform move = new TranslateTransform();
            element.RenderTransform = move;
            element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
            };
            DoubleAnimation moveAnimation = new DoubleAnimation()
            {
                From = Posfrom,                                   //起始值
                To = Posleto,                                     //结束值
                EasingFunction = easeFunction,              //缓动函数
                FillBehavior = FillBehavior.HoldEnd,
                Duration = time,  //动画播放时间

            };
            if (completed != null)
            {
                moveAnimation.Completed += completed;
            }
            move.BeginAnimation(TranslateTransform.XProperty, moveAnimation);
        }
        
        public static void YPositionAnimation(UIElement element, double Posfrom, double Posleto, TimeSpan time, EventHandler completed)
        {
            element.Visibility = Visibility.Visible;
            TranslateTransform move = new TranslateTransform();
            element.RenderTransform = move;
            element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
            };
            DoubleAnimation moveAnimation = new DoubleAnimation()
            {
                From = Posfrom,                                   //起始值
                To = Posleto,                                     //结束值
                EasingFunction = easeFunction,              //缓动函数
                FillBehavior = FillBehavior.HoldEnd,
                Duration = time,  //动画播放时间

            };
            if (completed != null)
            {
                moveAnimation.Completed += completed;
            }
            move.BeginAnimation(TranslateTransform.YProperty, moveAnimation);
        }
        public static void WRotateAnimation(UIElement element, double Posfrom, double Posleto, TimeSpan time, EventHandler completed)
        {
            element.Visibility = Visibility.Visible;
            RotateTransform move = new RotateTransform();
            element.RenderTransform = move;
            element.RenderTransformOrigin = new System.Windows.Point(element.RenderSize.Width/2, element.RenderSize.Height / 2);
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
            };
            DoubleAnimation moveAnimation = new DoubleAnimation()
            {
                From = Posfrom,                                   //起始值
                To = Posleto,                                     //结束值
                EasingFunction = easeFunction,              //缓动函数
                FillBehavior = FillBehavior.HoldEnd,
                Duration = time,  //动画播放时间

            };
            if (completed != null)
            {
                moveAnimation.Completed += completed;
            }
            move.BeginAnimation(RotateTransform.AngleProperty, moveAnimation);
        }
        public static void RotateAnimation(UIElement element, double Anglefrom, double Angleto, TimeSpan time, EventHandler completed)
        {
            element.Visibility = Visibility.Visible;
            RotateTransform move = new RotateTransform();
            element.RenderTransform = move;
            element.RenderTransformOrigin = new System.Windows.Point(0.5,0.5);
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
            };
            DoubleAnimation moveAnimation = new DoubleAnimation()
            {
                From = Anglefrom,                                   //起始值
                To = Angleto,                                     //结束值
                EasingFunction = easeFunction,              //缓动函数
                FillBehavior = FillBehavior.HoldEnd,
                Duration = time,  //动画播放时间

            };
            if (completed != null)
            {
                moveAnimation.Completed += completed;
            }
            move.BeginAnimation(RotateTransform.AngleProperty, moveAnimation);
        }
        public static void ScaleAnimation(UIElement element, double Scalefrom, double Scaleto, TimeSpan time, EventHandler completed)
        {
            element.Visibility = Visibility.Visible;
            ScaleTransform move = new ScaleTransform();
            element.RenderTransform = move;
            element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
            };
            DoubleAnimation moveAnimation = new DoubleAnimation()
            {
                From = Scalefrom,                                   //起始值
                To = Scaleto,                                     //结束值
                EasingFunction = easeFunction,              //缓动函数
                FillBehavior = FillBehavior.HoldEnd,
                Duration = time,  //动画播放时间

            };
            if (completed != null)
            {
                moveAnimation.Completed += completed;
            }
            move.BeginAnimation(ScaleTransform.ScaleXProperty, moveAnimation);
            move.BeginAnimation(ScaleTransform.ScaleYProperty, moveAnimation);
        }

        public static DoubleAnimation GetYPositionAnimation(double Posfrom, double Posleto, TimeSpan time, EventHandler completed)
        {
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
            };
            DoubleAnimation moveAnimation = new DoubleAnimation()
            {
                From = Posfrom,                                   //起始值
                To = Posleto,                                     //结束值
                EasingFunction = easeFunction,              //缓动函数
                FillBehavior = FillBehavior.HoldEnd,
                Duration = time,  //动画播放时间

            };
            if (completed != null)
            {
                moveAnimation.Completed += completed;
            }
            return moveAnimation;
        }

        public static EasingFunctionBase GetEasingFunction()
        {
            EasingFunctionBase easeFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseInOut,
            };
            return easeFunction;
        }
    }
}
