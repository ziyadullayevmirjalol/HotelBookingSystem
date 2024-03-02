using HotelBookingSystem.Models;
using HotelBookingSystem.Services;

namespace HotelBookingSystem.SpectreUI.AdminPanel.Actions;

public class AdminActions
{
    private Admin admin;
    private AdminService adminService;
    public AdminActions(Admin admin, AdminService adminService)
    {
        this.admin = admin;
        this.adminService = adminService;
    }
    public async Task AddNewApartment()
    {

    }
    public async Task CheckApartmentsForStatus()
    {

    }
    public async Task HotelBalance()
    {

    }
    public async Task UpdateAdminPassword()
    {

    }
}
