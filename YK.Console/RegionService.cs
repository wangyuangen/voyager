using Microsoft.EntityFrameworkCore;
using YK.BackgroundJob.Abstractions;
using YK.Console.Business.Abstractors;
using YK.Console.Business.RegionInfos;
using YK.Console.Core.Consts;
using YK.Console.Core.Entities;
using YK.Console.Core.Enums;
using YK.Core.Attributes;
using YK.ORM.Abstractions;

namespace YK.Console;

/// <summary>
/// 行政区域服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup),Order(120)]
public class RegionService(ISender _sender,IBackgroundJob _job,IReadRepository<RegionInfo> _repo):IDynamicApi
{
    /// <summary>
    /// 同步行政区域
    /// </summary>
    /// <param name="regionLevel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public void SyncAsync(RegionLevel regionLevel = RegionLevel.City, CancellationToken cancellationToken = default)
        => _job.Enqueue<IRegionInfoOperation>(x => x.SyncAsync(regionLevel, cancellationToken));

    /// <summary>
    /// 创建行政区域
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreateRegionInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 编辑行政区域
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdateRegionInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 删除行政区域
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync(DeleteRegionInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 行政区域分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PaginationResponse<RegionInfoPageOutput>> PageAsync(RegionInfoPageRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 行政区域列表查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<List<RegionInfoSimpleOuput>> ListAsync(RegionInfoSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 行政区域启用设置
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> SetEnabledAsync(RegionInfoEnabledChangeRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 行政区域热度设置
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> SetHotAsync(RegionInfoHotChangeRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 获取区域父级代码
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public async Task<List<string>> ParentCodesAsync(string code,CancellationToken cancellationToken)
    {
        var regions = await _repo.NoDataPermissionQueryable()
             .AsNoTracking()
             .Select(x => new
             {
                 code = x.Code,
                 parentCode = x.ParentCode
             }).ToListAsync();


        string? parentCode = regions.FirstOrDefault(x=>x.code == code)?.parentCode;

        int index = 0;
        var parents = new Dictionary<int, string>()
        {
            { index,code}
        };
        while (!string.IsNullOrWhiteSpace(parentCode))
        {
            index++;
            parents.Add(index,parentCode);
            parentCode = regions.FirstOrDefault(x => x.code == parentCode)?.parentCode;
        }

        var result = parents.OrderByDescending(x => x.Key).Select(x => x.Value).ToList();

        return result;
    }
}
