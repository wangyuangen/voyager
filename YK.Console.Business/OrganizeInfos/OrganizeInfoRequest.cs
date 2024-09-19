namespace YK.Console.Business.OrganizeInfos;

/// <summary>
/// 创建机构命令
/// </summary>
public class CreateOrganizeInfoRequest : OrganizeInfoInput, IRequest<Guid> { }

/// <summary>
/// 编辑机构命令
/// </summary>
public class UpdateOrganizeInfoRequest:OrganizeInfoInput, IRequest<Guid> 
{
    /// <summary>
    /// 用户主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}

/// <summary>
/// 删除机构命令
/// </summary>
public class DeleteOrganizeInfoRequest:IRequest<int>
{
    /// <summary>
    /// 用户主键
    /// </summary>
    public Guid Id { get; set; }

    public DeleteOrganizeInfoRequest(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// 机构列表查询命令
/// </summary>
public class OrganizeInfoSearchRequest : BaseFilter, IRequest<List<OrganizeInfoOutput>>
{
    /// <summary>
    /// 机构类型
    /// </summary>
    public OrganizeTypeEnum? OrganizeType { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum? Enabled { get; set; }
}

/// <summary>
/// 获取子级部门列表
/// </summary>
public class GetChildOrgsByParentRequest : IRequest<List<OrganizeInfoOutput>>
{
    /// <summary>
    /// 父级
    /// </summary>
    [Required]
    public Guid ParentId { get; set; }

    public GetChildOrgsByParentRequest(Guid parentId)
    {
        ParentId = parentId;
    }
}
