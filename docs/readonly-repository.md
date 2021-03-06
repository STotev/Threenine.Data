# Read Only Repository

Threenine.Data supports readonly repository which enable retrieving data from from Entity Framework without the addiitional over head 
of the EF core query tracking.

Tracking behavior controls if Entity Framework Core will keep information about an entity instance in its change tracker. If an entity is tracked, 
any changes detected in the entity will be persisted to the database during `SaveChanges()`. EF Core will also fix up navigation properties between 
the entities in a tracking query result and the entities that are in the change tracker.

###No-tracking queries
No tracking queries are useful when the results are used in a read-only scenario. They're quicker to execute because there's no need to set up the change 
tracking information. If you don't need to update the entities retrieved from the database, then a no-tracking query should be used. You can swap an individual 
query to be no-tracking.

Threenine.Data provides a `ReadOnlyRepository` which is preconfigured to provide *No Tracking* queries.  

### Reaonly Repository Methods.

- SingleOrDefault
- GetList()

#### SingleOrDefault

Use the SingleOrDefault method to get a single matching entity if one exists.
 
 SingleOrDefault returns the default value for the entity, returning a single matching element, or the default value if no element is found.
 
#### GetList

Get list returns a paginated list of the items by default.
 