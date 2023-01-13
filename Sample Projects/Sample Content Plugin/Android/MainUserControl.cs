using Android.Content;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace Sample_Content_Plugin_Android
{
    public class MainUserControl
    {
        public FrameLayout RootLayout { get; private set; } = null;

        private readonly TextView TextTitle = null;
        private readonly TextView TextSomeText = null;
        private readonly ImageView ImageSample = null;

        #region Animation
        private bool animate = false;
        private AnimationSet animation;
        public bool Animate
        {
            get => animate;
            set
            {
                if (value != animate)
                {
                    animate = value;

                    if (animation == null)
                    {
                        Animation fadeIn = new AlphaAnimation(0, 1);
                        fadeIn.Interpolator = new DecelerateInterpolator();
                        fadeIn.Duration = 1000;

                        Animation fadeOut = new AlphaAnimation(1, 0);
                        fadeOut.Interpolator = new AccelerateInterpolator();
                        fadeOut.StartOffset = 1000;
                        fadeOut.Duration = 1000;

                        animation = new AnimationSet(false);
                        animation.RepeatCount = 1;
                        animation.AddAnimation(fadeIn);
                        animation.AddAnimation(fadeOut);
                        animation.Duration = 2000;
                        animation.AnimationEnd += Animation_AnimationEnd;
                    }

                    if (value)
                        ImageSample.StartAnimation(animation);
                }
            }
        }

        private void Animation_AnimationEnd(object sender, Animation.AnimationEndEventArgs e)
        {
            ImageSample.StartAnimation(e.Animation);
        }
        #endregion

        public MainUserControl(Context context, Settings settings)
        {
            // Load layout from xml is not possible, because accessing of android resources doesn't work with manual assembly loading.
            // So, in general you can create your layout.(a)xml but you need to implement it manually in code behind finally.
            // Resources like images etc. can be embedded via "Embedded Resource" and in this project you'll get an example!
            // Ensure to set Build Action to "Embedded Resource" for your resources!
            RootLayout = new FrameLayout(context);

            LinearLayout wrapperLayout = new LinearLayout(context)
            {
                Orientation = Android.Widget.Orientation.Vertical,
                LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent)
            };

            TextTitle = new TextView(context);
            TextTitle.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            TextTitle.SetTextSize(Android.Util.ComplexUnitType.Dip, 30);
            TextTitle.TextAlignment = TextAlignment.Center;
            TextTitle.Text = "Example-Plugin";            

            ImageSample = new ImageView(context);
            ImageSample.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            ImageSample.SetImageBitmap(ResourceHelper.LoadBitmapFromEmbeddedResource("/image.jpg"));

            TextSomeText = new TextView(context);
            TextSomeText.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            TextSomeText.SetTextSize(Android.Util.ComplexUnitType.Dip, 30);
            TextSomeText.TextAlignment = TextAlignment.Center;
            TextSomeText.Text = $"Text: {settings.SomeText}";

            wrapperLayout.AddView(TextTitle);
            wrapperLayout.AddView(ImageSample);
            wrapperLayout.AddView(TextSomeText);

            RootLayout.AddView(wrapperLayout);
        }
    }
}