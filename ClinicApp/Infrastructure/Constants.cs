namespace ClinicApp.Infrastructure
{
    public static class Constants
    {
        public const int VisitDurationMinutes = 15;

        public static class Roles
        {
            public const string ManagerRoleName = "Manager";
            public const string DoctorRoleName = "Doctor";
            public const string PatientRoleName = "Patient";
        }

        public static class Areas
        {
            public const string ManageAreaName = "Manage";
        }
    }
}
