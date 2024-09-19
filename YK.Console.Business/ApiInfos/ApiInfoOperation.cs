using YK.Console.Business.Abstractors;

namespace YK.Console.Business.ApiInfos;

public class ApiInfoOperation(
    ISwaggerDocumentService _swaggerDocService,
    IRepository<ApiInfo> _repo) : IApiInfoOperation
{
    public async Task<bool> SyncAsync(SyncApiFromSwaggerRequest request, CancellationToken cancellationToken)
    {
        var existApis = await _repo.ListAsync(cancellationToken);

        var allApiInfos = await _swaggerDocService.GetAllApiInfoAsync();

        //获取分组
        var groupPaths = allApiInfos.FindAll(a => a.ParentPath.IsNullOrEmpty()).Select(a => a.Path);
        var groups = existApis.Where(a => a.ParentId == Guid.Empty && groupPaths.Contains(a.Path)).ToList();
        var groupIds = groups.Select(a => a.Id);

        //获取模块
        var modules = existApis.Where(a => groupIds.Contains(a.ParentId??Guid.Empty)).ToList();
        var moduleIds = modules.Select(x => x.Id);

        //获取接口
        var apis = existApis.Where(a => moduleIds.Contains(a.ParentId??Guid.Empty)).ToList();
        apis = groups.Concat(modules).Concat(apis).ToList();

        var paths = apis.Select(a => a.Path).ToList();

        #region insert

        //执行父级api插入
        var parentApis = allApiInfos.FindAll(a => a.ParentPath.IsNullOrEmpty());
        var pApis = (from a in parentApis where !paths.Contains(a.Path) select a).ToList();
        if (pApis.Count > 0)
        {
            var entities = pApis.Adapt<List<ApiInfo>>();
            await _repo.AddRangeAsync(entities, cancellationToken);
            apis.AddRange(entities);
        }

        //执行子级api插入
        var childApis = allApiInfos.FindAll(a => !a.ParentPath.IsNullOrEmpty());
        var cApis = (from a in childApis where !paths.Contains(a.Path) select a).ToList();
        if (cApis.Count > 0)
        {
            var entities = cApis.Adapt<List<ApiInfo>>();
            await _repo.AddRangeAsync(entities, cancellationToken);
            apis.AddRange(entities);
        }

        #endregion

        #region update
        {
            //父级api修改
            ApiInfo? a;
            for (int i = 0, len = parentApis.Count; i < len; i++)
            {
                SwaggerApiInfoDto api = parentApis[i];
                a = apis.Find(a => a.Path == api.Path);
                if (a?.Id != Guid.Empty)
                {
                    a.ParentId = Guid.Empty;
                    a.Sort = i + 1;
                    a.Enabled = EnabledStatusEnum.Enabled;
                    a.Remark = api.Remark;
                    a.Name = api.Name;
                    a.Code = api.Code;
                }
            }
        }

        {
            //子级api修改
            ApiInfo? a, pa;
            for (int i = 0, len = childApis.Count; i < len; i++)
            {
                SwaggerApiInfoDto api = childApis[i];
                a = apis.Find(a => a.Path == api.Path);
                pa = apis.Find(a => a.Path == api.ParentPath);
                if (a?.Id != Guid.Empty)
                {
                    a.ParentId = pa?.Id ?? Guid.Empty;
                    a.Name = api.Name;
                    a.Remark = api.Remark;
                    a.HttpMethod = api.HttpMethod;
                    a.Sort = i + 1;
                    a.Enabled = EnabledStatusEnum.Enabled;
                    a.Code = api.Code;
                }
            }
        }

        {
            //模块和api禁用
            var inputPaths = allApiInfos.Select(a => a.Path).ToList();
            var disabledApis = (from a in apis where !inputPaths.Contains(a?.Path ?? "") select a).ToList();
            if (disabledApis.Count > 0)
            {
                foreach (var api in disabledApis)
                {
                    api.Enabled = EnabledStatusEnum.Disabled;
                }
            }
        }

        await _repo.UpdateRangeAsync(apis, cancellationToken);

        #endregion

        return true;
    }
}
