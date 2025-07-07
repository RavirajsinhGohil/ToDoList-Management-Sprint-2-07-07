using ToDoListManagement.Entity.Helper;
using ToDoListManagement.Entity.Models;
using ToDoListManagement.Entity.ViewModel;
using ToDoListManagement.Repository.Interfaces;
using ToDoListManagement.Service.Interfaces;

namespace ToDoListManagement.Service.Implementations;

public class LeaveService : ILeaveService
{
    private readonly ILeaveRepository _leaveRepository;
    public LeaveService(ILeaveRepository leaveRepository)
    {
        _leaveRepository = leaveRepository;
    }

    public async Task<Pagination<LeaveViewModel>> GetPaginatedLeavesAsync(Pagination<LeaveViewModel> pagination)
    {
        Pagination<Leave> leavePagination = new()
        {
            SearchKeyword = pagination.SearchKeyword,
            CurrentPage = pagination.CurrentPage,
            PageSize = pagination.PageSize,
            SortColumn = pagination.SortColumn,
            SortDirection = pagination.SortDirection
        };

        Pagination<Leave> leaves = await _leaveRepository.GetPaginatedLeavesAsync(leavePagination);

        List<LeaveViewModel> leaveViewModels = [];
        foreach (Leave leave in leaves.Items)
        {
            leaveViewModels.Add(new LeaveViewModel
            {
                LeaveId = leave.LeaveId,
                RequestedUserId = leave.RequestedUserId,
                ApprovalUserId = leave.ApprovalUserId,
                RequestedName = leave.RequestedUser?.Name ?? string.Empty,
                ApprovalName = leave.ApprovalUser?.Name ?? string.Empty,
                Reason = leave.Reason ?? string.Empty,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Status = leave.Status,
                CreatedAt = leave.CreatedAt,
            });
        }

        return new Pagination<LeaveViewModel>
        {
            Items = leaveViewModels,
            TotalPages = leaves.TotalPages,
            TotalRecords = leaves.TotalRecords
        };
    }
}
