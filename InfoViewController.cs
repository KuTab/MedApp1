using Foundation;
using System;
using UIKit;

namespace MedApp1
{
    public partial class InfoViewController : UIViewController
    {
        public string MedName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }
        public int Index { get; set; }

        public InfoViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            MedNameLabel.Text = MedName;
            FromDateLabel.Text = "From: " + FromDate.Date.ToString("dd:MM:yyyy");
            TillDateLabel.Text = "Till: " + TillDate.Date.ToString("dd:MM:yyyy");
            base.ViewDidLoad();
        }

        partial void DeleteButton_TouchUpInside(UIButton sender)
        {
            int index = this.NavigationController.ViewControllers.Length - 2;
            FirstViewController parrent = (FirstViewController)this.NavigationController.ViewControllers[index];
            parrent.Index = Index;
            this.NavigationController.PopViewController(true);
        }
    }
}