using cruddapperapitest.Dto;
using cruddapperapitest.Models;

namespace cruddapperapitest.Contracts
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> getDataUser();
        public Task<User> getUserById(int userid);
        public Task<User> setDataUser(UserForCreationDto user);
        public Task updateDataUser(int id, UserForUpdateDto user);
        public Task delDataUser(int userid);
    }
}
