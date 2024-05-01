using cruddapperapitest.Context;
using cruddapperapitest.Contracts;
using cruddapperapitest.Dto;
using cruddapperapitest.Models;
using Dapper;
using System.Data;

namespace cruddapperapitest.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext db;

        public UserRepository(ApplicationContext db) => this.db = db;

        public async Task delDataUser(int userid)
        {
            var query = "DELETE FROM tbl_user WHERE userid = @userid";


            using (var connection = db.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { userid });
            }
        }

        public async Task<IEnumerable<User>> getDataUser()
        {
            var query = "SELECT * FROM tbl_user";


            using (var connection = db.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);

                return users.ToList();
            }
        }

        public async Task<User> getUserById(int userid)
        {
            var query = "SELECT * FROM tbl_user WHERE userid = @userid";


            using (var connection = db.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { userid });

                return user;
            }
        }

        public async Task<User> setDataUser(UserForCreationDto user)
        {
            var query = "INSERT INTO tbl_user (namalengkap,username,password,status) VALUES (@namalengkap, @username, @password, @status)" +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";


            var parameters = new DynamicParameters();
            parameters.Add("namalengkap", user.namalengkap, DbType.String);
            parameters.Add("username", user.username, DbType.String);
            parameters.Add("password", user.password, DbType.String);
            parameters.Add("status", user.status.ToString(), DbType.String);

            using (var connection = db.CreateConnection())
            {
                var userid = await connection.QuerySingleAsync<int>(query, parameters);
                var createdUser = new User
                {
                    userid = userid,
                    namalengkap = user.namalengkap,
                    username = user.username,
                    password = user.password,
                    status = user.status.ToString(),
                };

                return createdUser;
            }
        }

        public async Task updateDataUser(int id, UserForUpdateDto user)
        {
            var query = "UPDATE tbl_user SET namalengkap = @namalengkap, username = @username, password = @password, status = @status WHERE userid = @userid";


            var parameters = new DynamicParameters();
            parameters.Add("userid", id, DbType.Int32);
            parameters.Add("namalengkap", user.namalengkap, DbType.String);
            parameters.Add("username", user.username, DbType.String);
            parameters.Add("password", user.password, DbType.String);
            parameters.Add("status", user.status.ToString(), DbType.String);


            using (var connection = db.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
