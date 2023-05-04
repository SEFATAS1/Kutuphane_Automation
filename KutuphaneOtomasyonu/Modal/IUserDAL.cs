namespace Modal
{
    public interface IUserDAL
    {
        string UserName { get; set; }
        string UserSurname { get; set; }
        string PhoneNumber { get; set; }
        string UserMail { get; set; }
        string Password { get; set; }
        void Insert();
        bool Login();
        bool IsThere();
    }
}
