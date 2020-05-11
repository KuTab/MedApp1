using CoreGraphics;
using EventKit;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MedApp1
{
    public partial class AddViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //DatePicker1
            #region
            datePicker1 = new UIDatePicker();
            datePicker1.Mode = UIDatePickerMode.Date;
            datePicker1.Locale = new NSLocale("RUS");
            datePicker1.ValueChanged += (sender, e) =>
            {
                NSDateFormatter dateFormat = new NSDateFormatter();
                dateFormat.DateFormat = "dd/MM/yyyy";
                var text = FromDateTextBox;
                text.Text = dateFormat.ToString(datePicker1.Date);
            };
            //Сегодняшняя дата не отображается!!!
            FromDateTextBox.InputView = datePicker1;
            #endregion

            //DatePicker2
            #region
            datePicker2 = new UIDatePicker();
            datePicker2.Mode = UIDatePickerMode.Date;
            datePicker2.Locale = new NSLocale("RUS");
            datePicker2.ValueChanged += (sender, e) =>
            {
                NSDateFormatter dateFormat = new NSDateFormatter();
                dateFormat.DateFormat = "dd/MM/yyyy";
                var text = (UITextField)TillDateTextBox;
                text.Text = dateFormat.ToString(datePicker2.Date);
            };
            //Сегодняшняя дата не отображается!!!
            TillDateTextBox.InputView = datePicker2;
            #endregion


            this.NavigationItem.SetHidesBackButton(true, false);
            //EndEditing on tap
            var g = new UITapGestureRecognizer(() => View.EndEditing(true));
            g.CancelsTouchesInView = false;
            View.AddGestureRecognizer(g);

            //NumberPicker
            #region
            numbPicker = new UIPickerView();
            var numbList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            var numbModel = new NumbPickerModel(numbList);
            numbModel.NumbChanged += (sender, e) =>
            {
                numberTextBox.Text = numbModel.Number.ToString();
            };
            numbPicker.Model = numbModel;
            numberTextBox.InputView = numbPicker;
            #endregion
        }

        UIDatePicker datePicker1, datePicker2;
        UIPickerView numbPicker;
        //private EKEventStore eventStore;

        public AddViewController(IntPtr handle) : base(handle)
        {
        }

        partial void ConfirmButton_TouchUpInside(UIButton sender)
        {
            /*if (MedNameTextBox.Text != "" && FromDateTextBox.Text != "" && TillDateTextBox.Text != "" && (DateTime)datePicker1.Date <= (DateTime)datePicker2.Date && numberTextBox.Text != "")
            {
                int index = this.NavigationController.ViewControllers.Length - 2;
                FirstViewController parrent = (FirstViewController)this.NavigationController.ViewControllers[index];
                parrent.Med = MedNameTextBox.Text;
                parrent.FromDate = (DateTime)datePicker1.Date;
                parrent.TillDate = (DateTime)datePicker2.Date;
                parrent.TabBarController.TabBar.Hidden = false;
                this.NavigationController.PopViewController(true);
                //FirstViewController parrent = (FirstViewController)this.NavigationController.TopViewController;
                //parrent.Med = MedNameTextBox.Text;
            }
            */
            
            if (CheckFullfill())
            {
                TimePickViewController controller = this.Storyboard.InstantiateViewController("timePickViewController") as TimePickViewController;
                controller.Number = int.Parse(numberTextBox.Text);
                controller.Name = MedNameTextBox.Text;
                controller.FromDate = (DateTime)datePicker1.Date;
                controller.TillDate = (DateTime)datePicker2.Date;
                NavigationController.PushViewController(controller, true);
            }
            else
            {
                var okAlertController = UIAlertController.Create("Error", "You didnt fill in all fields, \nor you have an error with dates.", UIAlertControllerStyle.Alert);

                okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                PresentViewController(okAlertController, true, null);
            }
        }

        public bool CheckFullfill()
        {
            if (MedNameTextBox.Text == "")
                return false;
            if (FromDateTextBox.Text == "")
                return false;
            if (TillDateTextBox.Text == "")
                return false;
            if ((DateTime)datePicker1.Date > (DateTime)datePicker2.Date)
                return false;
            if (numberTextBox.Text == "")
                return false;
            return true;
        }
    }
}