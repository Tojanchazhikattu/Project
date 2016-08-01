--select dbo.ufGetDotNetGetDataType ('bigint',0)
IF OBJECT_ID (N'dbo.ufGetDotNetGetDataType', N'FN') IS NOT NULL  
  DROP FUNCTION ufGetDotNetGetDataType;  
GO  
CREATE FUNCTION dbo.ufGetDotNetGetDataType(@sqlDataType varchar(max), @IS_NULLABLE bit)  
RETURNS varchar(max)   
AS   
-- Returns the stock level for the product.  
BEGIN  
    DECLARE @DotNetGetDataType varchar(max) ;  
    SELECT @DotNetGetDataType=
         CASE @sqlDataType   
            WHEN 'bigint' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Int64?' ELSE 'Int64' END
            WHEN 'binary' THEN 'Byte[]'
            WHEN 'bit' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Boolean?' ELSE 'Boolean' END            
            WHEN 'char' THEN 'String'
            WHEN 'date' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'DateTime?' ELSE 'DateTime' END                        
            WHEN 'datetime' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'DateTime?' ELSE 'DateTime' END                        
            WHEN 'datetime2' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'DateTime?' ELSE 'DateTime' END                        
            WHEN 'datetimeoffset' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'DateTimeOffset?' ELSE 'DateTimeOffset' END                                    
            WHEN 'decimal' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Decimal?' ELSE 'Decimal' END                                    
            WHEN 'float' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Single?' ELSE 'Single' END                                    
            WHEN 'image' THEN 'Byte[]'
            WHEN 'int' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'int?' ELSE 'int' END
            WHEN 'money' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Decimal?' ELSE 'Decimal' END                                                
            WHEN 'nchar' THEN 'String'
            WHEN 'ntext' THEN 'String'
            WHEN 'numeric' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Decimal?' ELSE 'Decimal' END                                                            
            WHEN 'nvarchar' THEN 'String'
            WHEN 'real' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Double?' ELSE 'Double' END                                                                        
            WHEN 'smalldatetime' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'DateTime?' ELSE 'DateTime' END                                    
            WHEN 'smallint' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Int16?' ELSE 'Int16'END            
            WHEN 'smallmoney' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Decimal?' ELSE 'Decimal' END                                                                        
            WHEN 'text' THEN 'String'
            WHEN 'time' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'TimeSpan?' ELSE 'TimeSpan' END                                                                                    
            WHEN 'timestamp' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'DateTime?' ELSE 'DateTime' END                                    
            WHEN 'tinyint' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Byte?' ELSE 'Byte' END                                                
            WHEN 'uniqueidentifier' THEN 'Guid'
            WHEN 'varbinary' THEN 'Byte[]'
            WHEN 'varchar' THEN 'String'
            ELSE 'Object'
        END 
    RETURN @DotNetGetDataType;  
END;  
GO  