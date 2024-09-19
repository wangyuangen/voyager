using YK.Console.Business.PostInfos;
using YK.Console.Business.UserStaffInfos;
using YK.Module.Core.Abstractions;

namespace YK.Console.Business;

public class ConsoleService(ISender _sender) : IConsoleService
{
    public Task<List<PostInfoOutput>> GetPostsAsync(List<Guid> postIds, CancellationToken cancellationToken)
    {
        var filter = new AdvancedFilter();
        filter.Logic = ORM.Enums.FilterLogicEnum.Or;
        var filters = new List<AdvancedFilter>();
        postIds.ForEach(postId =>
        {
            filters.Add(new AdvancedFilter
            {
                Field = nameof(PostInfo.Id),
                Operator = ORM.Enums.FilterOperatorEnum.EQ,
                Value = postId
            });
        });
        filter.Filters = filters;
        return _sender.Send(new PostInfoSearchRequest
        {
            Filter = filter
        }, cancellationToken);
    }

    public Task<UserStaffOutput> GetUserStaffAsync(Guid userStaffId, CancellationToken cancellationToken)
        => _sender.Send(new UserStaffInfoRequest(userStaffId), cancellationToken);

}