using System;

namespace SafeVault
{
    public enum UserRole
    {
        User,
        Admin
    }

    public class AuthorizationService
    {
        public bool CanViewVault(UserRole role)
        {
            return role == UserRole.User
                || role == UserRole.Admin;
        }

        public bool CanEditVault(UserRole role)
        {
            return role == UserRole.Admin;
        }

        public bool CanDeleteVault(UserRole role)
        {
            return role == UserRole.Admin;
        }

        public string GetRolePermissions(UserRole role)
        {
            switch (role)
            {
                case UserRole.Admin:
                    return "View, Edit, Delete";

                case UserRole.User:
                    return "View";

                default:
                    return "No Access";
            }
        }
    }
}