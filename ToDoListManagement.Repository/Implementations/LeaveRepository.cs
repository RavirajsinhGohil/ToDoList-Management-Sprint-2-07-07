using ToDoListManagement.Entity.Data;
using ToDoListManagement.Repository.Interfaces;
using ToDoListManagement.Entity.Models;
using Microsoft.EntityFrameworkCore;
using ToDoListManagement.Entity.Helper;

namespace ToDoListManagement.Repository.Implementations;

public class LeaveRepository : ILeaveRepository
{
    private readonly ToDoListDbContext _context;
    public LeaveRepository(ToDoListDbContext context)
    {
        _context = context;
    }

    public async Task<List<Leave>> GetAllAsync()
    {
        return await _context.Leaves.ToListAsync();
    }
    
    public async Task<Leave?> GetByIdAsync(int id)
    {
        return await _context.Leaves.FindAsync(id);
    }
    
    public async Task<bool> AddAsync(Leave entity)
    {
        await _context.Leaves.AddAsync(entity);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(Leave entity)
    {
        _context.Leaves.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Pagination<Leave>> GetPaginatedLeavesAsync(Pagination<Leave> pagination)
    {
        IQueryable<Leave> query = _context.Leaves.Include(l => l.RequestedUser).Include(l => l.ApprovalUser).AsQueryable();
        if (!string.IsNullOrEmpty(pagination.SearchKeyword))
        {
            // query = query.Where(l => );
        }
        int totalRecords = await query.CountAsync();

        query = pagination.SortColumn switch
        {
            "startDate" => (pagination.SortDirection?.ToLower() ?? "asc") == "asc"
                                ? query.OrderBy(p => p.StartDate)
                                : query.OrderByDescending(p => p.StartDate),

            "endDate" => (pagination.SortDirection?.ToLower() ?? "asc") == "asc"
                                ? query.OrderBy(p => p.EndDate)
                                : query.OrderByDescending(p => p.EndDate),

            _ => pagination.SortDirection?.ToLower() == "asc"
                                ? query.OrderBy(p => p.StartDate)
                                : query.OrderByDescending(p => p.StartDate),
        };

        List<Leave> pagedData = await query
            .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        int totalPages = (int)Math.Ceiling((double)totalRecords / pagination.PageSize);

        return new Pagination<Leave>
        {
            Items = pagedData,
            CurrentPage = pagination.CurrentPage,
            TotalPages = totalPages,
            TotalRecords = totalRecords
        };
    }
}
