--select dbo.ufGetConvertToDotNetGetDataType ('bigint',0)
IF OBJECT_ID (N'dbo.ufGetConvertToDotNetGetDataType', N'FN') IS NOT NULL  
  DROP FUNCTION ufGetConvertToDotNetGetDataType;  
GO  
CREATE FUNCTION dbo.ufGetConvertToDotNetGetDataType(@sqlDataType varchar(max), @IS_NULLABLE bit)  
RETURNS varchar(max)   
AS   
-- Returns the stock level for the product.  
BEGIN  
    DECLARE @DotNetGetDataType varchar(max) ;  
    SELECT @DotNetGetDataType=
         CASE @sqlDataType   
            WHEN 'bigint' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToInt32' ELSE 'Convert.ToInt32' END
            WHEN 'binary' THEN 'Convert.ToByte'
            WHEN 'bit' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToBoolean' ELSE 'Convert.ToBoolean' END            
            WHEN 'char' THEN 'Convert.ToString'
            WHEN 'date' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDateTime' ELSE 'Convert.ToDateTime' END                        
            WHEN 'datetime' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDateTime' ELSE 'Convert.ToDateTime' END                        
            WHEN 'datetime2' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDateTime' ELSE 'Convert.ToDateTime' END                        
            WHEN 'datetimeoffset' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDateTime' ELSE 'Convert.ToDateTime' END                                    
            WHEN 'decimal' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDecimal' ELSE 'Convert.ToDecimal' END                                    
            WHEN 'float' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDouble' ELSE 'Convert.ToDouble' END                                    
            WHEN 'image' THEN 'Convert.ToByte'
            WHEN 'int' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToInt32' ELSE 'Convert.ToInt32' END
            WHEN 'money' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDecimal' ELSE 'Convert.ToDecimal' END                                                
            WHEN 'nchar' THEN 'Convert.ToString'
            WHEN 'ntext' THEN 'Convert.ToString'
            WHEN 'numeric' THEN
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDecimal' ELSE 'Convert.ToDecimal' END                                                            
            WHEN 'nvarchar' THEN 'Convert.ToString'
            WHEN 'real' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDecimal' ELSE 'Convert.ToDecimal' END                                                                        
            WHEN 'smalldatetime' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDateTime' ELSE 'Convert.ToDateTime' END                                    
            WHEN 'smallint' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToInt16' ELSE 'Convert.ToInt16'END            
            WHEN 'smallmoney' THEN  
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDecimal' ELSE 'Convert.ToDecimal' END                                                                        
            WHEN 'text' THEN 'Convert.ToString'
            WHEN 'time' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDateTime' ELSE 'Convert.ToDateTime' END                                                                                    
            WHEN 'timestamp' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToDateTime' ELSE 'Convert.ToDateTime' END                                    
            WHEN 'tinyint' THEN 
                CASE @IS_NULLABLE
                    WHEN 1 THEN 'Convert.ToInt16' ELSE 'Convert.ToInt16' END                                                
            WHEN 'uniqueidentifier' THEN 'Convert.ToString'
            WHEN 'varbinary' THEN 'Convert.ToString'
            WHEN 'varchar' THEN 'Convert.ToString'
            ELSE 'Convert.ToString'
        END 
    RETURN @DotNetGetDataType;  
END;  
GO  