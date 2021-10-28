using Net_AhmedMohammedRabieAbdElwhab.BL.Interfaces;
using Net_AhmedMohammedRabieAbdElwhab.BL.Managers.User.Interface;
using Net_AhmedMohammedRabieAbdElwhab.BL.Repositry;
using Net_AhmedMohammedRabieAbdElwhab.BL.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net_AhmedMohammedRabieAbdElwhab.DL.Models;


namespace Net_AhmedMohammedRabieAbdElwhab.BL.Managers.User.UserManager
{
    public class UserManager : IUser
    {
      private readonly  GenericUnitOfWork _unitOfWork;
       private readonly IGenericRepositity<Net_AhmedMohammedRabieAbdElwhab.DL.Models.User> _repository;
        public UserManager()
        {
            _unitOfWork = new GenericUnitOfWork();
            _repository = _unitOfWork.GetRepoInstance<Net_AhmedMohammedRabieAbdElwhab.DL.Models.User>();
        }

       

        public bool Create(UserDTO userViewModel)
        {
            try
            {
                var checkUserNameAndEmail = _repository.GetFiltered(x => x.Email == userViewModel.Email || x.UserName == userViewModel.UserName).FirstOrDefault();
                if (checkUserNameAndEmail == null)
                {
                    DL.Models.User user = new DL.Models.User()
                    {
                        Name = userViewModel.Name,
                        Email = userViewModel.Email,
                        EmailConfirmed = false,
                        LastName = userViewModel.LastName,
                        Password = userViewModel.Password,
                        PhoneNumber = userViewModel.PhoneNumber,
                        UserName = userViewModel.UserName,
                    };
                    _repository.Add(user);

                    return _unitOfWork.SaveChanges();
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public IEnumerable<UserDTO> GetAll()
        {
            try
            {
                IEnumerable<UserDTO> query = _repository.Get().Select(item => new UserDTO()
                {
                    Id = item.Id,
                    Email = item.Email,
                    Name = item.Name,
                    EmailConfirmed = item.EmailConfirmed,
                    LastName = item.LastName,
                    Password = item.Password,
                    PhoneNumber = item.PhoneNumber,
                    UserName = item.UserName,

                });
                return   query;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public UserDTO GetById(int id)
        {
            try
            {
                var user = _repository.Get(id);
                

                UserDTO query =  user !=null? new UserDTO()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    EmailConfirmed = user.EmailConfirmed,
                    LastName = user.LastName,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,

                }:null;
                return  query;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Remove(int id)
        {
            try
            {


                if (id > 0)
                {
                    var item = _repository.Get(id);
                    if (item != null)
                    {
                        _repository.Delete(id);

                        return _unitOfWork.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
       }

        public bool Update(UserDTO userViewModel)
        {
            try
            {
                var User = _repository.Get(userViewModel.Id);
                if (User != null)
                {
                    User.Email = userViewModel.Email;
                    User.Name = userViewModel.Name;
                    User.LastName = userViewModel.LastName;
                    User.Password = userViewModel.Password;
                    User.PhoneNumber = userViewModel.PhoneNumber;
                    User.EmailConfirmed = userViewModel.EmailConfirmed;
                    User.UserName = userViewModel.UserName;
                    _repository.Update(User);
                    return _unitOfWork.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ConfirmEmail(string Email)
        {
            try
            {
                var user = _repository.GetFiltered(x => x.Email == Email).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                else
                {
                    user.EmailConfirmed = true;
                    _repository.Update(user);
                    return _unitOfWork.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
