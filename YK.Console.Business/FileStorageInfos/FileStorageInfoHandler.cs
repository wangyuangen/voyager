﻿namespace YK.Console.Business.FileStorageInfos;

internal class BulkUploadFileStorageInfoHandler(IFileUploadService _fileUpload, IRepository<FileStorageInfo> _repo) : IRequestHandler<BulkUploadFileStorageInfoRequest, List<FileStorageInfoSimpleOutput>>
{
    public async Task<List<FileStorageInfoSimpleOutput>> Handle(BulkUploadFileStorageInfoRequest request, CancellationToken cancellationToken)
    {
        var uploadResults = new List<FileUploadInfo>();

        FileUploadInfo uploadResult;
        foreach (var file in request.Files)
        {
            uploadResult = await _fileUpload.UploadAsync(file, request.ReName, cancellationToken);
            uploadResults.Add(uploadResult);
        }

        var entities = uploadResults.Adapt<List<FileStorageInfo>>();
        entities.ForEach(entity =>
        {
            entity.BizId = request.BizId;
            entity.BizName = request.BizName;
        });
        await _repo.AddRangeAsync(entities, cancellationToken);

        return entities.Adapt<List<FileStorageInfoSimpleOutput>>();
    }
}

internal class UploadFileStorageInfoHandler(IFileUploadService _fileUpload,IRepository<FileStorageInfo> _repo) : IRequestHandler<UploadFileStorageInfoRequest, FileStorageInfoSimpleOutput>
{
    public async Task<FileStorageInfoSimpleOutput> Handle(UploadFileStorageInfoRequest request, CancellationToken cancellationToken)
    {
        var fileUploadResult = await _fileUpload.UploadAsync(request.File,request.ReName, cancellationToken)
            ?? throw ResultOutput.Exception($"文件 {request.File.FileName} 上传失败");

        var entity = fileUploadResult.Adapt<FileStorageInfo>();
        entity.BizId = request.BizId;
        entity.BizName = request.BizName;
        await _repo.AddAsync(entity, cancellationToken);

        return entity.Adapt<FileStorageInfoSimpleOutput>();
    }
}

internal class DeleteFileStorageInfoHandler(IFileUploadService _fileUpload,IRepository<FileStorageInfo> _repo) : IRequestHandler<DeleteFileStorageInfoRequest, Guid>
{
    public async Task<Guid> Handle(DeleteFileStorageInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true)
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity != null)
        {
            var fileUploadInfo = entity.Adapt<FileUploadInfo>();

            var hasDelete = await _fileUpload.DeleteAsync(fileUploadInfo);

            if (!hasDelete) throw ResultOutput.Exception($"文件 {entity.FileName} 删除失败");

            await _repo.DeleteAsync(entity, cancellationToken);
        }

        return request.Id;
    }
}


internal class FileStorageInfoBindBizHandler(IRepository<FileStorageInfo> _repo) : IRequestHandler<FileStorageInfoBindBizRequest, Guid>
{
    public async Task<Guid> Handle(FileStorageInfoBindBizRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true).GetByIdAsync(request.Id, cancellationToken)
           ?? throw ResultOutput.Exception("文件不存在");

        entity.Update(bizId: request.BizId, bizName: request.BizName);

        await _repo.UpdateAsync(entity, cancellationToken);

        return entity.Id;
    }
}

internal class GetFileStorageInfoHandler(IReadRepository<FileStorageInfo> _repo) : IRequestHandler<GetFileStorageInfoRequest, FileStorageInfoSimpleOutput>
{
    public async Task<FileStorageInfoSimpleOutput> Handle(GetFileStorageInfoRequest request, CancellationToken cancellationToken)
        => await _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true).SimpleSingleAsync<FileStorageInfoSimpleOutput>(x => x.Id == request.Id, cancellationToken)
            ?? throw ResultOutput.Exception("文件不存在");
}

internal class FileStorageInfoSearchHandler(IReadRepository<FileStorageInfo> _repo) : IRequestHandler<FileStorageInfoSearchByBizRequest, List<FileStorageInfoSimpleOutput>>
{
    public Task<List<FileStorageInfoSimpleOutput>> Handle(FileStorageInfoSearchByBizRequest request, CancellationToken cancellationToken)
        => _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter: true)
            .SimpleListAsync<FileStorageInfoSimpleOutput>(x => x.BizId == request.BizId && x.BizName == request.BizName, cancellationToken);
}

internal class FileStorageInfoPageHandler(IReadRepository<FileStorageInfo> _repo): IRequestHandler<FileStorageInfoPageRequest, PaginationResponse<FileStorageInfoOutput>>
{
    public Task<PaginationResponse<FileStorageInfoOutput>> Handle(FileStorageInfoPageRequest request, CancellationToken cancellationToken)
        => _repo.SimplePageAsync<FileStorageInfoOutput>(request, cancellationToken: cancellationToken);
}

internal class FileStorageInfoSearchByBizListHandler(IReadRepository<FileStorageInfo> _repo) : IRequestHandler<FileStorageInfoSearchByBizListRequest, List<FileStorageInfoSimpleOutput>>
{
    public Task<List<FileStorageInfoSimpleOutput>> Handle(FileStorageInfoSearchByBizListRequest request, CancellationToken cancellationToken)
        => _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter: true)
            .SimpleListAsync<FileStorageInfoSimpleOutput>(x => x.BizId.HasValue && request.BizIds.Contains(x.BizId.Value) && x.BizName == request.BizName, cancellationToken);
}

