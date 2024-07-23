using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Views.Animations;

namespace Sample_Content_Plugin_Android
{
    public class MainUserControl
    {
        public FrameLayout RootLayout { get; private set; } = null;

        private readonly TextView TextTitle = null;
        private readonly TextView TextSomeText = null;
        private readonly ImageView ImageSample = null;

        #region Animation
        private bool stopAnimation = false;
        private AnimationSet animation;

        public void StartAnimation()
        {
            if (animation == null)
            {
                Animation fadeIn = new AlphaAnimation(0, 1)
                {
                    Interpolator = new DecelerateInterpolator(),
                    Duration = 1000
                };

                Animation fadeOut = new AlphaAnimation(1, 0)
                {
                    Interpolator = new AccelerateInterpolator(),
                    StartOffset = 1000,
                    Duration = 1000
                };

                animation = new AnimationSet(false)
                {
                    RepeatCount = 1,
                    Duration = 2000
                };
                animation.AddAnimation(fadeIn);
                animation.AddAnimation(fadeOut);
                animation.AnimationEnd += Animation_AnimationEnd;
            }

            ImageSample.StartAnimation(animation);
        }

        public void StopAnimation()
        {
            stopAnimation = true;
            ImageSample.ClearAnimation();
        }

        private void Animation_AnimationEnd(object sender, Animation.AnimationEndEventArgs e)
        {
            if (stopAnimation)
            {
                stopAnimation = false;
                return;
            }

            ImageSample.StartAnimation(e.Animation);
        }
        #endregion

        public MainUserControl(Context context, Settings settings)
        {
            // Load layout from xml is not possible, because accessing android resources don't work with manual assembly loading.
            // So, in general you can create your layout.(a)xml but you need to implement it manually in code behind finally.
            // Resources like images etc. can be embedded via "Embedded Resource" and in this project you'll get an example!
            // Ensure to set Build Action to "Embedded Resource" for your resources!
            RootLayout = new FrameLayout(context)
            {
                LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent)
            };

            LinearLayout wrapperLayout = new LinearLayout(context)
            {
                Orientation = Android.Widget.Orientation.Vertical,
                LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent)
            };

            TextTitle = new TextView(context) 
            { 
                LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent)
                {
                    TopMargin = 10,
                    BottomMargin = 10,
                }
            };
            TextTitle.SetTextSize(Android.Util.ComplexUnitType.Dip, 30);
            TextTitle.Text = "Example-Plugin";
            TextTitle.SetTextColor(Android.Graphics.Color.White);
            TextTitle.Typeface = Typeface.DefaultBold;
            TextTitle.Gravity = GravityFlags.Center;

            ImageSample = new ImageView(context)
            {
                LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent) { Gravity = GravityFlags.Center, Weight = 0.8f },
            };
            ImageSample.SetScaleType(ImageView.ScaleType.CenterCrop);
            ImageSample.SetImageBitmap(ResourceHelper.LoadBitmapFromEmbeddedResource("/image.jpg"));

            TextSomeText = new TextView(context)
            {
                LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent)
                {
                    TopMargin = 10,
                    BottomMargin = 10,
                },
                Gravity = GravityFlags.Center,
                Text = $"Text: {settings.SomeText}"
            };

            TextSomeText.SetTextSize(Android.Util.ComplexUnitType.Dip, 30);
            TextSomeText.SetTextColor(Android.Graphics.Color.White);

            wrapperLayout.AddView(TextTitle);
            wrapperLayout.AddView(ImageSample);
            wrapperLayout.AddView(TextSomeText);

            RootLayout.AddView(wrapperLayout);
        }
    }
}