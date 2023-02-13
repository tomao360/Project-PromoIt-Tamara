using PromoItProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoItProject.Entities
{
    public class Users: BaseEntity
    {
        public Users(Logger log) : base(log) { }

        public Dictionary<int, User> GetAllUsersFromDB()
        {
            Dictionary<int, User> usersDic = null;
            try
            {
                usersDic = new Dictionary<int, User>();
                Data.Sql.UserSql userSql = new Data.Sql.UserSql(base.Log);
                usersDic = (Dictionary<int, User>)userSql.LoadUsers();
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }
            
            return usersDic;
        }

        public void InsertUserToDB(string email)
        {
            try
            {
                Data.Sql.UserSql userSql = new Data.Sql.UserSql(base.Log);
                userSql.InsertUserToDB(email);
            }
            catch (Exception ex)
            {
                Log.LogException(ex.Message, ex);
                throw;
            }            
        }
    }
}
