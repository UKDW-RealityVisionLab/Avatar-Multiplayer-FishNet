using System.Collections.Generic;

public class PermissionConstants
{
    private static Dictionary<string, int> PERMISSION = new Dictionary<string, int>
    {
        { "STUDENT_MATH", 1 },
        { "TEACHER_MATH", 3 },
        { "ADMIN_MATH", 4 },

        { "STUDENT_BIOLOGY", 8 },
        { "TEACHER_BIOLOGY", 24 },
        { "ADMIN_BIOLOGY", 32 },

        { "STUDENT_PHYSICS",  64 },
        { "TEACHER_PHYSICS",  192 },
        { "ADMIN_PHYSICS",  448 },
    };

    // Enum to represent dropdown options
    public enum PermissionList
    {
        StudentMath,
        TeacherMath,
        AdminMath,
        StudentBiology,
        TeacherBiology,
        AdminBiology,
        StudentPhysics,
        TeacherPhysics,
        AdminPhysics,
    }

    // Dictionary to map enum values to integer permissions
    public static readonly Dictionary<PermissionList, int> Permissions = new Dictionary<PermissionList, int>
    {
        { PermissionList.StudentMath, PERMISSION["STUDENT_MATH"] },
        { PermissionList.TeacherMath, PERMISSION["TEACHER_MATH"] },       
        { PermissionList.AdminMath, PERMISSION["ADMIN_MATH"] },     
        { PermissionList.StudentBiology, PERMISSION["STUDENT_BIOLOGY"] },       
        { PermissionList.TeacherBiology, PERMISSION["TEACHER_BIOLOGY"] },   
        { PermissionList.AdminBiology, PERMISSION["ADMIN_BIOLOGY"] }, 
        { PermissionList.StudentPhysics, PERMISSION["STUDENT_PHYSICS"] },      
        { PermissionList.TeacherPhysics, PERMISSION["TEACHER_PHYSICS"] },  
        { PermissionList.AdminPhysics, PERMISSION["ADMIN_PHYSICS"] } 
    };
}
