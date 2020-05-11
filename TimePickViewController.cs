using CoreGraphics;
using EventKit;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MedApp1
{
    public partial class TimePickViewController : UIViewController
    {
        public int Number { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CreateTextBoxes(Number);
            //datePicker = new UIDatePicker();
            //datePicker.Mode = UIDatePickerMode.Time;
            foreach (var tx in texts)
            {
                AttachPickers(tx);
            }
        }

        public TimePickViewController(IntPtr handle) : base(handle)
        {
            texts = new List<UITextField>();
            picks = new List<UIDatePicker>();
            eventStore = new EKEventStore();
        }

        private EKEventStore eventStore;
        List<UITextField> texts;
        List<UIDatePicker> picks;

        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Creates new reminder about medicine
        /// </summary>
        /// <param name="Name">Name of medicine</param>
        /// <param name="time">Time of taking medicine</param>
        public void AddReminder(string Name, NSDate time)
        {
            /*
            EKReminder reminder = EKReminder.Create(eventStore);
            reminder.Title = Name;
            NSError e = new NSError();
            EKAlarm timeToRing = new EKAlarm();
            timeToRing.AbsoluteDate = time;
            reminder.Calendar = eventStore.DefaultCalendarForNewReminders;
            reminder.AddAlarm(timeToRing);
            //reminder.Calendar = eventStore.DefaultCalendarForNewReminders;
            eventStore.SaveReminder(reminder, true, out e);
            */
            eventStore.RequestAccess(EKEntityType.Event,
    (bool granted, NSError e) =>
    {
        if (granted)
        {
            EKEvent newEvent = EKEvent.FromStore(eventStore);
            newEvent.StartDate = (NSDate)DateTime.Now;
            newEvent.EndDate = (NSDate)DateTime.Now.AddMinutes(5);
            newEvent.Title = Name;
            newEvent.Calendar = eventStore.DefaultCalendarForNewEvents;
            eventStore.SaveEvent(newEvent, EKSpan.ThisEvent, out e);
        }
        //do something here
        else
        {
            var okAlertController = UIAlertController.Create("Error", "Application doesnt have permission for calendar.", UIAlertControllerStyle.Alert);

            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            PresentViewController(okAlertController, true, null);
        }
    });
        }

        /// <summary>
        /// Creates textboxes for chosing the time
        /// </summary>
        /// <param name="number">Number of textboxes</param>
        public void CreateTextBoxes(int number)
        {
            for (int i = 0; i < number; i++)
            {
                UIScrollView scrollView = new UIScrollView();



                var labelFrame = new CGRect(49, 100 + i * 40, 80, 21);
                var labelx = new UILabel(labelFrame);
                labelx.Text = $"Take {i + 1}:";
                View.Add(labelx);
                var frame = new CGRect(182, 100 + i * 40, 143, 30);
                var textFileldx = new UITextField(frame);
                textFileldx.BorderStyle = UITextBorderStyle.RoundedRect;
                texts.Add(textFileldx);
                View.Add(textFileldx);
            }
        }

        /// <summary>
        /// Attachs datepicker to textfield
        /// </summary>
        /// <param name="field">Current textfield</param>
        public void AttachPickers(UITextField field)
        {
            UIDatePicker picker = new UIDatePicker();
            picker.Mode = UIDatePickerMode.Time;
            picker.Locale = new NSLocale("RUS");
            picker.ValueChanged += (sender, e) =>
            {
                NSDateFormatter dateFormat = new NSDateFormatter();
                dateFormat.DateFormat = "hh:mm";
                var text = field;
                text.Text = dateFormat.ToString(picker.Date);
            };

            field.InputView = picker;

            picks.Add(picker);
        }

        partial void DoneMedButton_TouchUpInside(UIButton sender)
        {
            if (CheckFullfill())
            {
                //TimeSpan days = TillDate.Date - FromDate.Date;
                //DateTime time;
                //for (int i = 0; i < days.Days; i++)
               // {
                 //   for (int j = 0; j < Number; j++)
                  //  {
                  //      time = (DateTime)picks[j].Date;
                        AddReminder(Name, (NSDate)DateTime.Today.AddHours(1));
                  //  }
               // }

                int index1 = this.NavigationController.ViewControllers.Length - 3;
                FirstViewController parrent = (FirstViewController)this.NavigationController.ViewControllers[index1];
                parrent.Med = Name;
                parrent.FromDate = FromDate;
                parrent.TillDate = TillDate;
                parrent.TabBarController.TabBar.Hidden = false;
                this.NavigationController.PopToRootViewController(true);
            }
            else
            {
                var okAlertController = UIAlertController.Create("Error", "You didnt fill in all fields.", UIAlertControllerStyle.Alert);

                okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                PresentViewController(okAlertController, true, null);
            }
        }

        public bool CheckFullfill()
        {
            foreach (var tx in texts)
            {
                if (tx.Text == "")
                    return false;
            }
            return true;
        }
    }
}