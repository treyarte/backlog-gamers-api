﻿namespace xmlParseExample.Models;

public class Article : BaseMongoModel
{
    public Article(
        string title,
        string url,
        string shortDescription,
        string imageUrl,
        string content
        )
    {
        this.Title = title;
        this.Url = url;
        this.ShortDescription = shortDescription;
        this.ImageUrl = imageUrl;
        this.Content = content;
    }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ShortDescription { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
}