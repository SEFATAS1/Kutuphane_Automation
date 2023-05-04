using System.Windows.Controls;

namespace Modal
{
    public interface IProofingDocumentDAL
    {
        int PersonId { get; set; }
        string PersonName { get; set; }
        string PersonSurname { get; set; }
        string PersonalNumber { get; set; }
        string LookUpValue { get; set; }
        void PDFCreate();
        void PersonSelect(DataGrid dataGrid);
        bool PersonIsThere();
        void Find(DataGrid dataGrid);
        bool Inquiry();
    }
}
