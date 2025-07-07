using ToDoListManagement.Entity.Helper;
using ToDoListManagement.Entity.ViewModel;

namespace ToDoListManagement.Service.Interfaces;

public interface ILeaveService
{
    Task<Pagination<LeaveViewModel>> GetPaginatedLeavesAsync(Pagination<LeaveViewModel> pagination);
}
