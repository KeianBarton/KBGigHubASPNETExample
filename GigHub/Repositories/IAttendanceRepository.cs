using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(string userId, int gigId);
        IEnumerable<Attendance> GetFutureAttendancesForUser(string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}