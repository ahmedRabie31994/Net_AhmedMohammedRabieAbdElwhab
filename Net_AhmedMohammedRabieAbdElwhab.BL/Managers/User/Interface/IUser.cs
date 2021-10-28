using Net_AhmedMohammedRabieAbdElwhab.BL.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_AhmedMohammedRabieAbdElwhab.BL.Managers.User.Interface
{
    public interface IUser
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO GetById(int id);
        bool Create(UserDTO userViewModel);
        bool Update(UserDTO userViewModel);
        bool ConfirmEmail(string Email);
        bool Remove(int id);

    }
}
