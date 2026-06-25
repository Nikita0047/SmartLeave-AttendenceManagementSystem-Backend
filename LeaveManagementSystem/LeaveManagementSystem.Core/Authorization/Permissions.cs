using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Authorization
{
    public class Permissions
    {
        public static class Leaves
        {
            public const string View = "leaves.view";
            public const string Apply = "leaves.apply";
            public const string Approve = "leaves.approve";
            public const string Cancel = "leaves.cancel";
            public const string Export = "leaves.export";
        }

        public static class Attendance
        {
            public const string ViewOwn = "attendance.view_own";
            public const string ViewTeam = "attendance.view_team";
            public const string CheckIn = "attendance.checkin";
        }

        public static class Users
        {
            public const string View = "users.view";
            public const string Create = "users.create";
            public const string Edit = "users.edit";
        }

        public static class LeaveTypes
        {
            public const string Manage = "leavetypes.manage";
        }

        public static IEnumerable<string> All => new[]
        {
        Leaves.View, Leaves.Apply, Leaves.Approve, Leaves.Cancel, Leaves.Export,
        Attendance.ViewOwn, Attendance.ViewTeam, Attendance.CheckIn,
        Users.View, Users.Create, Users.Edit,
        LeaveTypes.Manage
       };
    }
}
