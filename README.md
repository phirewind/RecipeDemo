# RecipeDemo
MVC Core small application demo.

Sql tables expected:

	Recipe
		Id (bigint), not null, primary key, auto inc
		Title (nvarchar(50)), nullable
		Description (nvarchar(500)), nullable
		Image (nvarchar(50)), nullable
		
	RecipeDetail
    	Id (bigint), not null, primary key, auto inc
    	Description (nvarchar(100)), nullable,
    	DetailType (int), not null
    	RecipeId (bigint), not null  (no foreign key set in the demo)
    
