using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Extensions;

namespace Nest.Queryify.Queries
{
	public class IndexDocumentQuery<T> : ElasticClientQueryObject<IIndexResponse> where T : class
	{
		private readonly T _document;
		private readonly bool _refreshOnSave;

		public IndexDocumentQuery(T document, bool refreshOnSave = false)
		{
			_document = document;
			_refreshOnSave = refreshOnSave;
		}

		protected override IIndexResponse ExecuteCore(IElasticClient client, string index)
		{
			return client.Index(_document, desc => BuildQueryCore(desc, _refreshOnSave).Index(index));
		}

	    protected override Task<IIndexResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.IndexAsync(_document, desc => BuildQueryCore(desc, _refreshOnSave).Index(index));
        }

	    protected virtual IndexDescriptor<T> BuildQueryCore(IndexDescriptor<T> descriptor, bool refreshOnSave)
		{
			descriptor = descriptor
				.Type<T>()
				.Refresh(refreshOnSave);
			return BuildQuery(descriptor);
		}
        
		protected virtual IndexDescriptor<T> BuildQuery(IndexDescriptor<T> descriptor)
		{
			return descriptor;
		}
	}
}