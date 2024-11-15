﻿using System.Linq.Expressions;
using ToolGood.Words.Pinyin;
using YK.ORM.Specification;

namespace YK.Console.Business.RegionInfos;

internal class CreateRegionInfoHandler(IRepository<RegionInfo> _repo) : IRequestHandler<CreateRegionInfoRequest, Guid>
{
    public async Task<Guid> Handle(CreateRegionInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<RegionInfo>();
        entity.Pinyin = WordsHelper.GetPinyin(entity.Name);
        entity.PinyinFirst = WordsHelper.GetFirstPinyin(entity.Name);
        await _repo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class UpdateRegionInfoHandler(IRepository<RegionInfo> _repo) : IRequestHandler<UpdateRegionInfoRequest, Guid>
{
    public async Task<Guid> Handle(UpdateRegionInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken)
            ?? throw ResultOutput.Exception("行政区域不存在");

        if (entity.Code != request.Code && await _repo.SimpleAnyAsync(x => x.ParentCode == entity.Code))
            throw ResultOutput.Exception("不允许修改存在下级关联区域的区域编码");

        entity.Update(request.ParentCode, request.Name, request.Level, request.Code, null, null, request.Url, null, request.Sort, request.Hot, request.Enabled);

        entity.Pinyin = WordsHelper.GetPinyin(entity.Name);
        entity.PinyinFirst = WordsHelper.GetFirstPinyin(entity.Name);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class DeleteRegionInfoHandler(IRepository<RegionInfo> _repo) : IRequestHandler<DeleteRegionInfoRequest, int>
{
    public Task<int> Handle(DeleteRegionInfoRequest request, CancellationToken cancellationToken)
        => _repo.SoftDeleteAsync(x => x.Id == request.Id, cancellationToken);
}

internal class RegionInfoPageHandler(IReadRepository<RegionInfo> _repo) : IRequestHandler<RegionInfoPageRequest, PaginationResponse<RegionInfoPageOutput>>
{
    public Task<PaginationResponse<RegionInfoPageOutput>> Handle(RegionInfoPageRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<RegionInfo, bool>> expression = x => true;
        if (!request.ParentCode.IsNullOrEmpty())
            expression = expression.AndAlso(x => x.ParentCode == request.ParentCode);
        if (!request.Name.IsNullOrEmpty())
            expression = expression.AndAlso(x => x.Name.Contains(request.Name ?? string.Empty));
        if (request.Level.HasValue)
            expression = expression.AndAlso(x => x.Level == request.Level.Value);
        if (request.Hot.HasValue)
            expression = expression.AndAlso(x => x.Hot == request.Hot.Value);
        if (request.Enabled.HasValue)
            expression = expression.AndAlso(x => x.Enabled == request.Enabled.Value);
        return _repo.SimplePageAsync<RegionInfoPageOutput>(request, expression, cancellationToken);
    }
}

internal class RegionInfoSearchHandler(IReadRepository<RegionInfo> _repo): IRequestHandler<RegionInfoSearchRequest, List<RegionInfoSimpleOuput>>
{
    public Task<List<RegionInfoSimpleOuput>> Handle(RegionInfoSearchRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<RegionInfo, bool>> expression = x => x.ParentCode == request.ParentCode;
        if (request.Hot.HasValue)
            expression = expression.AndAlso(x => x.Hot == request.Hot.Value);
        if (request.Enabled.HasValue)
            expression = expression.AndAlso(x => x.Enabled == request.Enabled.Value);
        var spec = new EntitiesSortExpressionSpec<RegionInfo, RegionInfoSimpleOuput>(expression, new string[] { nameof(RegionInfo.Sort) });
        return _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true).ListAsync(spec, cancellationToken);
    }
}

internal class RegionInfoEnabledChangeHandler(IRepository<RegionInfo> _repo) : IRequestHandler<RegionInfoEnabledChangeRequest, Guid>
{
    public async Task<Guid> Handle(RegionInfoEnabledChangeRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken)
            ?? throw ResultOutput.Exception("行政区域不存在");
        entity.Update(enabled:request.Enabled);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class RegionInfoHotChangeHandler(IRepository<RegionInfo> _repo) : IRequestHandler<RegionInfoHotChangeRequest, Guid>
{
    public async Task<Guid> Handle(RegionInfoHotChangeRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken)
           ?? throw ResultOutput.Exception("行政区域不存在");
        entity.Update(hot:request.Hot);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}
