using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Extensions;

namespace Nest.Queryify.Queries.Common
{
    public class DeleteByIdQuery<T> : ElasticClientQueryObject<IDeleteResponse> where T : class
    {
        private readonly string _id;
        private readonly bool _refreshOnDelete;

        public DeleteByIdQuery(string id, bool refreshOnDelete = false)
        {
            _id = id;
            _refreshOnDelete = refreshOnDelete;
        }

        protected override IDeleteResponse ExecuteCore(IElasticClient client, string index)
        {
            var id = DocumentPath<T>.Id(_id);
            return client.Delete<T>(id ,descriptor => BuildQueryCore(descriptor).Index(index));
        }

        protected override Task<IDeleteResponse> ExecuteCoreAsync(IElasticClient client, string index)
        {
            var id = DocumentPath<T>.Id(_id);
            return client.DeleteAsync<T>(id, descriptor => BuildQueryCore(descriptor).Index(index));
        }

        protected virtual DeleteDescriptor<T> BuildQueryCore(DeleteDescriptor<T> descriptor)
        {
            descriptor = descriptor
                .Refresh(_refreshOnDelete);
            return BuildQuery(descriptor);
        }

        protected virtual DeleteDescriptor<T> BuildQuery(DeleteDescriptor<T> descriptor)
        {
            return descriptor;
        }
    }
}