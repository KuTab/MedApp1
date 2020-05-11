using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace MedApp1
{
    public class TableSource: UITableViewSource
    {
        List<Medicine> medicines;
        string CellIdentifier = "TableCell";
        UIViewController owner;

        public TableSource(List<Medicine> meds, UIViewController own)
        {
            medicines = meds;
            owner = own;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return medicines.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            Medicine item = medicines[indexPath.Row];

            if(cell==null)
            {
                cell=new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }
            cell.TextLabel.Text = item.Name;

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            InfoViewController controller = owner.Storyboard.InstantiateViewController("infoViewController") as InfoViewController;
            controller.MedName = medicines[indexPath.Row].Name;
            controller.FromDate = medicines[indexPath.Row].FromDate;
            controller.TillDate = medicines[indexPath.Row].TillDate;
            controller.Index = indexPath.Row;
            owner.NavigationController.PushViewController(controller, true);
        }
    }
}
