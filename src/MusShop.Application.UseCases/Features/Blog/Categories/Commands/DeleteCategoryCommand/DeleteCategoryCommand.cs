﻿using MediatR;
using MusShop.Application.Dtos.Blog.Category;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Application.UseCases.Features.Blog.Categories.Commands.DeleteCategoryCommand;

public record DeleteCategoryCommand(Guid Id) : IRequest<DomainResult<CategoryDto>>;