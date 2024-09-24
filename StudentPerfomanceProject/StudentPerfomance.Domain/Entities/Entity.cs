﻿namespace StudentPerfomance.Domain.Entities;

public abstract class Entity
{
	public Guid Id { get; }
	public int EntityNumber { get; private set; }
	public Entity(Guid id) => Id = id;
}
