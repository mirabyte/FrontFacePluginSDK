using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Sample_Content_Plugin
{
    public partial class MainUserControl : UserControl
    {
        private bool isAnimating;
        private readonly Storyboard sb;

        public MainUserControl()
        {
            InitializeComponent();

            sb = FindResource("sbAnimateBackground") as Storyboard;
            isAnimating = false;
        }

        /// <summary>
        /// Starts/Stops storyboard
        /// </summary>
        public bool Animate
        {
            get => isAnimating;
            set
            {
                if (isAnimating != value)
                {
                    isAnimating = value;
                    if (isAnimating)
                        sb?.Begin();
                    else
                        sb?.Stop();
                }
            }
        }

        private void tbSomeText_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Switch text decoration when clicked by mouse of touch
            if (tbSomeText.TextDecorations == null)
            {
                tbSomeText.TextDecorations = TextDecorations.Underline;
                tbSomeText.Foreground = Brushes.Blue;
            }
            else
            {
                tbSomeText.TextDecorations = null;
                tbSomeText.Foreground = Brushes.Black;
            }
        }
    }
}
