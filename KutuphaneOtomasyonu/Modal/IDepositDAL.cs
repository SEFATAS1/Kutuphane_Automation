using System;
using System.Windows.Controls;

namespace Modal
{
    public interface IDepositDAL
    {
        int DepositId { get; set; }
        int PersonId { get; set; }
        int BookId { get; set; }
        string DeliveryDate { get; set; }
        string DueDate { get; set; }
        string LookUpValue { get; set; }
        int FindId { get; set; }
        void DepositInsert();
        void PersonSelect(DataGrid dataGrid);
        void BookSelect(DataGrid dataGrid);
        void InstantReaderSelect(DataGrid dataGrid);
        void AcceptReturn();
        void DueSelect(DataGrid dataGrid);
        void DueAcceptReturn();
        void DueFindAndFill(DataGrid dataGrid);
        void InstantFindAndFill(DataGrid dataGrid);
        void DepositFind(DataGrid dataGrid);
        DateTime CountDays(DateTime dt, int nDays);
    }
}
