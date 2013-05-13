delimiter $$

CREATE DEFINER=`root`@`localhost` PROCEDURE `AddStock`(argAgentID int, argProductId int, argCount decimal(17,2), argQuality decimal(17,2))
BEGIN
	IF not exists(select * from Stock where CategoryID = argProductID) THEN
		INSERT Stock (AgentID, CategoryID, Count, Quality)
		VALUES (argAgentID, argProductID, argCount, argQuality);
	ELSE
		IF exists(select * from Stock 
					where AgentID = argAgentID and CategoryID = argProductID and ROUND(Quality) = ROUND(argQuality)) THEN
			UPDATE Stock
			SET Count = Count+ argCount, Quality = (Quality + argQuality)/2
			WHERE AgentID = argAgentID and CategoryID = argProductID and ROUND(Quality) = ROUND(argQuality);
		ELSE
			INSERT Stock (AgentID, CategoryID, Count, Quality)
			VALUES (argAgentID, argProductID, argCount, argQuality);
		END IF;
	END IF;
END$$

