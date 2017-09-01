﻿using Jambo.Domain.Model.Blogs;
using Jambo.Domain.Model.Blogs.Events;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Jambo.Consumer.Application.DomainEventHandlers.Blogs
{
    public class BlogUrlUpdatedEventHandler : IRequestHandler<BlogUrlUpdatedDomainEvent>
    {
        private readonly IBlogReadOnlyRepository _blogReadOnlyRepository;
        private readonly IBlogWriteOnlyRepository _blogWriteOnlyRepository;

        public BlogUrlUpdatedEventHandler(
            IBlogReadOnlyRepository blogReadOnlyRepository,
            IBlogWriteOnlyRepository blogWriteOnlyRepository)
        {
            _blogReadOnlyRepository = blogReadOnlyRepository ??
                throw new ArgumentNullException(nameof(blogReadOnlyRepository));
            _blogWriteOnlyRepository = blogWriteOnlyRepository ??
                throw new ArgumentNullException(nameof(blogWriteOnlyRepository));
        }
        public void Handle(BlogUrlUpdatedDomainEvent message)
        {
            Blog blog = _blogReadOnlyRepository.GetBlog(message.AggregateRootId).Result;

            if (blog.Version == message.Version)
            {
                blog.Apply(message);
                _blogWriteOnlyRepository.UpdateBlog(blog).Wait();
            }
        }
    }
}