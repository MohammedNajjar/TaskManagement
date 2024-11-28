using System;
using TaskManagement.Api.Dtos.CategoryDtos;

namespace TaskManagement.Api.Mapping.Category;

public static class CategoryMapping
{

    public static Entities.Category ToEntitiy(this CreateCategoryDto createCategory)
    {
        return new Entities.Category()
        {
            Name = createCategory.Name,
            Description = createCategory.Description,
            UserId = createCategory.UserId

        };
    }
    public static Entities.Category ToEntity(this UpdateCategoryDto updateCategory, int id)
    {

        return new Entities.Category()
        {
            Id = id,
            Name = updateCategory.Name,
            Description = updateCategory.Description,
            UserId = updateCategory.UserId ?? 0

        };
    }
    public static CategoryDetailsDto ToCategoryDetails(this Entities.Category category)
    {
        return new(
            category.Id,
            category.Name,
            category.Description,
         category.Tasks?.Count ?? 0


        );
    }
    public static CategorySummaryDto ToCategorySummary(this Entities.Category category)
    {
        return new(
           category.Id,
            category.Name,
            category.Description,
            category.Tasks?.Count ?? 0

        );
    }

}
