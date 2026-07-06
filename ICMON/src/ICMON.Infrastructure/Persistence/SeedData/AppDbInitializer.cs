using Microsoft.EntityFrameworkCore;
using ICMON.Domain.Entities;
using ICMON.Domain.Enums;
using ICMON.Domain.ValueObjects;

namespace ICMON.Infrastructure.Persistence.SeedData;

public static class AppDbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!await context.Roles.AnyAsync())
        {
            await SeedRolesAndPermissions(context);
        }
    }

    private static async Task SeedRolesAndPermissions(AppDbContext context)
    {
        var permissions = new List<Permission>
        {
            Permission.Create("JOB_CREATE", "Create Job", "Job"),
            Permission.Create("JOB_READ", "Read Job", "Job"),
            Permission.Create("JOB_UPDATE", "Update Job", "Job"),
            Permission.Create("JOB_DELETE", "Delete Job", "Job"),
            Permission.Create("JOB_CHANGE_STATUS", "Change Job Status", "Job"),

            Permission.Create("CUSTOMER_CREATE", "Create Customer", "Customer"),
            Permission.Create("CUSTOMER_READ", "Read Customer", "Customer"),
            Permission.Create("CUSTOMER_UPDATE", "Update Customer", "Customer"),
            Permission.Create("CUSTOMER_DELETE", "Delete Customer", "Customer"),

            Permission.Create("QUOTATION_CREATE", "Create Quotation", "Quotation"),
            Permission.Create("QUOTATION_READ", "Read Quotation", "Quotation"),
            Permission.Create("QUOTATION_UPDATE", "Update Quotation", "Quotation"),
            Permission.Create("QUOTATION_APPROVE", "Approve Quotation", "Quotation"),

            Permission.Create("INVENTORY_READ", "Read Inventory", "Inventory"),
            Permission.Create("INVENTORY_RECEIVE", "Receive Stock", "Inventory"),
            Permission.Create("INVENTORY_ISSUE", "Issue Stock", "Inventory"),
            Permission.Create("INVENTORY_ADJUST", "Adjust Stock", "Inventory"),

            Permission.Create("PO_CREATE", "Create Purchase Order", "Purchase"),
            Permission.Create("PO_READ", "Read Purchase Order", "Purchase"),
            Permission.Create("PO_APPROVE", "Approve Purchase Order", "Purchase"),

            Permission.Create("PAYMENT_CREATE", "Record Payment", "Payment"),
            Permission.Create("PAYMENT_READ", "Read Payment", "Payment"),
            Permission.Create("PAYMENT_REFUND", "Process Refund", "Payment"),

            Permission.Create("USER_MANAGE", "Manage Users", "Admin"),
            Permission.Create("ROLE_MANAGE", "Manage Roles", "Admin"),
            Permission.Create("PERMISSION_MANAGE", "Manage Permissions", "Admin"),

            Permission.Create("DASHBOARD_VIEW", "View Dashboard", "Dashboard"),
            Permission.Create("REPORT_VIEW", "View Reports", "Reports"),
            Permission.Create("REPORT_GENERATE", "Generate Reports", "Reports"),
        };

        context.Permissions.AddRange(permissions);
        await context.SaveChangesAsync();

        var adminRole = Role.Create("Admin", "System administrator with full access");
        var managerRole = Role.Create("Manager", "Manager with operational access");
        var userRole = Role.Create("User", "Regular user with basic access");

        context.Roles.AddRange(adminRole, managerRole, userRole);
        await context.SaveChangesAsync();

        var rolePermissions = new List<RolePermission>();

        foreach (var permission in permissions)
            rolePermissions.Add(RolePermission.Create(adminRole.Id, permission.Id));

        var managerPermissions = permissions.Where(p => p.GroupName != "Admin").ToList();
        foreach (var permission in managerPermissions)
            rolePermissions.Add(RolePermission.Create(managerRole.Id, permission.Id));

        var userPermissions = permissions.Where(p =>
            p.Code == "JOB_CREATE" || p.Code == "JOB_READ" ||
            p.Code == "CUSTOMER_CREATE" || p.Code == "CUSTOMER_READ" ||
            p.Code == "INVENTORY_READ" ||
            p.Code == "DASHBOARD_VIEW").ToList();
        foreach (var permission in userPermissions)
            rolePermissions.Add(RolePermission.Create(userRole.Id, permission.Id));

        context.RolePermissions.AddRange(rolePermissions);
        await context.SaveChangesAsync();

        var adminUser = User.Create(
            "admin",
            new Email("admin@icmon.com"),
            BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            "System",
            "Administrator",
            Guid.Empty
        );

        context.Users.Add(adminUser);
        await context.SaveChangesAsync();

        var adminUserRole = UserRole.Create(adminUser.Id, adminRole.Id);
        context.UserRoles.Add(adminUserRole);
        await context.SaveChangesAsync();
    }
}
