﻿using Ardalis.GuardClauses;
using Domain.Abstractions;
using SharedKernel;

namespace Domain.Entities;

public class Recipe : Entity, IAggregateRoot
{
    public Recipe(
        string title,
        string ingredients,
        string description,
        string? images,
        string authorId
    ) : base(Guid.NewGuid())
    {
        GuardAgainstInvalidInput(title, ingredients, description, authorId);
        Title = title;
        Ingredients = ingredients;
        Description = description;
        Images = images;
        AuthorId = authorId;
    }

    public string Title { get; private set; }

    public string Ingredients { get; private set; }

    public string Description { get; private set; }

    public string? Images { get; private set; }

    public string AuthorId { get; private set; }

    private static void GuardAgainstInvalidInput(string title, string ingredients, string description,
        string authorId)
    {
        Guard.Against.NullOrEmpty(title);
        Guard.Against.StringTooShort(title, 3);
        Guard.Against.StringTooLong(title, 100);

        Guard.Against.NullOrEmpty(ingredients);

        Guard.Against.NullOrEmpty(description);
        Guard.Against.StringTooShort(description, 3);
        Guard.Against.StringTooLong(description, 5000);

        Guard.Against.NullOrEmpty(authorId);
    }

    public void Update(
        string title,
        string ingredients,
        string description,
        string? images)
    {
        Guard.Against.NullOrEmpty(title);
        Guard.Against.NullOrEmpty(ingredients);
        Guard.Against.NullOrEmpty(description);

        Title = title;
        Ingredients = ingredients;
        Description = description;
        Images = images;
    }
}