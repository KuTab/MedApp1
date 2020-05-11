using System;
using System.Collections.Generic;
using UIKit;

namespace MedApp1
{
    internal class NumbPickerModel : UIPickerViewModel
    {
        List<int> list;

        public int Number { get; set; }

        public event EventHandler NumbChanged;

        public NumbPickerModel(List<int> list)
        {
            this.list = list;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return list.Count;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return list[(int)row].ToString();
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            var number = list[(int)row];
            Number = number;
            NumbChanged?.Invoke(null, null);
        }
    }
}