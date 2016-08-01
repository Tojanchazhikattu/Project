--  select dbo.ufGetColumnCharLength ('Engineer','Name') 
--  select dbo.ufGetColumnCharLength ('Engineer','Id') 
IF OBJECT_ID (N'dbo.ufGetColumnCharLength', N'FN') IS NOT NULL  
  DROP FUNCTION ufGetColumnCharLength;  
GO  
CREATE FUNCTION dbo.ufGetColumnCharLength(@TableName VARCHAR(MAX),@ColumnName varchar(max))  
RETURNS varchar(max)   
AS   
BEGIN 

declare @length varchar(max)

select @length= CASE CHARACTER_MAXIMUM_LENGTH
WHEN -1 THEN 5000
ELSE CHARACTER_MAXIMUM_LENGTH
END 
FROM    INFORMATION_SCHEMA.COLUMNS c
WHERE   c.TABLE_NAME = @TableName and ISNULL('dbo', c.TABLE_SCHEMA) = c.TABLE_SCHEMA 
and COLUMN_NAME=@ColumnName
and DATA_TYPE in ('nvarchar','varchar','nchar','char')

 RETURN @length;  

END