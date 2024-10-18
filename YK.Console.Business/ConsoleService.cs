using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using YK.Console.Business.FileStorageInfos;
using YK.Console.Business.OrganizeInfos;
using YK.Console.Core.DbContext;
using YK.Module.Core.Abstractions;

namespace YK.Console.Business;

public class ConsoleService(ISender _sender,ConsoleDbContext _dbContext) : IConsoleService
{
    public Task<Guid> DeleteFileAsync(Guid fileStorageId, CancellationToken cancellationToken)
    {
        return _sender.Send(new DeleteFileStorageInfoRequest(fileStorageId), cancellationToken);
    }

    public Task<FileStorageInfoSimpleOutput> FileUploadAsync(IFormFile file, BizInfoOutput bizInfo, bool reName = false, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new UploadFileStorageInfoRequest
        {
            BizId = bizInfo.BizId,
            BizName = bizInfo.BizFullName,
            File = file,
            ReName = reName
        }, cancellationToken);
    }

    public Task<List<OrganizeInfoOutput>> GetChildOrgsAsync(Guid parentOrgId, CancellationToken cancellationToken)
    {
        return _sender.Send(new GetChildOrgsByParentRequest(parentOrgId), cancellationToken);
    }

    public Task<List<FileStorageInfoSimpleOutput>> GetFilesByBizAsync(BizInfoOutput bizInfo, CancellationToken cancellationToken)
    {
        return _sender.Send(new FileStorageInfoSearchByBizRequest
        {
            BizId = bizInfo.BizId,
            BizName = bizInfo.BizFullName
        }, cancellationToken);
    }

    public Task<List<FileStorageInfoSimpleOutput>> GetFilesByBizAsync<T>(List<Guid> bizIds, CancellationToken cancellationToken)
    {
        return _sender.Send(new FileStorageInfoSearchByBizListRequest
        {
            BizIds = bizIds,
            BizName = typeof(T).FullName ?? typeof(T).Name
        }, cancellationToken);
    }

    public Task<List<PostInfoOutput>> GetPostsAsync(List<Guid> postIds, CancellationToken cancellationToken)
    {
        return _dbContext.Set<PostInfo>().AsNoTracking()
            .Where(x => postIds.Contains(x.Id))
            .Select(x => x.Adapt<PostInfoOutput>())
            .ToListAsync(cancellationToken);
    }

    public Task<UserStaffOutput?> GetUserStaffAsync(Guid userStaffId, CancellationToken cancellationToken)
    {
        return _dbContext.Set<UserStaffInfo>().AsNoTracking()
            .Where(x => x.Id == userStaffId)
            .Include(x=>x.Post)
            .Include(x=>x.Org)
            .Select(x => x.Adapt<UserStaffOutput>())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<UserStaffOutput>> GetUserStaffAsync(List<Guid> userStaffIds, CancellationToken cancellationToken)
    {
        return _dbContext.Set<UserStaffInfo>().AsNoTracking()
            .Where(x => userStaffIds.Contains(x.Id))
            .Include(x => x.Post)
            .Include(x => x.Org)
            .Select(x => x.Adapt<UserStaffOutput>())
            .ToListAsync(cancellationToken);
    }
}