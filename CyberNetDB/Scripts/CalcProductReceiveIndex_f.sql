delimiter $$

CREATE DEFINER=`root`@`localhost` FUNCTION `CalcProductReceiveIndex`(argAgentName varchar(45), 
								argProductID int, argReceivingProductID int) RETURNS decimal(17,4)
BEGIN

	DECLARE locAgentID int;
	DECLARE locProductReceiveIndex decimal(17,4);
	DECLARE locTeh decimal(17,4);
	DECLARE i int; 
	DECLARE RawCountProductCreate int;
	DECLARE locRawID int;
	DECLARE locActionID int;

	set @locAgentID = (select a.ID from Agents as a
						where a.Name = argAgentName);

		SET @i=0;
		SET @locTeh = 0;
		SET @RawCountProductCreate = GetRawCountProductCreate (argProductID, argReceivingProductID);
		label1: LOOP
			SET @i = @i + 1;
			IF @i < @RawCountProductCreate + 1 THEN

				call GetRawProductCreate (argProductID, argReceivingProductID, @i, 
											@locRawID, @locActionID, @CountIndex);

				IF IsProduct(@locRawID) = 1 THEN
					
					select @locTeh + (a.TehModifier *
						(ag.Force * (a.FMod/100) + ag.Intelligence * (a.IMod/100)))
					into @locTeh
					from Categories as c
						join Actions as a on a.ID = @locActionID
						join Agents as ag on ag.ID = @locAgentID
					where c.ID = argProductID;
				ELSE
					set @locTeh = -1;
				END IF;

				ITERATE label1;
			END IF;
			LEAVE label1;
		  END LOOP label1;
	
	IF @locTeh = -1 THEN 
		RETURN -1;
	ELSE
		set @locProductReceiveIndex = 
							(select c.ReceiveIndex *  (@locTeh/@RawCountProductCreate)
							from Categories as c
							where c.ID = argProductID);
	END IF;

RETURN @locProductReceiveIndex;
END$$

