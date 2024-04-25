using MMS.Data.Entities;

namespace MMS.Data.Services;

// This interface describes the operations that a UserService class should implement
public interface IUserService
{
    void Initialise();

    // ------------- User Management -------------------
    User Register(string name, string email, string password, Role role);
    User Authenticate(string email, string password);
    User GetUserByEmail(string email);
    User GetUser(int id);

    User GetUserByName(string name);


}

