--select dbo.ufGetSystemDataSqlDbType ('bigint',0)
IF OBJECT_ID (N'dbo.ufGetSystemDataSqlDbType', N'FN') IS NOT NULL  
  DROP FUNCTION ufGetSystemDataSqlDbType;  
GO  
CREATE FUNCTION dbo.ufGetSystemDataSqlDbType(@sqlDataType varchar(max), @IS_NULLABLE bit)  
RETURNS varchar(max)   
AS   

BEGIN  
    DECLARE @DotNetGetDataType varchar(max) ;  
    SELECT @DotNetGetDataType=
         CASE @sqlDataType   
            WHEN 'bigint' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'BigInt?' ELSE 'BigInt' END
            WHEN 'binary' THEN 'Binary'
            WHEN 'bit' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Bit?' ELSE 'Bit' END            
            WHEN 'char' THEN 'Char'
            WHEN 'date' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Date?' ELSE 'Date' END                        
            WHEN 'datetime' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'DateTime?' ELSE 'DateTime' END                        
            WHEN 'datetime2' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'DateTime2?' ELSE 'DateTime2' END                        
            WHEN 'datetimeoffset' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'DateTimeOffset?' ELSE 'DateTimeOffset' END                                    
            WHEN 'decimal' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Decimal?' ELSE 'Decimal' END                                    
            WHEN 'float' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Float?' ELSE 'Float' END                                    
            WHEN 'image' THEN 'Image'
            WHEN 'int' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Int?' ELSE 'Int' END
            WHEN 'money' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Money?' ELSE 'Money' END                                                
            WHEN 'nchar' THEN 'NChar'
            WHEN 'ntext' THEN 'NText'
            WHEN 'numeric' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Decimal?' ELSE 'Decimal' END                                                            
            WHEN 'nvarchar' THEN 'NVarchar'
            WHEN 'real' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Real?' ELSE 'Real' END                                                                        
            WHEN 'smalldatetime' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'SmallDateTime?' ELSE 'SmallDateTime' END                                    
            WHEN 'smallint' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'SmallInt?' ELSE 'SmallInt'END            
            WHEN 'smallmoney' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'SmallMoney?' ELSE 'SmallMoney' END                                                                        
            WHEN 'text' THEN 'Text'
            WHEN 'time' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Time?' ELSE 'Time' END                                                                                    
            WHEN 'timestamp' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'TimeStamp?' ELSE 'TimeStamp' END                                    
            WHEN 'tinyint' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'TinyInt?' ELSE 'TinyInt' END                                                
            WHEN 'uniqueidentifier' THEN 'UniqueIdentifier'
            WHEN 'varbinary' THEN 'VarBinary'
            WHEN 'varchar' THEN 'VarChar'
            ELSE 'Object'
        END 
    RETURN @DotNetGetDataType;  
END;  
GO  