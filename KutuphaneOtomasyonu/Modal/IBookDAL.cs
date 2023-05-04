using System.Windows.Controls;

namespace Modal
{
    public interface IBookDAL
    {
        int BookId { get; set; }
        string ISBN { get; set; }
        string RecordDate { get; set; }
        string BookName { get; set; }
        int Count { get; set; }
        int WriterId { get; set; }
        string WriterName { get; set; }
        string WriterSurname { get; set; }
        int PublisherId { get; set; }
        string PublisherName { get; set; }
        int BookStateId { get; set; }
        int CategoryId { get; set; }
        int FindId { get; set; }
        string LookUpValue { get; set; }
        void BookInsert();
        void WriterInsert();
        void PublisherInsert();
        void BookUpdate();
        void WriterUpdate();
        void PublisherUpdate();
        void BookSelect(DataGrid dataGrid);
        int LastInsertId();
        bool BookIsThere();
        bool WriterIsThere();
        bool PublisherIsThere();
        void BookFindAndFill(DataGrid dataGrid);
    }
}
