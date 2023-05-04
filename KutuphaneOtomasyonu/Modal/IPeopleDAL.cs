using System.Windows.Controls;

namespace Modal
{
    public interface IPeopleDAL
    {
        int PersonId { get; set; }
        string PersonName { get; set; }
        string PersonSurname { get; set; }
        string PersonalNumber { get; set; }
        string PhoneNumber { get; set; }
        int RoleId { get; set; }
        int FindId { get; set; }
        string LookUpValue { get; set; }
        void PersonInsert();
        void PersonSelect(DataGrid dataGrid);
        void PersonUpdate();
        void PersonDelete();
        void PersonFindAndFill(DataGrid dataGrid);
        bool PersonIsThere();
    }
}
