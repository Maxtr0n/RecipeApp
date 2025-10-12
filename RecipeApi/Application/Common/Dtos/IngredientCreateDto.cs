namespace Application.Common.Dtos;

public class IngredientCreateDto
{
    public string Name { get; set; }

    public QuantityCreateDto Quantity { get; set; }
}