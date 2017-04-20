using System.Collections.Generic;
using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Extensions;

namespace Nest.Queryify.Queries
{
	public class BulkIndexDocumentQuery<T> : ElasticClientQueryObject<IBulkResponse> where T : class
	{
		private readonly IEnumerable<T> _documents;
		private readonly bool _refreshOnSave;

		public BulkIndexDocumentQuery(IEnumerable<T> documents, bool refreshOnSave = false)
		{
			_documents = documents;
			_refreshOnSave = refreshOnSave;
		}

		protected override IBulkResponse ExecuteCore(IElasticClient client, string index)
		{
			return client.Bulk(desc => BuildQueryCore(desc, index, _refreshOnSave));
		}

	    protected override Task<IBulkResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.BulkAsync(desc => BuildQueryCore(desc, index, _refreshOnSave));
        }

	    protected virtual BulkDescriptor BuildQueryCore(BulkDescriptor descriptor,
			string index, bool refreshOnSave)
		{
			descriptor = descriptor
				.IndexMany(_documents, (d, i) => d
                    .Type(i.GetType())
					.Index(index)
				)
				.Refresh(refreshOnSave);
			return BuildQuery(descriptor);
		}

		protected virtual BulkDescriptor BuildQuery(BulkDescriptor descriptor)
		{
			return descriptor;
		}
	}
}