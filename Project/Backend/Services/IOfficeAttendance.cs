using CalendifyApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalendifyApp.Services
{
    public interface IOfficeAttendanceService
    {
        Task<bool> IsDateBookedAsync(int userId, DateOnly date);
        Task AddAttendanceAsync(Attendance attendance);
        Task<bool> RemoveAttendanceAsync(int userId, DateOnly date);
        Task<List<int>> GetUserIdsByDateAsync(DateOnly date);
        Task<List<DateOnly>> GetAttendanceDatesByUserAsync(int userId);
    }
}
