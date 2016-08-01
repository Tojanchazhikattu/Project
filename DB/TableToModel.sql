DECLARE @TableName VARCHAR(MAX) = 'ProductType' -- Replace 'NewsItem' with your table name
DECLARE @TableSchema VARCHAR(MAX) = 'dbo' -- Replace 'Markets' with your schema name
DECLARE @result varchar(max) = ''
declare @pk varchar(max)
declare @pkDatatype varchar(max)

SELECT @pk=COLUMN_NAME
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + CONSTRAINT_NAME), 'IsPrimaryKey') = 1
AND TABLE_NAME = @TableName AND TABLE_SCHEMA = @TableSchema

select @pkDatatype= DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS IC
where TABLE_NAME = @TableName and COLUMN_NAME = @pk


SET @result = @result + 'using System;' + CHAR(13) + CHAR(13) 

IF (@TableSchema IS NOT NULL) 
BEGIN
    SET @result = @result + 'namespace ' + @TableSchema  + CHAR(13) + '{' + CHAR(13) 
END

SET @result = @result + 'public class ' + @TableName + CHAR(13) + '{' + CHAR(13) 

SET @result = @result + '#region Instance Properties' + CHAR(13)  

SELECT @result = @result + CHAR(13) 
    + ' public ' + ColumnType + ' ' + ColumnName + ' { get; set; } ' + CHAR(13) 
FROM
(
    SELECT  c.COLUMN_NAME   AS ColumnName 
        , CASE c.DATA_TYPE   
            WHEN 'bigint' THEN
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Int64?' ELSE 'Int64' END
            WHEN 'binary' THEN 'Byte[]'
            WHEN 'bit' THEN 
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Boolean?' ELSE 'Boolean' END            
            WHEN 'char' THEN 'String'
            WHEN 'date' THEN
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'DateTime?' ELSE 'DateTime' END                        
            WHEN 'datetime' THEN
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'DateTime?' ELSE 'DateTime' END                        
            WHEN 'datetime2' THEN  
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'DateTime?' ELSE 'DateTime' END                        
            WHEN 'datetimeoffset' THEN 
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'DateTimeOffset?' ELSE 'DateTimeOffset' END                                    
            WHEN 'decimal' THEN  
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Decimal?' ELSE 'Decimal' END                                    
            WHEN 'float' THEN 
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Single?' ELSE 'Single' END                                    
            WHEN 'image' THEN 'Byte[]'
            WHEN 'int' THEN  
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'int?' ELSE 'int' END
            WHEN 'money' THEN
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Decimal?' ELSE 'Decimal' END                                                
            WHEN 'nchar' THEN 'String'
            WHEN 'ntext' THEN 'String'
            WHEN 'numeric' THEN
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Decimal?' ELSE 'Decimal' END                                                            
            WHEN 'nvarchar' THEN 'String'
            WHEN 'real' THEN 
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Double?' ELSE 'Double' END                                                                        
            WHEN 'smalldatetime' THEN 
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'DateTime?' ELSE 'DateTime' END                                    
            WHEN 'smallint' THEN 
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Int16?' ELSE 'Int16'END            
            WHEN 'smallmoney' THEN  
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Decimal?' ELSE 'Decimal' END                                                                        
            WHEN 'text' THEN 'String'
            WHEN 'time' THEN 
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'TimeSpan?' ELSE 'TimeSpan' END                                                                                    
            WHEN 'timestamp' THEN 
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'DateTime?' ELSE 'DateTime' END                                    
            WHEN 'tinyint' THEN 
                CASE C.IS_NULLABLE
                    WHEN 'YES' THEN 'Byte?' ELSE 'Byte' END                                                
            WHEN 'uniqueidentifier' THEN 'Guid'
            WHEN 'varbinary' THEN 'Byte[]'
            WHEN 'varchar' THEN 'String'
            ELSE 'Object'
        END AS ColumnType
        , c.ORDINAL_POSITION 
FROM    INFORMATION_SCHEMA.COLUMNS c
WHERE   c.TABLE_NAME = @TableName and ISNULL(@TableSchema, c.TABLE_SCHEMA) = c.TABLE_SCHEMA  
) t
ORDER BY t.ORDINAL_POSITION

SET @result = @result + CHAR(13) + '#endregion Instance Properties' + CHAR(13)  


SET @result = @result + CHAR(13) + '#region Data access logic' + CHAR(13)  

SET @result = @result + CHAR(13) + '#region Get()' + CHAR(13)  

SET @result = @result + 'protected override DataRow Get(int '+ @pk+')' + CHAR(13)  

SET @result = @result  + '	{' + CHAR(13)

SET @result = @result  + '			SqlParameter[] parameters = '+ CHAR(13)

SET @result = @result  + '		{' + CHAR(13)

SET @result = @result  + '			new SqlParameter(' +'"@'+@pk+'"'+', System.Data.SqlDbType.'+ dbo.ufGetSystemDataSqlDbType (@pkDatatype,0)+')' + CHAR(13)

SET @result = @result  + '		};' + CHAR(13)

SET @result = @result  + '	parameters[0].Value = '+@pk+';'+ CHAR(13)

SET @result = @result  + '	try {' + CHAR(13)


SET @result = @result  + '		OpenDatabase();' + CHAR(13)

SET @result = @result  + '		DataTable dataTable = DataBase.QueryDataTable(' + '"sp_Get' + @TableName+'"'+', parameters);'+ CHAR(13)

SET @result = @result  + '		return (dataTable.Rows.Count > 0) ? dataTable.Rows[0] : null;' + CHAR(13)


SET @result = @result  + '	}' + CHAR(13)

SET @result = @result  + '	finally {' + CHAR(13)

SET @result = @result  + '		CloseDatabase();' + CHAR(13)
SET @result = @result  + '	}' + CHAR(13)
SET @result = @result  + '}' + CHAR(13)

SET @result = @result + CHAR(13) + '#endregion Get()' + CHAR(13) 

SET @result = @result + CHAR(13) + '#region GetAll()' + CHAR(13)  

SET @result = @result + 'protected override DataTable GetAll()' + CHAR(13)  

SET @result = @result  + '	{' + CHAR(13)

SET @result = @result  + '	try {' + CHAR(13)


SET @result = @result  + '		OpenDatabase();' + CHAR(13)

SET @result = @result  + '		DataTable dataTable = DataBase.QueryDataTable(' + '"sp_Get' + @TableName+'List"'+',null);'+ CHAR(13)

SET @result = @result  + '		return  dataTable ;' + CHAR(13)


SET @result = @result  + '	}' + CHAR(13)

SET @result = @result  + '	finally {' + CHAR(13)

SET @result = @result  + '		CloseDatabase();' + CHAR(13)
SET @result = @result  + '	}' + CHAR(13)
SET @result = @result  + '}' + CHAR(13)



SET @result = @result + CHAR(13) + '#endregion GetAll()' + CHAR(13) 


SET @result = @result + CHAR(13) + '#region Update()' + CHAR(13)  

SET @result = @result + 'protected override void Update()' + CHAR(13)  

SET @result = @result  + '{' + CHAR(13)

SET @result = @result  + 'SqlParameter[] parameters = ' + CHAR(13)

SET @result = @result  + '{' + CHAR(13)

SELECT @result = @result + CHAR(13) 
    + ' new SqlParameter("' + COLUMN_NAME + '",System.Data.SqlDbType.' + --dbo.ufGetColumnCharLength(@TableName,COLUMN_NAME)
	+ case 
		WHEN Leng IS NULL THEN  dbo.ufGetSystemDataSqlDbType (DATA_TYPE,0) + '),' + CHAR(13) 
		ELSE dbo.ufGetSystemDataSqlDbType(DATA_TYPE,0) + ','+ Leng +'),' + CHAR(13) 
	END
FROM  
(
select c.COLUMN_NAME,c.DATA_TYPE,dbo.ufGetColumnCharLength(@TableName,c.COLUMN_NAME) as Leng, c.ORDINAL_POSITION 
FROM  INFORMATION_SCHEMA.COLUMNS c
WHERE   c.TABLE_NAME = @TableName and ISNULL(@TableSchema, c.TABLE_SCHEMA) = c.TABLE_SCHEMA  
) t
ORDER BY t.ORDINAL_POSITION


SET @result = @result  + '};' + CHAR(13)

SELECT @result = @result + CHAR(13) 
+ 'parameters['+ CAST ( t.ORDINAL_POSITION-1 AS varchar(2) )  +'].Value = (object)'+ t.COLUMN_NAME +'?? DBNull.Value;'
FROM  
(
select c.COLUMN_NAME,c.DATA_TYPE,dbo.ufGetColumnCharLength(@TableName,c.COLUMN_NAME) as Leng, c.ORDINAL_POSITION 
FROM  INFORMATION_SCHEMA.COLUMNS c
WHERE   c.TABLE_NAME = @TableName and ISNULL(@TableSchema, c.TABLE_SCHEMA) = c.TABLE_SCHEMA  
) t
ORDER BY t.ORDINAL_POSITION

SET @result = @result + CHAR(13)

SET @result = @result +  'DataBase.RunNonQuery("sp_Update'+ @TableName+'", parameters);' + CHAR(13)

SET @result = @result + CHAR(13) + '}' + CHAR(13)

SET @result = @result + CHAR(13) + '#endregion Update()' + CHAR(13)  


SET @result = @result + CHAR(13) + '#region Delete()' + CHAR(13)  

SET @result = @result + 'protected override void Delete()' + CHAR(13)  

SET @result = @result  + '{' + CHAR(13)

SET @result = @result  + '		Delete('+ @pk+');' + CHAR(13)

SET @result = @result  + '}' + CHAR(13)


SET @result = @result + 'protected override void Delete(int '+ @pk+')' + CHAR(13)  

SET @result = @result  + '{' + CHAR(13)

SET @result = @result  + '	SqlParameter[] parameters = '+ CHAR(13)

SET @result = @result  + '	{' + CHAR(13)

SET @result = @result  + '		new SqlParameter(' +'"@'+@pk+'"'+', System.Data.SqlDbType.'+ dbo.ufGetSystemDataSqlDbType (@pkDatatype,0)+')' + CHAR(13)

SET @result = @result  + '	};' + CHAR(13)

SET @result = @result  + '	parameters[0].Value = (object)'+@pk+'?? DBNull.Value;'+ CHAR(13)

SET @result = @result  + '	try {' + CHAR(13)


SET @result = @result  + '		OpenDatabase();' + CHAR(13)

SET @result = @result  + '		DataBase.RunNonQuery("sp_Delete'+@TableName+'", parameters);' + CHAR(13)


SET @result = @result  + '	}' + CHAR(13)

SET @result = @result  + '	finally {' + CHAR(13)

SET @result = @result  + '		CloseDatabase();' + CHAR(13)
SET @result = @result  + '	}' + CHAR(13)
SET @result = @result  + '}' + CHAR(13)

SET @result = @result + CHAR(13) + '#endregion Delete()' + CHAR(13) 


SET @result = @result + CHAR(13) + '#region Add()' + CHAR(13)  

--SET @result = @result + 'protected override DataRow Add(int '+@pk+')' + CHAR(13)  
SET @result = @result + 'protected override void Add()' + CHAR(13)  

SET @result = @result  + '{' + CHAR(13)

SET @result = @result  + 'SqlParameter[] parameters = ' + CHAR(13)

SET @result = @result  + '{' + CHAR(13)

SELECT @result = @result + CHAR(13) 
    + ' new SqlParameter("' + COLUMN_NAME + '",System.Data.SqlDbType.' + --dbo.ufGetColumnCharLength(@TableName,COLUMN_NAME)
	+ case 
		WHEN Leng IS NULL THEN  dbo.ufGetSystemDataSqlDbType (DATA_TYPE,0) + '),' + CHAR(13) 
		ELSE dbo.ufGetSystemDataSqlDbType(DATA_TYPE,0) + ','+ Leng +'),' + CHAR(13) 
	END
FROM  
(
select c.COLUMN_NAME,c.DATA_TYPE,dbo.ufGetColumnCharLength(@TableName,c.COLUMN_NAME) as Leng, c.ORDINAL_POSITION 
FROM  INFORMATION_SCHEMA.COLUMNS c
WHERE   c.TABLE_NAME = @TableName and ISNULL(@TableSchema, c.TABLE_SCHEMA) = c.TABLE_SCHEMA  
) t
ORDER BY t.ORDINAL_POSITION


SET @result = @result  + '};' + CHAR(13)

SELECT @result = @result + CHAR(13) 
+ 'parameters['+ CAST ( t.ORDINAL_POSITION-1 AS varchar(2) )  +'].Value = (object)'+ t.COLUMN_NAME +'?? DBNull.Value;'
FROM  
(
select c.COLUMN_NAME,c.DATA_TYPE,dbo.ufGetColumnCharLength(@TableName,c.COLUMN_NAME) as Leng, c.ORDINAL_POSITION 
FROM  INFORMATION_SCHEMA.COLUMNS c
WHERE   c.TABLE_NAME = @TableName and ISNULL(@TableSchema, c.TABLE_SCHEMA) = c.TABLE_SCHEMA  
) t
ORDER BY t.ORDINAL_POSITION

SET @result = @result + CHAR(13)

SET @result = @result + CHAR(13) + 'try{' + CHAR(13)

SET @result = @result + '		OpenDatabase();' + CHAR(13)


SET @result = @result +  '		DataBase.RunNonQuery("spAdd'+ @TableName+'", parameters);' + CHAR(13)
SET @result = @result + CHAR(13) + '}' + CHAR(13)

SET @result = @result + CHAR(13) + 'finally{' + CHAR(13)
SET @result = @result + '		CloseDatabase();' + CHAR(13)

SET @result = @result + CHAR(13) + '}' + CHAR(13)

SET @result = @result + CHAR(13) + '}' + CHAR(13)

SET @result = @result + CHAR(13) + '#endregion Add()' + CHAR(13)  

SET @result = @result + CHAR(13) + '#endregion Data access logic' + CHAR(13)  


SET @result = @result + CHAR(13) + '#region Methods' + CHAR(13)  


SET @result = @result + CHAR(13) + 'protected override void AddBusinessRules()' + CHAR(13)  
SET @result = @result  + '{' + CHAR(13)

SELECT @result = @result + CHAR(13) 
+'ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCritetia("'+COLUMN_NAME+'","'+COLUMN_NAME +' is required."));'

FROM  
(
select c.COLUMN_NAME,c.DATA_TYPE,dbo.ufGetColumnCharLength(@TableName,c.COLUMN_NAME) as Leng, c.ORDINAL_POSITION 
FROM  INFORMATION_SCHEMA.COLUMNS c
WHERE   c.TABLE_NAME = @TableName and ISNULL(@TableSchema, c.TABLE_SCHEMA) = c.TABLE_SCHEMA  
AND IS_NULLABLE='NO'
AND  COLUMN_NAME!=@PK
) t
ORDER BY t.ORDINAL_POSITION


SET @result =  @result +CHAR(13)+ + '}' + CHAR(13)


SET @result = @result + CHAR(13) + ' public override void Map(DataRow dataRow)' + CHAR(13)  
SET @result = @result  + '{' + CHAR(13)
SET @result = @result +'if (dataRow != null)'
SET @result = @result  + '{' + CHAR(13)

SELECT @result = @result + CHAR(13) 
+'if (!dataRow.IsNull("'+COLUMN_NAME+'")) '+COLUMN_NAME +'='+ dbo.ufGetConvertToDotNetGetDataType(DATA_TYPE,0)+'(dataRow["' + COLUMN_NAME +'"]);'

FROM  
(
select c.COLUMN_NAME,c.DATA_TYPE,dbo.ufGetColumnCharLength(@TableName,c.COLUMN_NAME) as Leng, c.ORDINAL_POSITION 
FROM  INFORMATION_SCHEMA.COLUMNS c
WHERE   c.TABLE_NAME = @TableName and ISNULL(@TableSchema, c.TABLE_SCHEMA) = c.TABLE_SCHEMA  
AND IS_NULLABLE='NO'
) t
ORDER BY t.ORDINAL_POSITION

SET @result =  @result +CHAR(13)+ + '}' + CHAR(13)

SET @result =  @result +CHAR(13)+ + '}' + CHAR(13)





SET @result = @result + CHAR(13) + 'public  void SetPropertySet()' + CHAR(13)  
SET @result = @result  + '{' + CHAR(13)

SELECT @result = @result + CHAR(13) +
'if '+ propset +CHAR(13)
FROM
(
select 
case DATA_TYPE
 when 'int' THEN '('+ COLUMN_NAME+' > 0) PropertySet("'+COLUMN_NAME+'");'
 else '(!string.IsNullOrEmpty('+ COLUMN_NAME+')) PropertySet("'+COLUMN_NAME+'");'
 end as propset
FROM  
(
select c.COLUMN_NAME,c.DATA_TYPE,dbo.ufGetColumnCharLength(@TableName,c.COLUMN_NAME) as Leng, c.ORDINAL_POSITION 
FROM  INFORMATION_SCHEMA.COLUMNS c
WHERE   c.TABLE_NAME = @TableName and ISNULL(@TableSchema, c.TABLE_SCHEMA) = c.TABLE_SCHEMA  
AND IS_NULLABLE='NO'
) t
--ORDER BY t.ORDINAL_POSITION
) as propset

SET @result = @result  + '}' + CHAR(13)

SET @result = @result + CHAR(13) + '#endregion Methods' + CHAR(13)  


SET @result = @result  + '}' + CHAR(13)

IF (@TableSchema IS NOT NULL) 
BEGIN
    SET @result = @result + CHAR(13) + '}' 
END

PRINT @result