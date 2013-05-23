delimiter $$

CREATE DEFINER=`root`@`localhost` FUNCTION `GetComposition`(argAgentName varchar(45), 
								argProductID int, argReceivingProductID int) RETURNS varchar(255) CHARSET utf8
BEGIN
	DECLARE locAgentID int;
	DECLARE locProductReceiveIndex decimal(17,4);
	DECLARE RetComposition varchar(255);
	DECLARE i int; 
	DECLARE RawCountProductCreate int;
	DECLARE locRawID int;
	DECLARE locActionID int;

	set @locAgentID = (select a.ID from Agents as a
						where a.Name = argAgentName);

	set RawCountProductCreate = GetRawCountProductCreate (argProductID, argReceivingProductID);

	set @locProductReceiveIndex = CalcProductReceiveIndex(argAgentName, argProductID, argReceivingProductID);

	IF @locProductReceiveIndex = -1 THEN
		SET @RetComposition = (select CONCAT('? ', c.Name, ' = ')
								from Categories as c
								where c.ID = argProductID);
	ELSE
		SET @RetComposition = (select CONCAT(ROUND(@locProductReceiveIndex,4), ' ', c.Name, ' = ')
								from Categories as c
								where c.ID = argProductID);
	END IF;

		SET @i=0;
		label1: LOOP
			SET @i = @i + 1;
			IF @i < RawCountProductCreate + 1 THEN

				call GetRawProductCreate (argProductID, argReceivingProductID, @i, 
											@locRawID, @locActionID, @CountIndex);
				/*select argProductID, argReceivingProductID, @i, @locRawID, 
							@locActionID, @CountIndex, IsProduct(@locRawID);*/
				IF IsProduct(@locRawID) = 1 THEN
					
					select  CONCAT(@RetComposition, c.Name, " ", SUM(IFNULL(s.Count,0)), '/',
							ROUND(@CountIndex * @locProductReceiveIndex, 4), "; ")
					into @RetComposition
					from Categories as c
							left join Stock as s on s.CategoryID = c.ID
					where c.ID = @locRawID
					group by c.ID;
				ELSE
					select  CONCAT(@RetComposition, c.Name, " ?; ")
					into @RetComposition
					from Categories as c
					where c.ID = @locRawID;
				END IF;

				ITERATE label1;
			END IF;
			LEAVE label1;
		  END LOOP label1;

RETURN @RetComposition;
END$$

