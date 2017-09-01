﻿using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Jambo.Domain.Model.Blogs;
using Jambo.Domain.Model.Posts;

namespace Jambo.Producer.Application.Queries
{
    public class BlogQueries : IBlogQueries
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Blog> Blogs
        {
            get
            {
                return _database.GetCollection<Blog>("Blogs");
            }
        }

        public IMongoCollection<Post> Posts
        {
            get
            {
                return _database.GetCollection<Post>("Posts");
            }
        }

        public BlogQueries(string connectionString, string database)
        {
            MongoClient mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(database);
        }


        public async Task<IEnumerable<dynamic>> GetBlogsAsync()
        {
            return await Blogs.Find(e => true).ToListAsync();
        }

        public async Task<dynamic> GetBlogAsync(Guid id)
        {
            return await Blogs.Find(e => e.Id == id).SingleAsync();
        }
    }
}