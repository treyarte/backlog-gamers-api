﻿using backlog_gamers_api.Models.Articles;
using backlog_gamers_api.Repositories.Templates.Interfaces;

namespace backlog_gamers_api.Repositories.Interfaces;

public interface IArticlesRepository : IBaseRepository<Article>
{
    public Task<int> CreateArticles(List<Article> articles);
    public Task FindDuplicates();
    public int DeleteAll();
}