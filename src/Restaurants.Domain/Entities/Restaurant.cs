﻿namespace Restaurants.Domain.Entities;

public  class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; }=default!;
    public string Category {  get; set; } = default!;
    public bool HasDelivery {  get; set; }
    public string ContactEmail { get; set; } = default!;
    public string ContactNumber {  get; set; } = default!;

    public Address? Address { get; set; }
    public List<Dish> Dishes { get; set; } = [];

    public User Owner { get; set; }=default!;
    public string OwnerId { get; set; } = default!;    

    public string? LogoUrl {  get; set; } = default!;
    

}
