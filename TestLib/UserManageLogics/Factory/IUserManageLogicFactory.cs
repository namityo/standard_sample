using TestLib.UserManageLogics.Logics;

namespace TestLib.UserManageLogics.Factory
{
    public interface IUserManageLogicFactory
    {
        IUserFindLogic CreateUserFindLogic();
    }
}
