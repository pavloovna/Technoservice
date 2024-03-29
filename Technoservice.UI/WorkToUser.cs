using Technoservice.Context.Models;

namespace Technoservice.UI
{
    public class WorkToUser
    {
        private static Worker worker;
        public static Worker Worker
        {
            get { return worker; }
            set { worker = value; }
        }
        /*public static bool CompareRole(Role role)
        {
            return Worker.RoleId == role.Id;
        }     */
        public static bool CompareRole(int roleId)
        {
            return Worker.RoleId == roleId;
        }
    }
}
